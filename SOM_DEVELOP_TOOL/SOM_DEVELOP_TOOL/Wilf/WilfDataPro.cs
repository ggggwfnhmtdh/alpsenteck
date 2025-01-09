using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using SOM_DEVELOP_TOOL;
using Microsoft.Win32;
using System.Windows.Forms;
using CodingSeb.ExpressionEvaluator;
//using MathNet.Numerics.Statistics;
namespace SOM_DEVELOP_TOOL
{
    public struct TestST
    {
        public UInt32 D0;
        public short D1;
        public string D2;
        public byte D3;
    }

    
    public static class WilfDataPro
    {
        public static DateTime StartTime = DateTime.Now;
        public static Queue<double>[] dataQueue;
        public static bool QueueInitFLag = false;
        public static int QueueMaxNum = 0;
        public static List<byte> m_data;
        public static Random rnd = new Random();
        public static byte[] Align(byte[] Data,UInt32 AlignSize, byte FillData=0x00)
        {
            byte[] tmp;
            if ((Data.Length%AlignSize) == 0)
                return Data;
            tmp = new byte[AlignSize*((Data.Length+AlignSize-1)/AlignSize)];
            Array.Copy(Data,0,tmp,0, Data.Length);
            for(int i=0;i<AlignSize - (Data.Length%AlignSize); i++)
            {
                tmp[Data.Length+i] = FillData;
            }
            return tmp;
        }
        public static bool Eq(byte[] DataL, byte[] DataR)
        {
            if (DataL.Length != DataR.Length)
                return false;
            for(int i = 0; i < DataL.Length; i++)
            {
                if (DataL[i] != DataR[i])
                    return false;
            }
            return true;

        }
        public static string ToStringVetor<T>(T[] FileContent)
        {
            StringBuilder Str = new StringBuilder();
            int i;
            for (i = 0; i < FileContent.Length - 1; i++)
            {
                Str.Append(FileContent[i].ToString() + Environment.NewLine);
            }
            Str.Append(FileContent[i].ToString());
            return Str.ToString();
        }
        public static string RemoveMulSapce(string InStr,string RepStr)
        {
            InStr = Regex.Replace(InStr.Trim(), @"\s+", RepStr);
            return InStr ;
        }

        public static bool IsNumeric(string value,bool Need0x = false)
        {
            if (Need0x == true)
            {
                if (value.Contains("0x") && value.IndexOf("0x") == 0)
                {
                    value = value.Substring(2, value.Length - 2);
                    return Regex.IsMatch(value, @"^[A-Fa-f0-9]+$");
                }
                else
                    return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
            }
            else
            {
                if (value.Contains("0x") && value.IndexOf("0x") == 0)
                {
                    value = value.Substring(2, value.Length - 2);
                }
                return  Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$") || Regex.IsMatch(value, @"^[A-Fa-f0-9]+$");
            }
        }

        public static UInt32 ToDec(string Value)
        {
            if (IsNumeric(Value))
            {
                if (Value.Contains("0x"))
                    return Convert.ToUInt32(Value, 16);
                else
                    return Convert.ToUInt32(Value, 10);
            }
            else
                return 0xFFFFFFFF;
        }

        public static void QueueInit(int QueueChNum, int FilteNum)
        {
            dataQueue = new Queue<double>[QueueChNum];
            QueueMaxNum = FilteNum;
            for (int i = 0; i < dataQueue.Length; i++)
            {
                dataQueue[i] = new Queue<double>(QueueMaxNum);
            }
        }

        public static double[] AvrFilter(double InData0, int ChNumIndex)
        {
            double[] OutData = new double[4];
            //double Noise = 0;
            //if (dataQueue[ChNumIndex].Count >= QueueMaxNum)
            //{
            //    Noise = 6 * dataQueue[ChNumIndex].ToArray().StandardDeviation();
            //    dataQueue[ChNumIndex].Dequeue();
            //}
            //dataQueue[ChNumIndex].Enqueue(InData0);
            //OutData[0] = dataQueue[ChNumIndex].Max();
            //OutData[1] = dataQueue[ChNumIndex].Min();
            //OutData[2] = dataQueue[ChNumIndex].Average();
            //OutData[3] = Noise;

            return OutData;
        }

