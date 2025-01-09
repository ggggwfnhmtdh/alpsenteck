using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Management;
using Microsoft.VisualBasic;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.VisualBasic.CompilerServices;
using System.Drawing;

namespace SOM_DEVELOP_TOOL
{
    public static class Device
    {
        public static bool Device_Online = false;

        public static void SetDeviceStatus(bool Status)
        {
            Device_Online = Status;
        }
        public static bool GetDeviceStatus()
        {
            return Device_Online;
        }

        public static string FindVisualCom(int VID,int PID)
        {
            string Pattern = "VID_XXXX";
            string[] available_spectrometers = SerialPort.GetPortNames();
            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            string commData = "";
            List<string> ComPortList = new List<string>();
            List<int> ComPortTypeList = new List<int>();
            Pattern = Pattern.Replace("XXXX", VID.ToString("X4"));
            Pattern = Pattern.Replace("YYYY", PID.ToString("X4"));

            ManagementObjectSearcher mObjs = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM WIN32_PnPEntity");
            try
            {
                enumerator = mObjs.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;

                    if (Strings.InStr(Conversions.ToString(current["Caption"]), "(COM", CompareMethod.Binary) <= 0)
                    {
                        continue;
                    }
                    string Msg = current["DeviceID"].ToString();
                    if (current["DeviceID"].ToString().Contains(Pattern))   //wifi
                    {
                        commData = current["Name"].ToString();
                        commData = commData.Substring(commData.IndexOf("(") + 1, commData.IndexOf(")") - commData.IndexOf("(") - 1);

                    }
                    else
                    {
                       // Debug.AppendMsg(Msg+Environment.NewLine);
                    }
                    
                }
               
            }
            finally
            {
                if (enumerator != null)
                {
                    enumerator.Dispose();
                }
            }
            return commData;

        }

        public static bool CheckCpuConnected()
        {
            return Open(JlinkOp.Protocol, JlinkOp.SpeedKHz, false);
        }

        public static bool Open(string ProtocolName, int Speed_KHz,bool ShowLog=true)
        {
            int SN = 0;
            sbyte[] acIn = new sbyte[256];
            sbyte[] acOut = new sbyte[256];
            int Result;
            string sError;
            ProtocolName = ProtocolName.ToUpper();
            SetDeviceStatus(false);
            if (FindJlinkDevice(JlinkOp.DeviceName) == true)
            {
                SN = JlinkOp.JLINKARM_GetSN();
                if (ShowLog == true)
                    Debug.AppendMsg("[Inf] Find Jlink Emulators Via Usb" +"(SN:"+SN+","+ProtocolName+","+Speed_KHz+"KHz)"+ Environment.NewLine, Color.Green);
            }
            else
            {
                if (ShowLog == true)
                    Debug.AppendMsg("[Error] Can't Find Jlink Emulators Via Usb"+Environment.NewLine, Color.Red);
                return false;
            }
            sError = JlinkOp.JLINKARM_OpenEx(null, JlinkOp.pJLINKARM_LOG_callback_pfErrorOut);
            if (sError != null)
            {
                if (ShowLog == true)
                    Debug.AppendMsg(sError);
                return false;
            }

            acIn = Array.ConvertAll((Encoding.ASCII.GetBytes("Device = " + "Cortex-M7")), q => Convert.ToSByte(q));
            JlinkOp.JLINKARM_ExecCommand(acIn, out acOut, 256);

            if (ProtocolName == "JTAG")
            {
                JlinkOp.JLINKARM_TIF_Select(0);           // JTAG
            }
            else
            {
                JlinkOp.JLINKARM_TIF_Select(1);           // SWD
            }
            JlinkOp.JLINKARM_SetSpeed(Speed_KHz);
            Result = JlinkOp.JLINKARM_Connect();

            if (Result < 0)
            {
                if (ShowLog == true)
                    Debug.AppendMsg("[Error] Can't Connect CPU Via Jlink Emulators"+Environment.NewLine, Color.Red);
                JlinkOp.JLINKARM_Close();
                return false;
            }
            else
            {
                if (ShowLog == true)
                    Debug.AppendMsg("[Inf] Connect CPU Via Jlink Emulators Successfully"+Environment.NewLine, Color.Green);
                SetDeviceStatus(true);
                return true;
            }

        }

        public static void Close()
        {
            JlinkOp.Close();
            Device.SetDeviceStatus(false);
        }
        public static bool FindJlinkDevice(string device_feature)
        {
#if false
            string device_name;
            string SN = "";
            device_feature = device_feature.ToUpper();
            ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
            ManagementObjectSearcher mObjs = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM WIN32_PnPEntity");
            try
            {
                enumerator = mObjs.Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ManagementObject current = (ManagementObject)enumerator.Current;
                    device_name = current.ToString().ToUpper().Replace("\"","");
                    if(device_name.Contains(device_feature))
                    {
                        string Temp = device_name.Substring(device_name.IndexOf(device_feature), device_name.Length - device_name.IndexOf(device_feature));
                        if (Temp.Contains("&MI_") == false)
                        {
                            SN = Temp.Substring(Temp.LastIndexOf("\\") + 1, Temp.Length - Temp.LastIndexOf("\\") - 1);
                            SN = SN.Trim(new char[] { '0' });
                            return true;
                        }
                    }
                }
            }
            finally
            {
                if (enumerator != null)
                {
                    enumerator.Dispose();
                }
            }
            return false;
#else
            UInt32 device_num;
            device_num = JlinkOp.JLINKARM_EMU_GetNumDevices();
            return device_num>0;
#endif

        }
    }
}
