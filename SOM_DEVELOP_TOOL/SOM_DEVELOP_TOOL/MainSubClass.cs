using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using MaterialSkin.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using MaterialSkin;

namespace SOM_DEVELOP_TOOL
{
    public partial class MainForm 
    {
        [DllImport("user32.dll")]
        public static extern int MessageBeep(uint uType);
        public byte[] GetDataFromFile(string FileName)
        {
            byte[] Data = null;
            string FileExeName;
            if (FileName == null || FileName == "")
            {
                return null;
            }
            FileExeName = Path.GetExtension(FileName).ToUpper();

            if (FileExeName == ".TXT" || FileExeName == ".CSV")
            {
                Data = WilfDataPro.GetDataFromStr(File.ReadAllText(FileExeName));
            }
            else if (FileExeName == ".BIN")
            {
                Data = File.ReadAllBytes(FileName);
            }
            else
            {
                Data = File.ReadAllBytes(FileName);
            }
            return Data;
        }

        public string RegAddrPro(string Cmd,out int StartBit,out int EndBit)
        {
            RegBitSruct RegInf;
            StartBit = 0;
            EndBit = 31;
            if (WilfDataPro.IsNumeric(Cmd))
                return Cmd;
            bool ret = CRegData.GetRegAddr(Cmd, out RegInf);
            StartBit = RegInf.StartBit;
            EndBit = RegInf.EndBit;
            if (ret == true)
                return "0x" + RegInf.Address.ToString("X8");
            else
                return null;
        }

        public UInt32 ReadRegFormName(string RegName)
        {
            int StartBit,EndBit;
            UInt32 Addr, RegValue = 0;
            int BitNum;
            string AddrStr = RegAddrPro(RegName, out StartBit, out EndBit);
            Addr = (UInt32)WilfDataPro.CalcExpression(AddrStr);
            bool ret = JlinkOp.ReadRegister(Addr, out RegValue);
            BitNum = EndBit - StartBit + 1;
            RegValue = (RegValue >> StartBit) & (~(0xFFFFFFFF << BitNum));
            return RegValue;
        }

        public void CmdCheck(string[] Cmds)
        {

            if (Cmds.Length < 2)
                return;
            if (MapParser.ArmNameHash ==null)
            {
                MapParser.GetArmHash(gCfgParam.ArmMapFile);
            }
            for(int i=1;i<Cmds.Length;i++)
            {
                Cmds[i] = MapParser.CalcExpress(Cmds[i]);
            }
        }



