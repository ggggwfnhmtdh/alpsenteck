using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
namespace SOM_DEVELOP_TOOL
{
    public class SOM
    {
        public static UInt32 DebugInDataAddr;
        public static UInt32 DebugOutDataAddr;
        public static int Freq = 12;
        public static int DQS = 0;
        public static int DTR = 0;
        public static int AddrWidth = 1; //1:32bit 0:24bit
        public static int xSPI_Line_Num = 0;
        public static bool NeedUpdateInterface = true;
        public static bool InitProtocol(string MapFile)
        {
            bool ret = true;
            MapParser.InitMapHashTable(MapFile);
            UInt32 StartAddress = MapParser.GetAddr("gpst_pro");
            DebugOutDataAddr =  MapParser.GetAddr("DebugOutData");
            DebugInDataAddr =  MapParser.GetAddr("DebugInData");
            if (StartAddress == UInt32.MaxValue || DebugOutDataAddr == UInt32.MaxValue)
            {
                Debug.AppendMsg("[Error]Map Inf Is Invalid"+Environment.NewLine);
                return false;
            }
            ret = jlink_protocol.Init(StartAddress);
            return ret;
        }

        public static void InitInterface(int Freq,int Interface, int AddressWidth, int DTR_Enable, int DQS_Enable)
        {
            byte[] xSpiCfg = new byte[5] { (byte)Freq , (byte)Interface , (byte)AddressWidth , (byte)DTR_Enable, (byte)DQS_Enable };
            byte[] Data = new byte[] {0};
            if (Interface == 0)
            {
                som_xspi_reg_write(21, 0x21);
                som_xspi_reg_write(22, 0x72);
                
            }
            else
            {
                som_xspi_reg_write(21, 0x10);
            }
           
            Thread.Sleep(10);
            int Temp = som_xspi_reg_read(0x0c);
            Data[0] = (byte)((Interface<<3)|(AddressWidth<<2)|(DTR_Enable<<1)|(DQS_Enable));
            som_xspi_reg_write(0x0c, Data, 1);
            Thread.Sleep(10);
            xSpiCfg[2] = (byte)((AddressWidth==1) ? 4 : 3);
            som_xspi_init(xSpiCfg);
            Thread.Sleep(10);
        }

        public static void InitInterface(byte[] xSpiCfg)
        {
            byte[] Data = new byte[] { 0 };
            int Freq = xSpiCfg[0];
            int Interface = xSpiCfg[1];
            int AddressWidth = xSpiCfg[2];
            int DTR_Enable = xSpiCfg[3];
            int DQS_Enable = xSpiCfg[4];
            if (Interface == 0)
            {
                som_xspi_reg_write(22, 0x72);
                som_xspi_reg_write(21, 0x21);
            }
            else
            {
                som_xspi_reg_write(21, 0x10);
            }
            Thread.Sleep(10);
            Data[0] = (byte)((Interface<<3)|(AddressWidth<<2)|(DTR_Enable<<1)|(DQS_Enable));
            som_xspi_reg_write(0x0c, Data, 1);
            Thread.Sleep(10);
            xSpiCfg[2] = (byte)((AddressWidth==1) ? 4 : 3);
            som_xspi_init(xSpiCfg);
            Thread.Sleep(10);
        }