        public static void InitData()
        {
            if(m_data != null)
            {
                m_data.Clear();
            }
            else
            {
                m_data = new List<byte>();
            }
        }
        public static byte[] Add<T>(T InData)
        {
            byte[] AppData = ToByte(InData);
            for(int i=0;i<AppData.Length;i++)
            {
                m_data.Add(AppData[i]);
            }
            return m_data.ToArray();
        }
        public static string RemoiveMulSapce(string InStr,bool KeepOne = true)
        {
            if(KeepOne == true)
               return Regex.Replace(InStr.Trim(), @"\s+", " ");
            else
                return Regex.Replace(InStr.Trim(), @"\s+", "");
        }

        public static byte[] AppendData(byte[] Data,int StartIndex0, byte[] AppendData, int StartIndex1,int Len)
        {

            if(Data.Length >= (StartIndex0 + Len))
            {
                Array.Copy(AppendData, StartIndex1, Data, StartIndex0, Len);
                return Data;
            }
            else
            {
                byte[] OutData = new byte[StartIndex0 + Len];
                Array.Copy(Data, 0, OutData, 0, Data.Length);
                Array.Copy(AppendData, StartIndex1, OutData, StartIndex0, Len);
                return OutData;
            }
        }

        public static byte[] AppendData<T>(byte[] Data,T InData,int StartIndex)
        {
            byte[] AppData = ToByte(InData);
            return AppendData( Data, StartIndex, AppData, 0, AppData.Length);
        }

        public static int[] ParseRange(string DelayStr)
        {
            List<int> DelayTime = new List<int>();
            int Start, Step, End;
            string[] TimeStr;
            int i;
            DelayStr = DelayStr.Trim();
            if (DelayStr.Contains(":"))
            {
                TimeStr = DelayStr.Split(':');
                Start = Convert.ToInt32(TimeStr[0].Trim());
                Step = Convert.ToInt32(TimeStr[1].Trim());
                End = Convert.ToInt32(TimeStr[2].Trim());
                for (i = Start; i <= End; i += Step)
                {
                    DelayTime.Add((ushort)i);
                }
                if (((End - Start) % Step) != 0)
                {
                    DelayTime.Add((ushort)End);
                }
            }
            else if (DelayStr.Contains(","))
            {
                TimeStr = DelayStr.Split(',');
                for (i = 0; i < TimeStr.Length; i++)
                {
                    DelayTime.Add(Convert.ToInt32(TimeStr[i].Trim()));
                }
            }
            else if (DelayStr.Contains("*"))
            {
                TimeStr = DelayStr.Split('*');
                Start = Convert.ToInt32(TimeStr[0].Trim());
                End = Convert.ToInt32(TimeStr[1].Trim());
                for (i = 0; i < Start; i++)
                {
                    DelayTime.Add(Convert.ToInt32(End));
                }
            }
            else
            {
                DelayTime.Add(Convert.ToInt32(DelayStr));
            }

            return DelayTime.ToArray();
        }

        public static UInt32[] ParseRange3(string DelayStr)
        {
            List<UInt32> DelayTime = new List<UInt32>();
            UInt32 Start, Step, End;
            string[] TimeStr;
            UInt32 i;
            DelayStr = DelayStr.Trim();
            if (DelayStr.Contains(":"))
            {
                TimeStr = DelayStr.Split(':');
                Start = Convert.ToUInt32(TimeStr[0].Trim());
                Step = Convert.ToUInt32(TimeStr[1].Trim());
                End = Convert.ToUInt32(TimeStr[2].Trim());
                for (i = Start; i <= End; i += Step)
                {
                    DelayTime.Add((ushort)i);
                }
                if (((End - Start) % Step) != 0)
                {
                    DelayTime.Add((ushort)End);
                }
            }
            else if (DelayStr.Contains(","))
            {
                TimeStr = DelayStr.Split(',');
                for (i = 0; i < TimeStr.Length; i++)
                {
                    DelayTime.Add(Convert.ToUInt32(TimeStr[i].Trim()));
                }
            }
            else if (DelayStr.Contains("*"))
            {
                TimeStr = DelayStr.Split('*');
                Start = Convert.ToUInt32(TimeStr[0].Trim());
                End = Convert.ToUInt32(TimeStr[1].Trim());
                for (i = 0; i < Start; i++)
                {
                    DelayTime.Add(Convert.ToUInt32(End));
                }
            }
            else
            {
                DelayTime.Add(Convert.ToUInt32(DelayStr));
            }

            return DelayTime.ToArray();
        }

