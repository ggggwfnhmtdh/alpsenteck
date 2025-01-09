using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using NPOI.HSSF.Model;
using NPOI.SS.Formula.Functions;
using System.Windows.Forms;
using Microsoft.JScript;
using NPOI.XSSF.Model;

namespace SOM_DEVELOP_TOOL
{
    public struct ArmMapInf
    {
        public string Name;
        public UInt32 Addr;
        public UInt32 Size;
        public string Type;
    }
    public static class MapParser
    {
        public static string[] ArmFunctionList = null;
        public static Hashtable ArmAddrHash;
        public static Hashtable ArmNameHash;
        public static Dictionary<string, object> ParamHash;
        public static string MapInfFile = Application.StartupPath + "\\MapInf.a";
        public static string DumpFile = Application.StartupPath + "\\FunctionInf.txt";

        public static string DumpHashTable(Hashtable InTable,UInt32 StartAddr,UInt32 EndAddr,UInt32 Offset=0)
        {
            StringBuilder sb = new StringBuilder();
            if (InTable == null)
            {
                return "";
            }
            foreach (UInt32 key in InTable.Keys)
            {
                if (key > StartAddr && key < EndAddr)
                {
                    sb.Append("0x" + (key + Offset).ToString("X8") + "," + (string)InTable[key] + Environment.NewLine);
                }
            }
            return sb.ToString();
        }

        public static UInt32 GetAddr(string VarName)
        {
            if (ArmNameHash!= null && ArmNameHash.ContainsKey(VarName))
                return ((ArmMapInf)ArmNameHash[VarName]).Addr;
            else
                return UInt32.MaxValue;
        }

        public static string CalcExpress(string ItemStr)
        {
            UInt32 Addr;
            if (WilfDataPro.IsNumeric(ItemStr) == false)
            {
                Addr = MapParser.GetAddr(ItemStr);
                if (Addr != 0xFFFFFFFF)
                {
                    ItemStr = Addr.ToString();
                }
                else
                {
                    Addr = (UInt32)WilfDataPro.CalcExpression(ItemStr, MapParser.ParamHash);
                    ItemStr = Addr.ToString();
                }
            }
            return ItemStr;
        }

        public static UInt32 GetSize(string VarName)
        {
            if (ArmNameHash!= null && ArmNameHash.ContainsKey(VarName))
                return ((ArmMapInf)ArmNameHash[VarName]).Size;
            else
                return 0;
        }

        public static string GetType(string VarName)
        {
            if (ArmNameHash!= null && ArmNameHash.ContainsKey(VarName))
                return ((ArmMapInf)ArmNameHash[VarName]).Type;
            else
                return "";
        }

        public static void GetArmHash(string FileName)
        {
            if (!File.Exists(FileName))
                return;
            List<ArmMapInf> lst = ParseArmMapFile(FileName);
            ArmNameHash = new Hashtable();
            ArmAddrHash = new Hashtable();
            ParamHash =  new Dictionary<string, object>();
            for (int i = 0; i < lst.Count; i++)
            {

                if (ArmNameHash.ContainsKey(lst[i].Name) == false)
                { 
                    ArmNameHash.Add(lst[i].Name, lst[i]);
                    ParamHash.Add(lst[i].Name, lst[i].Addr);
                }

                if (ArmAddrHash.ContainsKey(lst[i].Addr) == false)
                    ArmAddrHash.Add(lst[i].Addr, lst[i]);
            }
        }