        public static UInt32 CallAPI(string api_content, bool IsFile = false)
        {
            byte[] DataU8;
            UInt32[] DataU32;
            JlinkProtocolFrame js;
            
            TestCase.GetCase(api_content, IsFile);
            for (int i = 0; i<TestCase.CaseArray.Length; i++)
            {
                if (TestCase.CaseArray[i].IsInner == false)
                {
                    jlink_protocol.AutoTest(TestCase.CaseArray[i].Addr, TestCase.CaseArray[i].ParamValue, out js);
                    if (js.data != null && js.data.Length>0)
                    {
                        //Debug.AppendMsg(TestCase.CaseArray[i].CaseName+","+js.data[0]+Environment.NewLine);
                        return WilfDataPro.GetUint32(js.data,0);
                    }
                }
                else
                {
                    if (TestCase.CaseArray[i].FunName.ToUpper() == "SLEEP")
                    {
                        Thread.Sleep((int)TestCase.CaseArray[i].ParamValue[0]);
                        Debug.AppendMsg(TestCase.CaseArray[i].CaseName+Environment.NewLine);
                    }
                    else if (TestCase.CaseArray[i].FunName.ToUpper() == "SETOVERTIME")
                    {
                        jlink_protocol.SetOverTime((int)TestCase.CaseArray[i].ParamValue[0]);
                        Debug.AppendMsg(TestCase.CaseArray[i].CaseName+Environment.NewLine);
                    }
                    else if (TestCase.CaseArray[i].FunName.ToUpper() == "READMULTMETER")
                    {
                        string PortName;
                        double MultmeterValue;
                        PortName = V86E.FindDevice();
                        if (PortName != "")
                            MultmeterValue = V86E.ReadMultmeter(PortName);
                        else
                            MultmeterValue = 0;
                        Debug.AppendMsg(TestCase.CaseArray[i].CaseName+","+MultmeterValue+Environment.NewLine);
                    }
                    else if (TestCase.CaseArray[i].FunName.ToUpper() == "READU8")
                    {
                        JlinkOp.ReadMemory(TestCase.CaseArray[i].ParamValue[0], TestCase.CaseArray[i].ParamValue[1], out DataU8);
                        Debug.AppendMsg(TestCase.CaseArray[i].CaseName+","+WilfDataPro.ToString(DataU8, "X2", DataU8.Length, "0x")+Environment.NewLine);
                    }
                    else if (TestCase.CaseArray[i].FunName.ToUpper() == "READU32")
                    {
                        JlinkOp.ReadRegister(TestCase.CaseArray[i].ParamValue[0], out DataU32, TestCase.CaseArray[i].ParamValue[1]);
                        Debug.AppendMsg(TestCase.CaseArray[i].CaseName+","+WilfDataPro.ToString(DataU32, "X8", DataU32.Length, "0x")+Environment.NewLine);
                    }
                    else if (TestCase.CaseArray[i].FunName.ToUpper() == "WRITEU8")
                    {
                        JlinkOp.WritMemory(TestCase.CaseArray[i].ParamValue[0], 1, new byte[] { (byte)TestCase.CaseArray[i].ParamValue[1] });
                        Debug.AppendMsg(TestCase.CaseArray[i].CaseName+Environment.NewLine);
                    }
                    else if (TestCase.CaseArray[i].FunName.ToUpper() == "WRITEU32")
                    {
                        JlinkOp.WriteRegister(TestCase.CaseArray[i].ParamValue[0], TestCase.CaseArray[i].ParamValue[1]);
                        Debug.AppendMsg(TestCase.CaseArray[i].CaseName+Environment.NewLine);
                    }

                }
            }
            return 1;
        }

        public static void som_xspi_init(byte[] xSpiCfg)
        {
            JlinkOp.WritMemory(DebugInDataAddr, (UInt32)xSpiCfg.Length, xSpiCfg);
            TestCase.GetCase("som_xspi_init("+DebugInDataAddr+")");
            JlinkProtocolFrame js;
            jlink_protocol.AutoTest(TestCase.CaseArray[0].Addr, TestCase.CaseArray[0].ParamValue, out js);
            if (js.data != null && js.data.Length>0)
            {
                //Debug.AppendMsg(TestCase.CaseArray[0].CaseName+","+js.data[0]+Environment.NewLine);
            }

        }

        public static byte[] som_common_test(byte[] config_data,UInt32 Length,UInt32 OutLength=256)
        {
            byte[] Data;
            JlinkOp.WritMemory(DebugOutDataAddr, (UInt32)config_data.Length, config_data);
            TestCase.GetCase("som_common_test("+DebugOutDataAddr+","+Length+","+OutLength+")");
            JlinkProtocolFrame js;
            jlink_protocol.AutoTest(TestCase.CaseArray[0].Addr, TestCase.CaseArray[0].ParamValue, out js);
            if (js.data != null && js.data.Length>0)
            {
                //Debug.AppendMsg(TestCase.CaseArray[0].CaseName+","+js.data[0]+Environment.NewLine);
            }
            JlinkOp.ReadMemory(DebugOutDataAddr, OutLength, out Data);
            return Data;
        }

