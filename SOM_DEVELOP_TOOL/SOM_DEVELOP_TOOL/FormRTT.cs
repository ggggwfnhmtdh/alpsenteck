using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Collections;

namespace SOM_DEVELOP_TOOL
{
    public partial class FormRTT : MaterialForm
    {
        private static string LogDir = Application.StartupPath + "\\Log";
        private static int    LogFileCount = 0;
        private delegate void ThreadWorkStr(string Msg,int Ch);
        private delegate void ThreadWorkTable(int Index);
        private delegate void ThreadWorkStrColor(string Msg, Color color);
        private delegate void ThreadWorkStr2(string Msg0, string Msg1);
        public  bool ForceSaveLog = false;
        public int TimeCount = 0;
        public List<double> Timetamp = new List<double>(); 
        public FormRTT()
        {
            InitializeComponent();
        }

        public void AppendMsgRTT(string Msg,int Ch)
        {
            RichTextBox CurBox;
            if (Ch == 0)
                CurBox = MsgBox0;
            else
                CurBox = MsgBox1;
            try
            {
                if (CurBox.InvokeRequired)
                {

                    ThreadWorkStr fc = new ThreadWorkStr(AppendMsgRTT);
                    this.Invoke(fc, new object[2] { Msg, Ch });
                }
                else
                {
                    if (Msg == "")
                        CurBox.Clear();
                    else
                        CurBox.AppendText(Msg);

                    //if (gRttRefresh == true)
                    {
                        CurBox.Refresh();
                        CurBox.SelectionStart = MsgBox0.Text.Length;
                        CurBox.ScrollToCaret();
                    }
                    if (CurBox.Text.Length >= 200000)
                    {
                        CurBox.Clear();
                    }
                }
            }
            catch
            {
                return;
            }
        }

        public void AppendMsgRTTColor(string Msg,Color color)
        {

            if (this.MsgBox0.InvokeRequired)
            {
                ThreadWorkStrColor fc = new ThreadWorkStrColor(AppendMsgRTTColor);
                this.Invoke(fc, new object[2] { Msg, color });
            }
            else
            {
                if (Msg == "")
                    this.MsgBox0.Clear();
                else
                {
                    this.MsgBox0.Select(this.MsgBox0.Text.Length, 0);
                    //this.MsgBox.Focus();
                    MsgBox0.SelectionColor = color;
                    MsgBox0.AppendText(Msg);
                }

                this.MsgBox0.Refresh();
                this.MsgBox0.SelectionStart = MsgBox0.Text.Length;
                this.MsgBox0.ScrollToCaret();
            }
        }

        public void LogEnable(bool flag)
        {
            for(int i=0;i<MainForm.m_Log.Length;i++)
            {
                if(MainForm.m_Log[i].Enable)
                {
                    MainForm.m_Log[i].LogEnable(flag);
                }
            }
        }
        private void FormRTT_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLogToFile();
            Debug.AppendMsg("[Inf]Save Log To File Then Close Log Windows" + Environment.NewLine);
            if (JlinkOp.Error == false)
                LogEnable(false);
            JlinkOp.StopRttThtead();
            Thread.Sleep(100);
            Debug.AppendMsg("[Inf]Log Is Already Closed" + Environment.NewLine);
            Debug.ShowRTTMsg = null;
           
        }
        public void SaveLogToFile(bool IsReq = false)
        {
            string new_file_name;
            for (int i = 0; i < MainForm.m_Log.Length; i++)
            {
                if (MainForm.m_Log[i].Enable == false)
                    continue;
                if (IsReq == false)
                {
                    if (MainForm.m_Log[i].LogMsg.Length > 0)
                    {
                        WilfFile.WriteAppend(MainForm.m_Log[i].filename, MainForm.m_Log[i].LogMsg.ToString());
                        Server.LogSize += (ulong)MainForm.m_Log[i].LogMsg.Length;
                        MainForm.m_Log[i].LogMsg.Clear();
                        Debug.AppendMsg("[Inf]Save Left Log TO File" + Environment.NewLine, Color.Green);
                    }
                }
                else if (MainForm.m_Log[i].LogMsg.Length >= 200000)
                {
                    if (WilfFile.GetFileSize(MainForm.m_Log[i].filename) > 10 * 1024 * 1024)
                    {
                        new_file_name = "_extend_" + LogFileCount.ToString();
                        if (MainForm.m_Log[i].filename.Contains("_extend_"))
                        {
                            MainForm.m_Log[i].filename = MainForm.m_Log[i].filename.Substring(0, MainForm.m_Log[i].filename.IndexOf("_extend_")) + new_file_name + ".txt";
                        }
                        else
                            MainForm.m_Log[i].filename = WilfFile.FileNameInjectStr(MainForm.m_Log[i].filename, new_file_name);
                        LogFileCount++;
                    }
                    WilfFile.WriteAppend(MainForm.m_Log[i].filename, MainForm.m_Log[i].LogMsg.ToString());
                    Server.LogSize += (ulong)MainForm.m_Log[i].LogMsg.Length;
                    MainForm.m_Log[i].LogMsg.Clear();
                }
            }

        }

