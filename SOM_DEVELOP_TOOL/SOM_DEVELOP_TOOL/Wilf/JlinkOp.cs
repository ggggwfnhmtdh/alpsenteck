using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections;
using System.Drawing;
namespace SOM_DEVELOP_TOOL
{
    using U8 = System.Byte;   // unsigned char
    using U16 = System.UInt16; // unsigned short
    using U32 = System.UInt32; // unsigned int
    using U64 = System.UInt64; // unsigned long long

    using I8 = System.SByte; // signed char
    using I16 = System.Int16; // signed short
    using I32 = System.Int32; // signed int
    using I64 = System.Int64; // signed long long

    public struct CommType
    {
        public UInt32 Status;
        public UInt32 Cmd;
        public UInt32 Length;
        public byte[] Data;
    }
    public enum PinType
    {
        VTarget,
        TCK,
        TDI,
        TDO,
        TMS,
        RESET,
        TRST
    }
    public enum ARM_REG
    {
        ARM_REG_R0,                         // Index  0
        ARM_REG_R1,                         // Index  1
        ARM_REG_R2,                         // Index  2
        ARM_REG_R3,                         // Index  3
        ARM_REG_R4,                         // Index  4
        ARM_REG_R5,                         // Index  5
        ARM_REG_R6,                         // Index  6
        ARM_REG_R7,                         // Index  7
        ARM_REG_CPSR,                       // Index  8
        ARM_REG_R15,                        // Index  9
        ARM_REG_R8_USR,                     // Index 10
        ARM_REG_R9_USR,                     // Index 11
        ARM_REG_R10_USR,                    // Index 12
        ARM_REG_R11_USR,                    // Index 13
        ARM_REG_R12_USR,                    // Index 14
        ARM_REG_R13_USR,                    // Index 15
        ARM_REG_R14_USR,                    // Index 16
        ARM_REG_SPSR_FIQ,                   // Index 17
        ARM_REG_R8_FIQ,                     // Index 18
        ARM_REG_R9_FIQ,                     // Index 19
        ARM_REG_R10_FIQ,                    // Index 20
        ARM_REG_R11_FIQ,                    // Index 21
        ARM_REG_R12_FIQ,                    // Index 22
        ARM_REG_R13_FIQ,                    // Index 23
        ARM_REG_R14_FIQ,                    // Index 24
        ARM_REG_SPSR_SVC,                   // Index 25
        ARM_REG_R13_SVC,                    // Index 26
        ARM_REG_R14_SVC,                    // Index 27
        ARM_REG_SPSR_ABT,                   // Index 28
        ARM_REG_R13_ABT,                    // Index 29
        ARM_REG_R14_ABT,                    // Index 30
        ARM_REG_SPSR_IRQ,                   // Index 31
        ARM_REG_R13_IRQ,                    // Index 32
        ARM_REG_R14_IRQ,                    // Index 33
        ARM_REG_SPSR_UND,                   // Index 34
        ARM_REG_R13_UND,                    // Index 35
        ARM_REG_R14_UND,                    // Index 36
        ARM_REG_FPSID,                      // Index 37
        ARM_REG_FPSCR,                      // Index 38
        ARM_REG_FPEXC,                      // Index 39
        ARM_REG_FPS0,                       // Index 40
        ARM_REG_FPS1,                       // Index 41
        ARM_REG_FPS2,                       // Index 42
        ARM_REG_FPS3,                       // Index 43
        ARM_REG_FPS4,                       // Index 44
        ARM_REG_FPS5,                       // Index 45
        ARM_REG_FPS6,                       // Index 46
        ARM_REG_FPS7,                       // Index 47
        ARM_REG_FPS8,                       // Index 48
        ARM_REG_FPS9,                       // Index 49
        ARM_REG_FPS10,                      // Index 50
        ARM_REG_FPS11,                      // Index 51
        ARM_REG_FPS12,                      // Index 52
        ARM_REG_FPS13,                      // Index 53
        ARM_REG_FPS14,                      // Index 54
        ARM_REG_FPS15,                      // Index 55
        ARM_REG_FPS16,                      // Index 56
        ARM_REG_FPS17,                      // Index 57
        ARM_REG_FPS18,                      // Index 58
        ARM_REG_FPS19,                      // Index 59
        ARM_REG_FPS20,                      // Index 60
        ARM_REG_FPS21,                      // Index 61
        ARM_REG_FPS22,                      // Index 62
        ARM_REG_FPS23,                      // Index 63
        ARM_REG_FPS24,                      // Index 64
        ARM_REG_FPS25,                      // Index 65
        ARM_REG_FPS26,                      // Index 66
        ARM_REG_FPS27,                      // Index 67
        ARM_REG_FPS28,                      // Index 68
        ARM_REG_FPS29,                      // Index 69
        ARM_REG_FPS30,                      // Index 70
        ARM_REG_FPS31,                      // Index 71
        ARM_REG_R8,                         // Index 72
        ARM_REG_R9,                         // Index 73
        ARM_REG_R10,                        // Index 74
        ARM_REG_R11,                        // Index 75
        ARM_REG_R12,                        // Index 76
        ARM_REG_R13,                        // Index 77
        ARM_REG_R14,                        // Index 78
        ARM_REG_SPSR,                       // Index 79
        ARM_NUM_REGS
    };
    public static class JlinkOp
    {
        public enum CoreStatus
        {
            TARGET_NONE,
            TARGET_RUNNING,                         
            TARGET_HALTED,                     
            TARGET_RESET,                     
            TARGET_SLEEPING,                       
            TARGET_LOCKUP,                      
        };

