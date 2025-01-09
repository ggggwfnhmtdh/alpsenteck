using SOM_DEVELOP_TOOL;
using ELFSharp.ELF;
using ELFSharp.ELF.Sections;
using ELFSharp.UImage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Reflection;

public struct FlashSectors
{
    public UInt32 szSector;    // Sector Size in Bytes
    public UInt32 AddrSector;    // Address of Sector
};

public struct FlashDevice
{
    public ushort Vers;    // Version Number and Architecture
    public byte[] DevName;    // Device Name and Description
    public ushort DevType;    // Device Type: ONCHIP, EXT8BIT, EXT16BIT, ...
    public UInt32 DevAdr;    // Default Device Start Address
    public UInt32 szDev;    // Total Size of Device
    public UInt32 szPage;    // Programming Page Size
    public UInt32 Res;    // Reserved for future Extension
    public UInt32 valEmpty;    // Content of Erased Memory

    public UInt32 toProg;    // Time Out of Program Page Function
    public UInt32 toErase;    // Time Out of Erase Sector Function

    public FlashSectors[] sectors;
};

public struct FunctionInf
{
    public string Name;
    public UInt32 Offset;
    public UInt32 Addr;    // Sector Size in Bytes
    public UInt32 Size;    // Address of Sector
};

namespace SOM_DEVELOP_TOOL
{
    public class FLM
    {
        public UInt32 Stack_Addr;
        public UInt32 Static_Base;
        public UInt32 IRAM_StartAddr;
        public UInt32 IRAM_Buffer_Addr;
        public UInt32 IRAM_Buffer_Size;
        public string[] FunName = new string[] { "Init", "UnInit", "EraseChip", "EraseSector", "ProgramPage", "Verify", "BlankCheck", "Read" };
        public Hashtable FunTable = new Hashtable();
        public FunctionInf[] m_FunInf;
        public FlashDevice m_FlashInf;
        public byte[] CodeData;
        public UInt32[] CodeData32;
        public static string AlgoFile = @".\File\Apollo4.FLM";
        public static UInt32 AlgoLoadAddr = 0x10000000;

        public UInt32 GetFunAddr(string Name)
        {
            for(int i = 0; i < m_FunInf.Length; i++)
            {
                if (m_FunInf[i].Name.ToLower() == Name.ToLower())
                {
                    return m_FunInf[i].Addr;
                }
            }
            return 0;
        }
        public FLM(string FlmFile, UInt32 IRAM_Addr,bool IsAppollo=false)
        {
            byte[] PrgCode = null, PrgData = null, DataBin = null;
            byte[] Vector = null;
            List<FunctionInf> FunList = new List<FunctionInf>();
            m_FlashInf = new FlashDevice();
            IRAM_StartAddr = IRAM_Addr;
            Vector = WilfDataPro.ToByte(GetVector());

            for (int i = 0; i < FunName.Length; i++)
            {
                FunTable.Add(FunName[i], i);
            }

            IELF elf = ELFReader.Load(FlmFile);
            foreach (var header in elf.Sections)
            {
                //Debug.AppendMsg(header.Name, true);
                if (header.Name.ToUpper() == "PrgCode".ToUpper())
                {
                    PrgCode = header.GetContents();
                }
                if (header.Name.ToUpper() == "PrgData".ToUpper())
                {
                    PrgData = header.GetContents();
                }
            }


            var functions = ((ISymbolTable)elf.GetSection(".symtab")).Entries.Where(x => x.Type == SymbolType.Function);
            foreach (var f in functions)
            {
                FunctionInf fun = new FunctionInf();
                fun.Name = f.Name;
                fun.Offset = ((SymbolEntry<UInt32>)f).Value;
                fun.Size = ((SymbolEntry<UInt32>)f).Size;
                if (FunTable.ContainsKey(fun.Name))
                {
                    FunTable[fun.Name] = FunList.Count;
                    FunList.Add(fun);
                }
            }

            CodeData = new byte[Vector.Length+PrgCode.Length+PrgData.Length];
            Array.Copy(Vector, 0, CodeData, 0, Vector.Length);
            Array.Copy(PrgCode, 0, CodeData, Vector.Length, PrgCode.Length);
            Array.Copy(PrgData, 0, CodeData, Vector.Length+PrgCode.Length, PrgData.Length);
            CodeData32 = WilfDataPro.ToUint32(CodeData);


            var CfgData = ((ISymbolTable)elf.GetSection(".symtab")).Entries.Where(x => x.Type == SymbolType.Object);
            foreach (var Cfg in CfgData)
            {
                if (Cfg.Name.ToUpper() == "FlashDevice".ToUpper())
                {
                    DataBin = Cfg.PointedSection.GetContents();
                    m_FlashInf = Byte2FlashDevice(DataBin);
                }
            }
           

            IRAM_Buffer_Addr = IRAM_Addr + (UInt32)CodeData.Length;
            IRAM_Buffer_Size = m_FlashInf.szPage;
            Static_Base = IRAM_Addr + (UInt32)Vector.Length + (UInt32)PrgCode.Length;    

            Stack_Addr = IRAM_Buffer_Addr + 2*IRAM_Buffer_Size;

            m_FunInf = FunList.ToArray();
            for (int i = 0; i < m_FunInf.Length; i++)
            {
                m_FunInf[i].Addr = IRAM_StartAddr +(UInt32)Vector.Length+ m_FunInf[i].Offset;
            }
#if false
            Debug.AppendMsg("*************************************************m_FlashInf", true);
            Debug.AppendMsg("IRAM_Addr:0x" + IRAM_Addr.ToString("X8"), true);
            Debug.AppendMsg("CodeData.Length:0x" + CodeData.Length.ToString("X8"), true);
            Debug.AppendMsg("Static_Base:0x" + Static_Base.ToString("X8"), true);
            Debug.AppendMsg("IRAM_Buffer_Addr:0x" + IRAM_Buffer_Addr.ToString("X8"), true);
            Debug.AppendMsg("Stack_Addr:0x" + Stack_Addr.ToString("X8"), true);
            


            Debug.AppendMsg("*************************************************m_FlashInf", true);
            Debug.AppendMsg("m_FlashInf.Vers:0x" + m_FlashInf.Vers.ToString("X4"), true);
            Debug.AppendMsg("m_Flash[Inf]DevName" + Encoding.UTF8.GetString(m_FlashInf.DevName), true);
            Debug.AppendMsg("m_FlashInf.DevType:0x" + m_FlashInf.DevType.ToString("X2"), true);
            Debug.AppendMsg("m_FlashInf.DevAdr:0x" + m_FlashInf.DevAdr.ToString("X8"), true);
            Debug.AppendMsg("m_FlashInf.szDev:0x" + m_FlashInf.szDev.ToString("X8"), true);

            Debug.AppendMsg("m_FlashInf.szPage:0x" + m_FlashInf.szPage.ToString("X8"), true);
            Debug.AppendMsg("m_FlashInf.Res:" + m_FlashInf.Res.ToString("X8"), true);
            Debug.AppendMsg("m_FlashInf.valEmpty:" + m_FlashInf.valEmpty.ToString("X8"), true);
            Debug.AppendMsg("m_FlashInf.toProg:" + m_FlashInf.toProg.ToString(), true);
            Debug.AppendMsg("m_FlashInf.toProg:" + m_FlashInf.toErase.ToString(), true);
            Debug.AppendMsg("m_FlashInf.sectors[0].szSector:0x" + m_FlashInf.sectors[0].szSector.ToString("X8"), true);

            Debug.AppendMsg("*************************************************CodeData32", true);
            Debug.AppendMsg(WilfDataPro.ToString(CodeData32, "X8", 8, "0x"), true);
            Debug.AppendMsg("*************************************************m_FunInf", true);
            for (int i = 0; i<m_FunInf.Length; i++)
                Debug.AppendMsg(m_FunInf[i].Name+":0x"+m_FunInf[i].Addr.ToString("X8")+":0x"+m_FunInf[i].Size.ToString("X8")+":"+m_FunInf[i].Size.ToString(), true);
#endif
        }

