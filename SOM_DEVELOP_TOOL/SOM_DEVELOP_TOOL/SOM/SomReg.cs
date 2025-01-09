using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using Microsoft.VisualBasic.ApplicationServices;
using NPOI.SS.Formula.Functions;
using static NPOI.HSSF.Util.HSSFColor;
namespace SOM_DEVELOP_TOOL
{
    public struct RegData
    {
        public UInt32 Addr;
        public string ModuleName;
        public List<RegBitData> Data;
    }

    public enum RegTestType
    {
        Default,
        Reset,
        Crosstalk,
        RW,
        Attribute
    }

    public struct RegBitData
    {
        public string ModuleName;
        public UInt32 Addr;
        public string RegName;
        public UInt32 StartBit;
        public UInt32 EndBit;
        public UInt32 BitWidth;
        public UInt32 DefaultValue;
        public string Des;
        public string Attribute;
        public UInt32 Value;
        public UInt32 RecordValue;
        public bool Option;
    }
    public class SomReg
    {
        public static string[] Title = new string[] { "Module", "Address", "RegName", "BitRange", "Attribute", "Value", "DefaultValue", "Option", "Description" };
        public static RegBitData[] RegBitList;
        public static RegData[] RegList;
        public static Hashtable Name2Index;

        public static RegBitData[] LoadBitFile(string TableFile)
        {
            string Dir;
            string str;
            string[] strs;
            Name2Index = new Hashtable();
            List<RegBitData> RegList = new List<RegBitData>();
            if (File.Exists(TableFile) == false)
            {
                TableFile = WilfFile.OpenFile(".csv");
                if (File.Exists(TableFile) == false)
                    return null;
            }
            Dir = Path.GetDirectoryName(TableFile) +"\\"+ Path.GetFileNameWithoutExtension(TableFile);
            string[] lines = File.ReadAllLines(TableFile);
            for (int i = 1; i<lines.Length; i++)
            {
                RegBitData RegData = new RegBitData();
                string[] Content = lines[i].Split(',');
                RegData.ModuleName = Content[0];
                RegData.Addr = Convert.ToUInt32(Content[1], 16);
                RegData.RegName = Content[2];
                str = WilfDataPro.MidStr(Content[3], "[", "]");
                strs = str.Split(':');
                RegData.StartBit = Convert.ToUInt32(strs[0]);
                RegData.BitWidth = Convert.ToUInt32(strs[1])+1 - RegData.StartBit;
                RegData.EndBit = RegData.StartBit + RegData.BitWidth -1;
                RegData.Attribute = Content[4];
                RegData.Value =Convert.ToUInt32(Content[5], 16);
                RegData.DefaultValue =Convert.ToUInt32(Content[6], 16);
                RegData.Option = (Content[7].Trim().ToUpper()=="Y");
                RegData.Des = Content[8];
                RegList.Add(RegData);
                if (Name2Index.ContainsKey(RegData.RegName) == false)
                {
                    Name2Index.Add(RegData.RegName, i-1);
                }
            }
            return RegList.ToArray();
        }

        public static UInt32 GetValue(RegBitData[] Reg,string Name)
        {
            int index;
            if (Name2Index.ContainsKey(Name))
            {
                index = (int)Name2Index[Name];
                return Reg[index].Value;
            }
            else
                return 0;
        }
        public static string ToString(RegData[] LstData)
        {
            int i;
            StringBuilder Msg = new StringBuilder();
            UInt32 RegAddr;
            string RegName;
            string ModuleName;
            UInt32 DefaultValue;
            string BitRange;
            string Des;

            for (i = 0; i < Title.Length-1; i++)
            {
                Msg.Append(Title[i]+",");
            }
            Msg.Append(Title[i]+Environment.NewLine);

            for (i = 0; i < LstData.Length; i++)
            {
                ModuleName = LstData[i].ModuleName;
                RegAddr = LstData[i].Addr;
                for (int j = 0; j < LstData[i].Data.Count; j++)
                {
                    RegName = LstData[i].Data[j].RegName;
                    DefaultValue = LstData[i].Data[j].DefaultValue;
                    Des = LstData[i].Data[j].Des.Replace(',', ' ');
                    BitRange = LstData[i].Data[j].StartBit + ":" + (LstData[i].Data[j].StartBit + LstData[i].Data[j].BitWidth - 1).ToString();
                    Msg.Append(ModuleName + ",");
                    Msg.Append("0x" + RegAddr.ToString("X8") + ",");
                    Msg.Append(RegName + ",");
                    Msg.Append("["+BitRange +"]" + ",");
                    Msg.Append(LstData[i].Data[j].Attribute + ",");
                    Msg.Append("0x" + LstData[i].Data[j].Value.ToString("X2") + ",");
                    Msg.Append("0x" + DefaultValue.ToString("X2") + ",");
                    Msg.Append("Y" + ",");
                    Msg.Append(Des + Environment.NewLine);
                }
            }
            return Msg.ToString();
        }

