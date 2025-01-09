using Microsoft.VisualBasic.ApplicationServices;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using Microsoft.JScript;
using System.Drawing;
using System.IO;
using System.Security.Policy;
using System.Diagnostics;
using System.Xml.Linq;

namespace SOM_DEVELOP_TOOL
{
    public delegate void FunTemplet(int Value);
    public struct JlinkProtocolFrame
    {

        public ushort uid;
        public byte main_cmd;
        public byte sub_cmd;
        public UInt32[] payload_param;
        public UInt32 payload_size;
        public UInt32 payload_crc;
        public byte frame_counter;
        public byte last_frame;
        public byte direction;
        public byte direction_rev;
        public UInt32 head_crc;
        public byte[] data;
};

public  class jlink_protocol
    {
        
        public static UInt32[] guiDTable_CRC32 = new UInt32[256];
        public static UInt32 gui_cRC = 0xFFFFFFFF;
        private static bool is_crc_table_init_flag = false;

        public static int BUFFER_SIZE = 4096;
        public static int PARAM_NUM = 3;
       

        public static ushort UID = 0x55AA;
        public static UInt32 HEADER_SIZE = (UInt32)((5+PARAM_NUM)*4-4);
        public static UInt32 WriteFrameAddr = 0x20000000;
        public static UInt32 ReadFrameAddr;

        public static int NEW_DATA_INDEX = 0;
        public static int EXE_DATA_INDEX = 1;

        public static byte DIR_IDLE = 0;
        public static byte DIR_HOST_TO_SLAVE = 1;
        public static byte DIR_SLAVE_TO_HOST = 2;
        public static byte DIR_WAIT_CLEAR =    3;

        public static byte M_CMD_IDLE = 0;
        public static byte M_CMD_PROGRAM_FLASH = 1;
        public static byte M_CMD_RW_MEMORY = 2;
        public static byte M_CMD_AUTO_TEST = 3;

        public static byte S_CMD_FLASH_UNLOCK = 0;
        public static byte S_CMD_FLASH_WRITE = 1;
        public static byte S_CMD_FLASH_READ = 2;
        public static byte S_CMD_FLASH_CHECK = 3;
        public static byte S_CMD_FLASH_CYC_TEST = 4;

        public static byte S_CMD_WRITE = 0;
        public static byte S_CMD_READ = 1;

        public static UInt32 fill_data_len;
        public static byte frame_counter = 0;

        public static JlinkProtocolFrame gpst_pro = new JlinkProtocolFrame();
        public static FunTemplet pSetProBar = null;

        public static int m_over_time = 2000;


        public static void SetOverTime(int Time)
        {
            m_over_time = Time;
        }

        public static bool InitFlash(bool EnableLog=true)
        {
            bool ret;
            byte[] wdata = new byte[4096];
            UInt32[] Param = new UInt32[3];
            JlinkProtocolFrame jf;
            Debug.AppendMsg("[Inf]Inital Flash"+Environment.NewLine, Color.Green);
            ret = WriteFrame(M_CMD_PROGRAM_FLASH, S_CMD_FLASH_UNLOCK, Param, wdata);
            if (ret == false)
            {
                    Debug.AppendMsg("[Error]Write Frame Fail"+Environment.NewLine);
                return false;
            }
            ret = ReadFrame(out jf);
            if (ret == false)
            {
                Debug.AppendMsg("[Error]Read Frame Fail"+Environment.NewLine);
                return false;
            }
            return true;
        }

        public static bool AutoTest(UInt32 Addr, UInt32[] FunctionParam,out JlinkProtocolFrame jf)
        {
            bool ret;
            byte[] wdata = new byte[256];
            UInt32[] Param = new UInt32[3];
            jf = new JlinkProtocolFrame();
            UInt32 TempData;
            Param[0] = Addr;
            Param[1] = (UInt32)FunctionParam.Length;
            for (int i = 0;i<FunctionParam.Length;i++)
            {
                TempData = FunctionParam[i];
                wdata[4*i + 0] = (byte)(TempData>>0&0xFF);
                wdata[4*i + 1] = (byte)(TempData>>8&0xFF);
                wdata[4*i + 2] = (byte)(TempData>>16&0xFF);
                wdata[4*i + 3] = (byte)(TempData>>24&0xFF);
            }
            ret = WriteFrame(M_CMD_AUTO_TEST, 0, Param, wdata);
            if (ret == false)
            {
                Debug.AppendMsg("[Error]Write Frame Fail"+Environment.NewLine);
                return false;
            }
            ret = ReadFrame(out jf);
            if (ret == false)
            {
                Debug.AppendMsg("[Error]Read Frame Fail"+Environment.NewLine);
                return false;
            }
            return true;
        }