        public enum ControlStatus
        {
            C_DEBUGEN,
            C_HALT,
            C_STEP,
            C_MASKINTS,
            C_SNAPSTALL,
            S_REGRDY,
            S_HALT,
            S_SLEEP,
            S_LOCKUP,
            S_RETIRE_ST,
            S_RESET_ST
        };

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void JLINKARM_LOG(string s);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_Open", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern int JLINKARM_Open();
        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_GetSN", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern int JLINKARM_GetSN();
        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_SelectIP", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern int JLINKARM_SelectIP(string sHost, int Port);
        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_EMU_SelectByUSBSN", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern int JLINKARM_EMU_SelectByUSBSN(int SerilaNo);
        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_OpenEx", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern string JLINKARM_OpenEx(JLINKARM_LOG pfLog, JLINKARM_LOG pfErrorOut);
        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_ExecCommand", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static Int32 JLINKARM_ExecCommand(sbyte[] pIn, out sbyte[] pOut, int BufferSize);
        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_TIF_Select", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern int JLINKARM_TIF_Select(int Interface);
        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_SetSpeed", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern int JLINKARM_SetSpeed(int Speed);
        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_Connect", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern int JLINKARM_Connect();
        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_Close", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern int JLINKARM_Close();
        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_ReadMemU32", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int JLINKARM_ReadMemU32(UInt32 Addr, UInt32 NumItems, UInt32[] pData, byte[] pStatus);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_WriteU32", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int JLINKARM_WriteU32(UInt32 Addr, UInt32 pData);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_ReadMemU8", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int JLINKARM_ReadMemU8(UInt32 Addr, UInt32 NumItems, byte[] pData, byte[] pStatus);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_WriteU8", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int JLINKARM_WriteU8(UInt32 Addr, byte pData);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_ReadMem", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int JLINKARM_ReadMem(U32 Addr, U32 NumBytes, byte[] pData);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_WriteMem", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int JLINKARM_WriteMem(U32 Addr, U32 Count, IntPtr pData);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_Reset", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int JLINKARM_Reset();
        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_Go", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static void JLINKARM_Go();

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_Halt", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static void JLINKARM_Halt();

