using CSCS_TOOL;
using MaterialSkin;
using MaterialSkin.Controls;
using NPOI.SS.Formula.Functions;
using SplitAndMerge;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SOM_DEVELOP_TOOL
{
    public partial class MainForm : MaterialForm
    {
        public string TestResultDir = Application.StartupPath+"\\TestResult";
        private readonly MaterialSkinManager materialSkinManager;
        public string UpDateTool = "";
        public string UpDateName = "UpDateTool.exe";
        public string UpDataExeDir = @"\\172.17.2.16\大文件传输\文件中转\wangweifeng\Public";
        public string UpDataBinDir;
        public string UpDataFile;
        public string CsScriptDir = Application.StartupPath + "\\" + "Scripts";

        public FormRTT pFormRTT;
        public LogForm plogForm;
        public IdForm pFormId;
        private Thread BackThread;
        private bool BackThreadEnable = false;
        private List<string> CmdStore = null;
        private delegate void ThreadWork();
        private delegate void ThreadWorkStr(string Msg);
        private delegate void ThreadWorkTable(int Index);
        private delegate void ThreadWorkStrColor(string Msg, Color color);
        private delegate void ThreadWorkStr2(string Msg0, string Msg1);
        public DataUI m_UI;
        public int ThemeIndex = 0;
        public static bool log_to_file_flag = false;
        public static string log_file = Application.StartupPath + "\\log.txt";
        public bool boot_from_bat = false;
        public string DataDir = Application.StartupPath + "\\Data";
        public string DataBaseDir = Application.StartupPath + "\\Database";
        public string ScriptDir = Application.StartupPath + "\\Script";
        public static CfgType gCfgParam;
        public bool gExit = false;
        public static easy_log[] m_Log = new easy_log[2];
        public string ScriptFileName;
        public string TestCaseFileName;
        public static UInt32[] MonitorAddr;
        public bool IsFormLoaded = false;
        public string Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public bool IsServerExist = false;
        public string ServerUserDir;
        public string ServerAdministratorDir;
        public string ServerLogDir;
        public string ServerAdministratorInf;
        private int colorSchemeIndex;
        public MainForm()
        {
            InitializeComponent();
            if (easy_log.CheckPermission() == false)
            {
                eDITToolStripMenuItem.Visible = false;
                vIEWToolStripMenuItem.Visible = false;
                tOOLToolStripMenuItem1.Visible = false;
                //materialTabControl1.TabPages.Remove(tabPage1);
                //materialTabSelector1.Visible = false;
            }
            this.AllowDrop = true;
            MsgBox.AllowDrop = true;
            MsgBox.DragDrop += new DragEventHandler(FileEnterEvent);

            // Initialize MaterialSkinManager
            materialSkinManager = MaterialSkinManager.Instance;

            // Set this to false to disable backcolor enforcing on non-materialSkin components
            // This HAS to be set before the AddFormToManage()
            materialSkinManager.EnforceBackcolorOnAllComponents = true;

            // MaterialSkinManager properties
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
            UpdateControlColor();
            MainMenu.Location = new Point(MainMenu.Location.X-58, MainMenu.Location.Y-5);
            SetFormStyle();
        }

        public MainForm(string[] args)
        {
            string FilePath = "";
            int FileType = 1;
            bool WaitFlag = true;
            boot_from_bat = true;
            InitializeComponent();
            log_to_file_flag = true;
            if (File.Exists(log_file))
                File.Delete(log_file);
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;

            // get the name of our process
            string p = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            // get the list of all processes by that name
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(p);
            System.Diagnostics.Process LastProcess;
            // if there is more than one process
            if (processes.Length > 1)
            {
                Debug.AppendMsg("[Inf]Process number:" + processes.Length + Environment.NewLine);
                LastProcess = processes[0];
                for (int i = 1; i < processes.Length; i++)
                {
                    if (processes[i].StartTime > LastProcess.StartTime)
                    {
                        LastProcess = processes[i];
                        LastProcess.Kill();
                    }
                    else
                        processes[i].Kill();
                }
            }
            OpenDevice();

            if (args.Length == 1)
            {
                FilePath = args[0];
            }
            else if (args.Length == 2)
            {
                FilePath = args[0];
                FileType = Convert.ToInt32(args[1]);
            }
            else
            {
                FilePath = args[0];
                FileType = Convert.ToInt32(args[1]);
                WaitFlag = Convert.ToBoolean(args[2]);
            }
            if (File.Exists(FilePath))
            {
                Debug.AppendMsg("[Inf]Over" + Environment.NewLine);
            }
            Device.Close();
        }

        private void seedListView()
        {

        }

        private void SetFormStyle()
        {
            MaterialForm.FormStyles SelectedStyle = (MaterialForm.FormStyles)Enum.Parse(typeof(MaterialForm.FormStyles), "ActionBar_35");
            if (this.FormStyle!= SelectedStyle) this.FormStyle = SelectedStyle;
        }

        private void UpdateControlColor()
        {
            MainMenu.BackColor = materialSkinManager.ColorScheme.PrimaryColor;
            VerLab.BackColor = materialSkinManager.ColorScheme.PrimaryColor;
            MainMenu.Font = new System.Drawing.Font("微软雅黑", 13);
            materialDivider1.BackColor = materialSkinManager.ColorScheme.PrimaryColor;
        }

        private void MaterialButton2_Click(object sender, EventArgs e)
        {
            ProBar.Value = Math.Min(ProBar.Value + 10, 100);
        }

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            ProBar.Value = Math.Max(ProBar.Value - 10, 0);
        }


        private void MaterialButton3_Click(object sender, EventArgs e)
        {

        }

        private void materialSwitch9_CheckedChanged(object sender, EventArgs e)
        {
            DrawerAutoShow = IdSortCB.Checked;
        }

        private void materialTextBox2_LeadingIconClick(object sender, EventArgs e)
        {

        }

        private void materialButton23_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (boot_from_bat)
                Application.Exit();
            else
            {
                for (int i = 0; i < m_Log.Length; i++)
                {
                    m_Log[i] = new easy_log();
                }
                m_UI = new DataUI(this);
                m_UI.LoadData();
                Init();
                if (!CheckUser())
                    Application.Exit();
                SetTheme(gCfgParam.ThemeIndex);
                LoadScript();
                BackThread = new Thread(Reload);
                BackThread.IsBackground = true;
                BackThread.Start();
                BackThreadEnable = true;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Focus();
                IsFormLoaded = true;
            }
        }

        private void LoadScript(string FileShow = "")
        {
            string[] file_list;
            WilfFile.CreateDirectory(CsScriptDir);
            file_list = WilfFile.GetFile(CsScriptDir, ".c", false);
            ScriptCBox.Items.Clear();   
            for (int i = 0; i < file_list.Length; i++)
            {
                ScriptCBox.Items.Add(Path.GetFileName(file_list[i]));
            }
            if (FileShow == "")
            {
                if (file_list.Length>0)
                    ScriptCBox.Text = Path.GetFileName(file_list[0]);
            }
            else
            {
                ScriptCBox.Text = Path.GetFileName(FileShow);
            }
            ScriptCBox.Refresh();
        }

        private void themeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorSchemeIndex++;
            if (colorSchemeIndex > 3)
                colorSchemeIndex = 0;
            SetTheme(colorSchemeIndex);
            UpdateControlColor();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReOpenJlink();
        }

        private void aLLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string BaseFile;
            gCfgParam = Json.LoadCfg();
            if (File.Exists(gCfgParam.ArmProjectFile))
            {
                BaseFile = gCfgParam.ArmProjectFile;
                gCfgParam.ArmBinFile = ProjectInf.GetArmFilePath(BaseFile, FileType.BIN);
                gCfgParam.ArmMapFile = ProjectInf.GetArmFilePath(BaseFile, FileType.MAP);
                gCfgParam.ArmAxfFile = ProjectInf.GetArmFilePath(BaseFile, FileType.AXF);
                gCfgParam.ArmHexFile = ProjectInf.GetArmFilePath(BaseFile, FileType.HEX);

                if (File.Exists(gCfgParam.ArmMapFile))
                {
                    MapParser.GetArmHash(gCfgParam.ArmMapFile);
                    Debug.AppendMsg("[Inf]Refresh Map Table"+Environment.NewLine);
                }

                if (File.Exists(gCfgParam.ArmHexFile) == false)
                {
                    Debug.AppendMsg("[Inf]Missing:" + gCfgParam.ArmHexFile + Environment.NewLine);
                }
                if (File.Exists(gCfgParam.ArmMapFile) == false)
                {
                    Debug.AppendMsg("[Inf]Missing:" + gCfgParam.ArmMapFile + Environment.NewLine);
                }
            }




            if (Directory.Exists(gCfgParam.KeilDir))
            {
                gCfgParam.KeilFromelf = gCfgParam.KeilDir + @"\ARM\ARMCC\bin\fromelf.exe";
            }
            else
            {
                if (Directory.Exists(@"D:\Keil_v5"))
                {
                    gCfgParam.KeilFromelf = gCfgParam.KeilDir + @"\ARM\ARMCC\bin\fromelf.exe";
                }
                if (Directory.Exists(@"C:\Keil_v5"))
                {
                    gCfgParam.KeilFromelf = gCfgParam.KeilDir + @"\ARM\ARMCC\bin\fromelf.exe";
                }
            }

            if (Directory.Exists(gCfgParam.LogicDir))
            {
                string PlusDir;
                PlusDir = gCfgParam.LogicDir + @"\resources\windows\Analyzers";
                if (Directory.Exists(PlusDir) == true)
                {
                    gCfgParam.LogicAnalyzersDir = PlusDir;
                }
                PlusDir = gCfgParam.LogicDir + @"\Analyzers";
                if (Directory.Exists(PlusDir) == true)
                {
                    gCfgParam.LogicAnalyzersDir = PlusDir;
                }
            }
            Json.SaveCfg(gCfgParam);
            Debug.AppendMsg("[Inf]Over"+Environment.NewLine);
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CfgType A = new CfgType();
            Json.SaveCfg(A);
        }

        private void loadHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file = WilfFile.OpenFile(".data");
            if (file != null)
            {
                gCfgParam = Json.LoadCfg(file);
                Json.BackupCfg();
                Json.SaveCfg(gCfgParam);
                Debug.AppendMsg("[Inf]load config param ok" + Environment.NewLine);
            }
        }

        private void loadScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (JlinkOp.CheckCpuID() == false)
            {
                MessageBox.Show("Jlink is offline. Please reconnect");
                return;
            }
            string FileName = WilfFile.OpenFile(".*");
            ScriptFileName = FileName;
            RunScript(ScriptFileName);
        }

        private void RunScript(string FileName)
        {
            string ExeName = Path.GetExtension(FileName);

            if (File.Exists(FileName))
            {
                Debug.AppendMsg("");
                string[] Lines = File.ReadAllLines(FileName);
                for (int i = 0; i < Lines.Length; i++)
                {
                    ExeCmd(Lines[i]);
                }
            }
        }

        private void monitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (JlinkOp.Monitor_Thread == null)
            {
                JlinkOp.Monitor_Thread = new Thread(new ParameterizedThreadStart(Monitor_Thread_Function));
                JlinkOp.Monitor_Thread.Start(this);
                JlinkOp.Monitor_Enable = true;
                Debug.AppendMsg("[Inf]Monitor Is Already Running" + Environment.NewLine);
            }
            else
            {
                JlinkOp.StopMonitor();
                Thread.Sleep(100);
                Debug.AppendMsg("[Inf]Monitor Is Already Closed" + Environment.NewLine);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Device.Close();
            Debug.AppendMsg("[Inf]Jlink is Closed" + Environment.NewLine);
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ret = JlinkOp.Reset(true);
            JlinkOp.JLINKARM_Go();
            Debug.AppendMsg("[Inf]Chip Reset" + Environment.NewLine);
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            easy_log.ConnectDelayTime = Convert.ToInt32(2000);

            if (IdRangeBox.Text.Trim() == "")
            {
                Int32[] RangeData = WilfDataPro.ParseRange2("0:255");
                easy_log.IdRange = WilfDataPro.IndexToBit(RangeData, 8);
            }
            else
            {
                Int32[] RangeData = WilfDataPro.ParseRange2(IdRangeBox.Text);
                easy_log.IdRange = WilfDataPro.IndexToBit(RangeData, 8);
            }
            RttLogPro();
        }

        private void generateIDTABLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int Index = 0; Index < m_Log.Length; Index++)
            {
                if (m_Log[Index].Enable) //dsp
                {
                    m_Log[Index].ID_Table.Clear();
                    if (Index == 0)
                    {
                        if (File.Exists(gCfgParam.ArmProjectFile))
                        {
                            GenerateArmID_Table(gCfgParam.ArmProjectFile, Index, IdSortCB.Checked);
                            m_Log[Index].ExportIdTable();
                        }
                        else
                        {
                            MessageBox.Show("Project path not found");
                        }
                    }
                }
            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JlinkOp.JLINKARM_Go();
            Debug.AppendMsg("[Inf]CPU Run" + Environment.NewLine);
        }

        private void haltToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JlinkOp.JLINKARM_Halt();
        }

        private void statusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int IsHalt = JlinkOp.JLINKARM_IsHalted();
            if (IsHalt == 0)
            {
                Debug.AppendMsg("[Inf]CPU is runing" + Environment.NewLine);
            }
            else
            {
                Debug.AppendMsg("[Inf]CPU is Halt" + Environment.NewLine);
            }
        }

        private void stackAnalysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UInt32[] RegValue;
            UInt32[] SpValue;
            StringBuilder sb = new StringBuilder();
            StringBuilder main_inf = new StringBuilder();
            Debug.AppendMsg("");
            if (JlinkOp.CheckCpuID())
            {
                JlinkOp.JLINKARM_Halt();
                JlinkOp.ReadAllCpuRegister(out RegValue);
                UInt32 LR = RegValue[14];
                UInt32 SP = RegValue[13];
                UInt32 PC = RegValue[15];
                UInt32 MSP = RegValue[17];
                UInt32 PSP = RegValue[18];

                main_inf.Append("LR:" + "." + "0x" + LR.ToString("X8")+ Environment.NewLine);
                main_inf.Append("SP:" + "." + "0x" + SP.ToString("X8")+ Environment.NewLine);
                main_inf.Append("MSP:" + "." + "0x" + MSP.ToString("X8")+ Environment.NewLine);
                main_inf.Append("PSP:" + "." + "0x" + PSP.ToString("X8")+ Environment.NewLine);
                main_inf.Append("PC:" + "." + "0x" + PC.ToString("X8")+ Environment.NewLine);

                sb.Append("CPU Status:" + Environment.NewLine);
                for (int i = 0; i < RegValue.Length; i++)
                {
                    sb.Append(i.ToString("D2") + "." + "0x" + RegValue[i].ToString("X8") + Environment.NewLine);
                }

                JlinkOp.ReadRegister(SP, out SpValue, 32);
                sb.Append("SP Status:" + Environment.NewLine);
                for (int i = 0; i < SpValue.Length; i++)
                {
                    sb.Append(i.ToString("D2") + "." +"0x" + SpValue[i].ToString("X8") + Environment.NewLine);
                }

                JlinkOp.ReadRegister(MSP, out SpValue, 32);
                sb.Append("MSP Status:" + Environment.NewLine);
                for (int i = 0; i < SpValue.Length; i++)
                {
                    sb.Append(i.ToString("D2") + "." +"0x" + SpValue[i].ToString("X8") + Environment.NewLine);
                }

                JlinkOp.ReadRegister(PSP, out SpValue, 32);
                sb.Append("PSP Status:" + Environment.NewLine);
                for (int i = 0; i < SpValue.Length; i++)
                {
                    sb.Append(i.ToString("D2") + "." +"0x" + SpValue[i].ToString("X8") + Environment.NewLine);
                }

                Debug.AppendMsg(sb.ToString());
                Debug.AppendMsg(main_inf.ToString());
            }
        }

        private void cPUToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void ihexToolStripMenuItem_Click(object sender, EventArgs e)
        {

            StringBuilder dump_msg = new StringBuilder();

            string FileName = WilfFile.OpenFile(".hex");
            StringBuilder sb = new StringBuilder();
            if (!File.Exists(FileName))
                return;
            DownloadSOM_RAM(FileName);
            gCfgParam.ArmHexFile = FileName;
        }

        private void m7FlashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadApollo();
        }

        private void DownloadApollo(string FwFile = "", UInt32 StartAddr = 0x40000)
        {
            bool ret;
            string file;
            string exe_name;
            string DependentFile = "ApolloFLM.hex";
            double ProDependentFileTime;
            BinDataType bd = new BinDataType();
            Debug.AppendMsg("");
            GetResFile(DependentFile, DependentFile);
            if (File.Exists(FwFile))
                file = FwFile;
            else
                file = WilfFile.OpenFile(".hex;*.bin");

            Debug.AppendMsg("[Inf]initial CPU...."+Environment.NewLine);
            WilfDataPro.InitStartTime();
            DownloadSOM_RAM(DependentFile, false);
            Thread.Sleep(10);

            ProDependentFileTime = WilfDataPro.GetDiffTime(true);
            Debug.AppendMsg("[Inf]initial time="+ProDependentFileTime.ToString("F3")+"ms"+Environment.NewLine);

            File.Delete(DependentFile);
            ret = jlink_protocol.Init(0x10160000);
            if (ret == false)
            {
                Debug.AppendMsg("[Error]Jlink Protocol Initial Fail");
                return;
            }

            if (File.Exists(file))
            {
                Debug.AppendMsg("[Inf]Load File@"+file+Environment.NewLine);
                exe_name = Path.GetExtension(file).ToLower();
                if (exe_name == ".hex")
                {
                    bd = HexData.HexToBin(file);
                }
                else if (exe_name == ".bin")
                {
                    bd.Addr = StartAddr;
                    bd.Data = File.ReadAllBytes(file);
                }
                else
                {
                    MessageBox.Show("This file is not support");
                    return;
                }
                ProBar.Visible= true;
                ret = jlink_protocol.DownloadToFlash(bd.Addr, bd.Data);
                ProBar.Visible= false;
                if (ret)
                    Debug.AppendMsg("[Inf]Download FW To Flash Successfully"+Environment.NewLine, Color.Green);
                else
                    Debug.AppendMsg("[Inf]Download FW To Flash Fail"+Environment.NewLine, Color.Red);

            }
        }

        private void mAPToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start(Path.GetDirectoryName(gCfgParam.ArmMapFile));
            System.Diagnostics.Process.Start("notepad++", gCfgParam.ArmMapFile);
        }

        private void directoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(gCfgParam.ArmProjectDir);
        }

        private void pathInfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad++", Json.m_CfgFile);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Msg = "";
            Msg += "Version : " + Version + Environment.NewLine;
            Msg += "Author : Wilf" + Environment.NewLine;
            Msg += "Public Time : " + GetCompileVersion() + Environment.NewLine;
            //Msg += "Require : .NET Framework 4.7.2" + Environment.NewLine;
            MessageBox.Show(Msg);
        }

        private void resetAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            if (WilfFile.OpenYesNo("Warning:are you sure to reset all Settings?"))
            {
                list.Add(Json.m_CfgFile);
                list.Add(Json.m_SoftInfFile);
                list.Add(m_UI.FileName);
                list.Add(m_UI.FileNameWithHistory);
                for (int i = 0; i<list.Count; i++)
                {
                    if (File.Exists(list[i]))
                    {
                        File.Delete(list[i]);
                    }
                }
                Application.Exit();
            }
        }

        private void helpDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(easy_log.HelpFIle))
            {
                System.Diagnostics.Process.Start(easy_log.HelpFIle);
            }
        }

        private void ComBox_KeyDown(object sender, KeyEventArgs e)
        {
            string str = ((System.Windows.Forms.TextBox)sender).Text;
            KeyDownEvent(sender, e, str);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TempPage.SelectedIndex == 0)
            {
                Debug.AppendMsg("");
            }
            else
            {
                ShowMsg("");
            }
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private int[] TestCheckSum(Int64 TestTime,int BitNum,bool Sum0Enable,bool Sum1Enable,bool CrcEnable)
        {
            List<UInt32> DB = new List<UInt32>();
            Random rnd = new Random();
            byte[] Data = new byte[4*1024];
            byte[] DataBackUp = new byte[4*1024];
            UInt32 CrcValue=0, CheckSumValue=0, CheckSumValueX=0, CrcValueRef=0, CheckSumValueRef=0, CheckSumValueRefX=0;
            UInt32[] ErrorPosition;
            byte[] CrcSha256, SumSha256, RefSha256;
            int ItemIndex=0, BitIndex=0;
            int CrcErrorCount = 0, CheckSumErrorCount = 0, CheckSumErrorCountX=0;
            int[] ErrorInf = new Int32[3];
            string CrcFileName = WilfFile.GetTimeStr()+"_crc.csv";
            string SumFileName = WilfFile.GetTimeStr()+"_sum.csv";
            string CrcInfFile = WilfFile.GetTimeStr()+"_crc_inf.csv";
            string SumInfFile = WilfFile.GetTimeStr()+"_sum_inf.csv";
            WilfDataPro.UpdateSeed();
            Data = WilfDataPro.GetRandData(Data.Length);
            Array.Copy(Data, DataBackUp,Data.Length);
            int[] InfData = new Int32[5];

            
            CrcValueRef = jlink_protocol.GetCrc32(Data, 0, (UInt32)Data.Length,true);
            CheckSumValueRef = jlink_protocol.GetCheckSum(Data,0);
            CheckSumValueRefX = jlink_protocol.GetCheckSum(Data,1);
            RefSha256 = SE.SHA256(Data);
            AppendMsg("RefSha256:"+SE.SHA256_String(RefSha256)+Environment.NewLine);
            //WilfFile.Write(CrcFileName, WilfDataPro.ToString(Data)+Environment.NewLine);
            //WilfFile.Write(SumFileName, WilfDataPro.ToString(Data)+Environment.NewLine);
            //WilfFile.Write(CrcInfFile, "i,ItemIndex,BitIndex,Crc,Sum,SHA256"+Environment.NewLine);
            //WilfFile.Write(SumInfFile, "i,ItemIndex,BitIndex,Crc,Sum,SHA256"+Environment.NewLine);
            for (Int64 i = 0; i<TestTime; i++)
            {
                InfData[0] = (int)i;
                ErrorPosition = WilfDataPro.GetRandData32(BitNum);
                for (int k = 0; k<ErrorPosition.Length; k++)
                {

                    ItemIndex =(int)(ErrorPosition[k]%Data.Length);
                    BitIndex =(int)((ErrorPosition[k]/Data.Length)%8);
                    DB.Add((UInt32)ItemIndex);
                    DB.Add(DataBackUp[ItemIndex]);
                    InfData[1] = ItemIndex;
                    InfData[2] = BitIndex;

                    if (((Data[ItemIndex]>>BitIndex)&0x01)==1)
                        Data[ItemIndex] = (byte)(Data[ItemIndex]&(byte)((~(1<<BitIndex))));
                    else
                        Data[ItemIndex] = (byte)(Data[ItemIndex]|(1<<BitIndex));
                }
                //byte[] RefSha256X = SE.SHA256(Data);
                //AppendMsg("NewRefSha256:"+SE.SHA256_String(RefSha256X)+" ItemIndex/BitIndex="+ItemIndex+"//"+BitIndex+Environment.NewLine);
                //int DiffNum = WilfDataPro.GetDiffNum(Data, DataBackUp, Data.Length);
                //AppendMsg("DiffNum:"+DiffNum+Environment.NewLine);

                if (CrcEnable)
                CrcValue = jlink_protocol.GetCrc32(Data, 0, (UInt32)Data.Length,true);
                if(Sum0Enable)
                CheckSumValue = jlink_protocol.GetCheckSum(Data,0);
                if(Sum1Enable)
                CheckSumValueX = jlink_protocol.GetCheckSum(Data, 1);
                InfData[3] = (int)CrcValue;
                InfData[4] = (int)CheckSumValue;

                

                if (CheckSumValue == CheckSumValueRef && Sum0Enable)
                {
                    CheckSumErrorCount++;
                    SumSha256 = SE.SHA256(Data);
                    AppendMsg(i+"_SumSha256:"+SE.SHA256_String(SumSha256)+Environment.NewLine);
                    //WilfFile.WriteAppend(SumFileName, WilfDataPro.ToString(Data)+Environment.NewLine);
                    //WilfFile.WriteAppend(SumInfFile, WilfDataPro.ToString(InfData)+","+SE.SHA256_String(SumSha256)+Environment.NewLine);
                }

                if (CheckSumValueX == CheckSumValueRefX && Sum1Enable)
                {
                    CheckSumErrorCountX++;
                }

                if (CrcValue == CrcValueRef && CrcEnable)
                {
                    CrcErrorCount++;
                    CrcSha256 = SE.SHA256(Data);
                    AppendMsg(i+"_CrcSha256:"+SE.SHA256_String(CrcSha256)+Environment.NewLine);
                    //WilfFile.WriteAppend(CrcFileName, WilfDataPro.ToString(Data)+Environment.NewLine);
                    //WilfFile.WriteAppend(CrcInfFile, WilfDataPro.ToString(InfData)+","+SE.SHA256_String(CrcSha256)+Environment.NewLine);
                }

                //恢复数据
                for (int k = 0; k<DB.Count; k+=2)
                {
                    Data[(int)DB[k]] = (byte)DB[k+1];
                }
                DB.Clear();
                //byte[] RefSha256X = SE.SHA256(Data);
                //AppendMsg("NewRefSha256:"+SE.SHA256_String(RefSha256X)+" ItemIndex/BitIndex="+ItemIndex+"//"+BitIndex+Environment.NewLine);


            }
            AppendMsg("**************************************************"+TestTime+Environment.NewLine);
            AppendMsg("TestTime="+TestTime+Environment.NewLine);
            AppendMsg("BitNum="+BitNum+Environment.NewLine);
            AppendMsg("CheckSumErrorCount="+CheckSumErrorCount+Environment.NewLine);
            AppendMsg("CheckSumErrorCountX="+CheckSumErrorCountX+Environment.NewLine);
            AppendMsg("CrcErrorCount="+CrcErrorCount+Environment.NewLine);
            
            AppendMsg("Sum Error Ratio = "+(100*CheckSumErrorCount/(double)TestTime).ToString("F8")+"%"+Environment.NewLine);
            AppendMsg("Sum Error RatioX = "+(100*CheckSumErrorCountX/(double)TestTime).ToString("F8")+"%"+Environment.NewLine);
            AppendMsg("Crc Error Ratio = "+(100*CrcErrorCount/(double)TestTime).ToString("F8")+"%"+Environment.NewLine);
            ErrorInf[0] = CheckSumErrorCount;
            ErrorInf[1] = CheckSumErrorCountX;
            ErrorInf[2] = CrcErrorCount;
            return ErrorInf;

        }


        private void materialButton10_Click(object sender, EventArgs e)
        {
            string FwFile = ImageFileBox.Text;
            UInt32 Addr;
            if (JlinkOp.CheckCpuID() == true)
            {
                Addr = (UInt32)WilfDataPro.CalcExpression(materialTextBox5.Text);
                DownloadApollo(FwFile, Addr);
                MessageBox.Show("Download Successfully");
            }
            else
            {
                Debug.AppendMsg("[Inf]no device"+Environment.NewLine);
                MessageBox.Show("no device");
            }

        }

        private void materialButton11_Click(object sender, EventArgs e)
        {
            string file = WilfFile.OpenFile(".hex;*.bin");
            if (file != null)
            {
                ImageFileBox.Text = file;
                m_UI.Save();
            }

        }

        private void JlinkSpeedCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsFormLoaded == true)
            {
                if (JlinkSpeedCBox.Text.Trim() != "")
                {
                    JlinkOp.SpeedKHz = Convert.ToInt32(JlinkSpeedCBox.Text.Trim()) * 1000;
                    ReOpenJlink();
                    m_UI.Save();
                }
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            easy_log.BlockModeEnable = materialCheckBox3.Checked;
            m_UI.Save();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            easy_log.PcTimeEnable = checkBox1.Checked;
            m_UI.Save();
        }

        private void M7_CPU0_CB_CheckedChanged(object sender, EventArgs e)
        {
            UpDataLogParam();
            m_UI.Save();
        }

        private void M7_CPU1_CB_CheckedChanged(object sender, EventArgs e)
        {
            UpDataLogParam();
            m_UI.Save();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            easy_log.ShowLogEnable = checkBox2.Checked;
            m_UI.Save();
        }

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            easy_log.CpuTimeEnable = materialCheckBox1.Checked;
            m_UI.Save();
        }

        private void IdRangeBtn_Click(object sender, EventArgs e)
        {
            if (pFormId == null || pFormId.IsDisposed)
            {
                IdForm.IdRangeStr = "";
                IdForm.oldIdRangeStr = IdRangeBox.Text.Trim();
                pFormId = new IdForm();
                pFormId.ShowDialog();
                pFormId.Dispose();
                IdRangeBox.Text = IdForm.IdRangeStr;
                m_UI.Save();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath);
        }

        private void publicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Dir = Application.StartupPath + "\\Public";
            MapParser.InitMapHashTable(gCfgParam.ArmMapFile, true);
            generateIDTABLEToolStripMenuItem_Click(sender,e);
            WilfFile.CreateDirectory(Dir);  
            File.Copy(MapParser.DumpFile, Dir+"\\"+Path.GetFileName(MapParser.DumpFile), true);
            File.Copy(MapParser.MapInfFile,Dir+"\\"+Path.GetFileName(MapParser.MapInfFile),true);
            File.Copy(gCfgParam.ArmHexFile, Dir+"\\"+Path.GetFileName(gCfgParam.ArmHexFile),true);
            File.Copy(gCfgParam.ArmAxfFile, Dir+"\\"+Path.GetFileName(gCfgParam.ArmAxfFile), true);
            File.Copy(m_Log[0].IdTableFile, Dir+"\\"+Path.GetFileName(m_Log[0].IdTableFile), true);
            Debug.AppendMsg("Over"+Environment.NewLine);
        }

        private void IdSortCB_CheckedChanged(object sender, EventArgs e)
        {
            m_UI.Save();
        }

        private void materialExpansionPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void ConfigPage_Click(object sender, EventArgs e)
        {

        }

        private void m7DebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string DependentFile = "IPC.hex";
            double ProDependentFileTime;
            Debug.AppendMsg("");
            GetResFile(DependentFile, DependentFile);

            Debug.AppendMsg("[Inf]initial CPU...."+Environment.NewLine);
            WilfDataPro.InitStartTime();
            DownloadM7(DependentFile, false);
            Thread.Sleep(100);
            ProDependentFileTime = WilfDataPro.GetDiffTime(true);
            Debug.AppendMsg("[Inf]initial time="+ProDependentFileTime.ToString("F3")+"ms"+Environment.NewLine);
            Debug.AppendMsg("[Inf]Download Debug FW Successfully"+Environment.NewLine, Color.Green);
        }

        private void resetToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            JlinkOp.Reset();
            Debug.AppendMsg("CPU Reset" + Environment.NewLine);
        }

        private void loadTestCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            TestCaseFileName = WilfFile.OpenFile(".*");
            if (File.Exists(TestCaseFileName) == false)
                return;
            RunTestCase(TestCaseFileName, true);
        }

        private void RunTestCase(string Content, bool IsFile = false)
        {
           
            UInt32 StartAddress;
            switch2jlinkToolStripMenuItem_Click(null, null);
            MapParser.InitMapHashTable(gCfgParam.ArmMapFile);
            StartAddress = MapParser.GetAddr("gpst_pro");

            if (StartAddress == UInt32.MaxValue)
            {
                Debug.AppendMsg("[Error]Map Inf Is Invalid"+Environment.NewLine);
                return;
            }
            bool ret = jlink_protocol.Init(StartAddress);
            SOM.CallAPI(Content, IsFile);
        }
        private void loadTestCaseToolStripMenuItem_ClickX(object sender, EventArgs e)
        {
            string FileName;
            JlinkProtocolFrame js;
            string FunctionName;
            string[] Param;
            UInt32 FunctionAddr;
            UInt32[] ParamValue;
            StringBuilder sb = new StringBuilder();
            bool ret;
            string TempStr;
            double MultmeterValue = 0;

            FileName = WilfFile.OpenFile(".*");
            if (File.Exists(FileName) == false)
                return;
            Debug.AppendMsg("");
            MapParser.GetArmHash(gCfgParam.ArmMapFile);
            UInt32 StartAddress = MapParser.GetAddr("gpst_pro");
            ret = jlink_protocol.Init(StartAddress);
            if (ret == false)
            {
                Debug.AppendMsg("[Error]Jlink Protocol Init Fail"+Environment.NewLine);
                return;
            }
            string[] CaseStr = File.ReadAllLines(FileName);
            for (int i = 0; i<CaseStr.Length; i++)
            {
                string[] ItemStr;
                StringBuilder Msg = new StringBuilder();
                CaseStr[i] = WilfDataPro.RemoiveMulSapce(CaseStr[i]).Trim(';');
                ItemStr = CaseStr[i].Split(';');
                for (int k = 0; k<ItemStr.Length; k++)
                {
                    if (ItemStr[k].ToUpper().Contains("SLEEP"))
                    {
                        TempStr = WilfDataPro.MidStr(ItemStr[k], "(", ")");
                        Thread.Sleep(Convert.ToInt32(TempStr));
                    }
                    else if (ItemStr[k].ToUpper().Contains("SETOVERTIME"))
                    {
                        TempStr = WilfDataPro.MidStr(ItemStr[k], "(", ")");
                        jlink_protocol.SetOverTime(Convert.ToInt32(TempStr));
                    }
                    else if (ItemStr[k].ToUpper().Contains("READMULTMETER"))
                    {
                        string PortName;
                        PortName = V86E.FindDevice();
                        if (PortName != "")
                            MultmeterValue = V86E.ReadMultmeter(PortName);
                        else
                            MultmeterValue = 0;
                        Msg.Append(MultmeterValue.ToString()+",");
                    }
                    else
                    {
                        FunctionName = ItemStr[k].Substring(0, ItemStr[k].IndexOf('('));
                        string ParamStr = WilfDataPro.MidStr(ItemStr[k], "(", ")").Trim();
                        if (ParamStr.Contains(','))
                            Param = WilfDataPro.MidStr(ItemStr[k], "(", ")").Split(',');
                        else
                            Param = new string[0];
                        ParamValue = new UInt32[Param.Length];
                        for (int j = 0; j<Param.Length; j++)
                        {
                            ParamValue[j] = WilfDataPro.ToDec(Param[j]);
                        }
                        FunctionAddr = (UInt32)MapParser.GetAddr(FunctionName);
                        jlink_protocol.AutoTest(FunctionAddr, ParamValue, out js);
                        if (js.data != null && js.data.Length>0)
                            Msg.Append(ItemStr[k].Replace(",", " ")+","+js.data[0]+",");
                    }
                }
                Application.DoEvents();
                AppendMsg(i+","+ Msg.ToString().Trim(';')+Environment.NewLine);
                sb.Append(i+","+ Msg.ToString().Trim(';')+Environment.NewLine);
            }
            File.WriteAllText("TestLog.txt", sb.ToString());
            Debug.AppendMsg("[Inf]Over"+Environment.NewLine);
        }

        private void sNToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void switch2jlinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Switch2JlinkProtocol();
        }

        private static void Switch2JlinkProtocol()
        {
            UInt32 PC;
            UInt32 R9;
            JlinkOp.JLINKARM_Halt();
            R9 = JlinkOp.JLINKARM_ReadReg((ARM_REG)9);
            if (R9 != 0x5A5A5A5A)
            {
                JlinkOp.JLINKARM_Reset();
                JlinkOp.JLINKARM_Halt();
                JlinkOp.JLINKARM_WriteReg((ARM_REG)9, 0x5A5A5A5A);
                JlinkOp.ReadRegister(0x18000+4, out PC);
                JlinkOp.JLINKARM_WriteReg((ARM_REG)15, PC);
                JlinkOp.JLINKARM_Go();

            }
            else
            {
                JlinkOp.JLINKARM_Go();
            }
            Debug.AppendMsg("[Inf]Switch To Jlink Test Mode"+Environment.NewLine);
        }
        private void gOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.AppendMsg("CPU Run" + Environment.NewLine);
        }

        private void regToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UInt32[] RegValue;
            StringBuilder sb = new StringBuilder();
            Debug.AppendMsg("");
            string[] CPU_REG_Name = new string[] { "R0", "R1", "R2", "R3", "R4", "R5", "R6", "R7", "R8", "R9", "R10", "R11", "R12", "R13(SP)", "R14(LR)", "R15(PC)", "R16", "R17(MSP)", "R18(PSP)" };
            if (JlinkOp.CheckCpuID())
            {
                JlinkOp.JLINKARM_Halt();
                JlinkOp.ReadAllCpuRegister(out RegValue);
                UInt32 LR = RegValue[14];
                UInt32 SP = RegValue[13];
                UInt32 PC = RegValue[15];
                UInt32 MSP = RegValue[17];
                UInt32 PSP = RegValue[18];

                for (int i = 0; i<CPU_REG_Name.Length; i++)
                {
                    sb.Append(CPU_REG_Name[i] + "=" + "0x" + RegValue[i].ToString("X8")+ Environment.NewLine);
                }
            }
            AppendMsg(sb.ToString());
        }



        private bool FindValue(UInt32[] Data, UInt32 Value)
        {
            for (int i = 0; i<Data.Length; i++)
            {
                if (Data[i] == Value)
                    return true;

            }
            return false;
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {

            if (!File.Exists(FLM.AlgoFile))
            {
                GetResFile(FLM.AlgoFile);
            }

            if (File.Exists(FLM.AlgoFile))
            {
                if (JlinkOp.CheckCpuID() == true)
                {
                    EraseFlash(FLM.AlgoFile, FLM.AlgoLoadAddr);
                    Debug.AppendMsg("[Inf]Erase Successfully"+Environment.NewLine);
                }
                else
                    Debug.AppendMsg("[Inf]no device"+Environment.NewLine);
            }
            else
            {
                Debug.AppendMsg("[Inf]Download flash fail"+Environment.NewLine);
            }
        }

        public Thread MRAM_TestTh = null;
        public UInt32 MRAM_Addr;
        public UInt32 MRAM_Length;
        public UInt32 MRAM_TestTime;
        public bool MRAM_Debug = false;
        private void materialButton3_Click_1(object sender, EventArgs e)
        {
            UInt32 StartAddr = 0x00018000;
            UInt32 Size = 0x001E8000;
            m_UI.Save();
            if (MRAM_TestTh == null)
            {
                Debug.AppendMsg("[Inf]Reset CPU And Switch To Test Mode" + Environment.NewLine);
                switch2jlinkToolStripMenuItem_Click(sender, e);
                Thread.Sleep(50);
                MRAM_Addr = (UInt32)WilfDataPro.CalcExpression(TestAddrBox.Text);
                MRAM_Length = (UInt32)WilfDataPro.CalcExpression(TestLenBox.Text);
                MRAM_TestTime = (UInt32)WilfDataPro.CalcExpression(TestNoBox.Text);
                MRAM_Debug = DebugCB.Checked;
                if (MRAM_Addr<StartAddr || MRAM_Addr>(StartAddr+Size))
                {
                    MessageBox.Show("The Address or length is incorrectly set");
                    return;
                }
                if ((MRAM_Addr+MRAM_Length)<StartAddr || (MRAM_Addr+MRAM_Length)>(StartAddr+Size))
                {
                    MessageBox.Show("The Address or length is incorrectly set");
                    return;
                }

                MRAM_TestTh = new Thread(new ParameterizedThreadStart(MRAM_Test));
                MRAM_TestTh.Start(this);
                Debug.AppendMsg("[Inf]Test Start" + Environment.NewLine);
                MRAM_TestBtn.Text = "Stop";
            }
            else
            {
                MRAM_TestTh = null;
                Debug.AppendMsg("[Inf]Test End" + Environment.NewLine);
                MRAM_TestBtn.Text = "Start";
            }
        }

        private void MRAM_Test(object Param)
        {
            //MapParser.GetArmHash(ProjectInf.GetArmFilePath(gCfgParam.ArmProjectDir, FileType.MAP));
            UInt32 gpst_pro_addr = 0x10007908;
            bool ret;
            string Dir = Application.StartupPath + "\\MRAM\\"+WilfFile.GetTimeStr(false);
            string DumpFileName;
            WilfFile.CreateDirectory(Dir);
            byte[] DumpData;

            string MapFile = "";

            if (File.Exists(MapFile))
            {
                MapParser.GetArmHash(MapFile);
                gpst_pro_addr =  MapParser.GetAddr("gpst_pro");
            }
            else
                gpst_pro_addr = 0x10007908;

            jlink_protocol.Init(gpst_pro_addr);
            jlink_protocol.SetOverTime(100*1000);
            jlink_protocol.InitFlash();
            StringBuilder sb = new StringBuilder();

            for (UInt32 i = 0; i<MRAM_TestTime; i++)
            {
                sb.Clear();
                ret = jlink_protocol.TestFlash(MRAM_Addr, MRAM_Length, i, false);
                sb.Append(i+","+ret+",0x"+MRAM_Addr.ToString("X8")+Environment.NewLine);

                if (ret == false || MRAM_Debug == true)
                {
                    DumpFileName = Dir + "\\" + i+"_0x"+MRAM_Addr.ToString("X8")+".bin";
                    JlinkOp.ReadMemory(MRAM_Addr, MRAM_Length, out DumpData);
                    File.WriteAllBytes(DumpFileName, DumpData);
                }
                WilfFile.WriteAppend(Dir+"\\Log.txt", sb.ToString());
                if ((i%10) == 0)
                {
                    AppendMsg(sb.ToString());
                }
                if (MRAM_TestTh == null)
                    break;
            }

            Debug.AppendMsg("[Inf] Thread Exit"+Environment.NewLine);
            MessageBox.Show("Over");
        }

        private void materialExpansionPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void materialExpansionPanel1_CancelClick(object sender, EventArgs e)
        {

        }

        private void materialButton3_Click_2(object sender, EventArgs e)
        {
            SomReg.RegList = WilfOffice.Load();
            Debug.AppendMsg("[Inf]OK"+Environment.NewLine);
        }

        private void LoadRegFileBtn_Click(object sender, EventArgs e)
        {
            string TableFile = RegPathBox.Text;
            if (File.Exists(TableFile)==false)
            {
                return;
            }
            SomReg.RegBitList = SomReg.LoadBitFile(TableFile);
            Debug.AppendMsg("[Inf]OK"+Environment.NewLine);
        }

        private bool LoadSpiConfig()
        {
           SOM.xSPI_Line_Num = xSpiCombox.SelectedIndex;
           SOM.Freq = Convert.ToInt32(xSpiFreqbox.Text);
            if (DtrRadio.Checked)
               SOM.DTR = 1;
            else
               SOM.DTR = 0;

            if (DqsRadio.Checked)
               SOM.DQS = 1;
            else
               SOM.DQS = 0;

            if (AddrRadio.Checked)
               SOM.AddrWidth = 1;   //32bit
            else
               SOM.AddrWidth = 0;

            if (SOM.xSPI_Line_Num == 0)
            {
                //if (SOM.DTR == 1||SOM.DQS ==1||SOM.AddrWidth==1)
                //{
                //    MessageBox.Show("Error:x1 don't support DTR/DQS mode and 24 bit address");
                //    return false;
                //}
            }
            //else if (SOM.xSPI_Line_Num == 1)
            //{
            //    if (SOM.DQS ==1)
            //    {
            //        MessageBox.Show("Error:Apollo don't support DQS in x4 mode");
            //        return false;
            //    }
            //}
            else if (SOM.xSPI_Line_Num >= 2)
            {
                if (SOM.DTR == 0||SOM.AddrWidth==0)
                {
                    MessageBox.Show("Error:x8/x16 don't support STR mode and 24 bit address");
                    return false;
                }
            }


            return true;
        }
        private void materialButton3_Click_3(object sender, EventArgs e)
        {
            byte[] Data;
            bool ret;
            string Dir = TestResultDir;
   
            StringBuilder sb = new StringBuilder();
            Debug.AppendMsg("");
            WilfFile.CreateDirectory(Dir);
            switch2jlinkToolStripMenuItem_Click(sender, e);
            ret =SOM.InitProtocol(gCfgParam.ArmMapFile);

            if (ret == false)
                return;
            if (SomReg.RegBitList != null)
            {
                if (LoadSpiConfig() == false)
                    return;
                m_UI.Save();
                if (SOM.NeedUpdateInterface)
                {
                   SOM.InitInterface(SOM.Freq,SOM.xSPI_Line_Num,SOM.AddrWidth,SOM.DTR, SOM.DQS);
                }
               SOM.som_xspi_reg_read(0, out Data, 32);
                if (LoadDefaultCB.Checked)
                {
                   SOM.DumpRegister(SomReg.RegBitList, true, true);
                    LoadDefaultCB.Checked= false;
                }

                if (DefaultCB.Checked == true)
                {
                   SOM.Test(RegTestType.Default, Dir);
                }
                if (RW_CB.Checked == true)
                {
                   SOM.Test(RegTestType.RW, Dir, 10);
                }
                if (CrosstalkCB.Checked == true)
                {
                   SOM.Test(RegTestType.Crosstalk, Dir, 5);
                }
                if (AttrbuteCB.Checked == true)
                {
                   SOM.Test(RegTestType.Attribute, Dir);
                }
                if (ResetCB.Checked == true)
                {
                   SOM.Test(RegTestType.Reset, Dir);
                }
            }
            else
            {
                Debug.AppendMsg("Please Load Register Table At first"+Environment.NewLine);
            }
        }

        private void materialTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TempPage.SelectedIndex == 0)
            {
                Debug.ShowMsg = AppendMsg;
                Debug.ShowMsgColor = AppendMsgColor;
                Debug.ShowIndex = 0;
            }
            else if (TempPage.SelectedIndex == 1)
            {
                Debug.ShowMsg = ShowMsg;
                Debug.ShowMsgColor =ShowMsgColor;
                Debug.ShowIndex = 4;
            }
             else
            {
                if (plogForm == null || plogForm.IsDisposed)
                {
                    plogForm = new LogForm();
                }
                Debug.ShowMsg = plogForm.AppendMsg;
                Debug.ShowMsgColor =plogForm.AppendMsgColor;
                Debug.ShowIndex = 1;
            }
        }

        public void LogForm_Pro()
        {
           
            if (plogForm == null || plogForm.IsDisposed)
            {
                plogForm = new LogForm();
                plogForm.Show();
            }
            else
            {
                plogForm.Show();
                //plogForm.Focus();
            }
            Debug.ShowMsg = plogForm.AppendMsg;
            Debug.ShowMsgColor =plogForm.AppendMsgColor;
            
        }

        private void tESTToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void paToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Dir = Application.StartupPath+"\\Pack";
            string Cur;

            WilfFile.CreateDirectory(Dir);

            Cur = gCfgParam.ArmHexFile;
            if (File.Exists(Cur))
            {
                File.Copy(Cur, Dir+"\\"+Path.GetFileName(Cur), true);
            }

            Cur = gCfgParam.ArmAxfFile;
            if (File.Exists(Cur))
            {
                File.Copy(Cur, Dir+"\\"+Path.GetFileName(Cur), true);
            }

            Cur = Application.StartupPath + "\\" + "MapInf.a";
            if (File.Exists(Cur))
            {
                File.Copy(Cur, Dir+"\\"+Path.GetFileName(Cur), true);
            }
        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            byte[] Data;
            bool ret;
            string Dir = TestResultDir;
            string FileName;
            RegBitData Reg;
            UInt32 Length;
            UInt32[] Addr;
            byte[] RegValue;
            StringBuilder sb = new StringBuilder();
            if (SomReg.RegBitList == null)
            {
                string TableFile = RegPathBox.Text;
                if (File.Exists(TableFile)==false)
                {
                    Debug.AppendMsg("[Error]Please Load Register Table At first"+Environment.NewLine);
                    return;
                }
                SomReg.RegBitList = SomReg.LoadBitFile(TableFile);
            }
            if (LoadSpiConfig() == false)
                return;
            WilfFile.CreateDirectory(Dir);
            if (SwitchProtocolCB.Checked)
            {
                SwitchProtocolCB.Checked = false;
                switch2jlinkToolStripMenuItem_Click(sender, e);
            }
            ret =SOM.InitProtocol(gCfgParam.ArmMapFile);
            m_UI.Save();
            if (ret == false)
                return;
            Addr = WilfDataPro.GetData32FromStr(RegAddrBox.Text);
            Length = (UInt32)WilfDataPro.CalcExpression(RegLengthBox.Text);
            RegValue = WilfDataPro.GetDataFromStr(RegValueBox.Text);

            if (SOM.NeedUpdateInterface)
               SOM.InitInterface(SOM.Freq,SOM.xSPI_Line_Num,SOM.AddrWidth,SOM.DTR, SOM.DQS);

            if (RegCombox.Text == "DumpRegister")
            {

                Data =SOM.DumpRegister(SomReg.RegBitList, false, false);
                FileName = Dir + "\\DumpReg_" + WilfFile.GetTimeStr()+".csv";
                File.WriteAllText(FileName, SomReg.ToString(SomReg.RegBitList), Encoding.UTF8);
                Debug.AppendMsg("[Inf]DumpFile:"+FileName+Environment.NewLine);
                Debug.AppendMsg("[Inf]"+Environment.NewLine+WilfDataPro.ToString(Data, "X2", 8, "0x")+Environment.NewLine);

                Debug.AppendMsg("[Inf]dqs_en="+SomReg.GetValue(SomReg.RegBitList, "dqs_en")+Environment.NewLine);
                Debug.AppendMsg("[Inf]dtr_sel="+SomReg.GetValue(SomReg.RegBitList, "dtr_sel")+Environment.NewLine);
                Debug.AppendMsg("[Inf]addr_width="+SomReg.GetValue(SomReg.RegBitList, "addr_width")+"(1:32bit 0:24bit)"+Environment.NewLine);
                Debug.AppendMsg("[Inf]io_mode="+SomReg.GetValue(SomReg.RegBitList, "io_mode")+"(0:x1 1:x4 2:x8 3:x16)"+Environment.NewLine);
                Debug.AppendMsg("[Inf]rdat_latp="+SomReg.GetValue(SomReg.RegBitList, "rdat_latp")+"(latency_p,latency=p+m+n-2)"+Environment.NewLine);
                Debug.AppendMsg("[Inf]rdat_latm="+SomReg.GetValue(SomReg.RegBitList, "rdat_latm")+"(latency_m,latency=p+m+n-2)"+Environment.NewLine);
                Debug.AppendMsg("[Inf]rdat_latn="+SomReg.GetValue(SomReg.RegBitList, "rdat_latn")+"(latency_n,latency=p+m+n-2)"+Environment.NewLine);
            }
            else if (RegCombox.Text == "Read")
            {
                if (Addr.Length == 1)
                {
                   SOM.som_xspi_reg_read(Addr[0], out Data, Length);
                    if (Length<8)
                        Debug.AppendMsg("[R]@0x"+Addr[0].ToString("X")+":"+WilfDataPro.ToString(Data, "X2", 8, "0x")+Environment.NewLine);
                    else
                        Debug.AppendMsg("[R]@0x"+Addr[0].ToString("X")+":"+Environment.NewLine+WilfDataPro.ToString(Data, "X2", 8, "0x")+Environment.NewLine);
                }
                else
                {
                    UInt32 Value =SOM.som_xspi_reg_read_bit(Addr[0], Addr[1], Addr[2]);
                    Debug.AppendMsg("[R]@0x"+Addr[0].ToString("X")+":"+"0x"+Value.ToString("X2")+Environment.NewLine);
                }

            }
            else if (RegCombox.Text == "Write")
            {
               SOM.som_xspi_reg_write(Addr[0], RegValue, (UInt32)RegValue.Length);
                Debug.AppendMsg("[W]@0x"+Addr[0].ToString("X")+":"+WilfDataPro.ToString(RegValue, "X2", 16, "0x")+Environment.NewLine);
            }
        }

        private void openTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(TestResultDir))
                System.Diagnostics.Process.Start(TestResultDir);
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            string TableFile = WilfFile.OpenFile(".csv");
            if (File.Exists(TableFile)==false)
            {
                return;
            }
            RegPathBox.Text = TableFile;
            LoadRegFileBtn_Click(sender, e);
        }

        private void xSpiCombox_SelectedIndexChanged(object sender, EventArgs e)
        {
           SOM.NeedUpdateInterface = true;
        }
        private int CmpByte(byte b1, byte b2, int BitNum)
        {
            int Count = 0;
            for (int i = 0; i<BitNum; i++)
            {
                if ((b1&0x01) == (b2&0x01))
                    Count++;
                b1>>=1;
                b2>>=1;
            }
            return Count;
        }
        private void materialButton5_Click(object sender, EventArgs e)
        {
            string PortName;
            double MultmeterValue0, MultmeterValue1;
            double RatioWork, WordTime, IdleTime, IO_I, Cap, Ratio_IO;
            UInt32 CfgAddr, Prescaler, Length;
            byte[] PatternData = new byte[] { 0xFF, 0xF0 };
            UInt32[] gPrescaler = new UInt32[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 14, 18, 20, 25, 30, 40, 50, 60, 70, 80, 90, 100, 200 };
            string Msg;
            string FileName;
            double Freq;
            int IO_Revert_Num = 0;
            int DelayTime = 3000;
            m_UI.Save();
            if (materialTextBox1.Text != string.Empty)
            {
                DelayTime = Convert.ToInt32(materialTextBox1.Text);
            }
            if (materialTextBox3.Text != string.Empty)
            {
                if (materialTextBox3.Text.Contains(":"))
                    gPrescaler = WilfDataPro.ParseRange3(materialTextBox3.Text);
                else
                    gPrescaler = WilfDataPro.GetData32FromStr(materialTextBox3.Text);
            }
            if (materialTextBox4.Text != string.Empty)
            {
                PatternData = WilfDataPro.GetDataFromStr(materialTextBox4.Text);
            }
            IO_Revert_Num = CmpByte((byte)(PatternData[1]>>4), (byte)((~PatternData[1])&0x0F), 4);
            ReOpenJlink();
            MapParser.InitMapHashTable(gCfgParam.ArmMapFile);
            CfgAddr = MapParser.GetAddr("ConfigData");
            Length = (UInt32)WilfDataPro.CalcExpression(materialTextBox2.Text);
            FileName = "TestPower_IO_"+IO_Revert_Num+"_Len_"+Length+"_PatternData_0x"+PatternData[0].ToString("X2")+"_0x"+PatternData[1].ToString("X2")+"_"+WilfFile.GetTimeStr()+".csv";
            WilfFile.WriteAppend(FileName, ("Prescaler,"+"Freq,"+"IdleTime(us),"+"WorkTime(us),"+"RatioWork,"+"Ratio_IO,"+"MultmeterValue0(mA),"+"MultmeterValue1(mA),"+"No Flip I(mA),"+"Flip I(mA),"+"Sigle IO(mA),"+"CL(pF),"+"Length"+Environment.NewLine));
            PortName = V86E.FindDevice();
            if (PortName == "" || CfgAddr == 0)
            {
                AppendMsg("[Error]PortName="+PortName+",CfgAddr=0x"+CfgAddr.ToString("X8")+Environment.NewLine);
                return;
            }

            for (int i = 0; i<gPrescaler.Length; i++)
            {
                Application.DoEvents();
                Prescaler = gPrescaler[gPrescaler.Length-1-i];
                Freq = 200.0/Prescaler +1;
                IdleTime = 6.25;
                WordTime = (8+8+4+Length*2.0)/Freq;
                Ratio_IO = (Length*2.0)/(8+8+4+Length*2.0);
                RatioWork =WordTime/(WordTime+IdleTime);
                SetParam(CfgAddr, Prescaler, PatternData[0], Length);
                Thread.Sleep(DelayTime);
                MultmeterValue0 = V86E.ReadMultmeter(PortName);

                SetParam(CfgAddr, Prescaler, PatternData[1], Length);
                Thread.Sleep(DelayTime);
                MultmeterValue1 = V86E.ReadMultmeter(PortName);

                IO_I = (MultmeterValue1-MultmeterValue0)/RatioWork/IO_Revert_Num/Ratio_IO;
                Cap = 1000*IO_I/(1.8*Freq*0.5);
                Msg = Prescaler+",";
                Msg += Freq.ToString("F2")+",";
                Msg += IdleTime.ToString("F2") + ",";
                Msg += WordTime.ToString("F2")+",";
                Msg += RatioWork.ToString("F2")+",";
                Msg += Ratio_IO.ToString("F2")+",";
                Msg += MultmeterValue0.ToString("F4")+",";
                Msg += MultmeterValue1.ToString("F4")+",";
                Msg += (MultmeterValue0/RatioWork).ToString("F4")+",";
                Msg += (MultmeterValue1/RatioWork).ToString("F4")+",";
                Msg += (IO_I).ToString("F4")+",";
                Msg += (Cap).ToString("F4")+",";
                Msg += Length.ToString()+Environment.NewLine;

                Debug.AppendMsg(Msg);
                WilfFile.WriteAppend(FileName, Msg.ToString());
            }
            Debug.AppendMsg("[Inf]OutPutFile:"+FileName+Environment.NewLine);
            MessageBox.Show("Over");
        }

        private void SetParam(UInt32 Addr, UInt32 Prescaler, UInt32 Data, UInt32 Length)
        {
            UInt32[] data = new UInt32[3];
            data[0] = Prescaler;
            data[1] = Data;
            data[2] = Length;
            JlinkOp.WriteRegister(Addr+4, data);
            JlinkOp.WriteRegister(Addr, 0x88888888);
            JlinkOp.Wait(Addr, 0x88888888, 1000, false);
            Debug.AppendMsg("Addr=0x"+ Addr.ToString("X8")+",Prescaler="+ Prescaler+",Data=0x"+ Data.ToString("X")+",Length="+ Length+Environment.NewLine);
        }

        private void reFlushToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void testToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void refreshMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(gCfgParam.ArmMapFile))
            {
                MapParser.GetArmHash(gCfgParam.ArmMapFile);
                Debug.AppendMsg("[Inf]Refresh Map Table"+Environment.NewLine);
            }
        }

        private void clearProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] files = Keil.ClearProject(gCfgParam.ArmProjectFile);
            for (int i = 0; i<files.Length; i++)
            {
                Debug.AppendMsg("[Inf]Delete:"+files[i]+Environment.NewLine);
            }
        }

        private void apolloBootLoaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string boot_code_file = "bootloader.hex";
            if (File.Exists(boot_code_file) == false)
            {
                GetResFile(boot_code_file);
            }
            if (File.Exists(boot_code_file) == true)
            {
                DownloadApollo(boot_code_file);
            }
            else
            {
                Debug.AppendMsg("[Error] Don't find bootloader.hex file", true);
            }
        }
        public Thread m_Thread = null;
        private void RunBtn_Click(object sender, EventArgs e)
        {
            string script_file = CsScriptDir + "\\" + ScriptCBox.Text;
            if (MapParser.ArmNameHash == null)
            {
                MapParser.InitMapHashTable(gCfgParam.ArmMapFile, true);
            }
            ShowMsg("");
            //if (m_Thread == null)
            {
                Interpreter.Enable(true);
                m_Thread = new Thread(new ParameterizedThreadStart(Thread_Function)); 
                m_Thread.SetApartmentState(ApartmentState.STA); 
                m_Thread.Start(script_file);
                //RunBtn.Text = "Stop";
            }
            //else
            //{
            //    Interpreter.Enable(false);
            //    Thread.Sleep(100);
            //    m_Thread.Abort();
            //    m_Thread = null;
            //    RunBtn.Text = "Start";
            //}

        }

        private void Thread_Function(object obj)
        {
            var App = new CscsApp();
            string script_file = (string)obj;
            App.pShow = ShowMsg;
            if (File.Exists(script_file))
            {
                if(MapParser.ArmFunctionList == null)
                {
                    MapParser.InitMapHashTable(gCfgParam.ArmMapFile, true);
                }
                App.Run(script_file,MapParser.ArmFunctionList);
            }
        }

        private void openSciptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(CsScriptDir))
                System.Diagnostics.Process.Start(CsScriptDir);
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            string script_file = CsScriptDir + "\\" + ScriptCBox.Text;
            OpenFile(script_file);
        }

        private void OpenFile(string script_file)
        {
            string path0 = @"C:\Users\E0203\AppData\Local\Programs\Microsoft VS Code\Code.exe";
            string path1 = @"D:\Users\\E0203\AppData\Local\Programs\Microsoft VS Code\Code.exe";
            string path2 = @"C:\Program Files\Notepad++\notepad++.exe";
            string path3 = @"D:\Program Files\Notepad++\notepad++.exe";

            script_file = Exe.PathAddQuotes(script_file);
            if (File.Exists(path0))
                Exe.Run(path0, script_file,false);
            else if (File.Exists(path1))
                Exe.Run(path1, script_file,false);
            else if (File.Exists(path2))
                Exe.Run(path2, script_file, false);
            else if (File.Exists(path3))
                Exe.Run(path3, script_file, false);
            else
                System.Diagnostics.Process.Start("notepad", script_file);
        }

        private void NewBtn_Click(object sender, EventArgs e)
        {
            string default_value = "include(\"SOM_Test_Common.c\");\r\nxSpiCfg = {12,0,0,0,0};  // Freq/Interface/AddressWidth/int DQS_Enable/int DTR_Enable\r\nOpenJlink();\r\nret = InitJlinkProctol();\r\nret = InitInterface(xSpiCfg);\r\nprint(\"****************************************************************\");\r\n//Code for you\r\n\r\nprint(\"----------------------------------------------------------------\");";
            string script_file = CsScriptDir + "\\" + WilfFile.GetTimeStr()+".c";
            File.WriteAllText(script_file, default_value);
            LoadScript(script_file);
            OpenFile(script_file);
        }

        private void DelButton7_Click(object sender, EventArgs e)
        {
            string script_file = CsScriptDir + "\\" + ScriptCBox.Text;
            if(File.Exists(script_file))
            {
                if (WilfFile.OpenYesNo("确认删除文件:"+Path.GetFileName(script_file)+"?"))
                {
                    File.Delete(script_file);
                    LoadScript();
                }
            }
        }

        private void OpenBtn_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(CsScriptDir))
            {
                System.Diagnostics.Process.Start(CsScriptDir);
                LoadScript("");
            }
        }

        private void dumpFunctonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapParser.InitMapHashTable(gCfgParam.ArmMapFile,true);
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void materialButton7_Click(object sender, EventArgs e)
        {

            if (m_Thread == null)
            {
                CheckForIllegalCrossThreadCalls = false;
                AppendMsg("线程启动"+Environment.NewLine);
                StartBtn.Text = "Stop";
                Interpreter.Enable(true);
                m_Thread = new Thread(new ParameterizedThreadStart(TestX));
                m_Thread.SetApartmentState(ApartmentState.STA);
                m_Thread.Start(0);
            }
            else
            {
                StartBtn.Text = "Start";
                m_Thread.Abort();
                m_Thread = null;
                AppendMsg("线程退出"+Environment.NewLine);
            }
            
        }

        private void TestX(object Option)
        {
            bool SumRegularEnable, SumEnhanceEnable, CrcEnable;
            UInt32[] BitNum = WilfDataPro.GetData32FromStr(BitNumBox.Text);
            Int64 TestTime = Convert.ToInt64(TestTimeBox.Text);
            int RepeatTime = Convert.ToInt32(RepeatBox.Text);
            int[] ErrorInf;
            StringBuilder sb = new StringBuilder();
            string FileName = "SumVsCRC_Result.csv";
            SumRegularEnable = RegularCB.Checked;
            SumEnhanceEnable = EnhanceCB.Checked;
            CrcEnable = CrcCB.Checked;
            sb.Append("Number of tests"+","+"Random errors (bits)"+","+"regular CheckSum"+","+ "multiply-add CheckSum"+","+ "CRC"+Environment.NewLine);
            for (int k = 0; k<RepeatTime; k++)
            {
                for (int i = 0; i<BitNum.Length; i++)
                {
                    ErrorInf = TestCheckSum(TestTime, (int)BitNum[i], SumRegularEnable, SumEnhanceEnable, CrcEnable);
                    sb.Append(TestTime+","+BitNum[i]+","+WilfDataPro.ToString(ErrorInf)+Environment.NewLine);
                    Application.DoEvents();
                }
            }
            AppendMsg("------------------------------------------"+Environment.NewLine);
            AppendMsg(sb.ToString());
            File.WriteAllText(FileName, sb.ToString());
            MessageBox.Show("Over");
        }
    }
}