        public static bool ProgramFlash(UInt32 StartAddr, byte[] bin_data,bool EnableLog=false)
        {
            bool ret;
            int FrameNum, LeftNum;
            byte[] wdata = new byte[4096];
            UInt32[] Param = new UInt32[3];
            JlinkProtocolFrame jf;
            int i;
            LeftNum = bin_data.Length%wdata.Length;
            FrameNum = bin_data.Length/wdata.Length + (LeftNum>0 ? 1 : 0);
            Debug.AppendMsg("[Inf]Program Flash"+Environment.NewLine, Color.Green);
            for (i = 0; i<FrameNum; i++)
            {
                SetProBar(i+1, FrameNum);
                Param[0] = (UInt32)(i*wdata.Length) + StartAddr;
                Param[1] = (UInt32)wdata.Length;
                if (EnableLog)
                    Debug.AppendMsg("[Inf]Write Flash At:0x"+ Param[0].ToString("X8")+",length="+Param[1]+Environment.NewLine);
                if (i == FrameNum-1 && LeftNum>0)
                {
                    Array.Clear(wdata, 0, wdata.Length);
                    Array.Copy(bin_data, i*wdata.Length, wdata, 0, LeftNum);
                }
                else
                    Array.Copy(bin_data, i*wdata.Length, wdata, 0, wdata.Length);

                ret = WriteFrame(M_CMD_PROGRAM_FLASH, S_CMD_FLASH_WRITE, Param, wdata);
                if (ret == false)
                {
                    Debug.AppendMsg("[Error]Write Frame Fail"+Environment.NewLine);
                    return false;
                }
                ret = ReadFrame(out jf);
                if (ret == false)
                {
                        Debug.AppendMsg("[Error]Read Frame Fail"+Environment.NewLine);
                    return false;
                }
            }
            return true;
        }

        public static bool CheckFlash(UInt32 StartAddr, byte[] bin_data,bool EnableLog = true)
        {
            bool ret;
            byte[] wdata = new byte[4096];
            UInt32[] Param = new UInt32[3];
            JlinkProtocolFrame jf;
            UInt32 send_crc32, read_crc32;
            send_crc32 = GetCrc32(bin_data, 0, (UInt32)bin_data.Length);
            Debug.AppendMsg("[Inf]Verify Flash"+Environment.NewLine, Color.Green);
            Param[0] = StartAddr;
            Param[1] = (UInt32)bin_data.Length;
            Param[2] = send_crc32;
            if (EnableLog)
                Debug.AppendMsg("[Inf]Check Flash star"+Environment.NewLine);
            ret = WriteFrame(M_CMD_PROGRAM_FLASH, S_CMD_FLASH_CHECK, Param, wdata);
            if (ret == false)
            {
                Debug.AppendMsg("[Error]Write Frame Fail"+Environment.NewLine);
                return false;
            }
            ret = ReadFrame(out jf);
            if (ret == false)
            {
                Debug.AppendMsg("[Error]Read Frame Fail"+Environment.NewLine);
                return false;
            }
            else
            {
                read_crc32 = WilfDataPro.GetUint32(jf.data, 1);
            }
            return jf.data[0] == 1;
            //byte[] rdata = null;
            //if (send_crc32 != read_crc32)
            //{
            //    Debug.AppendMsg("[Inf]Read"+Environment.NewLine, Color.Green);
            //    Param[0] = 0;
            //    Param[1] = (UInt32)bin_data.Length;
            //    Debug.AppendMsg("[Inf]Read Flash star"+Environment.NewLine);
            //    ret = WriteFrame(M_CMD_PROGRAM_FLASH, S_CMD_FLASH_READ, Param, null);
            //    if (ret == false)
            //    {
            //        Debug.AppendMsg("[Error]Read Frame Fail"+Environment.NewLine);
            //        return false;
            //    }
            //    ret = ReadFrame(out rdata);
            //    if (ret == false)
            //    {
            //        Debug.AppendMsg("[Error]Read Frame Fail"+Environment.NewLine);
            //        return false;
            //    }
            //    File.WriteAllBytes("flash_dump.bin", rdata);
            //    Debug.AppendMsg("[Inf]dump flash data to file"+Environment.NewLine);
            //}
        }

