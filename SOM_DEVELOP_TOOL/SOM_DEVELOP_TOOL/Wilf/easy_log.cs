using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using System.IO;
using static SOM_DEVELOP_TOOL.MapParser;

namespace SOM_DEVELOP_TOOL
{

    public class log
    {
        public static string HostUserName = System.Environment.GetEnvironmentVariable("UserName").ToString();
        public static UInt32 ID_NAME = 0xAA55;

        public static UInt32 ID_OFFSET = (0);
        public static UInt32 VERSION_OFFSET = (1);
        public static UInt32 RDOFF_OFFSET = (2);
        public static UInt32 RDOFF_REV_OFFSET = (3);
        public static UInt32 WROFF_OFFSET = (4);
        public static UInt32 WROFF_REV_OFFSET = (5);
        public static UInt32 ENABLE_OFFSET = (6);
        public static UInt32 THRESHOLD = (7);
        public static UInt32 ID_RANGE_ADDR = (8);
        public static UInt32 UP_BUFFER_ADDR_OFFSET = (9);
        public static UInt32 UP_BUFFER_SIZE_OFFSET = (10);
        public static UInt32 LOG_SEND_NUM_OFFSET = (11);
        public static UInt32 LOG_SEND_OK_NUM_OFFSET = (12);
        public static UInt32 TIME_FLAG_OFFSET = (13);
        public static UInt32 CPU_CLOCK_OFFSET = (14);
        public static UInt32 BLOCK_MODE_OFFSET = (15);

        public static UInt32 CMD_CV = 0;
        public static UInt32 CMD_GV = 1;
        public static UInt32 CMD_FV = 2;
        public static UInt32 CMD_LV = 3;
        public static UInt32 CMD_AV = 4;
        public static UInt32 CMD_AVX = 5;
        public static UInt32 CMD_SV = 15;
    }
    public class easy_log : log
    {
        public struct DesType
        {
            public string Name;
            public int Type;
            public bool IsID;
            public Hashtable Data;
        }
        public struct LogHeader
        {
            public UInt32[] Data;
        }
        public int SaveLogID = -1;
        public LogHeader mLogHeader;
        public static string HelpFIle = "Help.pdf";
        public UInt32[] LeftData;
        public List<UInt32> list;
        public static bool PcTimeEnable = true;
        public static bool CpuTimeEnable = true;
        public static bool ShowLogEnable = true;
        public static bool WaitEnable = false;
        public static bool BlockModeEnable = true;
        public static UInt32 DelayTime = 0;
        public static double CpuTimerClock = 20;
        public static UInt32[] IdRange = new UInt32[8] {0xFFFFFFFF,0xFFFFFFFF,0xFFFFFFFF,0xFFFFFFFF,0xFFFFFFFF,0xFFFFFFFF,0xFFFFFFFF,0xFFFFFFFF};
        public bool Enable = false;
        public string ChipName = "NONE";
        public UInt32 BaseAddr = 0;
        public UInt32 RangeAddr = 0;
        public StringBuilder LogMsg = new StringBuilder();
        public string filename = "";
        public Hashtable ID_Table = new Hashtable();
        public Hashtable Des_Table = new Hashtable();
        public string IdTableFile;
        public string DesTableFile;
        public static int ConnectDelayTime = 2000;
        public  UInt32 GetUINT32(byte[] data,UInt32 startindex)
        {
            return (UInt32)((data[startindex + 0] << 0) + (data[startindex + 1] << 8) + (data[startindex + 2] << 16) + (data[startindex + 3] << 24));
        }
        //
        public void InitDesTableFile(string FileName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("dsp_Arm_common_ptr->dsp_to_Arm.sml1_configs.pcm.debug.phases,32,0=pcm_init_rf_avoid,1=pcm_gt,2=pcm_cmd,3=pcm_fdt_lsn,4=pcm_lsn_rsp,5=pcm_fdt_poll,6=pcm_rf_off");
            sb.Append(Environment.NewLine);
            File.WriteAllText(FileName, sb.ToString());

            
        }
        public void AddIdTable(Hashtable InTable,bool IsID)
        {
            if (InTable == null || InTable.Count == 0)
                return;
            ArmMapInf mapInf;
            foreach (var key in InTable.Keys)
            {
               
                if (IsID == true)
                {
                    ID_Table.Add("ID"+key.ToString(), InTable[key]);
                }
                else
                {
                    mapInf = (ArmMapInf)InTable[key];
                    ID_Table.Add("0x"+((UInt32)key).ToString("X8"), mapInf.Name);
                }
            }
        }

