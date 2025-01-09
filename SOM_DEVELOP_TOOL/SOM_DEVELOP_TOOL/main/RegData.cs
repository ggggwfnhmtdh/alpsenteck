using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace SOM_DEVELOP_TOOL
{
    public struct RegBitSruct
    {
        public string Module;
        public string RegName;
        public UInt32 Address;
        public int DefaultValue;
        public string BitRange;
        public int StartBit;
        public int EndBit;
        public string Direction;
        public string Description;
        public UInt32 RegValueBase;
        public UInt32 RegValueCur;
    }

    public struct RegModuleSruct
    {
        public string Module;
        public UInt32 StartAddress;
        public UInt32 EndtAddress;
        public UInt32 Length;
        public List<RegSruct> Reg;
    }

    public struct RegSruct
    {
        public string Module;
        public UInt32 Address;
        public List<RegBitSruct> Bit;
    }

    public struct DMUX_STRUC
    {
        public string Module;
        public string signal;
        public int Level;
        public string[] RegName;
        public UInt32[] RegValue;
    }

    public struct TMUX_STRUC
    {
        public string signal;
        public int Mode;
        public string RegName;
        public UInt32 RegValue;
        public int ATST;
    }

    public struct PINMAP_STRUC
    {
        public string PadName;
        public string RegName;
        public string[] FunName;
    }
    public  static class CRegData
    {
        public static List<RegBitSruct> RegBitInf;
        public static List<RegSruct> RegInf;
        public static List<RegModuleSruct> RegModuleInf;
        public static Hashtable RegNameHashInf;
        public static Hashtable RegNameHashInfRepetitive;
        public static Hashtable RegAddrHashInf;
        public static Hashtable RegModuleHashInf;
        public static string[] Title = new string[] { "Module", "RegName", "Address", "DefaultValue", "BitRange", "Direction", "Description" };
        public static string DmuxDir = @".\MUX\DMUX";
        public static string TmuxDir = @".\MUX\TMUX";
        public static string PinMapDir = @".\MUX\PIN_MAP";
        public static List<PINMAP_STRUC> gPinMapList;
        public static List<TMUX_STRUC> gTmuxLst;
        public static List<DMUX_STRUC> gDmuxLst;
        public static bool DataBaseOk = false;

        public static void LoadDataBase(string Dir)
        {
            string RegFile = Dir + @"\Regdata.csv";
            string DmuxFile = Dir + @"\DMUX.csv";
            string TmuxFile = Dir + @"\TMUX.csv";
            string PinMapFile = Dir + @"\PINMAP.csv";
            if(File.Exists(RegFile))
                LoadRegFile(RegFile);   
            if(File.Exists(DmuxFile) && File.Exists(TmuxFile) & File.Exists(PinMapFile))
            {
                gPinMapList = ParsePinMapFile(PinMapFile);
                gDmuxLst = ParseDmuxFile(DmuxFile);
                gTmuxLst = ParseTmuxFile(TmuxFile);
                DataBaseOk = true;
                //Debug.AppendMsg("Load DataBase Over" + Environment.NewLine);
            }
        }
        public static List<PINMAP_STRUC> ParsePinMapFile(string filename)
        {
            int i;
            StringBuilder sb = new StringBuilder();
            List<PINMAP_STRUC> PinMapList = new List<PINMAP_STRUC>();
            if (File.Exists(filename) == false)
                return null;
            string[] lines = File.ReadAllLines(filename);
            string line;
            
            for (i = 1; i < lines.Length; i++)
            {
                line = WilfDataPro.RemoveMulSapce(lines[i], "");
                string[] Item = line.Split(',');
                if (Item[0] == "")
                    continue;
                PINMAP_STRUC NewPinMap = new PINMAP_STRUC();
                NewPinMap.FunName = new string[4];
                NewPinMap.PadName = Item[0];
                NewPinMap.RegName = Item[1];
                for (int k=0;k<NewPinMap.FunName.Length;k++)
                    NewPinMap.FunName[k] = Item[2+k];
                PinMapList.Add(NewPinMap);
            }
            
            return PinMapList;
            //Debug.AppendMsg("Over->" + Count.ToString());
        }

        public static string GetPinMapScript(string PadName,string FunName)
        {
            StringBuilder sb = new StringBuilder();
            string pad_name_low;
            for (int i = 0; i < gPinMapList.Count; i++)
            {

                if(gPinMapList[i].PadName.ToUpper() == PadName.ToUpper())
                {
                    for (int j = 0; j < gPinMapList[i].FunName.Length; j++)
                    {
                        if (gPinMapList[i].FunName[j] == FunName)
                        {
                            sb.Clear();
                            pad_name_low = gPinMapList[i].PadName.ToLower();
                            sb.Append("write " + gPinMapList[i].RegName + " " + j + Environment.NewLine);
                            sb.Append("write " + pad_name_low + "_oe_n" + " " + 0 + Environment.NewLine);
                            return sb.ToString();
                        }
                    }
                }

            }
            return sb.ToString();
        }

        public static string[] GetPinMapPadFun(string PadName)
        {
            string[] sb = null;
            for (int i = 0; i < gPinMapList.Count; i++)
            {
                if (gPinMapList[i].PadName.ToUpper() == PadName.ToUpper())
                {
                    sb = new string[gPinMapList[i].FunName.Length];
                    for (int j = 0; j < gPinMapList[i].FunName.Length; j++)
                    {
                        sb[j] = gPinMapList[i].FunName[j];
                    }
                    return sb;
                }

            }
            return sb;
        }

        public static string[] GetPinMapPadName()
        {
            string[] sb = new string[gPinMapList.Count];
            for (int i = 0; i < gPinMapList.Count; i++)
            {
                sb[i] = gPinMapList[i].PadName;

            }
            return sb;
        }

        public static List<TMUX_STRUC> ParseTmuxFile(string filename)
        {
            int i;
            StringBuilder sb = new StringBuilder();
            List<TMUX_STRUC> TmuxLst = new List<TMUX_STRUC>();
            if (File.Exists(filename) == false)
                return null;
            string[] lines = File.ReadAllLines(filename);
            string line;
            for (i = 1; i < lines.Length; i++)
            {
                line = WilfDataPro.RemoveMulSapce(lines[i], "");
                //line = line.Replace(",,", ",");
                string[] Item = line.Split(',');
                if (Item[0] == "")
                    continue;
                TMUX_STRUC NewDmux = new TMUX_STRUC();
                NewDmux.signal = Item[0];
                NewDmux.Mode = Convert.ToInt32(Item[1]);
                NewDmux.RegName = Item[2];
                NewDmux.RegValue = Convert.ToUInt32(Item[3]);
                NewDmux.ATST = Convert.ToInt32(Item[4]);
                TmuxLst.Add(NewDmux);
            }
            return TmuxLst;

        }

        public static string GetTumxScript(int Mode,int ATST,string SignalName)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < gTmuxLst.Count; i++)
            {
                if (gTmuxLst[i].Mode == Mode && gTmuxLst[i].ATST == ATST && gTmuxLst[i].signal == SignalName)
                {
                    sb.Clear();
                    sb.Append("write aux_tmuxanaXXXXen_1vh 1".Replace("XXXX", gTmuxLst[i].ATST.ToString()) + Environment.NewLine);
                    sb.Append("write aux_tmuxen_1vao_actvidle_img 1" + Environment.NewLine);
                    sb.Append("write aux_tmuxen_1vao_actvlisten_img 1" + Environment.NewLine);
                    sb.Append("write aux_tmuxen_1vao_actvpoll_img 1" + Environment.NewLine);
                    sb.Append("write aux_tmuxmode_1vh XXXX".Replace("XXXX", gTmuxLst[i].Mode.ToString()) + Environment.NewLine);
                    sb.Append("write XXXX".Replace("XXXX", gTmuxLst[i].RegName + " " + gTmuxLst[i].RegValue) + Environment.NewLine);
                    break;
                }

            }
            return sb.ToString();
        }

        public static string[] GetTumxSignal(int ATST)
        {
            List<string> sb = new List<string>();
            for (int i = 0; i < gTmuxLst.Count; i++)
            {
                if (gTmuxLst[i].ATST == ATST)
                {
                    sb.Add(gTmuxLst[i].signal + "_" + gTmuxLst[i].Mode);
                }

            }
            return sb.ToArray();
        }

        public static List<DMUX_STRUC> ParseDmuxFile(string filename)
        {
            int i;
            StringBuilder sb = new StringBuilder();
            List<DMUX_STRUC> DmuxLst = new List<DMUX_STRUC> ();
            if (File.Exists(filename) == false)
                return null;
            string[] lines = File.ReadAllLines(filename);
            string line;
            for ( i = 2; i < lines.Length; i++)
            {
                line = WilfDataPro.RemoveMulSapce(lines[i], "");
                //line = line.Replace(",,", ",");
                string[] Item = line.Split(',');
                DMUX_STRUC NewDmux = new DMUX_STRUC();
                NewDmux.Module = Item[0];
                NewDmux.signal = Item[2];
                NewDmux.Level = Convert.ToInt32(Item[4]);
                NewDmux.RegName = new string[NewDmux.Level*4];
                NewDmux.RegValue = new UInt32[NewDmux.Level * 4];
                for(int j = 0;j < NewDmux.Level; j++)
                {
                    NewDmux.RegName[j * 4 + 0] = Item[5 + j * 9 + 1 + 0];
                    NewDmux.RegName[j * 4 + 1] = Item[5 + j * 9 + 1 + 2];
                    NewDmux.RegName[j * 4 + 2] = Item[5 + j * 9 + 1 + 4];
                    NewDmux.RegName[j * 4 + 3] = Item[5 + j * 9 + 1 + 6];

                    NewDmux.RegValue[j * 4 + 0] = Convert.ToUInt32(Item[5 + j * 9 + 1 + 1]);
                    NewDmux.RegValue[j * 4 + 1] = Convert.ToUInt32(Item[5 + j * 9 + 1 + 3]);
                    NewDmux.RegValue[j * 4 + 2] = Convert.ToUInt32(Item[5 + j * 9 + 1 + 5]);
                    NewDmux.RegValue[j * 4 + 3] = Convert.ToUInt32(Item[5 + j * 9 + 1 + 7]);
                }
                DmuxLst.Add(NewDmux);
            }
            return DmuxLst;
        }

        public static string GetDumxScript(string SignalName,int DMUX)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < gDmuxLst.Count; i++)
            {
                if (SignalName.ToUpper() == gDmuxLst[i].signal.ToUpper())
                {
                    sb.Clear();
                    for (int j = DMUX-1; j < gDmuxLst[i].RegName.Length; j += 4)
                    {
                        if (gDmuxLst[i].RegName[j].Contains("["))
                            gDmuxLst[i].RegName[j] = gDmuxLst[i].RegName[j].Substring(0, gDmuxLst[i].RegName[j].IndexOf("["));
                        sb.Append("write " + gDmuxLst[i].RegName[j] + " " + gDmuxLst[i].RegValue[j] + Environment.NewLine);
                    }
                    break;
                }              
            }
            return sb.ToString();
        }

        public static void DumpAllReg()
        {
            UInt32 OutValue;
            StringBuilder Msg = new StringBuilder();
            string FileName = "Register.txt";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < CRegData.RegBitInf.Count; i++)
            {
                JlinkOp.ReadRegister(CRegData.RegBitInf[i].Address, out OutValue, CRegData.RegBitInf[i].StartBit, CRegData.RegBitInf[i].EndBit);
                sb.Append(CRegData.RegBitInf[i].RegName + "=0x" + OutValue.ToString("X") + Environment.NewLine);
            }
            File.WriteAllText(FileName, sb.ToString());
        }

        public static void CompareChange()
        {
            for (int i = 0; i < RegBitInf.Count; i++)
            {
                if (RegBitInf[i].RegValueBase != RegBitInf[i].RegValueCur)
                {

                }
            }
        }
        public static bool GetRegAddr(string RegName,out RegBitSruct RegData)
        {
            RegData = new RegBitSruct();
            RegName = RegName.ToUpper().Trim();
            if (RegNameHashInf.Contains(RegName))
            {
                RegData = (RegBitSruct)RegNameHashInf[RegName];
                return true;
            }
            else
                return false;
        }

        public static RegSruct GetRegInf(UInt32 RegAddr)
        {
            RegSruct ret = new RegSruct();
            if (RegAddrHashInf.Contains(RegAddr))
                ret = (RegSruct)RegAddrHashInf[RegAddr];
            return ret;
        }

        public static bool CheckReg()
        {
            Debug.AppendMsg("1.Check RegInf"+Environment.NewLine);
            for(int i = 1;i < RegInf.Count;i++)
            {
                if(RegInf[i].Module == RegInf[i-1].Module && RegInf[i].Address - RegInf[i-1].Address != 4)
                {
                    Debug.AppendMsg("Fail" + Environment.NewLine);
                    return false;
                }
            }
            Debug.AppendMsg("Pass" + Environment.NewLine);

            Debug.AppendMsg("1.Check RegBitInf" + Environment.NewLine);
            for (int i = 0; i < RegBitInf.Count; i++)
            {
                if (i !=0 && RegBitInf[i].Address == RegBitInf[i - 1].Address && RegBitInf[i].StartBit - RegBitInf[i - 1].EndBit != 1)
                {
                    Debug.AppendMsg("Fail" + Environment.NewLine);
                    return false;
                }

                if (RegBitInf[i].EndBit < RegBitInf[i].StartBit || RegBitInf[i].StartBit<0 || RegBitInf[i].EndBit>=32)
                {
                    Debug.AppendMsg("Fail" + Environment.NewLine);
                    return false;
                }
            }
            Debug.AppendMsg("Pass" + Environment.NewLine);

            return true;
        }
        public static RegModuleSruct GetModule(string ModuleName)
        {
            RegModuleSruct ret = new RegModuleSruct();
            if (RegModuleHashInf.Contains(ModuleName.ToUpper()))
                ret = (RegModuleSruct)RegModuleHashInf[ModuleName.ToUpper()];
            return ret;
        }

        public static void ParseBitRange(string InStr,out int Start,out int End)
        {
            InStr = InStr.Replace(" ", "");
            int L_index = InStr.IndexOf("(");
            int M_index = InStr.IndexOf(":");
            int R_index = InStr.IndexOf(")");
            Start = Convert.ToInt32(InStr.Substring(L_index + 1, M_index - L_index - 1));
            End = Convert.ToInt32(InStr.Substring(M_index + 1, R_index - M_index - 1));
        }
        public static void LoadRegFile(string RegFile)
        {
            int i;
            string[] RegLines = File.ReadAllLines(RegFile);
            RegBitInf = new List<RegBitSruct>();
            RegNameHashInf = new Hashtable();
            RegNameHashInfRepetitive = new Hashtable();
            for (i = 1; i < RegLines.Length; i++)
            {
                string[] Str = RegLines[i].Split(',');
                RegBitSruct NewReg = new RegBitSruct();
                NewReg.Module = Str[0];
                NewReg.RegName = Str[1];
                NewReg.Address = Convert.ToUInt32(Str[2],16);
                NewReg.DefaultValue = Convert.ToInt32(Str[3]);
                NewReg.BitRange = Str[4];
                ParseBitRange(NewReg.BitRange, out NewReg.StartBit, out NewReg.EndBit);
                NewReg.Direction = Str[5];
                NewReg.Description = Str[6];
                RegBitInf.Add(NewReg);
                if (NewReg.RegName.ToUpper() != "Reserved".ToUpper())
                {
                    if (RegNameHashInf.Contains(NewReg.RegName.ToUpper()) == false)
                        RegNameHashInf.Add(NewReg.RegName.ToUpper(), NewReg);
                    else
                    {
                        RegNameHashInfRepetitive.Add(NewReg.RegName.ToUpper(), NewReg);
                        //Debug.AppendMsg("RegName Repeat:" + NewReg.Module + "," + NewReg.RegName + Environment.NewLine);
                    }
                }
            }

            RegAddrHashInf = new Hashtable();
            RegInf = new List<RegSruct>();
            UInt32 LastAddr = 0;
            UInt32 CurAddr = RegBitInf[0].Address;
            RegSruct RegData = new RegSruct();
            RegData.Address = RegBitInf[0].Address;
            RegData.Module = RegBitInf[0].Module;
            RegData.Bit = new List<RegBitSruct>();
            for (i = 0; i < RegBitInf.Count;i++)
            {
                LastAddr = CurAddr;
                CurAddr = RegBitInf[i].Address;
                if(LastAddr != CurAddr)
                {
                    RegInf.Add(RegData);
                    RegAddrHashInf.Add(RegData.Address, RegData);
                    RegData = new RegSruct();
                    RegData.Address = RegBitInf[i].Address;
                    RegData.Module = RegBitInf[i].Module;
                    RegData.Bit = new List<RegBitSruct>();
                }
                RegData.Bit.Add(RegBitInf[i]);             
            }
            RegInf.Add(RegData);
            RegAddrHashInf.Add(RegData.Address, RegData);

            RegModuleHashInf = new Hashtable();
            RegModuleInf = new List<RegModuleSruct>();
            string LastModule = "";
            string CurModule = RegInf[0].Module;
            RegModuleSruct RegModuleData = new RegModuleSruct();
            RegModuleData.StartAddress = RegInf[0].Address;
            RegModuleData.Module = RegInf[0].Module;
            RegModuleData.Reg = new List<RegSruct>();
            for (i = 0; i < RegInf.Count; i++)
            {
                LastModule = CurModule;
                CurModule = RegInf[i].Module;
                if (LastModule != CurModule)
                {
                    RegModuleData.EndtAddress = RegInf[i-1].Address;
                    RegModuleData.Length = RegModuleData.EndtAddress - RegModuleData.StartAddress + 4;
                    RegModuleInf.Add(RegModuleData);
                    RegModuleHashInf.Add(RegModuleData.Module.ToUpper(), RegModuleData);
                    RegModuleData = new RegModuleSruct();
                    RegModuleData.StartAddress = RegInf[i].Address;
                    RegModuleData.Module = RegInf[i].Module;
                    RegModuleData.Reg = new List<RegSruct>();
                }
                RegModuleData.Reg.Add(RegInf[i]);
            }
            RegModuleData.EndtAddress = RegInf[i - 1].Address;
            RegModuleData.Length = RegModuleData.EndtAddress - RegModuleData.StartAddress + 4;
            RegModuleInf.Add(RegModuleData);
            RegModuleHashInf.Add(RegModuleData.Module.ToUpper(), RegModuleData);
        }
    }
}