        public static bool ReadFlash(UInt32 StartAddr,out byte[] bin_data,UInt32 Length,bool EnableLog = true)
        {
            bool ret;
            UInt32[] Param = new UInt32[3];
            bin_data = null;
            
            if(EnableLog)
                Debug.AppendMsg("[Inf]Read Flash"+Environment.NewLine, Color.Green);
            Param[0] = StartAddr;
            Param[1] = Length;
            ret = WriteFrame(M_CMD_PROGRAM_FLASH, S_CMD_FLASH_READ, Param, null);
            if (ret == false)
            {
                Debug.AppendMsg("[Error]Read Frame Fail"+Environment.NewLine);
                return false;
            }
            ret = ReadFrame(out bin_data);
            if (ret == false)
            {
                Debug.AppendMsg("[Error]Read Frame Fail"+Environment.NewLine);
                return false;
            }
            return true;
        }

        public static bool TestFlash(UInt32 StartAddr, UInt32 Length,UInt32 TestTime=1, bool EnableLog = true)
        {
            bool ret;
            UInt32[] Param = new UInt32[3];
            JlinkProtocolFrame jf;
            if (EnableLog)
            {
                Debug.AppendMsg("[Inf]TestFlash Flash"+Environment.NewLine, Color.Green);
                Debug.AppendMsg("[Inf]StartAddr:0x"+StartAddr.ToString("X8")+Environment.NewLine, Color.Green);
            }
            Param[0] = StartAddr;
            Param[1] = Length;
            Param[2] = TestTime;
            ret = WriteFrame(M_CMD_PROGRAM_FLASH, S_CMD_FLASH_CYC_TEST, Param, null);
            if (ret == false)
            {
                Debug.AppendMsg("[Error]Read Frame Fail"+Environment.NewLine);
                return false;
            }
            ret = ReadFrame(out jf);
            if (ret == false)
            {
                Debug.AppendMsg("[Error]Read Frame Fail"+Environment.NewLine);
                return false;
            }
            return jf.data[0] == 1; ;
        }