        void FwPro()
        {
            string HexFile = ImageFileBox.Text;
            if(!File.Exists(HexFile))
            {
                HexFile = WilfFile.OpenFile(".hex");
                ImageFileBox.Text = HexFile;
            }
            if (File.Exists(HexFile))
            {
                if (JlinkOp.CheckCpuID() == true)
                {
                    DownloadApollo(HexFile);
                }
                else
                    Debug.AppendMsg("[Inf]no device"+Environment.NewLine);
            }
            MessageBeep(0x00000000);
        }
        bool ExeCmd(string CmdStr)
        {
            bool ret = true;
            string[] Cmds;
            byte[] Data;
            string Cmd;
            string FileName = "";
            CmdStr = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(CmdStr, " ").Trim();
            Cmds = CmdStr.Split(' ');
            UInt32 Addr, Value, Len;
            int StartBit,EndBit;
            try
            {
                CmdCheck(Cmds);
                Server.CmdOperationTime++;
                if (Cmds.Length > 0)
                {
                    Cmd = Cmds[0].ToUpper();
                    if (Cmd == "D")
                    {
                         FwPro();
                    }
                    else if(Cmd == "T")
                    {
                        if (Cmds.Length == 1)
                        {
                            if (File.Exists(TestCaseFileName) == false)
                            {
                                TestCaseFileName = WilfFile.OpenFile(".txt");
                            }
                            RunTestCase(TestCaseFileName,true);
                        }
                        else
                        {
                            RunTestCase(Cmds[1]);
                        }
                    }
                    else if (Cmd == "WS" || Cmd == "WRITE")
                    {
                        string Name = Cmds[1];
                        Cmds[1] = RegAddrPro(Cmds[1], out StartBit, out EndBit);
                        Addr = (UInt32)WilfDataPro.CalcExpression(Cmds[1]);
                        Value = (UInt32)WilfDataPro.CalcExpression(Cmds[2]);
                        ret = JlinkOp.WriteRegister(Addr, Value, StartBit, EndBit);
                        AppendMsgColor("WS:" + Name + "=" + Value.ToString() + Environment.NewLine, Color.Red);
                    }
                    else if (Cmd == "RS" || Cmd == "READ")
                    {
                        string Name = Cmds[1];
                        Cmds[1] = RegAddrPro(Cmds[1], out StartBit, out EndBit);
                        Addr = (UInt32)WilfDataPro.CalcExpression(Cmds[1]);
                        Value = 0;
                        ret = JlinkOp.ReadRegister(Addr, out Value, StartBit, EndBit);
                        AppendMsgColor("RS:" + Name + "=" + Value.ToString() + Environment.NewLine, Color.Green);
                    }
                    else if (Cmd == "WB")
                    {
                        Addr = (UInt32)WilfDataPro.CalcExpression(Cmds[1]);
                        StartBit = (int)WilfDataPro.CalcExpression(Cmds[2]);
                        EndBit = (int)WilfDataPro.CalcExpression(Cmds[3]);
                        Value = (UInt32)WilfDataPro.CalcExpression(Cmds[4]);
                        ret = JlinkOp.WriteRegister(Addr, Value, StartBit, EndBit);
                        Debug.AppendRegisterInf(Addr, Value, false, StartBit, EndBit);
                    }
                    else if (Cmd == "RB")
                    {
                        Addr = (UInt32)WilfDataPro.CalcExpression(Cmds[1]);
                        StartBit = (int)WilfDataPro.CalcExpression(Cmds[2]);
                        EndBit = (int)WilfDataPro.CalcExpression(Cmds[3]);
                        ret = JlinkOp.ReadRegister(Addr, out Value, StartBit, EndBit);
                        Debug.AppendRegisterInf(Addr, Value, true, StartBit, EndBit);
                    }
                    else if (Cmd == "W")
                    {

                        Addr = (UInt32)WilfDataPro.CalcExpression(Cmds[1]);
                        Value = (UInt32)WilfDataPro.CalcExpression(Cmds[2]);
                        ret = JlinkOp.WriteRegister(Addr, Value);
                        Debug.AppendRegisterInf(Addr, Value, false);
                    }
                    else if (Cmd == "R")
                    {
                        if (Cmds.Length <= 2)
                        {
                            Addr = (UInt32)WilfDataPro.CalcExpression(Cmds[1]);
                            Value = 0;
                            ret = JlinkOp.ReadRegister(Addr, out Value);
                            Debug.AppendRegisterInf(Addr, Value, true);
                        }
                        else
                        {
                            int Num = Convert.ToInt32(Cmds[2]);
                            Addr = (UInt32)WilfDataPro.CalcExpression(Cmds[1]);
                            UInt32[] DataValue = new UInt32[Num];
                            for (int j = 0; j < Num; j++)
                            {
                                ret = JlinkOp.ReadRegister(Addr, out DataValue[j]);
                                Addr += 4;
                            }
                            Debug.AppendRegisterInf((UInt32)WilfDataPro.CalcExpression(Cmds[1]), DataValue,true, Color.Green);
                        }
                    }
                    else if (Cmd == "WM")
                    {
                        Addr = (UInt32)WilfDataPro.CalcExpression(Cmds[1]);
                        if (Cmds.Length >= 3)
                            Data = WilfDataPro.GetDataFromStr(Cmds[2]);
                        else
                        {
                            FileName = WilfFile.OpenFile("*.*");
                            Data = GetDataFromFile(FileName);
                        }
                        if (Data != null)
                        {
                            ret = JlinkOp.WritMemory(Addr, (UInt32)Data.Length, Data);
                            Debug.AppendMsg("[Inf]WriteMemory:0x" + Addr.ToString("X8") + "(" + Data.Length + ")" + Environment.NewLine);
                        }
                        else
                        {
                            ret = false;
                            AppendMsgColor("[Error]" + "The loading parameters are incorrect" + Environment.NewLine, Color.Red);
                        }
                    }
                    else if (Cmd == "RM")
                    {
                        Addr = (UInt32)WilfDataPro.CalcExpression(Cmds[1]);
                        Len = (UInt32)WilfDataPro.CalcExpression(Cmds[2]);
                        JlinkOp.ReadMemory(Addr, (UInt32)Len, out Data);
                        Debug.AppendMsg("[Inf]@0x" + Addr.ToString("X8") + "(" + Data.Length + ")" + "->"+Environment.NewLine);
                        Debug.AppendMsg(WilfDataPro.ToString(Data, "X2", 32) + Environment.NewLine);
                    }
                    else if (Cmd == "DUMP" || Cmd == "DP")
                    {
                        Addr = (UInt32)WilfDataPro.CalcExpression(Cmds[1]);
                        Len = (UInt32)WilfDataPro.CalcExpression(Cmds[2]);
                        JlinkOp.ReadMemory(Addr, (UInt32)Len, out Data);
                        FileName = Application.StartupPath + "\\DUMP.bin";
                        string InjectStr = "_0x" + Addr.ToString("X8") + "_" + Data.Length;
                        string OutFileName = WilfFile.FileNameInjectStr(FileName, InjectStr);
                        File.WriteAllBytes(OutFileName, Data);
                        Debug.AppendMsg("[Inf]Output file:" + OutFileName + Environment.NewLine);
                    }
                    else if (Cmd == "READFLASH" || Cmd == "RF")
                    {
                        Addr = (UInt32)WilfDataPro.CalcExpression(Cmds[1]);
                        Len = (UInt32)WilfDataPro.CalcExpression(Cmds[2]);
                        ret = jlink_protocol.InitFlash(false);
                        if (ret==false)
                            return ret;
                        jlink_protocol.ReadFlash(Addr, out Data, Len, false);
                        FileName = Application.StartupPath + "\\ReadFlash.bin";
                        string InjectStr = "_0x" + Addr.ToString("X8") + "_" + Data.Length;
                        string OutFileName = WilfFile.FileNameInjectStr(FileName, InjectStr);
                        File.WriteAllBytes(OutFileName, Data);
                        Debug.AppendMsg("[Inf]Output file:" + OutFileName + Environment.NewLine);
                    }
                    else if (Cmd == "SubFile" || Cmd == "S")
                    {
                        FileName = WilfFile.OpenFile("*.*");
                        Data = GetDataFromFile(FileName);
                        Addr = (UInt32)WilfDataPro.CalcExpression(Cmds[1]);
                        Len = (UInt32)WilfDataPro.CalcExpression(Cmds[2]);
                        byte[] OutData = new byte[Len];
                        if (Addr + Len > Data.Length)
                        {
                            ret = false;
                            Debug.AppendMsg("[Error]" + "The loading parameters are incorrect" + Environment.NewLine, Color.Red);
                        }
                        else
                        {
                            Array.Copy(Data, Addr, OutData, 0, Len);
                            string InjectStr = "_0x" + Addr.ToString("X8") + "_" + Len;
                            string OutFileName = WilfFile.FileNameInjectStr(FileName, InjectStr);
                            File.WriteAllBytes(OutFileName, OutData);
                            Debug.AppendMsg("[Inf]Output file:" + OutFileName + Environment.NewLine);
                        }
                    }
                    else if (Cmd == "CLC" || Cmd == "CLEAR")
                    {
                        if (Cmds.Length == 1)
                        {
                            Debug.AppendMsg("");
                        }
                        else if (Cmds[1].ToUpper() == "ALL")
                        {
                            Debug.AppendMsg("");
                            CmdStore.Clear();
                        }
                    }
                    else if (Cmd == "WC")
                    {
                        Addr = (UInt32)WilfDataPro.CalcExpression(Cmds[1]);
                        Value = (UInt32)WilfDataPro.CalcExpression(Cmds[2]);
                        JlinkOp.JLINKARM_WriteReg((ARM_REG)Addr, Value);
                        AppendMsgColor("R" + Addr + "=0x" + Value.ToString("X8") + Environment.NewLine, Color.Red);
                    }
                    else if (Cmd == "RC")
                    {
                        Addr = (UInt32)WilfDataPro.CalcExpression(Cmds[1]);
                        JlinkOp.JLINKARM_Halt();
                        Value = JlinkOp.JLINKARM_ReadReg((ARM_REG)Addr);
                        JlinkOp.JLINKARM_Go();
                        AppendMsgColor("R" + Addr + "=0x" + Value.ToString("X8") + Environment.NewLine, Color.Green);
                    }
                    else if (Cmd == "SET")
                    {
                        string path;
                        path = Cmds[1];
                        FileName = Path.GetFileName(path).ToLower();
                        if (File.Exists(path) || Directory.Exists(path))
                        {
                            if (WilfFile.OpenYesNo("Load M7 Project?"))
                            {
                                gCfgParam.ArmProjectDir = path;
                                Json.SaveCfg(gCfgParam);
                                aLLToolStripMenuItem_Click(null, null);
                            }
                        }
                        else
                        {
                            MessageBox.Show("error:can't find path ->" + path + Environment.NewLine);
                        }
                    }
                    else if (Cmd == "SLEEP")
                    {
                        Thread.Sleep(Convert.ToInt32(Cmds[1]));
                    }
                    else if (Cmd == "FIND" || Cmd == "F")
                    {
                        if (Cmds[1].ToUpper() == "REG")
                        {
                            string str = RegAddrPro(Cmds[2], out StartBit, out EndBit);
                            Debug.AppendMsg(Cmds[2] + "," + str + "," + StartBit + "," + EndBit + Environment.NewLine);
                        }
                        else 
                        {
                            Debug.AppendMsg(Cmds[1] + "=0x" + MapParser.GetAddr(Cmds[1])+Environment.NewLine);
                        }
                    }
                    else if (Cmd == "RESET")
                    {
                        JlinkOp.Reset();
                        Debug.AppendMsg("[Inf]Reset" + Environment.NewLine);
                    }
                    else if(Cmd == "ADDR2LINE")
                    {
                        UInt32 ArmAddr;
                        ArmAddr = (UInt32)WilfDataPro.CalcExpression(Cmds[1]);
                        DumpCodeInf(ArmAddr);
                    }
                    else if (Cmd == "CALC")
                    {
                        Addr = (UInt32)WilfDataPro.CalcExpression(Cmds[1]);
                        Debug.AppendMsg("0x" + Addr.ToString("X8")+"(" + Addr +")" + Environment.NewLine);
                    }

                    else if (Cmd == "TEST")
                    {
                        Addr = (UInt32)WilfDataPro.CalcExpression(Cmds[1]);
                        SetTheme((int)Addr);
                        Debug.AppendMsg("Theme:"+Addr + Environment.NewLine);
                    }

                }
                
                else
                {
                    ret = false;
                }
            }
            catch(Exception ex)
            {
                AppendMsgColor("Command error:" + ex.Message+Environment.NewLine,Color.Red);
            }
            return ret;
        }