        public UInt32 FindIdTable(string Str)
        {
            foreach (var key in ID_Table.Keys)
            {
                if (ID_Table[key].ToString() == Str)
                {
                    string s = key.ToString();
                    if(s.Contains("ID"))
                    {
                        return Convert.ToUInt32(s.Substring(2));
                    }
                    else if (s.Contains("0x"))
                    {
                        return Convert.ToUInt32(s.Substring(2),16);
                    }
                    else
                        return Convert.ToUInt32(s.ToString());
                }
            }
            return 0;
        }

        public string FindIdTable(UInt32 Addr)
        {
            int i;
            Hashtable NewTb = new Hashtable();
            foreach (var key in ID_Table.Keys)
            {
                string s = key.ToString();
                if (s.Contains("0x"))
                {
                    NewTb.Add(Convert.ToUInt32(s,16), ID_Table[key]); 
                }
                
            }
            ArrayList akeys = new ArrayList(NewTb.Keys);
            akeys.Sort();

            for(i=0;i<akeys.Count-1;i++)
            {
                if(Addr>=(UInt32)akeys[i] && Addr<(UInt32)akeys[i+1])
                {
                    return (string)NewTb[(UInt32)akeys[i]];
                }
            }
            if(Addr >= (UInt32)akeys[i])
                return (string)NewTb["0x" + Addr.ToString("X8")];
            else
                return "";
        }