        public static bool DownloadToFlash(UInt32 StartAddr, byte[] bin_data)
        {
            bool ret;
            int FrameNum, LeftNum;
            UInt32[] Param = new UInt32[3];
            byte[] wdata = new byte[4096];
            byte[] read_bin_data;
            Hashtable TimeRecord = new Hashtable();

            LeftNum = bin_data.Length%wdata.Length;
            FrameNum = bin_data.Length/wdata.Length + (LeftNum>0 ? 1 : 0);
            WilfDataPro.InitStartTime();
            ret = InitFlash(false);
            if (ret==false)
                return ret;
            TimeRecord.Add("Unlock",WilfDataPro.GetDiffTime(true));

            ret = ProgramFlash(StartAddr, bin_data,false);
            if (ret==false)
                return ret;
            TimeRecord.Add("Program", WilfDataPro.GetDiffTime(true));

            ret = ReadFlash(StartAddr, out read_bin_data, (UInt32)bin_data.Length, false);
            if (ret==false)
                return ret;
            TimeRecord.Add("Read", WilfDataPro.GetDiffTime(true));

            ret=CheckFlash(StartAddr, bin_data, false);
            if (ret==false)
                return ret;
            TimeRecord.Add("Verify", WilfDataPro.GetDiffTime(true));

            
            Debug.AppendMsg("[Inf]Address=0x"+StartAddr.ToString("X8")+",Length="+bin_data.Length+Environment.NewLine);
            double TotolTime = 0;
            foreach(string key in TimeRecord.Keys)
            {
                TotolTime+=(double)TimeRecord[key];
                Debug.AppendMsg("[Inf]"+key+"Time:"+TimeRecord[key].ToString()+"ms"+Environment.NewLine);
            }
            Debug.AppendMsg("[Inf]"+"ProgramSpped"+":"+((double)TimeRecord["Program"]/FrameNum).ToString("F3")+" ms/Sector"+Environment.NewLine);
            Debug.AppendMsg("[Inf]"+"DownloadSpped"+":"+(((bin_data.Length)/1024.0)/(TotolTime/1000.0)).ToString("F3")+" Kbytes/s"+Environment.NewLine);
            Debug.AppendMsg("[Inf]"+"TotalTime:"+TotolTime.ToString("F3")+"ms"+Environment.NewLine);
            JlinkOp.Reset();
            JlinkOp.JLINKARM_Go();
            //File.WriteAllBytes("bin_data.bin", bin_data);
            //File.WriteAllBytes("read_bin_data.bin", read_bin_data);
            return true;
        }
        public static void Test()
        {
            bool ret;
            string msg;
            UInt32[] Param = new UInt32[3];
            byte[] wdata = new byte[4096];
            Random rnd = new Random();
            for(int i=0;i<wdata.Length;i++)
            {
                wdata[i] = (byte)rnd.Next();
            }

            JlinkProtocolFrame jf;
            for (int i = 0; i<1; i++)
            {
                Debug.AppendMsg("*******TestTime:"+i+"********"+Environment.NewLine);
                Param[0] = 0x38000000;
                Param[1] = (UInt32)wdata.Length;
                Debug.AppendMsg("[Inf]Write memory star"+Environment.NewLine);
                ret = WriteFrame(2, 0, Param, wdata);
                if (ret == false)
                {
                    Debug.AppendMsg("[Error]Write Frame Fail"+Environment.NewLine);
                    return;
                }
                else
                {
                    Debug.AppendMsg("[Inf]Write memory ok"+Environment.NewLine);
                }
                ret = ReadFrame(out jf);
                if (ret == true)
                {
                    Debug.AppendMsg("[Inf]cmd send sucessfully"+Environment.NewLine);
                    msg = WilfDataPro.ToString(jf.data, "X2", 64);
                    Debug.AppendMsg("Read Data:"+msg+Environment.NewLine);
                    Debug.AppendMsg("FrameCounter:"+jf.frame_counter+Environment.NewLine);
                }
                else
                {
                    Debug.AppendMsg("[Error]Read Frame Fail"+Environment.NewLine);
                }

                Param[0] = 0x38000000;
                Param[1] = (UInt32)wdata.Length;
                Debug.AppendMsg("[Inf]Read memory star"+Environment.NewLine);
                ret = WriteFrame(2, 1, Param, null);
                if(ret == false)
                {
                    Debug.AppendMsg("[Error]Write Frame Fail"+Environment.NewLine);
                    return;
                }
                ret = ReadFrame(out jf);
                if (ret == true)
                {
                    Debug.AppendMsg("[Inf]cmd send sucessfully"+Environment.NewLine);
                    msg = WilfDataPro.ToString(jf.data, "X2", 32);
                    Debug.AppendMsg("Read Data:"+Environment.NewLine+msg+Environment.NewLine);
                    Debug.AppendMsg("FrameCounter:"+jf.frame_counter+Environment.NewLine);
                }
                else
                {
                    Debug.AppendMsg("[Error]Read Frame Fail"+Environment.NewLine);
                }
                UInt32 crc0 = GetCrc32(jf.data, 0, (UInt32)jf.data.Length);
                UInt32 crc1 = GetCrc32(wdata, 0, (UInt32)wdata.Length);
                if(crc0 == crc1)
                {
                    Debug.AppendMsg("[Inf]crc pass"+Environment.NewLine);
                }
                else
                {
                    Debug.AppendMsg("[Error]crc fail"+Environment.NewLine);
                    break;
                }
            }
        }

