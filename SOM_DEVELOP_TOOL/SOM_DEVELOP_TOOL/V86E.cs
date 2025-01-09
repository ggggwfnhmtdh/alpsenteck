using NPOI.HSSF.Record.PivotTable;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM_DEVELOP_TOOL
{
    public class V86E
    {
        public static int VID = 0x10C4;
        public static int PID = 0xEA60;

        public static string FindDevice()
        {
            return Device.FindVisualCom(VID,PID);
        }

        private static byte[] Read(SerialPort serialPort1, int Length)
        {
            byte[] data = new byte[Length];
            int CurLen = 0;
            int LeftLen;
            LeftLen = Length;
            while (true)
            {
                int TempLen = serialPort1.Read(data, CurLen, Length-CurLen);
                CurLen += TempLen;
                if (CurLen>=Length)
                    break;
            }
            return data;
        }

        public static double ReadMultmeter(string PortName)
        {
            double Result = 0;
            byte[] data;
            byte[] r_data = new byte[14];
            bool is_ok = false;
            SerialPort serialPort1 = new SerialPort();
            serialPort1.BaudRate = 9600;
            serialPort1.PortName = PortName;
            serialPort1.Parity = Parity.None;
            int Time = 100;
            try
            {
                serialPort1.Open();
                while (true)
                {
                    if (is_ok)
                    {
                        r_data = Read(serialPort1,14);
                        break;
                    }
                    else
                    {
                        data = Read(serialPort1,1);
                        r_data[0] = data[0];
                        if (data[0] == 0x02B || data[0] == 0x02D)
                        {
                            data = Read(serialPort1, 13);
                            Array.Copy(data, 0, r_data, 1, data.Length);
                            break;
                        }
                        Time--;
                    }
                    if(Time == 0)
                    {
                        serialPort1.Close();
                        return 0;
                    }
                }

                string data_str = System.Text.Encoding.UTF8.GetString(r_data);
                int value = Convert.ToInt32(data_str.Substring(1, 5));
                int point = Convert.ToInt32(data_str.Substring(6, 1));
                Result = value / Math.Pow(10, point+1);
                is_ok = true;
                serialPort1.Close();
            }
            catch 
            {
                return 0;
            }
            return Result;
        }
    }
}