        public static double[] ParseRangeDouble(string DelayStr)
        {
            List<double> DelayTime = new List<double>();
            double Start, Step, End;
            string[] TimeStr;
            double i;
            DelayStr = DelayStr.Trim();
            if (DelayStr.Contains(":"))
            {
                TimeStr = DelayStr.Split(':');
                Start = Convert.ToDouble(TimeStr[0].Trim());
                Step = Convert.ToDouble(TimeStr[1].Trim());
                End = Convert.ToDouble(TimeStr[2].Trim());
                for (i = Start; i <= End; i += Step)
                {
                    DelayTime.Add(i);
                }
                if (((End - Start) % Step) != 0)
                {
                    DelayTime.Add(End);
                }
            }
            else if (DelayStr.Contains(","))
            {
                TimeStr = DelayStr.Split(',');
                for (i = 0; i < TimeStr.Length; i++)
                {
                    DelayTime.Add(Convert.ToDouble(TimeStr[(int)i].Trim()));
                }
            }
            else if (DelayStr.Contains("*"))
            {
                TimeStr = DelayStr.Split('*');
                Start = Convert.ToDouble(TimeStr[0].Trim());
                End = Convert.ToDouble(TimeStr[1].Trim());
                for (i = 0; i < Start; i++)
                {
                    DelayTime.Add(Convert.ToDouble(End));
                }
            }
            else
            {
                DelayTime.Add(Convert.ToDouble(DelayStr));
            }

            return DelayTime.ToArray();
        }

        public static byte[] ToByte(UInt32[] FileContent)
        {
            byte[] OutData = new byte[4 * FileContent.Length];
            for (int i = 0; i < FileContent.Length;i++)
            {
                OutData[4 * i + 0] = (byte)((FileContent[i] >> 0) & 0xFF);
                OutData[4 * i + 1] = (byte)((FileContent[i] >> 8) & 0xFF);
                OutData[4 * i + 2] = (byte)((FileContent[i] >> 16) & 0xFF);
                OutData[4 * i + 3] = (byte)((FileContent[i] >> 24) & 0xFF);
            }
            return OutData;
        }

        public static UInt32[] ToUint32(byte[] FileContent)
        {
            UInt32[] OutData = new UInt32[FileContent.Length/4];
            for (int i = 0; i < FileContent.Length; i+=4)
            {
                OutData[i/4] = (UInt32)((FileContent[i + 0] << 0) + (FileContent[i + 1] << 8) + (FileContent[i + 2] << 16) + (FileContent[i + 3] << 24));
            }
            return OutData;
        }

        public static UInt32 GetUint32(byte[] FileContent,int offset)
        {
            UInt32 OutData;
            OutData = (UInt32)((FileContent[offset + 0] << 0) + (FileContent[offset + 1] << 8) + (FileContent[offset + 2] << 16) + (FileContent[offset + 3] << 24));
            return OutData;
        }

        public static UInt16 GetUint16(byte[] FileContent, int offset)
        {
            UInt16 OutData;
            OutData = (UInt16)((FileContent[offset + 0] << 0) + (FileContent[offset + 1] << 8));
            return OutData;
        }

        public static byte GetUint8(byte[] FileContent, int offset)
        {
            byte OutData;
            OutData = FileContent[offset];
            return OutData;
        }

        public static byte[] GetUint8(byte[] FileContent, int offset,int Length)
        {
            byte[] OutData = new byte[Length];
            Array.Copy(FileContent,offset,OutData,0, Length);
            return OutData;
        }


        public static int AddU32(byte[] FileContent, int offset,UInt32 InData)
        {
            FileContent[offset+0] = (byte)((InData>>0)&0xFF);
            FileContent[offset+1] = (byte)((InData>>8)&0xFF);
            FileContent[offset+2] = (byte)((InData>>16)&0xFF);
            FileContent[offset+3] = (byte)((InData>>24)&0xFF);
            return sizeof(UInt32);
        }

        public static int AddU16(byte[] FileContent, int offset, ushort InData)
        {
            FileContent[offset+0] = (byte)((InData>>0)&0xFF);
            FileContent[offset+1] = (byte)((InData>>8)&0xFF);
            return sizeof(ushort);
        }