        public static bool Init(UInt32 StartAddress)
        {
            byte[] clear_data = new byte[2];
            byte[] data = new byte[HEADER_SIZE+4];
            byte[] R_UID = new byte[2];
            ushort[] R16_UID = new ushort[2];
            WriteFrameAddr = StartAddress;
            ReadFrameAddr = (UInt32)(WriteFrameAddr+2*(BUFFER_SIZE+HEADER_SIZE+4));
            JlinkOp.ReadMemory(WriteFrameAddr, 2, out R_UID);
            R16_UID[0] = (ushort)(R_UID[0] + R_UID[1]*256);
            JlinkOp.ReadMemory(ReadFrameAddr, 2, out R_UID);
            R16_UID[1] = (ushort)(R_UID[0] + R_UID[1]*256);
            if (R16_UID[0] != R16_UID[1] || R16_UID[0] != jlink_protocol.UID)
            {
                Debug.AppendMsg("[Error]UID error, StartAddress=0x"+StartAddress.ToString("X8"), true);
                return false;
            }
            Array.Clear(data, 0, data.Length);
            clear_data[0] = DIR_IDLE;
            clear_data[1] = (byte)(~DIR_IDLE);
            WilfDataPro.AddU16(data, 0, UID);
            WilfDataPro.AddU8(data, (int)(HEADER_SIZE-2), clear_data);
            JlinkOp.WritMemoryAt(WriteFrameAddr, (UInt32)(HEADER_SIZE+4), data, 0);
            JlinkOp.WritMemoryAt(ReadFrameAddr, (UInt32)(HEADER_SIZE+4), data, 0);
            return true;
        }

        public static bool WriteFrame(byte main_cmd,byte sub_cmd, UInt32[] Param,byte[] lupb_data)
        {
            bool ret = false;
            int index = 0;
            byte[] data;
            UInt32 payload_crc32,header_crc32;
            UInt32 lui_length;
            if (Param == null)
            {
                Param = new UInt32[PARAM_NUM];
                Array.Clear(Param, 0, Param.Length);
            }
            if(lupb_data == null)
            {
                lupb_data = new byte[0];
            }
            lui_length = (UInt32)lupb_data.Length;
            data = new byte[HEADER_SIZE+4+lui_length];
            Array.Clear(data,0,data.Length);
            payload_crc32 = jlink_protocol.GetCrc32(lupb_data, 0, lui_length);
            index +=WilfDataPro.AddU16(data, index, UID);
            index +=WilfDataPro.AddU8(data, index, main_cmd);
            index +=WilfDataPro.AddU8(data, index, sub_cmd);
            index +=WilfDataPro.AddU8(data, index, WilfDataPro.ToByte(Param));
            index +=WilfDataPro.AddU32(data, index, (UInt32)lupb_data.Length);
            index +=WilfDataPro.AddU32(data, index, payload_crc32);
            index +=WilfDataPro.AddU8(data, index, frame_counter++);
            index +=WilfDataPro.AddU8(data, index, 1);   //last frame
            index +=WilfDataPro.AddU8(data, index, DIR_HOST_TO_SLAVE);
            index +=WilfDataPro.AddU8(data, index, (byte)(~DIR_HOST_TO_SLAVE));
            header_crc32 = jlink_protocol.GetCrc32(data, 0, (UInt32)index);
            index +=WilfDataPro.AddU32(data, index, header_crc32);
            index +=WilfDataPro.AddU8(data, index, lupb_data);
            JlinkOp.WritMemoryAt(WriteFrameAddr+HEADER_SIZE+4, (UInt32)(data.Length -HEADER_SIZE-4), data, (int)(HEADER_SIZE+4));  //write payload at first
            JlinkOp.WritMemoryAt(WriteFrameAddr, (UInt32)(HEADER_SIZE+4), data,0);
            ret = CheckState();
            return ret;
        }


        public static bool CheckState(int OverTime = 3000)
        {
            byte[] data;
            JlinkProtocolFrame jf= new JlinkProtocolFrame();
            DateTime StartTime = DateTime.Now;
            TimeSpan DiifTime;
           
            while (true)
            {
                JlinkOp.ReadMemory(WriteFrameAddr, HEADER_SIZE+4, out data);
                jf = ByteToFrame(data);
                if(jf.direction == DIR_IDLE && jf.direction == (byte)(~jf.direction_rev))
                {
                    return true;
                }
                DiifTime = DateTime.Now.Subtract(StartTime);
                if (DiifTime.TotalMilliseconds>=OverTime)
                    break;
            }

            return false;
        }

