using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace SOM_DEVELOP_TOOL
{
    public struct SectionData
    {
        public UInt32 Addr;
        public UInt32 PC_Start;
        public UInt32 NextAddr;
        public List<byte> Data;
    }

    public struct BinDataType
    {
        public UInt32 Addr;
        public byte[] Data;
    }
    public static class HexData
    {
        public static List<SectionData> Test(string FileName)
        {
            List<SectionData> list = Parse(FileName);
            for (int i = 0; i<list.Count; i++)
            {
                File.WriteAllBytes(Path.GetDirectoryName(FileName)+"\\"+"Image_0x" + list[i].Addr.ToString("X8") +"_"+ list[i].Data.Count+ ".bin", list[i].Data.ToArray());
            }
            return list;
        }


        private static byte[] GetData(string InStr)
        {
            byte[] Data = new byte[InStr.Length/2];
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = Convert.ToByte(InStr.Substring(2*i, 2), 16);
            }
            return Data;
        }

        private static void ParseLineData(byte[] Data, out byte DataNum, out byte DataType, out ushort Offset, out byte[] OutData)
        {
            DataNum = Data[0];
            DataType = Data[3];
            Offset = (ushort)(Data[1]*256 + Data[2]);
            OutData = new byte[DataNum];
            for (int i = 0; i<DataNum; i++)
            {
                OutData[i] = Data[i+4];
            }
        }

        private static ushort GetAddr(string str)
        {
            str = str.Substring(1, str.Length - 1);
            string data = str.Substring(2, 4);
            return Convert.ToUInt16(data, 16);
        }

        private static int GetDataType(string str)
        {
            str = str.Substring(1, str.Length - 1);
            string data = str.Substring(6, 2);
            return Convert.ToInt32(data, 16);
        }

        public static byte[] ToByte(List<SectionData> lst)
        {
            byte[] Data;
            int Len = 0;
            int index = 0;
            for (int i = 0; i < lst.Count; i++)
            {
                Len += 8 + lst[i].Data.Count();
            }
            Data = new byte[Len];
            for (int i = 0; i < lst.Count; i++)
            {
                Data[index + 0] = (byte)((lst[i].Addr >> 0) & 0xFF);
                Data[index + 1] = (byte)((lst[i].Addr >> 8) & 0xFF);
                Data[index + 2] = (byte)((lst[i].Addr >> 16) & 0xFF);
                Data[index + 3] = (byte)((lst[i].Addr >> 24) & 0xFF);
                Data[index + 4] = 0x50;
                Data[index + 5] = 0x44;
                Data[index + 6] = (byte)((lst[i].Data.Count >> 0) & 0xFF);
                Data[index + 7] = (byte)((lst[i].Data.Count >> 8) & 0xFF);
                Array.Copy(lst[i].Data.ToArray(), 0, Data, index + 8, lst[i].Data.Count);
                index += (8 + lst[i].Data.Count);
            }
            return Data;
        }

        public static BinDataType SectionToBin(List<SectionData> list)
        {
            UInt32 StartAddr;
            UInt32 TotalLen;
            BinDataType BinData;
            int Count = list.Count;
            UInt32 CurAddr;
            list = Sort(list);
            StartAddr = list[0].Addr;
            TotalLen = list[Count-1].Addr + (UInt32)list[Count-1].Data.Count - StartAddr;
            BinData.Addr = StartAddr;
            BinData.Data = new byte[TotalLen];
            Array.Clear(BinData.Data, 0, BinData.Data.Length);
            for (int i = 0; i < list.Count; i++)
            {
                CurAddr = list[i].Addr - StartAddr;
                for (int j = 0; j<list[i].Data.Count; j++)
                {
                    BinData.Data[CurAddr + j] = list[i].Data[j];
                }
            }
            return BinData;
        }

        public static BinDataType HexToBin(string[] FileNames)
        {
            List<SectionData> AllSection = ParseMul(FileNames);
            return SectionToBin(AllSection);
        }

        public static BinDataType HexToBin(string FileName)
        {
            List<SectionData> AllSection = Parse(FileName);
            return SectionToBin(AllSection);
        }

        public static List<SectionData> ParseMul(string[] FileName)
        {
            List<SectionData>[] list = new List<SectionData>[FileName.Length];
            List<SectionData> merge_list = new List<SectionData>();
            for (int i = 0; i < FileName.Length; i++)
            {
                list[i] = Parse(FileName[i]);
                for (int j = 0; j<list[i].Count; j++)
                {
                    merge_list.Add(list[i][j]);
                }
            }
            return merge_list;
        }

        public static List<SectionData> Sort(List<SectionData> list)
        {
            SectionData Temp;
            int length = list.Count; // 数组未排序序列的长度
            do
            {
                for (int j = 0; j < length - 1; j++) // 数组未排序序列的倒第二数
                {
                    if (list[j].Addr > list[j + 1].Addr)
                    {
                        Temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = Temp;
                    }
                }
                length--;
            } while (length > 1);
            return list;
        }

        public static List<SectionData> Parse(string FileName)
        {
            string[] Lins = File.ReadAllLines(FileName);
            byte DataNum;
            byte DataType;
            ushort Offset;
            byte[] Data;
            UInt32 SectionAddr = 0;
            //UInt32 PyAddr=0;
            bool flag = false;
            bool SectionStart = false;
            List<SectionData> list = new List<SectionData>();
            SectionData NewSection = new SectionData();
            bool need_new_section = false;
            for (int m = 0; m<Lins.Length; m++)
            {
                string str = Lins[m].Substring(1, Lins[m].Length - 1);
                byte[] AllData = GetData(str);
                ParseLineData(AllData, out DataNum, out DataType, out Offset, out Data);
                if (DataType == 0)   //data record
                {
                    if (SectionStart == true)
                    {
                        NewSection.Addr += Offset;
                        SectionStart = false;
                    }
                    flag = true;
                    for (int i = 0; i<Data.Length; i++)
                    {
                        NewSection.Data.Add(Data[i]);
                    }
                    need_new_section = false;
                    if (m != Lins.Length-1 && GetDataType(Lins[m + 1]) == 0 && Data.Length == 16)
                    {
                        if (GetAddr(Lins[m + 1]) != GetAddr(Lins[m]) + 16)
                        {
                            need_new_section = true;
                        }
                    }
                    if (Data.Length != 0x10 || need_new_section)
                    {
                        list.Add(NewSection);
                        NewSection = new SectionData();
                        NewSection.Data = new List<byte>();
                        NewSection.Addr = SectionAddr << 16;
                        NewSection.NextAddr = 0;
                        SectionStart = true;
                    }
                }
                else if (DataType == 1)    //end of file 
                {
                    if (flag == true && NewSection.Data.Count>0)
                    {
                        list.Add(NewSection);
                    }
                }
                else if (DataType == 2)    //Extended Segment Address Record 
                {

                }
                else if (DataType == 3)    //Start Segment Address Record ：段地址 STM32不用
                {

                }
                else if (DataType == 4)    //Extended Linear Address Record ：用来标识扩展线性地址
                {
                    if (flag == true && NewSection.Data.Count > 0)
                    {
                        flag = false;
                        list.Add(NewSection);
                    }

                    SectionAddr = 0;
                    for (int i = 0; i < Data.Length; i++)
                    {
                        SectionAddr += (UInt32)(Data[Data.Length - i - 1] << (i * 8));
                    }
                    NewSection = new SectionData();
                    NewSection.Data = new List<byte>();
                    NewSection.Addr = SectionAddr << 16;
                    NewSection.NextAddr = 0;
                    SectionStart = true;


                }
                else if (DataType == 5)    //Start Linear Address Record ：程序启动运行的地址
                {
                    for (int i = 0; i < Data.Length; i++)
                    {
                        NewSection.PC_Start += (UInt32)(Data[Data.Length - i - 1] << (i * 8));
                    }
                }
            }
            bool merge_flag = false;
            while (true)
            {
                merge_flag = false;
                for (int i = 1; i<list.Count; i++)
                {
                    if ((list[i-1].Addr + list[i-1].Data.Count) == list[i].Addr)
                    {
                        for (int j = 0; j<list[i].Data.Count; j++)
                        {
                            list[i-1].Data.Add(list[i].Data[j]);
                        }
                        merge_flag = true;
                        list.RemoveAt(i);
                        break;
                    }
                }
                if (merge_flag == false || list.Count == 1)
                    break;
            }
            return list;
        }
    }
}