        private void GetLogName()
        {
            for (int i = 0; i < MainForm.m_Log.Length; i++)
            {
                MainForm.m_Log[i].filename =  LogDir + "\\" + easy_log.HostUserName + "_" + MainForm.m_Log[i].ChipName+"_"+ WilfFile.GetTimeStr(false) + ".txt";
            }
        }

        private void ShowControl()
        {
            if (MainForm.m_Log[0].Enable && MainForm.m_Log[1].Enable)
            {
                splitContainer1.SplitterDistance = splitContainer1.Width / 2;
            }
            else if (MainForm.m_Log[0].Enable)
            {
                splitContainer1.SplitterDistance = splitContainer1.Width;
            }
            else
            {
                splitContainer1.SplitterDistance = 0;
            }
        }
        private void FormRTT_Load(object sender, EventArgs e)
        {
            ShowControl();
            if (!Directory.Exists(LogDir))
            {
                Directory.CreateDirectory(LogDir);
            }
            GetLogName();
            //Debug.ShowRTTMsg = AppendMsgRTT;
            OpenLog();
        }

        private void OpenLog()
        {
            timer1.Enabled = true;
            LogFileCount = 0;
            Server.LogOperationTime++;
            if (JlinkOp.RTT_Thread == null)
            {
                JlinkOp.RTT_Thread = new Thread(new ParameterizedThreadStart(RTT_Thread_Function));
                JlinkOp.RTT_Thread.Start(this);
                JlinkOp.RTT_Enable = true;
            }
            Debug.AppendMsg("[Inf]Log Is Already Running" + Environment.NewLine);
        }

        private void CloseLog()
        {
            if (JlinkOp.RTT_Thread != null)
            {
                JlinkOp.StopRttThtead();
            }
            Thread.Sleep(100);
            Debug.AppendMsg("[Inf]Log Is Already Close" + Environment.NewLine);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(LogDir);
        }