        public static void som_xspi_reg_read(UInt32 Addr, out byte[] Data,UInt32 Length )
        {
            TestCase.GetCase("som_xspi_reg_read("+Addr+","+DebugOutDataAddr+","+Length+")");
            JlinkProtocolFrame js;

            jlink_protocol.AutoTest(TestCase.CaseArray[0].Addr, TestCase.CaseArray[0].ParamValue, out js);
            if (js.data != null && js.data.Length>0)
            {
                //Debug.AppendMsg(TestCase.CaseArray[0].CaseName+","+js.data[0]+Environment.NewLine);
            }
            JlinkOp.ReadMemory(DebugOutDataAddr, Length, out Data);
        }

        public static byte[] som_xspi_reg_read(UInt32 Addr, UInt32 Length)
        {
            byte[] Data;
            TestCase.GetCase("som_xspi_reg_read("+Addr+","+DebugOutDataAddr+","+Length+")");
            JlinkProtocolFrame js;

            jlink_protocol.AutoTest(TestCase.CaseArray[0].Addr, TestCase.CaseArray[0].ParamValue, out js);
            if (js.data != null && js.data.Length>0)
            {
                //Debug.AppendMsg(TestCase.CaseArray[0].CaseName+","+js.data[0]+Environment.NewLine);
            }
            JlinkOp.ReadMemory(DebugOutDataAddr, Length, out Data);
            return Data;
        }

        public static void som_xspi_reg_write(UInt32 Addr, byte Value)
        {
            UInt32 Length = 1;
            byte[] Data = new byte[1];
            Data[0] = Value;
            TestCase.GetCase("som_xspi_reg_write("+Addr+","+DebugInDataAddr+","+Length+")");
            JlinkProtocolFrame js;
            JlinkOp.WritMemory(DebugInDataAddr, Length, Data);
            jlink_protocol.AutoTest(TestCase.CaseArray[0].Addr, TestCase.CaseArray[0].ParamValue, out js);
            if (js.data != null && js.data.Length>0)
            {
                //Debug.AppendMsg(TestCase.CaseArray[0].CaseName+","+js.data[0]+Environment.NewLine);
            }
            
        }

        public static byte som_xspi_reg_read(UInt32 Addr)
        {
            UInt32 Length = 1;
            byte[] Data = new byte[1];
            TestCase.GetCase("som_xspi_reg_read("+Addr+","+DebugOutDataAddr+","+Length+")");
            JlinkProtocolFrame js;

            jlink_protocol.AutoTest(TestCase.CaseArray[0].Addr, TestCase.CaseArray[0].ParamValue, out js);
            if (js.data != null && js.data.Length>0)
            {
                //Debug.AppendMsg(TestCase.CaseArray[0].CaseName+","+js.data[0]+Environment.NewLine);
            }
            JlinkOp.ReadMemory(DebugOutDataAddr, Length, out Data);
            return Data[0]; 
        }

        public static void som_xspi_reg_write(UInt32 Addr, byte[] Data, UInt32 Length)
        {
            TestCase.GetCase("som_xspi_reg_write("+Addr+","+DebugInDataAddr+","+Length+")");
            JlinkProtocolFrame js;
            JlinkOp.WritMemory(DebugInDataAddr, Length, Data);
            jlink_protocol.AutoTest(TestCase.CaseArray[0].Addr, TestCase.CaseArray[0].ParamValue, out js);
            if (js.data != null && js.data.Length>0)
            {
                //Debug.AppendMsg(TestCase.CaseArray[0].CaseName+","+js.data[0]+Environment.NewLine);
            }
        }

        public static byte[] DumpRegister(RegBitData[] Reg,bool UpDateDefaultValue,bool UpDateRecordValue=false)
        {
            byte[] Data;
            som_xspi_reg_read(0, out Data, 24);
            for(int i=0;i<Reg.Length;i++)
            {
                Reg[i].Value = WilfDataPro.GetBit(Data, Reg[i].Addr, Reg[i].StartBit, Reg[i].BitWidth);
                if (UpDateDefaultValue)
                    Reg[i].DefaultValue =  Reg[i].Value;
                if (UpDateRecordValue)
                    Reg[i].RecordValue =  Reg[i].Value;
            }
            return Data;
        }