        public static bool CheckDefultValue(RegBitData[] LstData)
        {
            bool ret = true;
            for (int i = 0; i < LstData.Length; i++)
            {
                if (LstData[i].Value != LstData[i].DefaultValue)
                {
                    Debug.AppendMsg("[Error]"+LstData[i].RegName+"="+LstData[i].Value+"("+"Addr:0x"+LstData[i].Addr.ToString("X2")+" Default:"+LstData[i].DefaultValue+" StartBit:"+LstData[i].StartBit+" BitWidth:"+LstData[i].BitWidth+")"+Environment.NewLine);
                    ret = false;
                }
            }
            return ret;
        }

        public static bool CheckRecordValue(RegBitData[] LstData)
        {
            bool ret = true;
            for (int i = 0; i < LstData.Length; i++)
            {
                if (LstData[i].Value != LstData[i].RecordValue)
                {
                    Debug.AppendMsg("[Error]"+LstData[i].RegName+"="+LstData[i].Value+"("+"Addr:0x"+LstData[i].Addr.ToString("X2")+" Record:"+LstData[i].DefaultValue+" StartBit:"+LstData[i].StartBit+" BitWidth:"+LstData[i].BitWidth+")"+Environment.NewLine);
                    ret = false;
                }
            }
            return ret;
        }
        public static string ToString(RegBitData[] LstData)
        {
            int i;
            StringBuilder Msg = new StringBuilder();
            string BitRange;
            string Des;

            for (i = 0; i < Title.Length-1; i++)
            {
                Msg.Append(Title[i]+",");
            }
            Msg.Append(Title[i]+Environment.NewLine);

            for (i = 0; i < LstData.Length; i++)
            {
                
                Des = LstData[i].Des.Replace(',', ' ');
                BitRange = LstData[i].StartBit + ":" + (LstData[i].StartBit + LstData[i].BitWidth - 1).ToString();
                Msg.Append(LstData[i].ModuleName + ",");
                Msg.Append("0x" + LstData[i].Addr.ToString("X8") + ",");
                Msg.Append(LstData[i].RegName + ",");
                Msg.Append("["+BitRange +"]" + ",");
                Msg.Append(LstData[i].Attribute + ",");
                Msg.Append("0x" + LstData[i].Value.ToString("X") + ",");
                Msg.Append("0x" + LstData[i].DefaultValue.ToString("X") + ",");
                Msg.Append("Y" + ",");
                Msg.Append(Des + Environment.NewLine);
                
            }
            return Msg.ToString();
        }

        public static UInt32 GetRevertBit(UInt32 BitWidth,int Type)
        {
            UInt32[] Pattern5 = new UInt32[] { 0x55555555, 0xAAAAAAAA, 0x00000000, 0xFFFFFFFF,0xFFFF0000,0x0000FFFF};
            UInt32 MASK;
            int StartBit, EndBit;
            int Range;
            StartBit = (int)(16-BitWidth/2);
            EndBit = (int)(StartBit+BitWidth-1);
            MASK = WilfDataPro.GetMask(StartBit, EndBit);

            if (Type<Pattern5.Length)
                return (Pattern5[Type]&MASK)>>StartBit;
            else
            {
                Random rnd = new Random();  
                return (UInt32)((rnd.Next()&MASK)>>StartBit);
            }
        }
    }
}