        public static int AddU8(byte[] FileContent, int offset, byte InData)
        {
            FileContent[offset+0] = (byte)((InData>>0)&0xFF);
            return sizeof(byte);
        }

        public static int AddU8(byte[] FileContent, int offset, byte[] InData)
        {
            Array.Copy(InData, 0, FileContent, offset, InData.Length);
            return InData.Length;
        }

        public static byte[] ToByte<T>(T FileContent)
        {
            
            int ByteLen = Marshal.SizeOf(typeof(T));
            byte[] Data = new byte[ByteLen];
            UInt64 TempData = 0;

            if (FileContent is byte)
            {
                TempData = (byte)Convert.ChangeType(FileContent, typeof(T));

            }
            else if (FileContent is sbyte)
            {
                TempData = (byte)((sbyte)Convert.ChangeType(FileContent, typeof(T)));

            }
            else if (FileContent is ushort)
            {
                TempData = (ushort)Convert.ChangeType(FileContent, typeof(T));

            }
            else if (FileContent is short)
            {
                TempData = (ushort)((short)Convert.ChangeType(FileContent, typeof(T)));

            }
            else if (FileContent is UInt32)
            {
                TempData = (UInt32)Convert.ChangeType(FileContent, typeof(T));

            }
            else if (FileContent is Int32)
            {
                TempData = (UInt32)((Int32)Convert.ChangeType(FileContent, typeof(T)));

            }

            else if (FileContent is UInt64)
            {
                TempData = (UInt64)Convert.ChangeType(FileContent, typeof(T));

            }
            else if (FileContent is Int64)
            {
                TempData = (UInt64)((Int64)Convert.ChangeType(FileContent, typeof(T)));

            }
            else if(FileContent is TestST)
            {
                int structSize = Marshal.SizeOf(typeof(TestST));
                byte[] buffer = new byte[structSize];
                GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(FileContent, handle.AddrOfPinnedObject(), false);
                handle.Free();
                return buffer;
            }

            for (int i = 0; i < ByteLen; i++)
            {
                Data[i] = (byte)(TempData & 0xFF);
                TempData >>= 8;
            }

            return Data;
        }

       

        public static double[] ListAvr(List<double[]> ListData)
        {
            double[] Avr = new double[ListData[0].Length];
            int i,j;
            
            for (j = 0; j < Avr.Length; j++)
            {
                for (i = 0; i < ListData.Count; i++)
                {
                    Avr[j] += ListData[i][j]; 
                }
            }

            for (j = 0; j < Avr.Length; j++)
            {
               
                    Avr[j] /= ListData.Count;
                
            }

            return Avr;
        }

        public static double[,] ListAvr(List<double[,]> ListData)
        {
            int Row, Col;
            double[,] Avr = new double[ListData[0].GetLength(0), ListData[0].GetLength(1)];
            int i, j,k;
            Row = ListData[0].GetLength(0);
            Col = ListData[0].GetLength(1);
            Avr = new double[Row, Col];
            for (i = 0; i < Row; i++)
            {
                for (j = 0; j < Col; j++)
                {
                    for (k = 0; k < ListData.Count; k++)
                    {
                        Avr[i,j] += ListData[k][i,j];
                    }
                }
            }

            for (i = 0; i < Row; i++)
            {
                for (j = 0; j < Col; j++)
                {
                    Avr[i, j] /= ListData.Count;
                }
            }

            return Avr;
        }

        public static string ToString<T>(T[] FileContent)
        {
            StringBuilder Str = new StringBuilder();
            int i;
            for(i=0;i< FileContent.Length-1;i++)
            {
                if(FileContent[i] != null)
                Str.Append(FileContent[i].ToString() + ",");
            }
            if (FileContent[i] != null)
                Str.Append(FileContent[i].ToString());
            return Str.ToString();
        }