        public static bool ReadFrame(out JlinkProtocolFrame jf)
        {
            byte[] data;
            jf= new JlinkProtocolFrame();
            DateTime StartTime = DateTime.Now;
            TimeSpan DiifTime;
            int OverTime = m_over_time;
            while (true)
            {
                JlinkOp.ReadMemory(ReadFrameAddr, HEADER_SIZE+4, out data);
                if(CheckCrc(data))
                {
                    jf = ByteToFrame(data);
                    if (jf.direction == DIR_SLAVE_TO_HOST)
                    {
                        JlinkOp.ReadMemory(ReadFrameAddr+HEADER_SIZE+4, jf.payload_size, out jf.data);
                        if (CheckCrc(jf))
                        {
                            byte[] clear_data = new byte[2];
                            clear_data[0] = DIR_IDLE;
                            clear_data[1] = (byte)(~DIR_IDLE);
                            JlinkOp.WritMemory(ReadFrameAddr+HEADER_SIZE-2, (UInt32)clear_data.Length, clear_data);
                            return true;
                        }
                    }
                }
                DiifTime = DateTime.Now.Subtract(StartTime);
                if (DiifTime.TotalMilliseconds>=OverTime)
                    break;
            }
           
            return false;
        }

        public static bool ReadFrame(out byte[] out_data)
        {
            JlinkProtocolFrame jf;
            List<byte[]> data = new List<byte[]>();
            int len = 0, index = 0;
            out_data = null;
            while (true)
            {
                if (ReadFrame(out jf))
                {
                    data.Add(jf.data);
                    if (jf.last_frame == 1)
                        break;
                }
                else
                    return false;
            }

            for (int i = 0; i<data.Count; i++)
            {
                len +=data[i].Length;
            }
            out_data = new byte[len];
            for (int i = 0; i<data.Count; i++)
            {
                Array.Copy(data[i], 0, out_data, index, data[i].Length);
                index+=data[i].Length;
            }
            return true;
        }

        private static bool CheckCrc(byte[] data,bool check_all=false)
        {
            UInt32 payload_crc32_calc, header_crc32_calc;
            UInt32 payload_crc32_ref, header_crc32_ref;
            bool payload_crc_result = true,header_crc_result = true;
            byte direction, direction_calc;

            direction = WilfDataPro.GetUint8(data, (int)HEADER_SIZE-2);
            direction_calc = (byte)(~WilfDataPro.GetUint8(data, (int)HEADER_SIZE-1));
            if(direction != direction_calc)
                return false;
            if (check_all == true)
            {
                header_crc32_calc = jlink_protocol.GetCrc32(data, 0, HEADER_SIZE);
                payload_crc32_calc = jlink_protocol.GetCrc32(data, HEADER_SIZE, (UInt32)(data.Length-HEADER_SIZE));
                header_crc32_ref = WilfDataPro.GetUint32(data, (int)HEADER_SIZE);
                payload_crc32_ref = WilfDataPro.GetUint32(data, 4+PARAM_NUM*4+4);
                header_crc_result = (header_crc32_ref == header_crc32_calc);
                payload_crc_result = (payload_crc32_ref == payload_crc32_calc);
            }
            else
            {
                header_crc32_calc = jlink_protocol.GetCrc32(data, 0, HEADER_SIZE);
                header_crc32_ref = WilfDataPro.GetUint32(data, (int)HEADER_SIZE);
                header_crc_result = (header_crc32_ref == header_crc32_calc);
            }
            return (payload_crc_result&&header_crc_result);
        }