        public void DownloadArm(string BinFile)
        {
            string FileName;
            long FileSize;
            byte[] DownBinData;
            byte[] ReadBinData;
            Server.DownLoadOperationTime++;
            Debug.AppendMsg("********************Arm Process*********************" + Environment.NewLine);
            Debug.AppendMsg("Halt Cpu" + Environment.NewLine);
            JlinkOp.JLINKARM_Halt();
            Debug.AppendMsg("map ram to rom space" + Environment.NewLine);

            Debug.AppendMsg("Start to Program " + Environment.NewLine);
            if (File.Exists(BinFile))
                FileName = BinFile;
            else
                FileName = WilfFile.OpenFile(".bin");
            FileSize = WilfFile.GetFileSize(FileName);
            DownBinData = File.ReadAllBytes(FileName);
            bool ret = JlinkOp.DownloadFw(FileName, 0x00000000);
            if (ret == true)
                Debug.AppendMsg("Download file size " + FileSize.ToString() + Environment.NewLine);
            else
            {
                AppendMsgColor("Program failed" + Environment.NewLine, Color.Red);
                return;
            }

            Debug.AppendMsg("Verify Code " + Environment.NewLine);
            ret = JlinkOp.ReadMemory(0x20000000, (UInt32)FileSize, out ReadBinData);
            if (ret == true)
            {
                bool retx = WilfDataPro.CmparaArray(ReadBinData, 0, DownBinData, 0, DownBinData.Length);
                if (retx == true)
                {
                    AppendMsgColor("Verify pass" + Environment.NewLine, Color.Green);
                    AppendMsgColor("Download successful" + Environment.NewLine, Color.Green);
                }
                else
                {
                    AppendMsgColor("Verify failed" + Environment.NewLine, Color.Red);
                }
            }
            else
            {
                AppendMsgColor("read memory failed" + Environment.NewLine, Color.Red);
            }
            if (JlinkOp.BreakPointAddress != 0 && JlinkOp.IsBpNeed == true)
            {
                JlinkOp.JLINKARM_SetBP(0, JlinkOp.BreakPointAddress);
                Debug.AppendMsg("Set breakpoint for dsp at 0x" + JlinkOp.BreakPointAddress.ToString("X8")+ Environment.NewLine);
            }

            Debug.AppendMsg("cpu run" + Environment.NewLine);
            JlinkOp.JLINKARM_Go();
        }

        public void DownloadM7(string FileName,bool EnableLog=true)
        {
            UInt32 PC=0;
            UInt32 SP=0;
            string Msg;
            StringBuilder dump_msg = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            Server.DownLoadOperationTime++;
            Debug.AppendMsg("[Inf]Start To Download Data" + Environment.NewLine);
            JlinkOp.Reset();
            if (EnableLog)
                Debug.AppendMsg("Reset Chip" + Environment.NewLine);
            Thread.Sleep(10);
            ReOpenJlink();
            if (EnableLog)
                Debug.AppendMsg("Halt Cpu" + Environment.NewLine);
            JlinkOp.JLINKARM_Halt();

            if (EnableLog)
                Debug.AppendMsg("Start to Write Memory " + Environment.NewLine);
           
            if (!File.Exists(FileName))
                return;
            if (EnableLog)
                Debug.AppendMsg("Load File:" + FileName + Environment.NewLine);
            List<SectionData> list = HexData.Parse(FileName);
            if (EnableLog)
                Debug.AppendMsg("Start to download" + Environment.NewLine);
            
            for (int i = 0; i < list.Count; i++)
            {

                UInt32 wAddr = list[i].Addr;
                UInt32 wLength = (UInt32)list[i].Data.Count;
                JlinkOp.WritMemory(wAddr, wLength, list[i].Data.ToArray(), 4);
                Msg = "Write:0x" + wAddr.ToString("X8") + ",Length=" + wLength.ToString();
                if (EnableLog)
                    Debug.AppendMsg(Msg + Environment.NewLine);
            }
            if (EnableLog)
                AppendMsgColor("Download Successfully" + Environment.NewLine, Color.Green);

            byte[] bindata = list[0].Data.ToArray();
            SP = WilfDataPro.GetUint32(bindata,0);
            PC = WilfDataPro.GetUint32(bindata, 4);
            if (EnableLog)
                Debug.AppendMsg("Initial PC,SP......" + Environment.NewLine);
            JlinkOp.MapMemory(SP,PC,0x30020000);
            if (EnableLog)
                Debug.AppendMsg("cpu run" + Environment.NewLine);
            JlinkOp.JLINKARM_Go();
        }