        private void logToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void logToFileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < MainForm.m_Log.Length; i++)
            {
                if (MainForm.m_Log[i].Enable)
                {
                    if (File.Exists(MainForm.m_Log[i].filename))
                    {
                        WilfFile.WriteAppend(MainForm.m_Log[i].filename, MsgBox0.Text);
                    }
                }
            }
        }

        public void LogInitData()
        {
            for (int i = 0; i < MainForm.m_Log.Length; i++)
            {
                if (MainForm.m_Log[i].Enable)
                {
                    if (MainForm.m_Log[i].IsEasyLogDevice())
                    {
                        MainForm.m_Log[i].InitData();
                    }
                }
            }
        }

        void ClearLogMsg(int LogID)
        {
            if (LogID <= 1)
                MainForm.m_Log[LogID].LogMsg.Clear();
            else
            {
                for (int i = 0; i < MainForm.m_Log.Length; i++)
                {
                    MainForm.m_Log[i].LogMsg.Clear();
                }
            }
        }
        bool IsEasyLogDevice()
        {
            bool ret = true;
            for (int i = 0; i < MainForm.m_Log.Length; i++)
            {
                if (MainForm.m_Log[i].Enable)
                {
                    if(MainForm.m_Log[i].IsEasyLogDevice() == false)
                    {
                        ret = false;
                        Debug.AppendMsg("[Error]" + MainForm.m_Log[i] + " is not easy log device" + Environment.NewLine);
                    }
                }
            }
            return ret;
        }
        void RTT_Thread_Function(object InPam)
        {
            string[] Msg = new string[MainForm.m_Log.Length];
            int i;
            LogInitData();
            JlinkOp.RTT_StartFlag = true;
            ClearLogMsg(int.MaxValue);
            Array.Clear(Msg,0, Msg.Length);

            while (JlinkOp.RTT_Thread != null)
            {
                if (JlinkOp.RTT_Enable == true)
                {
                    if (JlinkOp.Error == false)
                    {
                        for (i = 0; i < MainForm.m_Log.Length; i++)
                        {
                            if (MainForm.m_Log[i].Enable == true)
                            {
                                Msg[i] = MainForm.m_Log[i].DataPro();
                            }
                        }
                    }

                    if (JlinkOp.Error == true)
                    {
                        SaveLogToFile(false);
                        if (JlinkOp.ErrorPro() == false)
                        {
                            Debug.AppendMsg("[Inf]Jlink goto error status!" + Environment.NewLine,Color.Red);
                            Thread.Sleep(easy_log.ConnectDelayTime);                    
                        }
                        else
                        {
                            Debug.AppendMsg("[Inf]Try To Check Log Online?" + Environment.NewLine, Color.Red);
                            if (IsEasyLogDevice() == true)
                            {
                                Debug.AppendMsg("[Inf]Log is Online" + Environment.NewLine, Color.Green);
                                LogInitData();
                            }
                            else
                            {
                                Debug.AppendMsg("[Inf]Log is Offline" + Environment.NewLine, Color.Red);
                                Thread.Sleep(easy_log.ConnectDelayTime);
                            }
                        }
                    }
                    for (i = 0; i < MainForm.m_Log.Length; i++)
                    {
                        if (MainForm.m_Log[i].Enable == false)
                            continue;
                        if (Msg[i] != null && Msg[i] != "")
                        {
                            MainForm.m_Log[i].LogMsg.Append(Msg[i]);
                            SaveLogToFile(true);
                            if (easy_log.ShowLogEnable == true)
                                AppendMsgRTT(Msg[i],i);

                        }
                    }
                }
                else
                {
                    Thread.Sleep(100);
                }
            }

            JlinkOp.RTT_StartFlag = false;
            LogEnable(false);
            Debug.AppendMsg("[Inf]Log Exist" + Environment.NewLine);

        }

        private void openLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveLogToFile();
                GetLogName();
                OpenLog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void closeLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CloseLog();
                SaveLogToFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void checkLogStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id_range_str="";
            for (int i = 0; i < MainForm.m_Log.Length; i++)
            {
                if (MainForm.m_Log[i].Enable)
                {
                    string Msg = MainForm.m_Log[i].GetLogInf();
                    Debug.AppendMsg(Msg + Environment.NewLine);
                }
            }

            for (int m = 0; m < easy_log.IdRange.Length; m++)
            {
                id_range_str += easy_log.IdRange[m].ToString("X8") + Environment.NewLine;
            }
            Debug.AppendMsg("[Inf]Id Range:" + Environment.NewLine + id_range_str);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void mergeLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void openLogToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveLogToFile();
            for (int i = 0; i < MainForm.m_Log.Length; i++)
            {
                if (MainForm.m_Log[i].Enable)
                {
                    System.Diagnostics.Process.Start("notepad++", MainForm.m_Log[i].filename);
                }
            }
                   
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double ratio;
            string id_range_str = "";
            StringBuilder sb = new StringBuilder();
            TimeCount++;
            if (JlinkOp.RTT_Thread != null)
            {
                for (int i = 0; i < MainForm.m_Log.Length; i++)
                {
                    if (MainForm.m_Log[i].Enable == false || MainForm.m_Log[i].mLogHeader.Data == null)
                        continue;
                    if (MainForm.m_Log[i].mLogHeader.Data[easy_log.ENABLE_OFFSET] == 1)
                    {
                        sb.Append(MainForm.m_Log[i].ChipName +":" + Environment.NewLine);

                        if (MainForm.m_Log[i].mLogHeader.Data[easy_log.LOG_SEND_NUM_OFFSET] == 0)
                            ratio = 0;
                        else
                            ratio = ((MainForm.m_Log[i].mLogHeader.Data[easy_log.LOG_SEND_NUM_OFFSET] - MainForm.m_Log[i].mLogHeader.Data[easy_log.LOG_SEND_OK_NUM_OFFSET]) * 100.0) / MainForm.m_Log[i].mLogHeader.Data[easy_log.LOG_SEND_NUM_OFFSET];
                        if (MainForm.m_Log[i].mLogHeader.Data[easy_log.ID_OFFSET] == easy_log.ID_NAME)
                        {
                            sb.Append("online" + Environment.NewLine);
                        }
                        else
                        {
                            sb.Append("off line" + Environment.NewLine);
                        }

                        for(int m=0;m < easy_log.IdRange.Length;m++)
                        {
                            id_range_str += easy_log.IdRange[m].ToString("X8") + Environment.NewLine;
                        }

                        sb.Append("Log FW Version:" + MainForm.m_Log[i].mLogHeader.Data[easy_log.VERSION_OFFSET] + Environment.NewLine);
                        sb.Append("Log Lost Ratio:" + ratio.ToString("F2") + "%" + Environment.NewLine);
                        sb.Append("Log Receive Number:" + MainForm.m_Log[i].mLogHeader.Data[easy_log.LOG_SEND_OK_NUM_OFFSET] + Environment.NewLine);
                        sb.Append("Log Send Number:" + MainForm.m_Log[i].mLogHeader.Data[easy_log.LOG_SEND_NUM_OFFSET] + Environment.NewLine);
                        sb.Append("Log Eable:" + MainForm.m_Log[i].mLogHeader.Data[easy_log.ENABLE_OFFSET] + Environment.NewLine);
                        sb.Append("Log Threshold:" + MainForm.m_Log[i].mLogHeader.Data[easy_log.THRESHOLD] + Environment.NewLine);
                        sb.Append("Log Buffer Size:" + MainForm.m_Log[i].mLogHeader.Data[easy_log.UP_BUFFER_SIZE_OFFSET] + Environment.NewLine);
                        sb.Append("Log Time Flag:" + MainForm.m_Log[i].mLogHeader.Data[easy_log.TIME_FLAG_OFFSET] + Environment.NewLine);
                        sb.Append("Log Time Clock:" + MainForm.m_Log[i].mLogHeader.Data[easy_log.CPU_CLOCK_OFFSET] +"Hz"+ Environment.NewLine);
                        sb.Append("Log BlockMode Flag:" + MainForm.m_Log[i].mLogHeader.Data[easy_log.BLOCK_MODE_OFFSET] + Environment.NewLine);
                        sb.Append("Log Config Addr:0x" + MainForm.m_Log[i].BaseAddr.ToString("X8")+ Environment.NewLine);
                        sb.Append("Log IdRange Addr:0x" + MainForm.m_Log[i].mLogHeader.Data[easy_log.ID_RANGE_ADDR].ToString("X8") + Environment.NewLine);
                    }
                }
                string msg = sb.ToString();
                LogStatusBox.Text = msg;
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MainForm.m_Log.Length; i++)
            {
                if (MainForm.m_Log[i].Enable == true)
                {
                    MainForm.m_Log[i].InitLogParam();
                }
            }
        }

        private void clearToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AppendMsgRTT("", 0);
            AppendMsgRTT("", 1);
        }

        private void ShowTimetamp()
        {
            double time_ns;
            double time_p = ((UInt64)0x100000000 / easy_log.CpuTimerClock)*1000;

            StringBuilder sb = new StringBuilder();
            sb.Append("Time[Inf]" + Environment.NewLine);
            for (int i=0;i<Timetamp.Count;i++)
            {
                if(Timetamp[i] >= Timetamp[0])
                    time_ns = (Timetamp[i]- Timetamp[0]);
                else
                    time_ns = (Timetamp[i] + time_p - Timetamp[0]);
                sb.Append("T" + i +"["+ Timetamp[i] + "]"+":" + time_ns.ToString() + "ns" + Environment.NewLine);
            }
            TimeLabel.Text = sb.ToString();
        }
        private void setT0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = MsgBox0.SelectedText;
            if(str != null && WilfDataPro.IsNumeric(str))
            {
                Timetamp.Clear();
                Timetamp.Add(Convert.ToDouble(str));
                TimeLabel.Visible = true;
                ShowTimetamp();
            }
        }

        private void setTnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = MsgBox0.SelectedText;
            if (str != null && WilfDataPro.IsNumeric(str))
            {
                if (Timetamp.Count > 0)
                {
                    Timetamp.Add(Convert.ToDouble(str));
                    ShowTimetamp();
                }
                else
                {
                    MessageBox.Show("You must set T0 first" + Environment.NewLine);
                }
            }
        }

        private void checkLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file_name;
            List<int> LogID = new List<int>();
            string[] str = null;
            if(WilfFile.OpenYesNo("Open Current log file or other file?"))
            {
                file_name = MainForm.m_Log[0].filename;
            }
            else
            {
                file_name = WilfFile.OpenFile(".txt");
            }
            if (File.Exists(file_name))
            {
                str = File.ReadAllLines(file_name);
                for(int i = 0; i < str.Length; i++)
                {
                    str[i] = str[i].Trim();
                    string id_str = str[i].Substring(0,str[i].IndexOf(","));
                    LogID.Add(Convert.ToInt32(id_str));
                }

                for (int i = 1; i < LogID.Count; i++)
                {
                    if (LogID[i] != ((LogID[i - 1] + 1) % 256))
                    {
                        Debug.AppendMsg("[Error]" + i + Environment.NewLine);
                    }
                }
                Debug.AppendMsg("[Inf]LogID:" + LogID.Count + Environment.NewLine);
                Debug.AppendMsg("[Inf]FileLine:" + str.Length + Environment.NewLine);
            }
           
        }

        private void addrToFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AddrStr;
            string str = MsgBox0.SelectedText;
            UInt32 Addr;
            try
            {
                if (WilfDataPro.IsNumeric(str))
                {
                    if (str.Contains("0x9"))
                    {
                        str = str.Replace("0x9", "0x5");
                        Debug.AppendMsg("[Inf]Fix Header Errors" + Environment.NewLine);
                    }
                    Addr = Convert.ToUInt32(str, 16);
                    AddrStr = MainForm.m_Log[0].FindIdTable(Addr);
                    Debug.AppendMsg("[Inf]Result:" + str + "->" + AddrStr + Environment.NewLine);
                }
            }
            catch
            {
                MessageBox.Show("Error Operation");
            }
        }

        private string GetCallAdress(string str)
        {
            string AddrStr;
            UInt32 Addr;
            try
            {
                if (WilfDataPro.IsNumeric(str))
                {
                    if (str.Contains("0x9"))
                    {
                        str = str.Replace("0x9", "0x5");
                    }
                    Addr = Convert.ToUInt32(str, 16);
                    AddrStr = MainForm.m_Log[0].FindIdTable(Addr);
                    return AddrStr;
                }
                return "";
            }
            catch
            {
               return "";
            }
        }

        private void parseARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file_name;
            List<int> LogID = new List<int>();
            string[] str = null;
            bool edit_flag = false;
            if (WilfFile.OpenYesNo("Open Current log file or other file?"))
            {
                file_name = MainForm.m_Log[0].filename;
            }
            else
            {
                file_name = WilfFile.OpenFile(".txt");
            }
            if (File.Exists(file_name))
            {
                str = File.ReadAllLines(file_name);
                string[] sb;
                for (int i = 0; i < str.Length; i++)
                {
                    sb = str[i].Split(',');
                    if (sb.Length>=5)
                    {
                        edit_flag = true;
                        if (sb[3].Contains("AR[0]") || sb[3] == "AR")
                        {
                            str[i] += "," + GetCallAdress(sb[4].Trim()); 
                        }
                    }
                }
                if (edit_flag == true)
                {
                    File.WriteAllLines(file_name, str);
                    MessageBox.Show("OK");
                }
            }
            else
            {
                MessageBox.Show("Don't find Log file");
            }
        }

        private void stabilityTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeLogToolStripMenuItem_Click(sender, e);
            for (int i = 0; i < MainForm.m_Log.Length; i++)
            {
                if (MainForm.m_Log[i].Enable == false)
                    continue;
                MainForm.m_Log[i].LogTest();
            }
        }

        private void statisticalLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file_name;
            List<int> LogID = new List<int>();
            string[] str = null;
            Hashtable ht = new Hashtable();
            string[] Keys;
            int[] Value;
            if (WilfFile.OpenYesNo("Open Current log file or other file?"))
            {
                file_name = MainForm.m_Log[0].filename;
            }
            else
            {
                file_name = WilfFile.OpenFile(".txt");
            }
            if (File.Exists(file_name))
            {
                str = File.ReadAllLines(file_name);
                for (int i = 0; i < str.Length; i++)
                {
                    str[i] = str[i].Trim();
                    string[] Item = str[i].Split(',');
                    if(ht.ContainsKey(Item[3]))
                    {
                        int tp = (int)ht[Item[3]];
                        ht[Item[3]] = tp + 1;
                    }
                    else
                    {
                        ht.Add(Item[3], 1);
                    }
                   
                }
                WilfDataPro.SortHashTable(ht, out Keys, out Value);
                
                Debug.AppendMsg("");
                for(int i=0;i<Keys.Length; i++) 
                {
                    Debug.AppendMsg(Keys[i] + "," + Value[i]+Environment.NewLine); 
                }

            }
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogStatusBox.Text = "";
            LogStatusBox.Text += 1 + "\n";
            LogStatusBox.Text += 2 + "\n";
            LogStatusBox.Text += 3 + "\n";
        }
    }
}