        public static string ToString(UInt32[] InData, string Type, int ColNum, string Head = "")
        {
            int i;
            StringBuilder sb = new StringBuilder();
            if (InData.Length == 0)
                return String.Empty;
            for (i = 0; i < InData.Length-1; i++)
            {
                if (((i+1)%ColNum) == 0)
                    sb.Append(Head + InData[i].ToString(Type) + Environment.NewLine);
                else
                    sb.Append(Head + InData[i].ToString(Type)+",");
            }
            sb.Append(Head + InData[i].ToString(Type));
            return sb.ToString();
        }
        public static string ToString<T>(T[] FileContent,int StartIndex,int Len)
        {
            StringBuilder Str = new StringBuilder();
            int i;
            for (i = 0; i < Len - 1; i++)
            {
                Str.Append(FileContent[i+ StartIndex].ToString() + ",");
            }
            Str.Append(FileContent[i+ StartIndex].ToString());
            return Str.ToString();
        }

        public static string ToString<T>(List<T[,]> FileContent)
        {
            StringBuilder Str = new StringBuilder();
            int i,j;
            int Row = FileContent[0].GetLength(0);
            int Col = FileContent[0].GetLength(1);
            for (int m = 0; m < FileContent.Count; m++)
            {
                for (i = 0; i < Row; i++)
                {
                    for (j = 0; j < Col - 1; j++)
                    {
                        Str.Append(FileContent[m][i, j].ToString() + ",");
                    }
                    Str.Append(FileContent[m][i, j].ToString() + Environment.NewLine);
                }
            }

            return Str.ToString();
        }


        public static string ToString<T>(T[] FileContent0, T[] FileContent1)
        {
            StringBuilder Str = new StringBuilder();
            int i;

            for (i = 0; i < FileContent0.Length - 1; i++)
            {
                Str.Append(FileContent0[i].ToString() + ",");
            }
            Str.Append(FileContent0[i].ToString());

            Str.Append(Environment.NewLine);
            for (i = 0; i < FileContent1.Length - 1; i++)
            {
                Str.Append(FileContent1[i].ToString() + ",");
            }
            Str.Append(FileContent1[i].ToString());

            return Str.ToString();
        }

        public static string ToString<T>(T[] FileContent,int ColNum)
        {
            StringBuilder Str = new StringBuilder();
            int i,j;
            for (i = 0; i < FileContent.Length; i += ColNum)
            {
                for (j = 0; j < ColNum - 1; j++)
                {
                    Str.Append(FileContent[i + j].ToString() + ",");
                }
                Str.Append(FileContent[i + j].ToString() + Environment.NewLine);
            }
            return Str.ToString();
        }

        public static string ToString<T>(T[,] FileContent)
        {
            int Row = FileContent.GetLength(0);
            int Col = FileContent.GetLength(1);
            StringBuilder Str = new StringBuilder();
            int i, j;
            for (i = 0; i < Row; i++)
            {
                for (j = 0; j < Col - 1; j++)
                {
                    Str.Append(FileContent[i,j].ToString() + ",");
                }
                Str.Append(FileContent[i,j].ToString() + Environment.NewLine);
            }
            return Str.ToString();
        }

        public static string ToString<T>(T[,] FileContent,bool MulRow = true)
        {
            int Row = FileContent.GetLength(0);
            int Col = FileContent.GetLength(1);
            StringBuilder Str = new StringBuilder();
            int i, j;
            for (i = 0; i < Row; i++)
            {
                for (j = 0; j < Col; j++)
                {
                    Str.Append(FileContent[i, j].ToString() + ",");
                }
            }
            return Str.ToString().Trim(',');
        }

        public static string ToString<T>(List<T[]> FileContent)
        {
            int Row = FileContent.Count;
            int Col = FileContent[0].Length;
            StringBuilder Str = new StringBuilder();
            int i, j;
            for (i = 0; i < Row; i++)
            {
                for (j = 0; j < Col - 1; j++)
                {
                    Str.Append(FileContent[i][j].ToString() + ",");
                }
                Str.Append(FileContent[i][j].ToString() + Environment.NewLine);
            }
            return Str.ToString();
        }

        public static T[] GetOneRow<T>(T[,] InData, int RowIndex)
        {
            int Col = InData.GetLength(1);
            T[] OutData = new T[Col];
            for (int i = 0; i < Col; i++)
            {
                OutData[i] = InData[RowIndex, i];
            }
            return OutData;
        }

        public static void SetOneRow<T>(T[,] InData,T[] RowData, int RowIndex)
        {
            int Col = InData.GetLength(1);
            for (int i = 0; i < Col; i++)
            {
                InData[RowIndex, i] = RowData[i];
            }
        }