        public void DownloadSOM_RAM(string FileName, bool EnableLog = true)
        {
            UInt32 PC = 0;
            UInt32 SP = 0;
            string Msg;
            StringBuilder dump_msg = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            Server.DownLoadOperationTime++;
            Debug.AppendMsg("[Inf]Start To Download Data" + Environment.NewLine);
            JlinkOp.Reset();
            if (EnableLog)
                Debug.AppendMsg("Reset Chip" + Environment.NewLine);
            Thread.Sleep(10);
            ReOpenJlink();
            if (EnableLog)
                Debug.AppendMsg("Halt Cpu" + Environment.NewLine);
            JlinkOp.JLINKARM_Halt();

            if (EnableLog)
                Debug.AppendMsg("Start to Write Memory " + Environment.NewLine);

            if (!File.Exists(FileName))
                return;
            if (EnableLog)
                Debug.AppendMsg("Load File:" + FileName + Environment.NewLine);
            List<SectionData> list = HexData.Parse(FileName);
            if (EnableLog)
                Debug.AppendMsg("Start to download" + Environment.NewLine);

            for (int i = 0; i < list.Count; i++)
            {

                UInt32 wAddr = list[i].Addr;
                UInt32 wLength = (UInt32)list[i].Data.Count;
                JlinkOp.WritMemory(wAddr, wLength, list[i].Data.ToArray(), 4);
                Msg = "Write:0x" + wAddr.ToString("X8") + ",Length=" + wLength.ToString();
                if (EnableLog)
                    Debug.AppendMsg(Msg + Environment.NewLine);
            }
            if (EnableLog)
                AppendMsgColor("Download Successfully" + Environment.NewLine, Color.Green);

            byte[] bindata = list[0].Data.ToArray();
            SP = WilfDataPro.GetUint32(bindata, 0);
            PC = WilfDataPro.GetUint32(bindata, 4);
            if (EnableLog)
                Debug.AppendMsg("Initial PC,SP......" + Environment.NewLine);
            JlinkOp.MapMemory(SP, PC, 0x10000000);
            if (EnableLog)
                Debug.AppendMsg("cpu run" + Environment.NewLine);
            JlinkOp.JLINKARM_Go();
        }

        public UInt32 FindVarAddr(string MapFilePath,string ObjStr)
        {
            UInt32 Addr=0;
            string[] Strs = File.ReadAllLines(MapFilePath);
            for(int i = 0; i < Strs.Length; i++)
            {
                if(Strs[i].Contains(ObjStr))
                {
                    string TempStr = WilfDataPro.RemoiveMulSapce(Strs[i]);
                    string[] StrSplit = TempStr.Split(' ');
                    if(StrSplit.Length>=3 && StrSplit[2] == "Data")
                    {
                        Addr = Convert.ToUInt32(StrSplit[1],16);
                        break;
                    }
                }
            }
            return Addr;
        }


        void UpDataLogParam()
        {
            if (M7_CPU0_CB.Checked == true)
            {
                m_Log[0].Enable = true;
                m_Log[0].ChipName = "CPU0";
                m_Log[0].IdTableFile = "ID_Table_CPU0.data";
                m_Log[0].DesTableFile = "DES_Table_CPU0.data";
            }
            else
            {
                m_Log[0].Enable = false;
                m_Log[0].ChipName = "CPU0";
                m_Log[0].IdTableFile = "ID_Table_CPU0.data";
                m_Log[0].DesTableFile = "DES_Table_CPU0.data";
            }

            if (M7_CPU1_CB.Checked == true)
            {

            }
            else
            {
                m_Log[1].Enable = false;
            }
        }
        private void DspLogCB_CheckedChanged(object sender, EventArgs e)
        {
            UpDataLogParam();
        }

        private void DumpCodeInf(UInt32 ArmAddr, bool full_flag = false)
        {
            string Msg;
            string addr2line_exe = Application.StartupPath + @"\Tool\addr2line.exe";
            string xt_addr2line_exe = Application.StartupPath + @"\Tool\Dsp\addr2line.exe";
            string xt_objcopy_exe = Application.StartupPath + @"\Tool\Dsp\objcopy.exe";
            string xt_objdump_exe = Application.StartupPath + @"\Tool\Dsp\objdump.exe";
            string ConfigDir = Application.StartupPath + @"\Tool\Dsp\config";
            //string xt_nm_exe = @"D:\usr\xtensa\XtDevTools\install\tools\RI-2021.6-win32\XtensaTools\bin\xt-nm.exe";

            if (File.Exists(gCfgParam.ArmAxfFile) && ArmAddr != 0)
            {
                if (full_flag == true)
                {
                    Exe.ArmDumpAsm(gCfgParam.KeilFromelf, gCfgParam.ArmAxfFile);
                    Exe.ArmDumpBin(gCfgParam.KeilFromelf, gCfgParam.ArmAxfFile);
                    Exe.ArmDumpHex(gCfgParam.KeilFromelf, gCfgParam.ArmAxfFile);
                }
                Msg = Exe.ArmAddr2Line(addr2line_exe, gCfgParam.ArmAxfFile, ArmAddr);
                Debug.AppendMsg("Arm:" + Msg + Environment.NewLine);
            }

            //if (File.Exists(gCfgParam.DspAxfFile) && DspAddr != 0)
            //{
            //    if (full_flag == true)
            //    {
            //        Exe.DspDumpAsm(xt_objdump_exe, ConfigDir, gCfgParam.DspAxfFile);
            //        Exe.DspDumpBin(xt_objcopy_exe, ConfigDir, gCfgParam.DspAxfFile);
            //        Exe.DspDumpHex(xt_objcopy_exe, ConfigDir, gCfgParam.DspAxfFile);
            //        string SymbolFile = Exe.DspDumpSymbol(xt_nm_exe, ConfigDir, gCfgParam.DspAxfFile);
            //        Exe.DspDumpAllInf(SymbolFile, gCfgParam.DspMapFile);
            //    }
            //    Msg = Exe.DspAddr2Line(xt_addr2line_exe, ConfigDir, gCfgParam.DspAxfFile, DspAddr);
            //    Debug.AppendMsg("Dsp:" + Msg + Environment.NewLine);
            //}
        }

