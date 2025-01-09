using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace SOM_DEVELOP_TOOL
{
    public static class Debug
    {
        public delegate void ShowMsgFun(string Msg);
        public delegate void ShowMsgFunColor(string Msg,Color color);
        public static ShowMsgFun ShowMsg = null;
        public static ShowMsgFunColor ShowMsgColor = null;
        public static ShowMsgFun ShowRTTMsg = null;
        public static int ShowIndex = 0;
        public static MainForm pMainF;

        public static ShowMsgFun ShowMsg2 = null;
        public static ShowMsgFunColor ShowMsgColor2 = null;

        public static void AppendMsgRTT(string Msg)
        {
            if (ShowRTTMsg != null)
                ShowRTTMsg(Msg);
        }

        public static void LoadLogFrom()
        {
            if(ShowIndex == 1)
            {
                pMainF.LogForm_Pro();
            }
        }

        public static void AppendMsg(string Msg)
        {
            LoadLogFrom();
            if (ShowMsg != null)
                ShowMsg(Msg);
        }

        public static void AppendMsg(string Msg, bool NewLine = false)
        {
            LoadLogFrom();
            if (ShowMsg != null)
            {
                if (NewLine)
                    ShowMsg(Msg+Environment.NewLine);
                else
                    ShowMsg(Msg);
            }
        }

        public static void AppendMsg(string Msg,Color color)
        {
            LoadLogFrom();
            if (ShowMsgColor != null)
                ShowMsgColor(Msg,color);
        }

        public static void AppendMsgMore(string Msg)
        {
            StackTrace st = new StackTrace(new StackFrame(true));
            StackFrame sf = st.GetFrame(0);
            if (ShowMsg != null)
            {
                ShowMsg(sf.GetMethod() + ":" + Msg);
            }
        }

        public static void AppendData<T>(T[] Data)
        { 
            string Msg = WilfDataPro.ToString(Data);
            ShowMsg(Msg);
        }

        public static void AppendRegisterInf(UInt32 Addr, UInt32 Value,bool IsRead)
        {
            string Msg = "";
            string prefix;
            if (IsRead)
                prefix = "R";
            else
                prefix = "W";
            Msg += prefix + "[0x" + Addr.ToString("X8") + "]=";
            Msg += "0x" + Value.ToString("X8");
            Msg += "(" + Value.ToString() + ")";
            if(IsRead)
                AppendMsg(Msg + Environment.NewLine,Color.Green);
            else
                AppendMsg(Msg + Environment.NewLine, Color.Red);
        }

        public static void AppendRegisterInf(UInt32 Addr, UInt32 Value, bool IsRead,int StartBit,int EndBit)
        {
            string Msg = "";
            string prefix;
            if (IsRead)
                prefix = "R";
            else
                prefix = "W";
            Msg += prefix + "[0x" + Addr.ToString("X8") + "]=";
            Msg += "0x" + Value.ToString("X8");
            Msg += "(" + Value.ToString() + ")";
            if((StartBit == 0 && EndBit == 31) == false)
                Msg += "    //Bit[" + StartBit + ":" + EndBit+"]";
            if (IsRead)
                AppendMsg(Msg + Environment.NewLine, Color.Green);
            else
                AppendMsg(Msg + Environment.NewLine, Color.Red);
        }

        public static void AppendRegisterInf(UInt32 Addr, UInt32[] Value,bool IsRead, Color color)
        {
            int i;
            string Msg = "";
            string prefix;
            if (IsRead)
                prefix = "R";
            else
                prefix = "W";
            Msg += prefix + "[0x" + Addr.ToString("X8") + "]=";
            for(i = 0; i < Value.Length-1; i++)
                Msg += "0x" + Value[i].ToString("X8") +"("+Value[i].ToString()+")"+",";
            Msg += "0x" + Value[i].ToString("X8") + "(" + Value[i].ToString() + ")" + Environment.NewLine;
            AppendMsg(Msg, color);
        }


    }
}