        public static T[] GetOneCol<T>(T[,] InData, int ColIndex)
        {
            int Row = InData.GetLength(0);
            T[] OutData = new T[Row];
            for (int i = 0; i < Row; i++)
            {
                OutData[i] = InData[i, ColIndex];
            }
            return OutData;
        }

        public static void SetOneCol<T>(T[,] InData,T[] ColData, int ColIndex)
        {
            int Row = InData.GetLength(0);
            int Col = InData.GetLength(1);
            for (int i = 0; i < Row; i++)
            {
                InData[i, ColIndex] = ColData[i];
            }
        }

        public static void SetOneCol<T>(T[,] InData, T ColData, int ColIndex)
        {
            int Row = InData.GetLength(0);
            int Col = InData.GetLength(1);
            for (int i = 0; i < Row; i++)
            {
                InData[i, ColIndex] = ColData;
            }
        }

        public static double CalcExpression(string expression)
        {
            if (expression == null || expression == "")
            {
                return 0;
            }
            try
            {
                var ex = Microsoft.JScript.Vsa.VsaEngine.CreateEngine();
                object returnValue = Microsoft.JScript.Eval.JScriptEvaluate(expression, ex);
                string Msg = returnValue.ToString();
                return Convert.ToDouble(Msg);
            }
            catch
            {
                return 0;
            }
        }

        public static double CalcExpression(string expression,Dictionary<string,object> Ht)
        {
            double res=0;
            ExpressionEvaluator evaluator = new ExpressionEvaluator();
            evaluator.Variables = Ht;
            try
            {
                res  =  Convert.ToDouble(evaluator.Evaluate(expression));
                return res;
            }
            catch
            { return 0; }   
        }

        public static string CalcExpressionToString(string expression)
        {
            var ex = Microsoft.JScript.Vsa.VsaEngine.CreateEngine();
            object returnValue = Microsoft.JScript.Eval.JScriptEvaluate(expression, ex);
            string Msg = returnValue.ToString();
            return Msg;
        }