        public void LoadIdTable()
        {
            bool use_default_id_table = WilfFile.OpenYesNo("Whether To Update ID_TABLE File? This File Should Be Consistent With The FW And Update Only At First Time");
            if (use_default_id_table == true)
            {
                string filename = WilfFile.OpenFile(".data");
                if (filename != null && File.Exists(filename))
                {
                    if (filename != Path.GetFullPath(IdTableFile))
                    {
                        File.Copy(filename, IdTableFile, true);
                        Debug.AppendMsg("UpDateFile:" + IdTableFile + Environment.NewLine);
                    }
                    string desfile = Path.GetDirectoryName(filename) + "\\" + Path.GetFileName(DesTableFile);
                    if (File.Exists(desfile))
                    {
                        if (desfile != Path.GetFullPath(DesTableFile))
                        {
                            File.Copy(desfile, DesTableFile, true);
                            Debug.AppendMsg("UpDateFile:" + DesTableFile + Environment.NewLine);
                        }
                    }
                }
            }

            if (File.Exists(IdTableFile))
            {
                string[] lines = File.ReadAllLines(IdTableFile);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] str = lines[i].Split(',');
                    ID_Table.Add(str[0], str[1]);
                }
            }    
            else
            {
                Debug.AppendMsg("Warning: There is no ID_TABLE File" + Environment.NewLine);
            }
        }

        public void LoadDesTable()
        {
            if (File.Exists(DesTableFile) == false)
            {
                InitDesTableFile(DesTableFile);
            }

            if (File.Exists(DesTableFile))
            {
                string[] lines = File.ReadAllLines(DesTableFile);
                try
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        lines[i] = lines[i].Replace(" ", "");
                        string[] str = lines[i].Split(',');
                        DesType NewDes = new DesType();
                        NewDes.Name = str[0];
                        NewDes.Type = Convert.ToInt32(str[1]);
                        NewDes.Data = new Hashtable();
                        for (int j = 2; j < str.Length; j++)
                        {
                            string[] sb = str[j].Split('=');
                            NewDes.Data.Add(Convert.ToUInt32(sb[0]), sb[1]);
                        }
                        if(Des_Table.ContainsKey(NewDes.Name) == false)
                            Des_Table.Add(NewDes.Name, NewDes);
                    }
                }
                catch
                {
                    Debug.AppendMsg("The  format of DesTableFile is incorrect and cannot be parsed" + Environment.NewLine);
                }
            }
        }

        public string GetDesTableResult(string VarName,string IdName,UInt32 Value)
        {
            bool flag = false;
            DesType CurDes = new DesType();
            StringBuilder sb = new StringBuilder(); 
            if (Des_Table.ContainsKey(VarName))
            {
                CurDes = (DesType)Des_Table[VarName];
                flag = true;
            }
            if (Des_Table.ContainsKey(IdName))
            {
                CurDes = (DesType)Des_Table[IdName];
                flag = true;
            }
            if(flag == true)
            {
                if (CurDes.Type == 32)
                {
                    if(CurDes.Data.Contains(Value))
                        sb.Append(CurDes.Data[Value]);
                }
                else if(CurDes.Type ==1)
                {
                    foreach(var key in CurDes.Data.Keys)
                    {
                        int bit = Convert.ToInt32(key.ToString());
                        if(((Value>>bit)&0x01) == 1)
                        {
                            sb.Append(CurDes.Data[key]+"|");
                        }
                    }
                }
                else
                {
                    return "";
                }
                return sb.ToString().Trim('|');
            }
            else
            {
                return "";
            }
        }

        public void ExportIdTable()
        {
             StringBuilder sb = new StringBuilder();
            
            foreach(var key in ID_Table.Keys)
            {
                sb.Append(key.ToString()+","+ ID_Table[key] + Environment.NewLine);
            }
            File.WriteAllText(IdTableFile, sb.ToString());
            Debug.AppendMsg("Generate ID_TABLE File:" + ChipName + Environment.NewLine);
        }

        public  void InitLogParam()
        {
            JlinkOp.WriteRegister(BaseAddr + LOG_SEND_NUM_OFFSET * 4, 0);
            JlinkOp.WriteRegister(BaseAddr + LOG_SEND_OK_NUM_OFFSET * 4, 0);
            //JlinkOp.WriteRegister(BaseAddr + WAIT_OFFSET * 4, (UInt32)((WaitEnable == true) ? 1 : 0));
            //JlinkOp.WriteRegister(BaseAddr + DELAY_TIME_OFFSET * 4, DelayTime);

        }
        public  bool LogEnable(bool IsEnable)
        {
            if (IsEnable == true)
            {
                JlinkOp.WriteRegister(BaseAddr + RDOFF_OFFSET*4, 0);
                JlinkOp.WriteRegister(BaseAddr + WROFF_OFFSET*4, 0);
                JlinkOp.WriteRegister(BaseAddr + RDOFF_REV_OFFSET * 4, 0xFFFFFFFF);
                JlinkOp.WriteRegister(BaseAddr + LOG_SEND_NUM_OFFSET * 4, 0);
                JlinkOp.WriteRegister(BaseAddr + LOG_SEND_OK_NUM_OFFSET * 4, 0);
                //JlinkOp.WriteRegister(BaseAddr + WAIT_OFFSET * 4, (UInt32)((WaitEnable == true)?1:0));
                //JlinkOp.WriteRegister(BaseAddr + DELAY_TIME_OFFSET * 4, DelayTime);
                JlinkOp.WriteRegister(BaseAddr + TIME_FLAG_OFFSET * 4, (UInt32)((CpuTimeEnable == true) ? 1 : 0));
                JlinkOp.WriteRegister(BaseAddr + ENABLE_OFFSET * 4, 1);
                JlinkOp.WriteRegister(BaseAddr + BLOCK_MODE_OFFSET * 4, (UInt32)((BlockModeEnable == true) ? 1 : 0));
                if (RangeAddr > 0)
                {
                    for (int i = 0; i < IdRange.Length; i++)
                    {
                        JlinkOp.WriteRegister((UInt32)(RangeAddr + i * 4), IdRange[i]);
                    }
                }
            }
            else
            {
                JlinkOp.WriteRegister(BaseAddr + ENABLE_OFFSET*4, 0);
                JlinkOp.WriteRegister(BaseAddr + RDOFF_OFFSET*4, 0);
                JlinkOp.WriteRegister(BaseAddr + RDOFF_REV_OFFSET * 4, 0xFFFFFFFF);
                JlinkOp.WriteRegister(BaseAddr + WROFF_OFFSET*4, 0);
                JlinkOp.WriteRegister(BaseAddr + LOG_SEND_NUM_OFFSET * 4, 0);
                JlinkOp.WriteRegister(BaseAddr + LOG_SEND_OK_NUM_OFFSET * 4, 0);
                //JlinkOp.WriteRegister(BaseAddr + WAIT_OFFSET * 4, 0);
                //JlinkOp.WriteRegister(BaseAddr + DELAY_TIME_OFFSET * 4, 0);
                JlinkOp.WriteRegister(BaseAddr + TIME_FLAG_OFFSET * 4, (UInt32)((CpuTimeEnable == true) ? 1 : 0));
                JlinkOp.WriteRegister(BaseAddr + BLOCK_MODE_OFFSET * 4, 0);
            }
            return false;
        }

        public  bool IsLogOnline()
        {
            UInt32 Flag = 0;
            if (IsEasyLogDevice() == false)
                return false;
            JlinkOp.ReadRegister(BaseAddr + ENABLE_OFFSET*4, out Flag);
            return Flag > 0;
        }

        public  string GetLogInf()
        {
            string Msg = "";
            UInt32[] OutData;
            JlinkOp.ReadRegister(BaseAddr, out OutData,16);
            Msg = ChipName + Environment.NewLine;
            for (int i=0;i< OutData.Length; i++)
            {
                Msg += "0x" + OutData[i].ToString("X8") + Environment.NewLine;
            }
            return Msg;
        }

        public  void InitData()
        {
            if (list == null)
                list = new List<UInt32>();
            else
                list.Clear();
            LogEnable(true);
        }

        public  void InitLog()  //wilf
        {
            if (IsEasyLogDevice() == true)
            {
                JlinkOp.WriteRegister(BaseAddr + ID_OFFSET*4, 0);
                InitData();
            }
        }

        public  void Synchronize(List<UInt32> InData)
        {
            UInt32 ID_Name = 0;
            if (InData.Count > 0)
            { 
                ID_Name = InData[0];
                while (ID_NAME != ID_Name)
                {
                    InData.RemoveAt(0);
                    if (InData.Count > 0)
                        ID_Name = InData[0] >> 16;
                    else
                        break;
                }
            }

        }
        public  UInt32[] GetFrame(List<UInt32> InData,out byte[] Data,int FrameLength)
        {
            UInt32 ID_Name;
            UInt32 DataLen;
            UInt32 Cmd;
            UInt32[] OutData = new UInt32[0];
            ID_Name = InData[0] >> 16;
            //DataLen = ((InData[0] >> 8) & 0xFF)/4;
            DataLen = 0;
            Cmd = InData[0]& 0xFF;
            Data = null;
            if (ID_Name == ID_NAME)
            {
                if (InData.Count >= DataLen + FrameLength)
                {
                    OutData = new UInt32[FrameLength + DataLen];
                    for (int i = 0; i < OutData.Length; i++)
                    {
                        OutData[i] = InData[i];
                    }
                    InData.RemoveRange(0, OutData.Length);
                }
                return OutData;
            }
            else
            {
                InData.RemoveAt(0);
                Debug.AppendMsg("Log error and try to fix" + Environment.NewLine);
                return OutData;
            }
        }

        public  bool IsEasyLogDevice()
        {
            byte[] Cmds;
            JlinkOp.ReadMemory(BaseAddr, 16*4, out Cmds);
            GetLogHeader(Cmds);
            if (ID_NAME == mLogHeader.Data[ID_OFFSET])
            {
                RangeAddr = mLogHeader.Data[ID_RANGE_ADDR];
                return true;
            }
            else
            {
                return false;
            }
        }

        public  void GetLogHeader(byte[] Cmd)
        {
            mLogHeader.Data = new UInt32[Cmd.Length/4];
            for(UInt32 i=0;i<Cmd.Length;i+=4)
            {
                mLogHeader.Data[i/4] = GetUINT32(Cmd, i);
            }
        }

        public  void LogTest()
        {
            byte[] Cmds;
            byte[] WriteData = new byte[1024];
            byte[] ReadData;
            bool ret;
            Random rnd = new Random();
            JlinkOp.ReadMemory(BaseAddr, 16 * 4, out Cmds);
            GetLogHeader(Cmds);
            if (mLogHeader.Data[ID_OFFSET] == ID_NAME)
            {
                Debug.AppendMsg("");
                Debug.AppendMsg("Start Log Test " + Environment.NewLine);
                for (int i = 0; i < 5000; i++)
                {
                    for (int j = 0; j < WriteData.Length; j++)
                    {
                        WriteData[j] = (byte)rnd.Next();
                    }
                    JlinkOp.WritMemory(mLogHeader.Data[UP_BUFFER_ADDR_OFFSET], (UInt32)WriteData.Length, WriteData);
                    JlinkOp.ReadMemory(mLogHeader.Data[UP_BUFFER_ADDR_OFFSET], (UInt32)WriteData.Length, out ReadData);
                    ret = true;
                    for (int j = 0; j < WriteData.Length; j++)
                    {
                        if (WriteData[j] != ReadData[j])
                        {
                            ret = false;
                            break;
                        }
                    }
                    if (ret == true)
                    {
                        Debug.AppendMsg("TestCase" + i + ":PASS" + Environment.NewLine);
                    }
                    else
                    {
                        Debug.AppendMsg("TestCase" + i + ":FAIL" + Environment.NewLine);
                        break;
                    }
                }
            }
            else
            {
                Debug.AppendMsg("Log is not online" + Environment.NewLine);
            }
        }
        public  string DataPro()
        {
            byte[] Cmds;
            byte[] DataArray;
            int FrameLen = 2;
            int Len, LenSplit0, LenSplit1;
            StringBuilder Msg = new StringBuilder();
            JlinkOp.ReadMemory(BaseAddr, 16*4, out Cmds);
            GetLogHeader(Cmds);
            
            if (mLogHeader.Data[ID_OFFSET] == ID_NAME)
            {
                if (mLogHeader.Data[TIME_FLAG_OFFSET] == 1)
                {
                    FrameLen = 3;
                }
                CpuTimerClock = mLogHeader.Data[CPU_CLOCK_OFFSET]/1000000;

                if (mLogHeader.Data[ENABLE_OFFSET] == 0)
                {
                    LogEnable(true);
                    Synchronize(list);
                }
            }
            else
            {
                JlinkOp.Error = true;
                return "";
            }

            if (mLogHeader.Data[VERSION_OFFSET] >= 5)
            {
                if (mLogHeader.Data[WROFF_OFFSET] != (~mLogHeader.Data[WROFF_REV_OFFSET]) || mLogHeader.Data[UP_BUFFER_SIZE_OFFSET]>=32*1024 || mLogHeader.Data[RDOFF_OFFSET] >= 32 * 1024 || mLogHeader.Data[WROFF_OFFSET] >= 32 * 1024)
                {
                    return "";
                }           
                
            }

            if (mLogHeader.Data[RDOFF_OFFSET] != mLogHeader.Data[WROFF_OFFSET])
            {
                if (mLogHeader.Data[RDOFF_OFFSET] < mLogHeader.Data[WROFF_OFFSET])
                {
                    Len = (int)(mLogHeader.Data[WROFF_OFFSET] - mLogHeader.Data[RDOFF_OFFSET]);
                    JlinkOp.ReadMemory((UInt32)(mLogHeader.Data[UP_BUFFER_ADDR_OFFSET] + mLogHeader.Data[RDOFF_OFFSET]), (UInt32)Len, out DataArray);
                    for (int i = 0; i < Len; i += 4)
                    {
                        list.Add(GetUINT32(DataArray, (UInt32)i));
                    }
                }
                else
                {
                    LenSplit0 = (int)(mLogHeader.Data[UP_BUFFER_SIZE_OFFSET] - mLogHeader.Data[RDOFF_OFFSET]);
                    LenSplit1 = (int)mLogHeader.Data[WROFF_OFFSET];
                    Len = LenSplit0 + LenSplit1;
                    if (Len > 0)
                    {
                        JlinkOp.ReadMemory((UInt32)(mLogHeader.Data[UP_BUFFER_ADDR_OFFSET] + mLogHeader.Data[RDOFF_OFFSET]), (UInt32)LenSplit0, out DataArray);
                        for (int i = 0; i < LenSplit0; i += 4)
                        {
                            list.Add(GetUINT32(DataArray, (UInt32)i));
                        }

                        if (LenSplit1 > 0)
                        {
                            JlinkOp.ReadMemory(mLogHeader.Data[UP_BUFFER_ADDR_OFFSET], (UInt32)LenSplit1, out DataArray);
                            for (int i = 0; i < LenSplit1; i += 4)
                            {
                                list.Add(GetUINT32(DataArray, (UInt32)i));
                            }
                        }
                    }
                }
                UInt32[] REOFF_VALUE = new UInt32[2];
                REOFF_VALUE[0] = mLogHeader.Data[WROFF_OFFSET];
                REOFF_VALUE[1] = ~REOFF_VALUE[0];
                byte[] REOFF_DATA = WilfDataPro.ToByte(REOFF_VALUE);
                JlinkOp.WritMemory(BaseAddr + RDOFF_OFFSET * 4, (UInt32)REOFF_DATA.Length, REOFF_DATA);
                //JlinkOp.WriteRegister(BaseAddr + RDOFF_OFFSET*4, REOFF_VALUE);
            }

            if (list.Count == 0)
                return "";

            UInt32 ID, Data,LogID,Timetamp = 0;
            byte[] Payload;
            string Name;
            UInt32[] CurFrame;
            string TimeStr = "";
            while (true)
            {
                if (list.Count >= FrameLen)
                    CurFrame = GetFrame(list, out Payload, FrameLen);
                else
                {
                    break;
                }
                if (CurFrame == null || CurFrame.Length == 0)
                {
                    Msg.Append("Error: Frame is abnormal and try to fix @" + DateTime.Now.ToString("MddHHmmss_fff") + Environment.NewLine);
                    break;
                }

                LogID = (CurFrame[0]>>8) & 0xFF;
                ID = CurFrame[0] & 0xFF;
                Data = CurFrame[1];

                Msg.Append(LogID.ToString("D3") + ",");
                if (PcTimeEnable == true)
                {
                    TimeStr = DateTime.Now.ToString("MddHHmmss_fff");
                    Msg.Append(TimeStr + ",");
                }

                if (FrameLen >= 3)
                {
                    Timetamp = CurFrame[2];
                    TimeStr = (((double)Timetamp/ CpuTimerClock)*1000).ToString("f0");
                    Msg.Append(TimeStr + ",");
                }
                else
                {
                    Msg.Append(",");
                }

                if (ID == 0)
                {
                    Name = (string)ID_Table["0x"+Data.ToString("X8")] + "," + "0x" + Data.ToString("X8") + "," + Data.ToString() + "," +"ID0";
                    Msg.Append(Name);
                    Msg.Append(Environment.NewLine);
                }
                else
                {
                    string ID_Str = "ID" + ID.ToString();
                    if (ID_Table.ContainsKey(ID_Str))
                    {
                        string str = (string)ID_Table[ID_Str];
                        string DataMsg = Data.ToString();
                        string des = GetDesTableResult(str, ID_Str, Data);
                        Msg.Append(str + ",");     
                        if(des == "")
                             Msg.Append("0x" + Data.ToString("X8") + "," + Data.ToString() + "," + ID_Str);
                        else
                            Msg.Append("0x" + Data.ToString("X8") + "," + Data.ToString() + "," + ID_Str + "," + des );
                    }
                    else
                    {
                        Msg.Append("ID" + ID.ToString() +",0x" + Data.ToString("X8") + "," + Data.ToString() + "," + ID_Str);
                    }
                    Msg.Append(Environment.NewLine);
                }
            }
            return Msg.ToString();
        }
        public static bool CheckPermission()
        {
            string[] RootUser = { "E0203","wilf","wangweifeng" };
            if(File.Exists("debug.a"))
            {
                return false;
            }
            for (int i = 0; i < RootUser.Length; i++)
            {
                if (RootUser[i].ToUpper() == easy_log.HostUserName.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

    }
}