        private static bool CheckCrc(JlinkProtocolFrame jf)
        {
            UInt32 payload_crc32_calc;
            UInt32 payload_crc32_ref;
            bool payload_crc_result = true;

            payload_crc32_calc = jlink_protocol.GetCrc32(jf.data, 0, (UInt32)jf.data.Length);
            payload_crc32_ref = jf.payload_crc;
            payload_crc_result = (payload_crc32_ref == payload_crc32_calc);

            return payload_crc_result;
        }
        private static JlinkProtocolFrame ByteToFrame(byte[] data)
        {
            JlinkProtocolFrame gpst_pro = new JlinkProtocolFrame();
            int index = 0;
            int payload_size;
            payload_size = data.Length-(int)HEADER_SIZE-4;
            gpst_pro.payload_param = new UInt32[PARAM_NUM];
            gpst_pro.uid = WilfDataPro.GetUint16(data, index); index+=2;
            gpst_pro.main_cmd = WilfDataPro.GetUint8(data, index); index+=1;
            gpst_pro.sub_cmd = WilfDataPro.GetUint8(data, index); index+=1;
            for (int i = 0;i<PARAM_NUM;i++)
            {
                gpst_pro.payload_param[i] = WilfDataPro.GetUint32(data, index); index+=4;
            }
            gpst_pro.payload_size = WilfDataPro.GetUint32(data, index); index+=4;
            gpst_pro.payload_crc = WilfDataPro.GetUint32(data, index); index+=4;
            gpst_pro.frame_counter = WilfDataPro.GetUint8(data, index); index+=1;
            gpst_pro.last_frame = WilfDataPro.GetUint8(data, index); index+=1;
            gpst_pro.direction = WilfDataPro.GetUint8(data, index); index+=1;
            gpst_pro.direction_rev = WilfDataPro.GetUint8(data, index); index+=1;
            gpst_pro.head_crc = WilfDataPro.GetUint32(data, index); index+=4;
            if(payload_size>0)
            {
                gpst_pro.data= new byte[payload_size];
                Array.Copy(data,HEADER_SIZE,gpst_pro.data,0,payload_size);
            }
            return gpst_pro;
        }


        public static void ResetCrc32()
        {
            gui_cRC = 0xFFFFFFFF;
        }

        /*******************************************************************************
         * Function Name  : Crc32InitTable.
         * Description    : Crc32
         * Input          : None
         * Output         : None
         * Return         : None
         *******************************************************************************/
        public static void Crc32InitTable()
        {
            UInt32 i32;
            UInt32 j32;
            UInt32 lul_n_data32;
            UInt32 lul_n_accum32;

            for (i32 = 0; i32 < 256; i32++)
            {
                lul_n_data32 = (UInt32)(i32 << 24);
            lul_n_accum32 = 0;

            for (j32 = 0; j32 < 8; j32++)
            {
                if (((lul_n_data32 ^ lul_n_accum32) & 0x80000000)>0)
                {
                    lul_n_accum32 = (lul_n_accum32 << 1) ^ (0x04C11DB7);
                }
                else
                {
                    lul_n_accum32 <<= 1;
                }

                lul_n_data32 <<= 1;
            }

            guiDTable_CRC32[i32] = lul_n_accum32;
        }
        ResetCrc32();
    }

        /*******************************************************************************
         * Function Name  : GetCrc32.
         * Description    : ¼ÆËãCrc32
         * Input          : pchMsg£º²Ù×÷Êý¾Ý¶ÔÏóµØÖ·  wDataLen£º³¤¶È
         * Output         : Crc32¼ÆËã½á¹û
         * Return         : None
         *******************************************************************************/
    public static UInt32 GetCrc32(byte[] lpub_pch_msg, UInt32 offset, UInt32 luw_w_data_len,bool Reset=true)
    {
        byte lub_ch_char;
        UInt32 lui_cRC32_value;
        int i;
        if(is_crc_table_init_flag == false)
        {
               
            Crc32InitTable();
            is_crc_table_init_flag = true;
         }
        if (Reset == true)
        {
            ResetCrc32();
        }
         for (i=0; i<luw_w_data_len; i++)
        {
            lub_ch_char = lpub_pch_msg[i+offset];
            gui_cRC = guiDTable_CRC32[((gui_cRC >> 24) ^ lub_ch_char) & 0xff] ^ (gui_cRC << 8);
        }

        lui_cRC32_value = gui_cRC;

        return lui_cRC32_value;
    }
        public static UInt32 GetCheckSum(byte[] lpub_pch_msg,int Type = 0)
        {
            UInt32[] D32 = WilfDataPro.ToUint32(lpub_pch_msg);
            UInt32 Sum = 0;
            if (Type == 0)
            {
                for (int i = 0; i<D32.Length; i++)
                {
                    Sum+=D32[i];
                }
            }
            else
            {
                for (int i = 0; i<D32.Length; i+=2)
                {
                    Sum+=D32[i]*D32[i+1];
                }
            }
            return Sum;
        }


        private static void SetProBar(int Value)
        {
            if (pSetProBar != null)
                pSetProBar(Value);
        }

        private static void SetProBar(int CurValue,int TotalValue)
        {
            int Value = (100*CurValue)/TotalValue;
            SetProBar(Value);
        }
    }
}