        public static void InitMapHashTable(string MapFile,bool NeedDump = false)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder[] sbx = new StringBuilder[3];
            List<string> FunList= new List<string>();
            if (File.Exists(MapFile))
            {
                MapParser.GetArmHash(MapFile);
                foreach(var key in ArmNameHash.Keys)
                {
                    ArmMapInf MapInf = (ArmMapInf)ArmNameHash[key];
                    sb.Append(MapInf.Name+",0x"+MapInf.Addr.ToString("X8")+","+MapInf.Type+","+MapInf.Size+Environment.NewLine);
                    if (MapInf.Type.ToLower() == "code")
                    {
                        FunList.Add(MapInf.Name);
                    }
                }
                File.WriteAllText(MapInfFile,sb.ToString());

                if (NeedDump)
                {
                    sbx[0] = new StringBuilder();
                    sbx[1] = new StringBuilder();
                    sbx[2] = new StringBuilder();
                    string[] Module = new string[3] { "public const string XXXX = \"XXXX\";", "Translation.Add(languageSection, Constants.XXXX, tr1, tr2);", "RegisterFunction(Constants.XXXX, new APIFunction());" };
                    foreach (var key in ArmNameHash.Keys)
                    {
                       
                        ArmMapInf MapInf = (ArmMapInf)ArmNameHash[key];
                        if (MapInf.Name.Contains("$"))
                            continue;
                        if (MapInf.Type.ToLower() == "code")
                        {
                            for (int i = 0; i < Module.Length; i++)
                            {
                                sbx[i].Append(Module[i].Replace("XXXX", MapInf.Name)+Environment.NewLine);
                            }
                        }
                    }
                    for (int i = 0; i < Module.Length; i++)
                    {
                        if(i==0)
                        File.WriteAllText(DumpFile, sbx[i].ToString());
                        WilfFile.WriteAppend(DumpFile, sbx[i].ToString());
                    }
                }
            }
            else
            {
                if(File.Exists(MapInfFile))
                {
                    List<ArmMapInf> lst = new List<ArmMapInf>();
                   string[] lines = File.ReadAllLines(MapInfFile);
                    for(int i=0;i<lines.Length;i++)
                    {
                        string[] strs = lines[i].Split(',');
                        ArmMapInf Item = new ArmMapInf();
                        Item.Name = strs[0];
                        Item.Addr = System.Convert.ToUInt32(strs[1], 16);
                        Item.Type = strs[2];
                        Item.Size = System.Convert.ToUInt32(strs[3]);
                        lst.Add(Item);
                    }

                    ArmNameHash = new Hashtable();
                    ArmAddrHash = new Hashtable();
                    for (int i = 0; i < lst.Count; i++)
                    {

                        if (ArmNameHash.ContainsKey(lst[i].Name) == false)
                            ArmNameHash.Add(lst[i].Name, lst[i]);

                        if (ArmAddrHash.ContainsKey(lst[i].Addr) == false)
                            ArmAddrHash.Add(lst[i].Addr, lst[i]);

                        if (lst[i].Type.ToLower() == "code")
                        {
                            FunList.Add(lst[i].Name);
                        }
                    }
                }
            }
            ArmFunctionList = FunList.ToArray();    
        }
        public static List<ArmMapInf> ParseArmMapFile(string FileName)
        {
            List<ArmMapInf> lst = new List<ArmMapInf>();
            string[] Lines = File.ReadAllLines(FileName);
            for (int i = 0; i < Lines.Length; i++)
            {
                if (Lines[i].Contains("Thumb Code"))
                {
                    Lines[i] = Lines[i].Replace("Thumb Code", "Code");
                }
                Lines[i] = WilfDataPro.RemoiveMulSapce(Lines[i]);
                string[] strs = Lines[i].Split(' ');
                if (strs.Length >= 4 && strs[1].Contains("0x") && (strs[2].Contains("Data")|| strs[2].Contains("Code")))
                {
                    if (WilfDataPro.IsNumeric(strs[0]) == false && WilfDataPro.IsNumeric(strs[1]) == true && WilfDataPro.IsNumeric(strs[3]) == true)
                    {
                        ArmMapInf Item = new ArmMapInf();
                        Item.Name = strs[0];
                        Item.Addr = System.Convert.ToUInt32(strs[1], 16);
                        Item.Type = strs[2];
                        Item.Size = System.Convert.ToUInt32(strs[3]);
                        lst.Add(Item);
                    }
                }
            }
            return lst;
        }

        public static void DumpTable()
        {
            ArmMapInf Item;
            StringBuilder sb = new StringBuilder();
            foreach (UInt32 key in ArmNameHash.Keys)
            {
                Item = (ArmMapInf)ArmNameHash[key];
                sb.Append(Item.Name+",0x"+Item.Addr.ToString("X8")+","+Item.Size+","+Item.Type+Environment.NewLine);  
            }
            File.WriteAllText("NameHash.txt",sb.ToString());
        }
    }
}