        public FlashDevice Byte2FlashDevice(byte[] InData)
        {
            int Index = 0;
            FlashDevice flashDevice = new FlashDevice();
            flashDevice.DevName = new byte[128];
            flashDevice.sectors = new FlashSectors[512];

            flashDevice.Vers = WilfDataPro.GetUint16(InData, 0);
            Index+=2;
            Array.Copy(InData, Index, flashDevice.DevName, 0, flashDevice.DevName.Length);
            Index+= flashDevice.DevName.Length;
            flashDevice.DevType = WilfDataPro.GetUint16(InData, Index);
            Index+=2;
            flashDevice.DevAdr = WilfDataPro.GetUint32(InData, Index);
            Index+=4;
            flashDevice.szDev = WilfDataPro.GetUint32(InData, Index);
            Index+=4;
            flashDevice.szPage = WilfDataPro.GetUint32(InData, Index);
            Index+=4;
            flashDevice.Res = WilfDataPro.GetUint32(InData, Index);
            Index+=4;
            flashDevice.valEmpty = WilfDataPro.GetUint32(InData, Index);
            Index+=4;
            flashDevice.toProg = WilfDataPro.GetUint32(InData, Index);
            Index+=4;
            flashDevice.toErase = WilfDataPro.GetUint32(InData, Index);
            Index+=4;

            for (int i = 0; i<flashDevice.sectors.Length; i++)
            {
                flashDevice.sectors[i].szSector =  WilfDataPro.GetUint32(InData, Index);
                Index+=4;
                flashDevice.sectors[i].AddrSector =  WilfDataPro.GetUint32(InData, Index);
                Index+=4;
            }
            return flashDevice;
        }

        public UInt32[] GetVector()
        {
            UInt32[] Vector;
            
                Vector = new UInt32[14];
                Vector[0] = 0xE00ABE00;
                Vector[1] = 0x062D780D;
                Vector[2] = 0x24084068;
                Vector[3] = 0xD3000040;
                Vector[4] = 0x1E644058;
                Vector[5] = 0x1C49D1FA;
                Vector[6] = 0x2A001E52;
                Vector[7] = 0x4770D1F2;

                Vector[8] = 0x7803E005;
                Vector[9] = 0x42931C40;
                Vector[10] = 0x2001D001;
                Vector[11] = 0x1E494770;
                Vector[12] = 0x2000D2F7;
                Vector[13] = 0x00004770;
            
            return Vector;
        }
    }
}