        public static void som_xspi_reg_write_bit(UInt32 Addr, UInt32 StartBit, UInt32 size,UInt32 Value)
        {
            byte[] Data;
            UInt32 MASK = WilfDataPro.GetMask((int)StartBit, (int)(StartBit+size-1));
            som_xspi_reg_read(Addr, out Data, 1);
            Data[0] = (byte)((Data[0]&(~MASK))|(Value<<(int)StartBit));
            som_xspi_reg_write(Addr, Data, 1);
        }

        public static UInt32 som_xspi_reg_read_bit(UInt32 Addr, UInt32 StartBit, UInt32 size)
        {
            byte[] Data;
            UInt32 MASK = WilfDataPro.GetMask((int)StartBit, (int)(StartBit+size-1));
            som_xspi_reg_read(Addr, out Data, 1);
            return (Data[0]&MASK)>>(int)StartBit;
        }

        public static bool Test(RegTestType Type, string Dir, int RefPatternNum = 10)
        {
            int TestTime;
            byte[] Data;
            UInt32 wData, rData;
            bool ret = false;
            RegBitData Reg;
            StringBuilder sb = new StringBuilder();

            if (Type == RegTestType.Default)
            {
                Debug.AppendMsg("********************************************"+Environment.NewLine);
                Debug.AppendMsg("Title:Test Default Value "+Environment.NewLine);
                DumpRegister(SomReg.RegBitList, false);
                ret = SomReg.CheckDefultValue(SomReg.RegBitList);
                File.WriteAllText(Dir+"\\DefaultValue.csv", SomReg.ToString(SomReg.RegBitList), Encoding.UTF8);
                Debug.AppendMsg("ret="+ret+Environment.NewLine);
                Debug.AppendMsg("--------------------------------------------"+Environment.NewLine);
            }
            else if (Type == RegTestType.RW)
            {
                Debug.AppendMsg("********************************************"+Environment.NewLine);
                Debug.AppendMsg("Title:Test RW "+Environment.NewLine);

                sb.Clear();
                sb.Append("RegName,Addr,StartBit,BitWidth,Write,Read,Result"+Environment.NewLine);
                ret = true;
                DumpRegister(SomReg.RegBitList, false, true);
                for (int i = 0; i<SomReg.RegBitList.Length; i++)
                {
                    Reg = SomReg.RegBitList[i];
                    if (Reg.Attribute == "RW" && Reg.Option == true)
                    {
                        TestTime = Math.Min(RefPatternNum, 1<<(int)Reg.BitWidth);
                        for (int j = 0; j<TestTime; j++)
                        {
                            if (Reg.RegName == "drive_str")
                                wData = (UInt32)(j%5);
                            else
                            {
                                if (TestTime < RefPatternNum)
                                    wData = (UInt32)j;
                                else
                                    wData = SomReg.GetRevertBit(Reg.BitWidth, j);
                            }
                            som_xspi_reg_write_bit(Reg.Addr, Reg.StartBit, Reg.BitWidth, wData);
                            SomReg.RegBitList[i].RecordValue = wData;
                            rData = som_xspi_reg_read_bit(Reg.Addr, Reg.StartBit, Reg.BitWidth);

                            if (wData != rData)
                            {
                                ret = false;
                                Debug.AppendMsg(Reg.RegName + ",0x" + Reg.Addr.ToString("X")+","+Reg.StartBit+","+Reg.BitWidth+","+wData+ ","+rData+Environment.NewLine);
                            }
                            sb.Append(Reg.RegName + ",0x" + Reg.Addr.ToString("X")+","+Reg.StartBit+","+Reg.BitWidth+",0x"+wData.ToString("X")+ ",0x"+rData.ToString("X")+","+"wData == rData"+Environment.NewLine);
                        }
                    }
                }
                Debug.AppendMsg("ret="+ret+Environment.NewLine);
                File.WriteAllText(Dir+"\\RW.csv", sb.ToString(), Encoding.UTF8);

                Debug.AppendMsg("--------------------------------------------"+Environment.NewLine);
            }
            else if (Type == RegTestType.Crosstalk)
            {
                Debug.AppendMsg("********************************************"+Environment.NewLine);
                Debug.AppendMsg("Title:Test Crosstalk "+Environment.NewLine);

                sb.Clear();
                sb.Append("RegName,Addr,StartBit,BitWidth,Write,Read,Result"+Environment.NewLine);
                ret = true;
                DumpRegister(SomReg.RegBitList, false, true);
                for (int i = 0; i<SomReg.RegBitList.Length; i++)
                {
                    Reg = SomReg.RegBitList[i];
                    if (Reg.Attribute == "RW" && Reg.Option == true)
                    {
                        TestTime = Math.Min(RefPatternNum, 1<<(int)Reg.BitWidth);
                        for (int j = 0; j<TestTime; j++)
                        {
                            if (Reg.RegName == "drive_str")
                                wData = (UInt32)(j%5);
                            else
                            {
                                if (TestTime < RefPatternNum)
                                    wData = (UInt32)j;
                                else
                                    wData = SomReg.GetRevertBit(Reg.BitWidth, j);
                            }
                            som_xspi_reg_write_bit(Reg.Addr, Reg.StartBit, Reg.BitWidth, wData);
                            SomReg.RegBitList[i].RecordValue = wData;
                            rData = som_xspi_reg_read_bit(Reg.Addr, Reg.StartBit, Reg.BitWidth);

                            DumpRegister(SomReg.RegBitList, true);
                            bool retx = SomReg.CheckRecordValue(SomReg.RegBitList);
                            if (retx == false)
                            {
                                ret = false;
                            }
                            sb.Append(Reg.RegName + ",0x" + Reg.Addr.ToString("X")+","+Reg.StartBit+","+Reg.BitWidth+",0x"+wData.ToString("X")+ ",0x"+rData.ToString("X")+","+retx+Environment.NewLine);
                        }
                    }
                }
                Debug.AppendMsg("ret="+ret+Environment.NewLine);
                File.WriteAllText(Dir+"\\Crosstalk.csv", sb.ToString(), Encoding.UTF8);
                Debug.AppendMsg("--------------------------------------------"+Environment.NewLine);
            }
            else if (Type == RegTestType.Attribute)
            {
                Debug.AppendMsg("********************************************"+Environment.NewLine);
                Debug.AppendMsg("Title:Test Attribute "+Environment.NewLine);
                sb.Clear();
                sb.Append("RegName,Addr,StartBit,BitWidth,Write,Read,Result"+Environment.NewLine);
                ret = true;
                for (int i = 0; i<SomReg.RegBitList.Length; i++)
                {
                    Reg = SomReg.RegBitList[i];
                    if (Reg.Attribute == "RO" && Reg.Option == true)
                    {
                        wData = (~Reg.DefaultValue)&(WilfDataPro.GetMask((int)Reg.StartBit, (int)Reg.EndBit)>>(int)Reg.StartBit);
                        som_xspi_reg_write_bit(Reg.Addr, Reg.StartBit, Reg.BitWidth, wData);
                        rData = som_xspi_reg_read_bit(Reg.Addr, Reg.StartBit, Reg.BitWidth);
                        if (Reg.DefaultValue != rData)
                        {
                            ret = false;
                            Debug.AppendMsg(Reg.RegName + ",0x" + Reg.Addr.ToString("X")+","+Reg.StartBit+","+Reg.BitWidth+","+wData+ ","+rData+Environment.NewLine);
                        }
                        sb.Append(Reg.RegName + ",0x" + Reg.Addr.ToString("X")+","+Reg.StartBit+","+Reg.BitWidth+",0x"+wData.ToString("X")+ ",0x"+rData.ToString("X")+","+(rData == Reg.DefaultValue)+Environment.NewLine);

                    }
                }
                Debug.AppendMsg("ret="+ret+Environment.NewLine);
                File.WriteAllText(Dir+"\\Attribute.csv", sb.ToString(), Encoding.UTF8);
                Debug.AppendMsg("--------------------------------------------"+Environment.NewLine);
            }
            return ret;
        }

    }
}
