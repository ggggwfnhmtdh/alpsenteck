using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Yolov5App
{
    public struct SectionData
    {
        public UInt32 Addr;
        public UInt32 PC_Start;
        public UInt32 NextAddr;
        public List<byte> Data;
    }
    public static class HexData
    {
        public static void Test(string FileName)
        {
            List<SectionData> list = Parse(FileName);
            for(int i=0;i<list.Count;i++)
            {
                File.WriteAllBytes("Address_0x" + list[i].Addr.ToString("X8") +"_Length_"+ list[i].Data.Count+ ".bin", list[i].Data.ToArray());
            }
        }
        private static byte[] GetData(string InStr)
        {
            byte[] Data = new byte[InStr.Length/2];
            for(int i = 0; i < Data.Length; i++)
            {
                Data[i] = Convert.ToByte(InStr.Substring(2*i,2),16);
            }
            return Data;
        }

        private static void ParseLineData(byte[] Data,out byte DataNum,out byte DataType,out ushort Offset,out byte[] OutData)
        {
            DataNum = Data[0];
            DataType = Data[3];
            Offset = (ushort)(Data[2] + Data[3] * 256);
            OutData = new byte[DataNum];
            for (int i=0;i<DataNum;i++)
            {
                OutData[i] = Data[i+4];
            }
        }
        public static List<SectionData> Parse(string FileName)
        {
            string[] Lins = File.ReadAllLines(FileName);
            byte DataNum;
            byte DataType;
            ushort Offset;
            byte[] Data;
            UInt32 SectionAddr =0;
            //UInt32 PyAddr=0;
            bool flag = false;
            List<SectionData> list = new List<SectionData>();
            SectionData NewSection = new SectionData();
            foreach (string L in Lins)
            {
                string str = L.Substring(1, L.Length - 1);
                byte[] AllData = GetData(str);
                ParseLineData(AllData, out DataNum, out DataType, out Offset, out Data);
                if (DataType == 0)   //data record
                {
                    flag = true;
                    for (int i=0;i<Data.Length;i++)
                    {
                        NewSection.Data.Add(Data[i]);
                    }
                }
                else if (DataType == 1)    //end of file 
                {
                    if (flag == true)
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
                    if (flag == true)
                    {
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
                    
                }
                else if (DataType == 5)    //Start Linear Address Record ：程序启动运行的地址
                {
                    for (int i = 0; i < Data.Length; i++)
                    {
                        NewSection.PC_Start += (UInt32)(Data[Data.Length - i - 1] << (i * 8));
                    }
                }               
            }
            return list;
        }
    }
}