        public void GenerateDspID_Table(string Dir, int LogIndex, bool SortEnable)
        {
           
        }

        public void GenerateArmID_Table(string ProjectFile, int LogIndex, bool SortEnable)
        {
            Hashtable IdTable = new Hashtable();
            MapParser.GetArmHash(ProjectInf.GetArmFilePath(ProjectFile, FileType.MAP));
            m_Log[LogIndex].AddIdTable(MapParser.ArmAddrHash, false);
            IdTable = SphinxCodePro.ParseLog(ProjectFile, gCfgParam.AutoEditEnable, SortEnable);
            m_Log[LogIndex].AddIdTable(IdTable, true);
        }

        private void RttLogPro()
        {
            int Index;
            try
            {
                m_UI.Save();
                Index = 0;
                if (m_Log[Index].Enable) //Arm
                {
                    m_Log[Index].ID_Table.Clear();
                    m_Log[Index].Des_Table.Clear();
                    if (m_Log[Index].ChipName == "CPU0")
                    {
                        m_Log[Index].ID_Table.Clear();
                        if (File.Exists(gCfgParam.ArmProjectFile))
                        {
                            GenerateArmID_Table(gCfgParam.ArmProjectFile, Index, IdSortCB.Checked);
                        }
                        if (m_Log[Index].ID_Table.Count == 0)
                            m_Log[Index].LoadIdTable();
                        else
                            m_Log[Index].ExportIdTable();

                        m_Log[Index].LoadDesTable();
                        if (Directory.Exists(gCfgParam.LogicAnalyzersDir) && File.Exists(m_Log[Index].IdTableFile))
                        {
                            File.Copy(m_Log[Index].IdTableFile, gCfgParam.LogicAnalyzersDir + "\\" + Path.GetFileName(m_Log[Index].IdTableFile), true);
                        }
                        if (m_Log[Index].ID_Table.Count > 0)
                        {
                            m_Log[Index].BaseAddr = m_Log[Index].FindIdTable("EASY_LOG_BOX");
                            m_Log[Index].RangeAddr = m_Log[Index].FindIdTable("IdRange");
                            Debug.AppendMsg(m_Log[Index].ChipName + " log enable 0x" + m_Log[Index].BaseAddr.ToString("X8") + Environment.NewLine);
                        }
                        else
                        {
                            MessageBox.Show("Please load IdTable" + Environment.NewLine);
                            return;
                        }
                    }
                    else
                    {
                        //
                    }
                }
                //if (JlinkOp.CheckCpuID() == false)
                //{
                //    MessageBox.Show("Jlink is offline. Please reconnect");
                //    return;
                //}
                FormRTT_Pro();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormRTT_Pro()
        {
            if (pFormRTT == null || pFormRTT.IsDisposed)
            {
                pFormRTT = new FormRTT();
                pFormRTT.Show();
            }
            else
            {
                pFormRTT.Show();
                pFormRTT.Focus();
            }
        }

        private void SeleTable(int Index)
        {
            if (TempPage.SelectedIndex == Index)
                return;

            if (this.TempPage.InvokeRequired)
            {
                ThreadWorkTable fc = new ThreadWorkTable(SeleTable);
                this.Invoke(fc, new object[1] { Index });
            }
            else
            {
                TempPage.SelectedIndex = Index;
            }
        }

        private void KeyDownEvent(object sender, KeyEventArgs e, string CmdStr)
        {
            if (e.KeyCode == Keys.Enter)
            {
                m_UI.Save();
                string[] Cmds;
                CmdStr = WilfDataPro.RemoiveMulSapce(CmdStr).Trim();
                if (CmdStr.Contains(";"))
                {
                    Cmds = CmdStr.Split(';');
                    for (int i = 0; i < Cmds.Length; i++)
                        ExeCmd(Cmds[i]);
                }
                else
                    ExeCmd(CmdStr);
            }
            if (easy_log.CheckPermission() == true)
            {
                if (e.KeyCode == Keys.F5)
                {
                    ExeCmd("FD 111");
                }
            }
        }

        private void ReOpenJlink()
        {
            if (Device.GetDeviceStatus() == true)
            {
                Device.Close();
            }
            bool dev_ok = Device.Open(JlinkOp.Protocol, JlinkOp.SpeedKHz);

            if (dev_ok == true)
            {
                Device.SetDeviceStatus(true);
            }
            else
            {
                Device.SetDeviceStatus(false);
            }
        }

        protected override void WndProc(ref Message m)
        {
            try
            {
                //检测到USB口发生了变化,这里USB口变化时wParam的值是7，表示系统中添加或者删除了设备
                if (m.WParam.ToInt32() == 7)
                {
                    BackThreadEnable = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);　　//异常处理函数
            }
            base.WndProc(ref m);　　//这个是windos的异常处理，一定要添加，不然会运行不了
        }

        public void AppendMsg(string Msg)
        {
            if (this.MsgBox.InvokeRequired)
            {
                ThreadWorkStr fc = new ThreadWorkStr(AppendMsg);
                this.Invoke(fc, new object[1] { Msg });
            }
            else
            {
                if (MainForm.log_to_file_flag == false)
                {
                    if (Msg == "")
                        this.MsgBox.Clear();
                    else
                        this.MsgBox.AppendText(Msg);

                    if(MsgBox.Text.Length>=90000)
                    {
                        MsgBox.Clear();
                    }

                    this.MsgBox.Refresh();
                    this.MsgBox.SelectionStart = MsgBox.Text.Length;
                    this.MsgBox.ScrollToCaret();
                }
                else
                {
                    WilfFile.WriteAppend(log_file, Msg);
                }
            }
        }

        public void ShowMsg(string Msg)
        {
            if (this.MsgBox1.InvokeRequired)
            {
                ThreadWorkStr fc = new ThreadWorkStr(ShowMsg);
                this.Invoke(fc, new object[1] { Msg });
            }
            else
            {
               
                    if (Msg == "")
                        this.MsgBox1.Clear();
                    else
                        this.MsgBox1.AppendText(Msg);

                    if (MsgBox1.Text.Length>=90000)
                    {
                        MsgBox1.Clear();
                    }

                    this.MsgBox1.Refresh();
                    this.MsgBox1.SelectionStart = MsgBox1.Text.Length;
                    this.MsgBox1.ScrollToCaret();

            }
        }

        public void ShowMsgColor(string Msg, Color color)
        {
            if (this.MsgBox1.InvokeRequired)
            {
                ThreadWorkStrColor fc = new ThreadWorkStrColor(ShowMsgColor);
                this.Invoke(fc, new object[2] { Msg, color });
            }
            else
            {
                if (Msg == "")
                    this.MsgBox1.Clear();
                else
                {
                    this.MsgBox1.Select(this.MsgBox1.Text.Length, 0);
                    //this.MsgBox.Focus();
                    MsgBox1.SelectionColor = color;
                    MsgBox1.AppendText(Msg);
                }

                this.MsgBox1.Refresh();
                this.MsgBox1.SelectionStart = MsgBox1.Text.Length;
                this.MsgBox1.ScrollToCaret();
            }
        }

        public void AppendMsgColor(string Msg, Color color)
        {
            if (this.MsgBox.InvokeRequired)
            {
                ThreadWorkStrColor fc = new ThreadWorkStrColor(AppendMsgColor);
                this.Invoke(fc, new object[2] { Msg, color });
            }
            else
            {
                if (Msg == "")
                    this.MsgBox.Clear();
                else
                {
                    this.MsgBox.Select(this.MsgBox.Text.Length, 0);
                    //this.MsgBox.Focus();
                    MsgBox.SelectionColor = color;
                    MsgBox.AppendText(Msg);
                }

                this.MsgBox.Refresh();
                this.MsgBox.SelectionStart = MsgBox.Text.Length;
                this.MsgBox.ScrollToCaret();
            }
        }

        private bool CmpFile(string FileName0, string FileName1)
        {
            byte[] BinDataUpData = File.ReadAllBytes(FileName0);
            byte[] BinDataCur = File.ReadAllBytes(FileName1);
            if (BinDataCur.Length == BinDataUpData.Length)
            {
                for (int i = 0; i < BinDataCur.Length; i++)
                {
                    if (BinDataCur[i] != BinDataUpData[i])
                    {
                        return false;
                    }
                }
            }
            else
                return false;
            return true;
        }

        private void UpDataThread(object InTxt)
        {
            string HelpFile;
            UpDataFile = (string)InTxt;
            bool ret = true;
            try
            {
                if (File.Exists(UpDataFile))
                {
                    ret = CmpFile(UpDataFile, Application.ExecutablePath);
                    if (ret == false)
                    {
                        string Arg = "";
                        Arg += UpDataFile + " ";
                        Arg += Application.ExecutablePath;
                        string new_vesion = File.ReadAllText(Path.GetDirectoryName(UpDataFile) + "\\" + "VersionInf.txt");
                        HelpFile = Path.GetDirectoryName(UpDataFile) + "\\" + Path.GetFileName(easy_log.HelpFIle);
                        if (MessageBox.Show("Whether to upgrade to " + new_vesion + " from " + Version, "Upgrade Options", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes)
                        {
                            string[] dlls = WilfFile.GetFile(UpDataExeDir, ".dll", false);
                            for (int i = 0; i < dlls.Length; i++)
                            {
                                string dll_name = Application.StartupPath + "\\" + Path.GetFileName(dlls[i]);
                                if (!File.Exists(dll_name))
                                {
                                    File.Copy(HelpFile, dll_name, true);
                                    Debug.AppendMsg("UpdateFile:" + Path.GetFileName(dll_name) + Environment.NewLine);
                                }
                            }
                            File.Copy(HelpFile, easy_log.HelpFIle, true);
                            File.Copy(Application.ExecutablePath, WilfFile.FileNameInjectStr(Application.ExecutablePath, "_back_up_" + WilfFile.GetTimeStr()), true);
                            BackThreadEnable = false;
                            Thread.Sleep(500);
                            if (!File.Exists(UpDateTool))
                            {
                                GetResFile(Path.GetFileName(UpDateTool));
                            }
                            Process.Start(UpDateTool, Arg);
                            Application.Exit();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (File.Exists(UpDateTool))
                        {
                            File.Delete(UpDateTool);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool GetResFile(string FileName)
        {
            Assembly assm = this.GetType().Assembly;//Assembly.LoadFrom(程序集路径);
            string[] ResName = assm.GetManifestResourceNames();
            for (int i = 0; i < ResName.Length; i++)
            {
                if (ResName[i].Contains(Path.GetFileName(FileName)))
                {
                    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResName[i]);
                    byte[] ResData = new byte[stream.Length];
                    stream.Read(ResData, 0, (int)stream.Length);
                    try
                    {
                        File.WriteAllBytes(FileName, ResData);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool GetResFile(string FileName, string out_file)
        {
            Assembly assm = this.GetType().Assembly;//Assembly.LoadFrom(程序集路径);
            string[] ResName = assm.GetManifestResourceNames();
            for (int i = 0; i < ResName.Length; i++)
            {
                if (ResName[i].Contains(FileName))
                {
                    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResName[i]);
                    byte[] ResData = new byte[stream.Length];
                    stream.Read(ResData, 0, (int)stream.Length);
                    try
                    {
                        File.WriteAllBytes(out_file, ResData);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void Init()
        {
           
            VerLab.Text = Version;
            Debug.ShowMsg = AppendMsg;
            Debug.ShowMsgColor = AppendMsgColor;
            Debug.pMainF = this;
            Debug.AppendMsg("");
            jlink_protocol.pSetProBar = SetProBarValue;
            gCfgParam = Json.LoadCfg();
            WilfFile.CreateDirectory(Exe.OutDir);
            WilfFile.CreateDirectory(DataBaseDir);
            WilfFile.CreateDirectory(ScriptDir);
            easy_log.PcTimeEnable = checkBox1.Checked;
            easy_log.ShowLogEnable = checkBox2.Checked;
            easy_log.WaitEnable = materialCheckBox3.Checked;
            easy_log.CpuTimeEnable = materialCheckBox1.Checked;
            easy_log.BlockModeEnable = materialCheckBox3.Checked;
            JlinkOp.SpeedKHz = Convert.ToInt32(JlinkSpeedCBox.Text.Trim()) * 1000;
            UpDataLogParam();
            easy_log.DelayTime = 2000;
        }

        void SetProBarValue(int Value)
        {
            if (Value<=ProBar.Maximum && Value>=ProBar.Minimum)
            {
                ProBar.Value= Value;
            }
        }

        private void Reload()
        {
            bool ret;
            if (Directory.Exists(DataBaseDir))
            {
                try
                {
                    //LoadDataBaseFile();
                    //CRegData.LoadDataBase(DataBaseDir);
                    //if (CRegData.DataBaseOk == true)
                    //{
                    //    UpDataPadName("");
                    //    UpDataTmux(TmuxATSTBox.Text);
                    //    if (easy_log.CheckPermission())
                    //        m_UI.LoadData();
                    //    //Debug.AppendMsg("Load Data Complete" + Environment.NewLine);
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            while (true)
            {
                if (BackThreadEnable == false)
                {
                    Thread.Sleep(100);
                }
                else
                {
                    ret = Device.FindJlinkDevice(JlinkOp.DeviceName);
                    if (ret == true)
                    {
                        OpenDevice();
                    }
                    else
                    {
                        if (Device.GetDeviceStatus() == true)
                        {
                            Device.Close();
                            Debug.AppendMsg("Close Jlink Successful" + Environment.NewLine);
                        }
                    }

                    BackThreadEnable = false;
                }
            }
        }

        private bool OpenDevice()
        {
            bool dev_ok = false;
            if (Device.GetDeviceStatus() == false)
            {
                dev_ok = Device.Open(JlinkOp.Protocol, JlinkOp.SpeedKHz);
            }
            return dev_ok;
        }

        private void SetTheme(int colorSchemeIndex)
        {
            switch (colorSchemeIndex)
            {
                case 0:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.BlueGrey800,
                        Primary.BlueGrey900,
                        Primary.BlueGrey500,
                        Accent.LightBlue200,
                        TextShade.WHITE);
                    break;
                case 1:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.Green600,
                        Primary.Green700,
                        Primary.Green200,
                        Accent.Red100,
                        TextShade.WHITE);
                    break;
                case 2:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? Primary.Teal500 : Primary.Indigo500,
                        materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? Primary.Teal700 : Primary.Indigo700,
                        materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? Primary.Teal200 : Primary.Indigo100,
                        Accent.Pink200,
                        TextShade.WHITE);
                    break;
                case 3:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.Pink500,
                        Primary.Pink600,
                        Primary.Pink100,
                        Accent.Pink100,
                        TextShade.WHITE);
                    break;
            }
            Invalidate();
            UpdateControlColor();
        }

        private void FileEnterEvent(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            string FileName = Path.GetFileName(path).ToLower();
            string ExeName = Path.GetExtension(path);
            Debug.AppendMsg(path + Environment.NewLine);

            //if (easy_log.CheckPermission() == false)
            //{
            //    return;
            //}
            if (Directory.Exists(path))
            {

                if (WilfFile.OpenYesNo("Load Project?"))
                {
                    gCfgParam.ArmProjectDir = path;
                    string[] lst = WilfFile.GetFile(path,".uvprojx",false);
                    if (lst != null && lst.Length>=1)
                    {
                        gCfgParam.ArmProjectFile=lst[0];
                        Json.SaveCfg(gCfgParam);
                        aLLToolStripMenuItem_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Can't Found Any *.uvprojx File");
                    }
                }
            }
            else if(ExeName == ".uvprojx")
            {
                if (WilfFile.OpenYesNo("Load Project?"))
                {
                    gCfgParam.ArmProjectFile= path; 
                    gCfgParam.ArmProjectDir = Path.GetDirectoryName(gCfgParam.ArmProjectFile);
                    Json.SaveCfg(gCfgParam);
                    aLLToolStripMenuItem_Click(sender, e);
                }
            }
            else if (FileName.Contains(".map"))
            {
                gCfgParam.ArmMapFile = path;
                Json.SaveCfg(gCfgParam);
            }
            else if (FileName.Contains(".hex"))
            {
                gCfgParam.ArmHexFile = path;
                Json.SaveCfg(gCfgParam);
            }
            else if (FileName.Contains(".bin"))
            {
                gCfgParam.ArmBinFile = path;
                Json.SaveCfg(gCfgParam);
            }
            else if (FileName.Contains("Arm_fw.axf"))
            {
                if (WilfFile.OpenYesNo("Arm_FW.axf?"))
                {
                    gCfgParam.ArmAxfFile = path;
                    Json.SaveCfg(gCfgParam);
                    aLLToolStripMenuItem_Click(sender, e);
                }
            }
            else if (FileName == "inf.data")
            {
                if (WilfFile.OpenYesNo("Inf.data"))
                {
                    string Content = File.ReadAllText(path);
                    Content = AES.Decrypt(Content);
                    Debug.AppendMsg(Content);

                }
            }

        }

        public bool CheckUser()
        {
            LoadForm loadForm = new LoadForm();
            if (File.Exists(Json.m_SoftInfFile) == false)
            {
                loadForm.ShowDialog();
                gCfgParam = Json.LoadCfg();
                gCfgParam.ThemeIndex = LoadForm.ThemeIndex;
                Json.SaveCfg(gCfgParam);
            }
            if (loadForm.CheckOutTime())
            {
                loadForm.ShowDialog();
                if (LoadForm.TimeOut == false)
                {
                    loadForm.ShowDialog();
                    gCfgParam = Json.LoadCfg();
                    gCfgParam.ThemeIndex = LoadForm.ThemeIndex;
                    Json.SaveCfg(gCfgParam);
                }
                else
                {
                    return false;
                }
                if (!File.Exists(Json.m_SoftInfFile))
                {
                    return false;
                }

            }
            return true;
        }

        private void Monitor_Thread_Function(object InPam)
        {
            string MonitorFile = "Monitor.txt";
            MainForm Form = (MainForm)InPam;
            if (File.Exists(MonitorFile) == false)
            {
                File.WriteAllText(MonitorFile, "rs err_handler_pll_lockdet" + Environment.NewLine);
            }
            string[] CmdLine = File.ReadAllLines(MonitorFile);
            while (JlinkOp.Monitor_Thread != null)
            {
                if (JlinkOp.Monitor_Enable == true)
                {
                    Debug.AppendMsg("");
                    for (int i = 0; i < CmdLine.Length; i++)
                    {
                        ExeCmd(CmdLine[i]);
                    }
                    Thread.Sleep(1);
                }
                else
                {
                    Thread.Sleep(500);
                }
            }
            JlinkOp.StopMonitor();
            Debug.AppendMsg("Monitor Exist" + Environment.NewLine);
        }

        string GetCompileVersion()
        {
            string OriginVersion = "" + System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location);
            int MsgCnt = 0;
            string year = "";
            string month = "";
            string data = "";
            for (int i = 0; i < OriginVersion.Length && MsgCnt < 3; i++)
            {
                char ch = OriginVersion[i];
                if (ch >= '0' && ch <= '9')
                {
                    switch (MsgCnt)
                    {
                        case 0: year += ch; break;
                        case 1: month += ch; break;
                        case 2: data += ch; break;
                    }
                }
                else
                {
                    MsgCnt++;
                }
            }
            while (year.Length < 4) year = "0" + year;
            while (month.Length < 2) month = "0" + month;
            while (data.Length < 2) data = "0" + data;
            return "v" + year + "." + month + "." + data;
        }

        public bool  DownloadFlash(string HexFile, string AlgoFile,UInt32 LoadAddr)
        {
            Debug.AppendMsg("********************Download*********************" + Environment.NewLine);
            FLM flm ;
            byte[] BinData, TmpBinData;
            byte[] PageData;
            UInt32 SectorSize,PageSize,CurAddr;
            BinDataType BinInf = HexData.HexToBin(HexFile);
            TmpBinData = BinInf.Data;
            flm = new FLM(AlgoFile, LoadAddr);
            SectorSize = flm.m_FlashInf.sectors[0].szSector;
            PageSize = flm.m_FlashInf.szPage;
            CurAddr = BinInf.Addr;
            
            BinData = WilfDataPro.Align(TmpBinData, SectorSize,0x00);
            PageData = new byte[PageSize];

            Debug.AppendMsg("[Inf]DevAddr"+"=0x" +flm.m_FlashInf.DevAdr.ToString("X8")+ Environment.NewLine);
            Debug.AppendMsg("[Inf]Program Addr"+"=0x" +CurAddr.ToString("X8")+ Environment.NewLine);
            Debug.AppendMsg("[Inf]Code Size"+"=0x" +TmpBinData.Length.ToString("X8")+ Environment.NewLine);
            Debug.AppendMsg("[Inf]szSector"+"=0x" +flm.m_FlashInf.sectors[0].szSector.ToString("X8")+ Environment.NewLine);
            Debug.AppendMsg("[Inf]szPage"+"=0x" +flm.m_FlashInf.szPage.ToString("X8")+ Environment.NewLine);
            Debug.AppendMsg("[Inf]RAM Addr"+"=0x" +LoadAddr.ToString("X8")+ Environment.NewLine);
            Debug.AppendMsg("[Inf]Hex File Path"+"=" +HexFile+ Environment.NewLine);

            SetProBarValue(0);
            JlinkOp.Reset();
            JlinkOp.JLINKARM_Halt();
            JlinkOp.WritMemory(flm.IRAM_StartAddr, (UInt32)flm.CodeData.Length, flm.CodeData);

            JlinkOp.JLINKARM_WriteReg((ARM_REG)9,flm.Static_Base);
            JlinkOp.JLINKARM_WriteReg((ARM_REG)13, flm.Stack_Addr);
            SetProBarValue(10);
            for (UInt32 i = 0; i < BinData.Length; i+=SectorSize)
            {
                CurAddr += i; 
                JlinkOp.CallFunction(flm.GetFunAddr("Init"), flm.IRAM_StartAddr, 0, 1, 0, flm.IRAM_StartAddr, true);
                JlinkOp.CallFunction(flm.GetFunAddr("EraseSector"), CurAddr, 0, 0, 0, flm.IRAM_StartAddr, true);
                JlinkOp.CallFunction(flm.GetFunAddr("UnInit"), 1, 0, 0, 0, flm.IRAM_StartAddr, true);
                for (UInt32 j = 0; j < SectorSize; j+=PageSize)
                {
                    Array.Copy(BinData, i+j, PageData,0, PageSize);
                    JlinkOp.WritMemory(flm.IRAM_Buffer_Addr, PageSize, PageData);

                    JlinkOp.CallFunction(flm.GetFunAddr("Init"), flm.IRAM_StartAddr, 0, 2, 0, flm.IRAM_StartAddr, true);
                    JlinkOp.CallFunction(flm.GetFunAddr("ProgramPage"), CurAddr+j, PageSize, flm.IRAM_Buffer_Addr, 0, flm.IRAM_StartAddr, true);
                    JlinkOp.CallFunction(flm.GetFunAddr("UnInit"), 2, 0, 0, 0, flm.IRAM_StartAddr, true);
                }
                SetProBarValue((int)(10+90.0*((double)i/BinData.Length)));
            }
            byte[] ReadData;
            JlinkOp.ReadMemory(BinInf.Addr, (UInt32)BinData.Length, out ReadData);
            File.WriteAllBytes("BinData.bin", BinData);
            File.WriteAllBytes("ReadData.bin", ReadData);
            if (WilfDataPro.Eq(ReadData,BinData))
            {
                Debug.AppendMsg("PASS"+Environment.NewLine);
            }
            else
            {
                Debug.AppendMsg("FAIL"+Environment.NewLine);
            }
            JlinkOp.Reset();
            JlinkOp.JLINKARM_Go();
            SetProBarValue(100);
            return true;
        }

        public bool EraseFlash(string AlgoFile, UInt32 LoadAddr)
        {
            Debug.AppendMsg("********************Erase*********************" + Environment.NewLine);
            FLM flm;
            UInt32 SectorSize, PageSize, CurAddr;
            flm = new FLM(AlgoFile, LoadAddr);
            SectorSize = flm.m_FlashInf.sectors[0].szSector;
            PageSize = flm.m_FlashInf.szPage;
            CurAddr = flm.m_FlashInf.DevAdr;

            Debug.AppendMsg("[Inf]DevAddr"+"=0x" +flm.m_FlashInf.DevAdr.ToString("X8")+ Environment.NewLine);
            SetProBarValue(0);
            JlinkOp.Reset();
            JlinkOp.JLINKARM_Halt();
            JlinkOp.WritMemory(flm.IRAM_StartAddr, (UInt32)flm.CodeData.Length, flm.CodeData);

            JlinkOp.JLINKARM_WriteReg((ARM_REG)9, flm.Static_Base);
            JlinkOp.JLINKARM_WriteReg((ARM_REG)13, flm.Stack_Addr);
            SetProBarValue(50);
            JlinkOp.CallFunction(flm.GetFunAddr("Init"), flm.IRAM_StartAddr, 1000000, 3, 0, flm.IRAM_StartAddr, true);
            JlinkOp.CallFunction(flm.GetFunAddr("EraseChip"), CurAddr, 0, 0, 0, flm.IRAM_StartAddr, true);
            JlinkOp.CallFunction(flm.GetFunAddr("UnInit"), 3, 0, 0, 0, flm.IRAM_StartAddr, true);
            SetProBarValue(100);
            return true;
        }
    }
}