        //[DllImport("JLinkARM.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        [DllImport("JLinkARM.dll", EntryPoint = "JLINK_DownloadFile", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int JLINK_DownloadFile(IntPtr sFileName, U32 Addr);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_WriteReg", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static char JLINKARM_WriteReg(ARM_REG RegIndex, U32 Data);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_ReadReg", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static U32 JLINKARM_ReadReg(ARM_REG RegIndex);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_ReadRegs", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int JLINKARM_ReadRegs(UInt32[] paRegIndex, UInt32[] paData, byte[] paStatus, U32 NumRegs);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINK_RTTERMINAL_Control", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int JLINK_RTTERMINAL_Control(U32 Cmd, U32[] Data);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINK_RTTERMINAL_Read", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int JLINK_RTTERMINAL_Read(U32 BufferIndex, byte[] sBuffer, U32 BufferSize);
        [DllImport("JLinkARM.dll", EntryPoint = "JLINK_RTTERMINAL_Write", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int JLINK_RTTERMINAL_Write(U32 BufferIndex, byte[] sBuffer, U32 BufferSize);


        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_WriteMemEx", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int JLINKARM_WriteMemEx(U32 Addr, U32 NumBytes, IntPtr pData, U32 Flags);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_SetRESET", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static void JLINKARM_SetRESET();

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_ClrRESET", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static void JLINKARM_ClrRESET();


        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_GetId", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static U32 JLINKARM_GetId();

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_IsConnected", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static char JLINKARM_IsConnected();

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_SetBP", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static void JLINKARM_SetBP(U32 BPIndex, U32 Addr);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_ClrBP", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static void JLINKARM_ClrBP(U32 BPIndex);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_IsHalted", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static char JLINKARM_IsHalted();

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_CORESIGHT_Configure", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int JLINKARM_CORESIGHT_Configure(IntPtr cmdstr);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINK_EMU_GPIO_GetProps", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int JLINK_EMU_GPIO_GetProps(byte[] paDesc, U32 MaxNumDesc);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_GetHWStatus", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int JLINKARM_GetHWStatus(byte[] pStat);

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_SetTDI", CallingConvention = CallingConvention.Cdecl)]    //pass
        unsafe public extern static void JLINKARM_SetTDI();
        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_ClrTDI", CallingConvention = CallingConvention.Cdecl)]    //pass
        unsafe public extern static void JLINKARM_ClrTDI();


        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_SetTMS", CallingConvention = CallingConvention.Cdecl)]   //pass
        unsafe public extern static void JLINKARM_SetTMS();
        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_ClrTMS", CallingConvention = CallingConvention.Cdecl)]    //pass
        unsafe public extern static void JLINKARM_ClrTMS();

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_SetTRST", CallingConvention = CallingConvention.Cdecl)]    //fail
        unsafe public extern static void JLINKARM_SetTRST();
        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_ClrTRST", CallingConvention = CallingConvention.Cdecl)]    //fail
        unsafe public extern static void JLINKARM_ClrTRST();

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_SetTCK", CallingConvention = CallingConvention.Cdecl)]    //pass
        unsafe public extern static int JLINKARM_SetTCK();
        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_ClrTCK", CallingConvention = CallingConvention.Cdecl)]    //pass
        unsafe public extern static int JLINKARM_ClrTCK();

        [DllImport("JLinkARM.dll", EntryPoint = "JLINKARM_EMU_GetNumDevices", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static U32 JLINKARM_EMU_GetNumDevices();

        public static string JlinkConfigFile = "Jlink.cfg";
        public static UInt32 JLINKARM_RTTERMINAL_CMD_START = 0;
        public static UInt32 JLINKARM_RTTERMINAL_CMD_STOP = 1;
        public static UInt32 JLINKARM_RTTERMINAL_CMD_GETDESC = 2;
        public static UInt32 JLINKARM_RTTERMINAL_CMD_GETNUMBUF = 3;
        public static UInt32 JLINKARM_RTTERMINAL_CMD_GETSTAT = 4;
        public static Thread RTT_Thread = null;
        public static Thread Monitor_Thread = null;
        public static bool RTT_Enable = false;
        public static bool RTT_StartFlag = false;
        public static bool Monitor_Enable = false;
        public static UInt32 BreakPointAddress = 0;
        public static bool IsBpNeed = false;

        public static UInt32 CPU_ID_Arm = 0x0BC11477;
        public static UInt32 CPU_ID_M7 = 0x6ba02477;

        public static bool Error = false;
        public static string LogVarName;

        public static JLINKARM_LOG pJLINKARM_LOG_callback_pfErrorOut = JLINKARM_LOG_callback_pfErrorOut;

        public static string DeviceName = "VID_1366";
        public static string Protocol = "SWD";
        public static int SpeedKHz = 10000;
        public static bool SpeedUpdate = false;

        public static UInt32 INTERFACE_FLAG_ADDR = 0x50108120;
        public static UInt32 INTERFACE_ADDR = 0x50108124;

        public static UInt32 INTERFACE_FLAG_VALUE = 0x55AA55AA;
        public static UInt32 STATUS_IDLE = 0xFFFF0000;
        public static UInt32 STATUS_READY = 0xFFFF0001;
        public static UInt32 CommStructAddr = 0;


        public static bool CheckCpuID()
        {
            UInt32 ID;
            int Status;
            Status = JLINKARM_IsConnected();
            if (Status == 1)
            {
                ID = JLINKARM_GetId();
                if (ID == JlinkOp.CPU_ID_M7)
                    return true;
                else
                {
                    Debug.AppendMsg("Jlink is offline,Connect cpu fail,Please reconnect jlink" + Environment.NewLine, Color.Red);
                    return false;
                }
            }
            else
            {
                Debug.AppendMsg("Jlink is offline,Connect cpu fail,Please reconnect jlink" + Environment.NewLine, Color.Red);
                return false;
            }
        }
        public static void LoadJlinkConfig()
        {

        }
        //public static bool WaitComFlagReady(int OverTime)
        //{
        //    UInt32 Flag;
        //    ReadRegister(INTERFACE_FLAG_ADDR, out Flag);
        //    while (Flag != INTERFACE_FLAG_VALUE)
        //    {
        //        ReadRegister(INTERFACE_FLAG_ADDR, out Flag);
        //        if (OverTime > 0)
        //            OverTime--;
        //        else
        //            return false;
        //        Thread.Sleep(1);
        //    }
        //    ReadRegister(INTERFACE_ADDR, out CommStructAddr);
        //    return true;
        //}

        public static bool JLINK_EMU_GPIO_GetProps(out string[] GpioName)
        {
            int MaxGpioNum = 32;
            byte[] data = new byte[MaxGpioNum*36];
            int ret = JLINK_EMU_GPIO_GetProps(data, (UInt32)MaxGpioNum);
            GpioName = null;
            if (ret>0)
            {
                GpioName = new string[ret];
                for (int i = 0; i<ret; i++)
                {
                    GpioName[i] =  Encoding.UTF8.GetString(data, i*36, 36);
                }
                return true;
            }
            else
                return false;
        }

        public static int JLINKARM_GetHWStatus(PinType PinName)
        {
            byte[] data = new byte[8];
            int ret = JLINKARM_GetHWStatus(data);
            int state = 0;
            if (ret==0)
            {
                int index = (int)PinName;
                if (PinName == PinType.VTarget)
                {
                    state = data[index] + data[index+1]*256;
                }
                else
                {
                    state = data[index + 1];
                }

                return state;
            }
            else
                return -1;
        }
        public static CommType GetComData(bool NeedData = false)
        {
            CommType ComData = new CommType();
            ReadRegister(CommStructAddr, out ComData.Status);
            ReadRegister(CommStructAddr, out ComData.Cmd);
            ReadRegister(CommStructAddr, out ComData.Length);
            ComData.Data = new byte[ComData.Length];
            ReadMemory(CommStructAddr, (UInt32)ComData.Data.Length, out ComData.Data);
            return ComData;
        }

        public static void StopRttThtead()
        {
            JlinkOp.RTT_Enable = false;
            JlinkOp.RTT_Thread = null;
        }

        public static void StopMonitor()
        {
            JlinkOp.Monitor_Enable = false;
            JlinkOp.Monitor_Thread = null;
        }
        public static bool RTT_Config(UInt32 Addr)
        {
            int ret;
            UInt32[] Data = new U32[4];
            Array.Clear(Data, 0, 4);
            Data[0] = Addr;
            ret = JLINK_RTTERMINAL_Control(JLINKARM_RTTERMINAL_CMD_START, Data);
            return ret >= 0;
        }

        public static bool RTT_Close()
        {
            int ret;
            ret = JLINK_RTTERMINAL_Control(JLINKARM_RTTERMINAL_CMD_STOP, null);
            return ret >= 0;
        }

        public static bool RTT_Read(out string Msg)
        {
            int ret;
            int effective_Len = 0;
            byte[] Data = new byte[16];
            ret = JLINK_RTTERMINAL_Read(0, Data, (UInt32)Data.Length);
            for (int i = Data.Length - 1; i >= 0; i--)
            {
                if (Data[i] != 0)
                {
                    effective_Len = i + 1;
                    break;
                }
            }
            Msg = Encoding.UTF8.GetString(Data, 0, effective_Len);
            return ret >= 0;
        }

        public static byte[] RTT_Read(int Len)
        {
            int ret;
            byte[] Data = new byte[Len];
            ret = JLINK_RTTERMINAL_Read(0, Data, (UInt32)Data.Length);
            if (ret < 0)
                return null;
            return Data;
        }

        public static bool ReadAllCpuRegister(out UInt32[] RegValue)
        {
            int ret;
            byte[] acTmp = new byte[100];
            UInt32[] paRegIndex = new UInt32[32];
            RegValue = new UInt32[paRegIndex.Length];
            for (UInt32 i = 0; i < paRegIndex.Length; i++)
            {
                paRegIndex[i] = i;
            }
            ret = JLINKARM_ReadRegs(paRegIndex, RegValue, acTmp, (UInt32)paRegIndex.Length);
            return ret >= 0;
        }

        public static UInt32 ReadAllCpuRegister(int index)
        {

            return JLINKARM_ReadReg((ARM_REG)index);
        }

        public static void JLINKARM_LOG_callback_pfErrorOut(string s)
        {
            Debug.AppendMsg("Error:" + s);
        }

        public static bool DownloadFw(string FileName, UInt32 StartAddr)
        {
            byte[] Data = null;
            Data = System.Text.Encoding.Default.GetBytes(FileName);
            IntPtr input = Marshal.AllocHGlobal((I32)Data.Length + 1);
            for (int i = 0; i <= Data.Length; i++)
            {
                Marshal.WriteByte(input, i, 0);
            }
            Marshal.Copy(Data, 0, input, (I32)Data.Length);
            int ret = JLINK_DownloadFile(input, StartAddr);
            Marshal.FreeHGlobal(input);
            if (ret < 0)
                return false;
            else
                return true;
        }

        public static bool JLINKARM_CORESIGHT_Configure(string cmdstr)
        {
            byte[] Data = null;
            Data = System.Text.Encoding.Default.GetBytes(cmdstr);
            IntPtr input = Marshal.AllocHGlobal((I32)Data.Length + 1);
            for (int i = 0; i <= Data.Length; i++)
            {
                Marshal.WriteByte(input, i, 0);
            }
            Marshal.Copy(Data, 0, input, (I32)Data.Length);
            int ret = JLINKARM_CORESIGHT_Configure(input);
            Marshal.FreeHGlobal(input);
            if (ret < 0)
                return false;
            else
                return true;
        }
        public static bool Reset(bool Is_CPU_Reset=true)
        {
            int ret = 1;
            if (Is_CPU_Reset)
            {
                ret = JLINKARM_Reset();
            }
            else
            {
                JLINKARM_ClrRESET();
                Thread.Sleep(1);
                JLINKARM_SetRESET();
                Thread.Sleep(1);
            }
            if (ret < 0)
                return false;
            else
                return true;
        }
        public static bool ReadRegister(UInt32 Address, out UInt32 RegValue)
        {
            byte[] Data;
            bool ret;
            RegValue = 0;
            ret = ReadMemory(Address, 4, out Data);
            if (ret == false)                                 // Error occured
            {
                return false;
            }
            else
            {
                RegValue = (UInt32)((Data[0] << 0) | (Data[1] << 8) | (Data[2] << 16) | (Data[3] << 24));
                return true;
            }

        }

        public static bool ReadRegister(UInt32 Address, out UInt32 RegValue, int StartBit, int EndBit)
        {
            byte[] Data;
            bool ret;
            RegValue = 0;
            ret = ReadMemory(Address, 4, out Data);
            int BitNum = EndBit - StartBit + 1;
            if (ret == false)                                 // Error occured
            {
                return false;
            }
            else
            {
                RegValue = (UInt32)((Data[0] << 0) | (Data[1] << 8) | (Data[2] << 16) | (Data[3] << 24));
                RegValue = (RegValue&WilfDataPro.GetMask(StartBit, EndBit))>>StartBit;
                return true;
            }

        }

        public static bool ReadRegister(UInt32 Address, out UInt32[] RegValue, UInt32 Num)
        {
            byte[] Data;
            bool ret;
            RegValue = new UInt32[Num];
            ret = ReadMemory(Address, 4* Num, out Data);
            if (ret == false)                                 // Error occured
            {
                return false;
            }
            else
            {
                for (int i = 0; i < Num; i++)
                    RegValue[i] = (UInt32)((Data[4*i+0] << 0) | (Data[4 * i + 1] << 8) | (Data[4 * i + 2] << 16) | (Data[4 * i + 3] << 24));
                return true;
            }

        }

        public static bool WriteRegister(UInt32 Address, UInt32 RegValue)
        {
            byte[] Data = new byte[4];
            bool ret;
            Data[0] = (byte)((RegValue >> 0) & 0xFF);
            Data[1] = (byte)((RegValue >> 8) & 0xFF);
            Data[2] = (byte)((RegValue >> 16) & 0xFF);
            Data[3] = (byte)((RegValue >> 24) & 0xFF);
            ret = WritMemory(Address, 4, Data);
            if (ret == false)                                 // Error occured
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool Wait(UInt32 Address, UInt32 Value,int Time,bool Equl = true)
        {
            UInt32 ReadValue;
            ReadRegister(Address, out ReadValue);
            int Counter = 0;
            if (Equl == true)
            {
                while (ReadValue != Value)
                {
                    Thread.Sleep(1);
                    ReadRegister(Address, out ReadValue);
                    Counter++;
                    if (Counter>Time)
                        break;
                }
                return ReadValue == Value;
            }
            else
            {
                while (ReadValue == Value)
                {
                    Thread.Sleep(1);
                    ReadRegister(Address, out ReadValue);
                    Counter++;
                    if (Counter>Time)
                        break;
                }
                return ReadValue != Value;
            }
           
        }

        public static bool WriteRegister(UInt32 Address, UInt32[] RegValue)
        {
            byte[] Data = new byte[4*RegValue.Length];
            bool ret;
            for (int i = 0; i < RegValue.Length; i++)
            {
                Data[4*i + 0] = (byte)((RegValue[i] >> 0) & 0xFF);
                Data[4*i + 1] = (byte)((RegValue[i] >> 8) & 0xFF);
                Data[4*i + 2] = (byte)((RegValue[i] >> 16) & 0xFF);
                Data[4*i + 3] = (byte)((RegValue[i] >> 24) & 0xFF);
            }
            ret = WritMemory(Address, (UInt32)Data.Length, Data);
            if (ret == false)                                 // Error occured
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool WriteRegister(UInt32 Address, UInt32 RegValue, int Startbit, int Endbit)
        {
            byte[] Data = new byte[4];
            bool ret;
            UInt32 Mask;
            UInt32 ReadValue;
            Mask = WilfDataPro.GetMask(Startbit, Endbit);
            ReadRegister(Address, out ReadValue);
            RegValue = (ReadValue & (~Mask)) | (RegValue << Startbit);

            Data[0] = (byte)((RegValue >> 0) & 0xFF);
            Data[1] = (byte)((RegValue >> 8) & 0xFF);
            Data[2] = (byte)((RegValue >> 16) & 0xFF);
            Data[3] = (byte)((RegValue >> 24) & 0xFF);
            ret = WritMemory(Address, 4, Data);
            if (ret == false)                                 // Error occured
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool ReadMemory(UInt32 Address, UInt32 Length, out byte[] Data)
        {
            int ret;
            Data = new byte[Length];

            ret = JLINKARM_ReadMem(Address, Length, Data);
            if (ret < 0)                                 // Error occured
            {
                Debug.AppendMsg("[Error]Failed.\n");
                return false;
            }
            return true;
        }

        public static bool WritMemory(UInt32 Address, UInt32 Length, byte[] Data)
        {
            int ret;
            IntPtr input = Marshal.AllocHGlobal((I32)Length);
            Marshal.Copy(Data, 0, input, (I32)Length);
            ret = JLINKARM_WriteMem(Address, Length, input);
            if (ret < 0)                                 // Error occured
            {
                Debug.AppendMsg("[Error]Failed.\n");
                Marshal.FreeHGlobal(input);
                return false;
            }
            Marshal.FreeHGlobal(input);
            return true;
        }

        public static bool WritMemoryAt(UInt32 Address, UInt32 Length, byte[] Data,int offset)
        {
            int ret;
            IntPtr input = Marshal.AllocHGlobal((I32)Length);
            Marshal.Copy(Data, offset, input, (I32)Length);
            ret = JLINKARM_WriteMem(Address, Length, input);
            if (ret < 0)                                 // Error occured
            {
                Debug.AppendMsg("[Error]Failed.\n");
                Marshal.FreeHGlobal(input);
                return false;
            }
            Marshal.FreeHGlobal(input);
            return true;
        }

        public static bool WritMemory(UInt32 Address, UInt32 Length, byte[] Data, UInt32 AccessWidth)
        {
            int ret;
            IntPtr input = Marshal.AllocHGlobal((I32)Length);
            Marshal.Copy(Data, 0, input, (I32)Length);
            ret = JLINKARM_WriteMemEx(Address, Length, input, AccessWidth);
            if (ret < 0)                                 // Error occured
            {
                Debug.AppendMsg("[Error]Failed.\n");
                Marshal.FreeHGlobal(input);
                return false;
            }
            Marshal.FreeHGlobal(input);
            return true;
        }

        public static bool ErrorPro()
        {
            bool ret = Device.FindJlinkDevice(JlinkOp.DeviceName);
            if (ret == true)
            {
                Debug.AppendMsg("[Inf]Try To Reopen Jlink" + Environment.NewLine, Color.Red);
                Device.Close();
                bool IsJlinkOpen = Device.Open(JlinkOp.Protocol, JlinkOp.SpeedKHz);
                if (IsJlinkOpen == true && JlinkOp.CheckCpuID() == true)
                {
                    JlinkOp.Error = false;
                    return true;

                }
                else
                    return false;

            }
            return false;
        }
        public static void Close()
        {
            JLINKARM_Close();
        }

        public static bool MapMemory(UInt32 SP, UInt32 PC, UInt32 VectorAddr)
        {
            JLINKARM_WriteReg((ARM_REG)13, SP);  //SP
            JLINKARM_WriteReg((ARM_REG)15, PC);  //PC
            WriteRegister(0xE000ED08, VectorAddr);   //06:RAM映射到ROM 04：正常映射
            return true;
        }

        public static CoreStatus GetCoreStatus()
        {
            UInt32 DHCSR = 0xE000EDF0;
            UInt32 Value,NewValue;
            ReadRegister(DHCSR, out Value);
            if ((Value&(1<<25)) > 0)
            {
                ReadRegister(DHCSR, out NewValue);
                if ((NewValue&(1<<25)) > 0 && (NewValue&(1<<24)) == 0)
                    return CoreStatus.TARGET_RESET;
            }

            if ((Value&(1<<17)) > 0)
                return CoreStatus.TARGET_HALTED;
            else if ((Value&(1<<18)) > 0)
                return CoreStatus.TARGET_SLEEPING;
            else if ((Value&(1<<19)) > 0)
                return CoreStatus.TARGET_LOCKUP;
            else
                return CoreStatus.TARGET_RUNNING;
        }
        public static UInt32 CallFunction(UInt32 PC_Value, UInt32 R0, UInt32 R1, UInt32 R2, UInt32 R3, UInt32 lr,bool Wait = false)
        {
            JLINKARM_WriteReg((ARM_REG)15, PC_Value);
            JLINKARM_WriteReg((ARM_REG)0, R0);
            JLINKARM_WriteReg((ARM_REG)1, R1);
            JLINKARM_WriteReg((ARM_REG)2, R2);
            JLINKARM_WriteReg((ARM_REG)3, R3);
            JLINKARM_WriteReg((ARM_REG)14, lr+1);
            JLINKARM_Go();
            if(Wait == true)
            {
                CoreStatus st = GetCoreStatus();
                while (st == CoreStatus.TARGET_RUNNING)
                {
                    st = GetCoreStatus();
                }
                R0 = JLINKARM_ReadReg((ARM_REG)0);
                return R0;
            }
            else
            {
                return 0;
            }
        }

    }
}