        public static string MidStr(string sourse, string startstr, string endstr)
        {
            string result = string.Empty;
            int startindex, endindex;
            try
            {
                startindex = sourse.IndexOf(startstr);
                if (startindex == -1)
                    return result;
                string tmpstr = sourse.Substring(startindex + startstr.Length);
                endindex = tmpstr.IndexOf(endstr);
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch (Exception )
            {
                ;
            }
            return result;
        }
        public static bool CmparaArray(byte[] Data0,int Start0, byte[] Data1,int Start1,int Len)
        {
            if ((Start0 + Len) > Data0.Length || (Start1 + Len) > Data1.Length)
                return false;
            for (int i = 0; i < Len; i++)
            {
                if(Data0[Start0+i] != Data1[Start1+i])
                    return false;
            }
            return true;
        }

        public static bool CmparaArray(byte[] Data0, byte[] Data1)
        {
            if (Data0.Length != Data1.Length)
                return false;
            for (int i = 0; i < Data1.Length; i++)
            {
                if (Data0[i] != Data1[i])
                    return false;
            }
            return true;
        }

        public static byte[] GetDataFromStr(string Context)
        {
            byte[] mData;
            string[] LineStr;

            if (Context.Contains("{"))
                Context = MidStr(Context, "{", "}");
            if (Context.Contains("["))
                Context = MidStr(Context, "[", "]");
            if (Context.Contains(","))
            {
                Context = WilfDataPro.RemoiveMulSapce(Context, false);
                LineStr = Context.Split(',');
            }
            else
            {
                Context = WilfDataPro.RemoiveMulSapce(Context, true).Trim();
                LineStr = Context.Split(' ');
            }
            mData = new byte[LineStr.Length];
            for (int i = 0; i < LineStr.Length; i++)
            {
                if(LineStr[i].Contains("0x")==true)
                    mData[i] = Convert.ToByte(LineStr[i],16);
                else
                    mData[i] = Convert.ToByte(LineStr[i]);
            }
            return mData;
        }

        public static UInt32[] GetData32FromStr(string Context)
        {
            UInt32[] mData;
            string[] LineStr;
            if (Context.Contains("{"))
                Context = MidStr(Context,"{", "}");
            if (Context.Contains("["))
                Context = MidStr(Context, "[", "]");
            if (Context.Contains(","))
            {
                Context = WilfDataPro.RemoiveMulSapce(Context, false);
                LineStr = Context.Split(',');
            }
            else
            {
                Context = WilfDataPro.RemoiveMulSapce(Context, true).Trim();
                LineStr = Context.Split(' ');
            }
            mData = new UInt32[LineStr.Length];
            for (int i = 0; i < LineStr.Length; i++)
            {
                mData[i] =  (UInt32)CalcExpression(LineStr[i]);
            }
            return mData;
        }

        public static string ToString(byte[] InData,string Type,int ColNum)
        {
            int i;
            StringBuilder sb = new StringBuilder();
            if (InData==null || InData.Length==0)
                return "";
            for(i = 0;i < InData.Length-1;i++)
            {
                if(((i+1)%ColNum) == 0)
                    sb.Append(InData[i].ToString(Type) + Environment.NewLine);
                else
                    sb.Append(InData[i].ToString(Type)+",");
            }
            sb.Append(InData[i].ToString(Type));
            return sb.ToString();
        }

        public static string ToString(byte[] InData, string Type, int ColNum, string Head = "")
        {
            int i;
            StringBuilder sb = new StringBuilder();
            for (i = 0; i < InData.Length-1; i++)
            {
                if (((i+1)%ColNum) == 0)
                    sb.Append(Head + InData[i].ToString(Type) + Environment.NewLine);
                else
                    sb.Append(Head + InData[i].ToString(Type)+",");
            }
            sb.Append(Head + InData[i].ToString(Type));
            return sb.ToString();
        }

        public static int[] ParseRange2(string InStr)
        {
            List<int> Range = new List<int>();
            int Start = 0, Step = 0, End = 0;
            InStr = InStr.Replace(" ", "");
            string[] str = InStr.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i].Contains(":"))
                {
                    string[] Tempstr = str[i].Split(':');
                    if (Tempstr.Length == 2)
                    {
                        Start = Convert.ToInt32(Tempstr[0]);
                        Step = 1;
                        End = Convert.ToInt32(Tempstr[1]);
                    }
                    else if (Tempstr.Length == 3)
                    {
                        Start = Convert.ToInt32(Tempstr[0]);
                        Step = Convert.ToInt32(Tempstr[1]);
                        End = Convert.ToInt32(Tempstr[2]);
                    }
                    for (int k = Start; k <= End; k++)
                    {
                        Range.Add(k);
                    }
                }
                else
                    Range.Add(Convert.ToInt32(str[i]));
            }
            return Range.ToArray();
        }

        public static UInt32[] IndexToBit(int[] InData, int Len)
        {
            UInt32[] Mask = new UInt32[Len];
            Array.Clear(Mask, 0, Mask.Length);
            int Value;
            for (int i = 0; i < InData.Length; i++)
            {
                Value = InData[i];
                if (Value >= 0)
                {
                    Mask[Value / 32] |= (UInt32)(1 << (int)(Value % 32));
                }
                else
                {
                    Value = Math.Abs(Value);
                    Mask[Value / 32] &= (UInt32)(~(1 << (int)(Value % 32)));
                }
            }
            return Mask;
        }

        public static void SortHashTable(Hashtable ht,out string[] Item,out int[] Value)
        {
            int k = 0;
            Item = new string[ht.Count];
            Value = new int[ht.Count];
            foreach (DictionaryEntry de in ht) //ht为一个Hashtable实例
            {
                Item[k] = (string)de.Key;
                Value[k] = (int)de.Value;
                k++;
            }
            SortId(Value, Item);
        }

        public static void SortId(int[] Id, string[] Name)
        {
            int Temp;
            string TempStr;
            int length = Id.Length; // 数组未排序序列的长度
            do
            {
                for (int j = 0; j < length - 1; j++) // 数组未排序序列的倒第二数
                {
                    if (Id[j] > Id[j + 1])
                    {
                        Temp = Id[j];
                        Id[j] = Id[j + 1];
                        Id[j + 1] = Temp;

                        TempStr = Name[j];
                        Name[j] = Name[j + 1];
                        Name[j + 1] = TempStr;
                    }
                }
                length--; // 每次遍历后，就会确定一个最大值，未排序序列的长度减1
            }
            while (length > 1); // 当数组未排序序列只剩最后一个数时，就不需要排序了

        }

        public static bool Cmp(byte[] data_l, byte[] data_r)
        {
            bool ret = true;
            if (data_l.Length != data_r.Length)
                return false;
            for(int i=0;i<data_l.Length;i++) 
            {
                if (data_l[i] != data_r[i])
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }

        public static void InitStartTime()
        {
             StartTime = DateTime.Now;
        }

        public static double GetDiffTime(bool IsNeedInitStartTime=false)
        {
            TimeSpan DiifTime;
            DiifTime = DateTime.Now.Subtract(StartTime);
            if (IsNeedInitStartTime == true)
                InitStartTime();
            return DiifTime.TotalMilliseconds;
        }

        public static void UpdateSeed()
        {
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            rnd = new Random(seekSeek);
        }
        public static byte[] GetRandData(int Length)
        {
            byte[] Data = new byte[Length];
            for (int i = 0; i<Data.Length; i++)
            {
                Data[i] = (byte)rnd.Next();
            }
            return Data;
        }

        public static UInt32[] GetRandData32(int Length)
        {
            UInt32[] Data = new UInt32[Length];

            for (int i = 0; i<Data.Length; i++)
            {
                Data[i] = (UInt32)rnd.Next();
            }
            return Data;
        }

        public static int GetDiffNum(byte[] Data0, byte[] Data1,int Length)
        {
            int Num = 0;
            for (int i = 0; i<Length; i++)
            {
                if (Data0[i]!=Data1[i])
                {
                    Num++;
                }
            }
            return Num;
        }

        public static void Clear(byte[] data, int index, int length, byte value)
        {
            for (int i = 0; i<length; i++)
            {
                data[index+i] = value;
            }
        }

        public static byte[] Revert(byte[] bytes)
        {
            byte[] rev_byte = new byte[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                rev_byte[bytes.Length-i-1] = bytes[i];
            }
            return rev_byte;
        }

        public static string FindApp(string KeyStr, bool RetrunDir = true)
        {
            string tempType = null;
            object displayName = null, uninstallString = null, releaseType = null;
            RegistryKey currentKey = null;
            int softNum = 0;
            KeyStr = KeyStr.ToUpper();
            RegistryKey pregkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");//获取指定路径下的键
            try
            {
                foreach (string item in pregkey.GetSubKeyNames())               //循环所有子键
                {
                    currentKey = pregkey.OpenSubKey(item);
                    displayName = currentKey.GetValue("DisplayName");           //获取显示名称
                    uninstallString = currentKey.GetValue("UninstallString");   //获取卸载字符串路径
                    releaseType = currentKey.GetValue("ReleaseType");           //发行类型,值是Security Update为安全更新,Update为更新
                    bool isSecurityUpdate = false;
                    if (releaseType != null)
                    {
                        tempType = releaseType.ToString();
                        if (tempType == "Security Update" || tempType == "Update")
                            isSecurityUpdate = true;
                    }
                    if (!isSecurityUpdate && displayName != null && uninstallString != null)
                    {
                        softNum++;

                        if (displayName.ToString().ToUpper().Contains(KeyStr))
                        {

                            if (RetrunDir == true)
                                return Path.GetDirectoryName(uninstallString.ToString());
                            else
                                return uninstallString.ToString();
                        }
                    }
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message.ToString());
            }

            pregkey.Close();
            return "";
        }

        public static UInt32 GetBit(byte[] Data,UInt32 Addr, UInt32 StartBit, UInt32 Size)
        {
            UInt32[] D32 = ToUint32(Data);
            UInt32 Value = D32[Addr/4];
            UInt32 OutValue = 0;
            StartBit += 8*(Addr%4);
            Value = Value>>(int)StartBit;
            for (int i=0;i<Size;i++) 
            { 
                if((Value&0x01) > 0)
                {
                    OutValue |= (UInt32)(1<<i);
                }
                Value >>=1;
            }
            return OutValue;
        }

        public static UInt32 GetMask(int StartBit, int EndBit)
        {
            UInt32 Mask = 0;
            for (int i = StartBit; i <= EndBit; i++)
                Mask |= (UInt32)(1 << i);
            return Mask;
        }
    }
}
