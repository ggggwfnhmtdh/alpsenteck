using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices;
//using MathNet.Numerics.Statistics;
namespace Yolov5App
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
        public static Queue<double>[] dataQueue;
        public static bool QueueInitFLag = false;
        public static int QueueMaxNum = 0;
        public static List<byte> m_data;


        public static string RemoveMulSapce(string InStr)
        {
            InStr = Regex.Replace(InStr.Trim(), @"\s+", " ");
            return InStr ;
        }
        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
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
        public static string RemoiveMulSapce(string InStr)
        {
           return Regex.Replace(InStr.Trim(), @"\s+", " ");
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
                Str.Append(FileContent[i].ToString() + ",");
            }
            Str.Append(FileContent[i].ToString());
            return Str.ToString();
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
            //var ex = Microsoft.JScript.Vsa.VsaEngine.CreateEngine();
            //object returnValue = Microsoft.JScript.Eval.JScriptEvaluate(expression, ex);
            //string Msg = returnValue.ToString();
            //return Convert.ToDouble(Msg); 
            return 0;
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

        public static byte[] GetDataFromStr(string Context)
        {
            byte[] mData;
            string[] LineStr;
            Context = Context.Replace(" ", "");
            Context.Trim('\n');
            Context.Trim('\r');
            Context.Trim(',');
            Context.Trim(';');
            LineStr = Context.Split(',');
            mData = new byte[LineStr.Length];
            for (int i = 0; i < LineStr.Length; i++)
            {
                mData[i] = Convert.ToByte(LineStr[i]);
            }
            return mData;
        }

        public static string ByteToString(byte[] InData,string Type,int ColNum)
        {
            int i;
            StringBuilder sb = new StringBuilder();
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
    }
}
