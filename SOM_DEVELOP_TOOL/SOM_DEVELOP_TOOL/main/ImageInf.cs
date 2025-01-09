using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using EllipticCurve;
using System.Security.Cryptography;

namespace SOM_DEVELOP_TOOL
{
    public struct BootType
    {
        public byte[] BootTag;
        public byte[] PublicKey;
        public byte[] ImageHeader;
        public byte[] NA;
        public string BootTagString;
    };

    public struct ImageType
    {
        public UInt32 start;
        public UInt32 size;
        public UInt32 exeAddr;
        public UInt32 directRunFlag;
        public byte[] hash;
        public byte[] r;
        public byte[] s;
    };
    public class ImageOp
    {
        public static int HeaderSize = 4*1024;
        public static string tag_str = "Innostar-Ipc-boot-rom";
       

        public static byte[] GenerageImage(string[] HexFiles)
        {
            byte[] sk, pk, k = null;
            bool ret;
            UInt32 StartAddr = 0x1000;
            string key_file = "key.txt";
            bool OpenDebugLog = true;
            BinDataType BinData = HexData.HexToBin(HexFiles);
            byte[] ImageBin;
            byte[] hash_data;
            if (File.Exists(key_file))
            {
                SE.LoadKey(key_file, out pk, out sk, out k);
                ret = SE.CheckKey(sk, pk);
                if (ret == false)
                {
                    Debug.AppendMsg("[Error]The sk/pk loaded from file is not valid"+Environment.NewLine);
                    return null;
                }
                else
                {
                    Debug.AppendMsg("[Inf]Load Key From File OK"+Environment.NewLine);
                }
            }
            else
            {
                SE.GenerateKeyPair(out sk, out pk, out k);
                Debug.AppendMsg("[Inf]Try To Generate Key"+Environment.NewLine);
            }
            if (StartAddr == 0xFFFFFFFF)
                StartAddr = 0x1000;
            List<byte[]> KeyData = new List<byte[]>();
            KeyData.Add(sk); KeyData.Add(pk); KeyData.Add(k);
            ret = SE.CheckKey(sk, pk);
            if (OpenDebugLog)
            {
                Debug.AppendMsg("[Inf]sk="+WilfDataPro.ToString(sk, "X2", 64, "0x")+Environment.NewLine);
                Debug.AppendMsg("[Inf]pk="+WilfDataPro.ToString(pk, "X2", 64, "0x")+Environment.NewLine);
                Debug.AppendMsg("[Inf]k ="+WilfDataPro.ToString(k, "X2", 64, "0x")+Environment.NewLine);
            }
            if (ret == true)
            {
                Debug.AppendMsg("[Inf]Sign FW And Generate Image File"+Environment.NewLine);
                ImageBin = ImageOp.GenerateImage(StartAddr, 0xFFFFFFFF, BinData.Data, KeyData, out hash_data);
            }
            else
            {
                Debug.AppendMsg("[Error]The sk/pk generate by code is not valid"+Environment.NewLine);
                return null;
            }
            if (OpenDebugLog)
            {
                Debug.AppendMsg("[Inf]hash_data="+WilfDataPro.ToString(hash_data, "X2", 64, "0x")+Environment.NewLine);
            }
            ret = ImageOp.VeryfyImage(ImageBin);

            if (ret == true)
            {
                Debug.AppendMsg("[Inf]Verify Image PASS"+Environment.NewLine);
            }
            else
            {
                Debug.AppendMsg("[Error]Verify Image Fail"+Environment.NewLine);
                return null;
            }
            return ImageBin;
        }

        public static byte[] GenerateImage(UInt32 StartAddr, UInt32 ExeAddr, byte[] FwBin, List<byte[]> KeyData, out byte[] hash_data)
        {
            byte[] PublicKey, Privatekey, k;
            byte[] tag_data;
            ImageType ImageHeader = new ImageType();
            byte[] ImageBin;
            byte[] FwBinPatch;
            int FillDataLen = 0;
            UInt32 FwPatchLength;
            Privatekey = KeyData[0];
            PublicKey = KeyData[1];
            k = KeyData[2];
            if ((FwBin.Length%256) !=0)
                FillDataLen = 256 - FwBin.Length%256;
            FwPatchLength = (UInt32)(FwBin.Length+ FillDataLen);
            ImageBin = new byte[HeaderSize +FwPatchLength];
            FwBinPatch = new byte[FwPatchLength];
            WilfDataPro.Clear(ImageBin, 0, HeaderSize, 0xFF);
            Array.Copy(FwBin, 0, ImageBin, HeaderSize, FwBin.Length);
            WilfDataPro.Clear(ImageBin, HeaderSize+FwBin.Length, FillDataLen, 0xFF);
            Array.Copy(ImageBin, HeaderSize, FwBinPatch, 0, FwBinPatch.Length);

            tag_data = Encoding.Default.GetBytes(tag_str);
            hash_data = SE.SHA256(FwBinPatch);
            Signature sig = SE.SignFw(FwBinPatch, Privatekey, k);
            ImageHeader.start = StartAddr;
            ImageHeader.size = FwPatchLength;
            ImageHeader.exeAddr = ExeAddr;
            ImageHeader.directRunFlag = 0x4452554E;
            ImageHeader.hash = Ecdsa.sha256(FwBinPatch);
            ImageHeader.r =  SE.RemoveZero(sig.r.ToByteArray());
            ImageHeader.s =  SE.RemoveZero(sig.s.ToByteArray());

            Set(ImageBin, 0, tag_data);
            Set(ImageBin, 256, PublicKey);
            Set(ImageBin, 2*256+0, ImageHeader.start);
            Set(ImageBin, 2*256+4, ImageHeader.size);
            Set(ImageBin, 2*256+8, ImageHeader.exeAddr);
            Set(ImageBin, 2*256+12, ImageHeader.directRunFlag);
            Set(ImageBin, 2*256+16, WilfDataPro.Revert(ImageHeader.hash));
            Set(ImageBin, 2*256+48, ImageHeader.r);
            Set(ImageBin, 2*256+80, ImageHeader.s);

            return ImageBin;
        }

        public static void Set(byte[] bin, int index, byte[] in_data)
        {
            for (int i = 0; i < in_data.Length; i++)
            {
                bin[i+index] = in_data[i];
            }
        }

        public static void Set(byte[] bin, int index, UInt32 in_data)
        {
            bin[index+0] = (byte)(in_data >> 0);
            bin[index+1] = (byte)(in_data >> 8);
            bin[index+2] = (byte)(in_data >> 16);
            bin[index+3] = (byte)(in_data >> 24);
        }

        public static bool VeryfyImage(byte[] data)
        {
            byte[] calc_fw_hash;
            bool ret;
            StringBuilder Msg = new StringBuilder();
            BootType BootHeader = new BootType();
            ImageType ImageHeader = new ImageType();
            byte[] FwBin;
            BootHeader.BootTag = new byte[256];
            BootHeader.PublicKey = new byte[256];
            BootHeader.ImageHeader = new byte[256];
            BootHeader.NA = new byte[0xD00];
            for (int i = 0; i < 256; i++)
            {
                BootHeader.BootTag[i] = data[0+i];
                BootHeader.PublicKey[i] = data[1*256+i];
                BootHeader.ImageHeader[i] = data[2*256+i];
            }

            ImageHeader.start = WilfDataPro.GetUint32(BootHeader.ImageHeader, 0);
            ImageHeader.size = WilfDataPro.GetUint32(BootHeader.ImageHeader, 4);
            ImageHeader.exeAddr = WilfDataPro.GetUint32(BootHeader.ImageHeader, 8);
            ImageHeader.directRunFlag = WilfDataPro.GetUint32(BootHeader.ImageHeader, 12);
            ImageHeader.hash = new byte[32];
            ImageHeader.r = new byte[32];
            ImageHeader.s = new byte[32];

            for (int i = 0; i < 32; i++)
            {
                ImageHeader.hash[i] = BootHeader.ImageHeader[16+i+0];
                ImageHeader.r[i] = BootHeader.ImageHeader[16+i+1*32];
                ImageHeader.s[i] = BootHeader.ImageHeader[16+i+2*32];
            }
            FwBin = new byte[ImageHeader.size];
            Array.Copy(data, ImageHeader.start, FwBin, 0, FwBin.Length);
            calc_fw_hash = Ecdsa.sha256(FwBin);
            ret = WilfDataPro.CmparaArray(calc_fw_hash, ImageHeader.hash);
            if (ret == true)
            {
                Debug.AppendMsg("[Inf]Check FW Sha256  Pass" + Environment.NewLine);
            }
            Signature sig = new Signature(new BigInteger(SE.AddZero(ImageHeader.r)), new BigInteger(SE.AddZero(ImageHeader.s)));
            ret = SE.VerifyFw(FwBin, sig, BootHeader.PublicKey);
            return ret;
        }

        public static void ParseImageHeader(byte[] data,out ImageType ImageHeader,out BootType BootHeader)
        {
            BootHeader = new BootType();
            ImageHeader = new ImageType();
            BootHeader.BootTag = new byte[256];
            BootHeader.PublicKey = new byte[256];
            BootHeader.ImageHeader = new byte[256];
            BootHeader.NA = new byte[0xD00];
            for (int i = 0; i < 256; i++)
            {
                BootHeader.BootTag[i] = data[0+i];
                BootHeader.PublicKey[i] = data[1*256+i];
                BootHeader.ImageHeader[i] = data[2*256+i];
            }

            ImageHeader.start = WilfDataPro.GetUint32(BootHeader.ImageHeader, 0);
            ImageHeader.size = WilfDataPro.GetUint32(BootHeader.ImageHeader, 4);
            ImageHeader.exeAddr = WilfDataPro.GetUint32(BootHeader.ImageHeader, 8);
            ImageHeader.directRunFlag = WilfDataPro.GetUint32(BootHeader.ImageHeader, 12);
            ImageHeader.hash = new byte[32];
            ImageHeader.r = new byte[32];
            ImageHeader.s = new byte[32];

            for (int i = 0; i < 32; i++)
            {
                ImageHeader.hash[i] = BootHeader.ImageHeader[16+i+0];
                ImageHeader.r[i] = BootHeader.ImageHeader[16+i+1*32];
                ImageHeader.s[i] = BootHeader.ImageHeader[16+i+2*32];
            }
            BootHeader.BootTagString = System.Text.Encoding.GetEncoding(1252).GetString(BootHeader.BootTag, 0, 21);
        }
        public static bool ParseImage(byte[] data,out string OutMsg)
        {
            byte[] calc_fw_hash;
            byte[] FwBin;
            bool ret;
            bool result = true;
            StringBuilder Msg = new StringBuilder();
            BootType BootHeader = new BootType();
            ImageType ImageHeader = new ImageType();

            BootHeader.BootTag = new byte[256];
            BootHeader.PublicKey = new byte[256];
            BootHeader.ImageHeader = new byte[256];
            BootHeader.NA = new byte[0xD00];
            for (int i = 0; i < 256; i++)
            {
                BootHeader.BootTag[i] = data[0+i];
                BootHeader.PublicKey[i] = data[1*256+i];
                BootHeader.ImageHeader[i] = data[2*256+i];
            }

            ImageHeader.start = WilfDataPro.GetUint32(BootHeader.ImageHeader, 0);
            ImageHeader.size = WilfDataPro.GetUint32(BootHeader.ImageHeader, 4);
            ImageHeader.exeAddr = WilfDataPro.GetUint32(BootHeader.ImageHeader, 8);
            ImageHeader.directRunFlag = WilfDataPro.GetUint32(BootHeader.ImageHeader, 12);
            ImageHeader.hash = new byte[32];
            ImageHeader.r = new byte[32];
            ImageHeader.s = new byte[32];

            for (int i = 0; i < 32; i++)
            {
                ImageHeader.hash[i] = BootHeader.ImageHeader[16+i+0];
                ImageHeader.r[i] = BootHeader.ImageHeader[16+i+1*32];
                ImageHeader.s[i] = BootHeader.ImageHeader[16+i+2*32];
            }
            FwBin = new byte[ImageHeader.size];
            Array.Copy(data, ImageHeader.start, FwBin, 0, FwBin.Length);
            calc_fw_hash = Ecdsa.sha256(FwBin);
            ret = WilfDataPro.CmparaArray(calc_fw_hash, WilfDataPro.Revert(ImageHeader.hash));
            if (ret == true)
            {
                Msg.Append("[Inf]Check FW Sha256  Pass" + Environment.NewLine);
            }
            else
            {
                Msg.Append("[Inf]Check FW Sha256  Fail" + Environment.NewLine);
                result = false;
            }

            Signature sig = new Signature(new BigInteger(SE.AddZero(ImageHeader.r)), new BigInteger(SE.AddZero(ImageHeader.s)));
            ret = SE.VerifyFw(FwBin, sig, BootHeader.PublicKey);
            if (ret == true)
            {
                Msg.Append("[Inf]Verify FW Signature Pass With ECDSA" + Environment.NewLine);
            }
            else
            {
                Msg.Append("[Inf]Verify FW Signature Fail With ECDSA" + Environment.NewLine);
                result = false;
            }
            Msg.Append("BootTag:" + Environment.NewLine + System.Text.Encoding.GetEncoding(1252).GetString(BootHeader.BootTag, 0, 21) + Environment.NewLine);
            Msg.Append("PublicKey:" + Environment.NewLine + WilfDataPro.ToString(BootHeader.PublicKey, "X2", 16, "0x")+ Environment.NewLine);
            Msg.Append("ImageHeader:"  + Environment.NewLine + WilfDataPro.ToString(BootHeader.ImageHeader, "X2", 16, "0x")+ Environment.NewLine);

            Msg.Append("start:0x" + ImageHeader.start.ToString("X8") + Environment.NewLine);
            Msg.Append("size:" + ImageHeader.size.ToString() + Environment.NewLine);
            Msg.Append("exeAddr:0x" + ImageHeader.exeAddr.ToString("X8") + Environment.NewLine);
            Msg.Append("directRunFlag:0x" + ImageHeader.directRunFlag.ToString("X8") + Environment.NewLine);
            Msg.Append("hash:"  + Environment.NewLine + WilfDataPro.ToString(ImageHeader.hash, "X2", 64, "0x")+ Environment.NewLine);
            Msg.Append("r:"  + Environment.NewLine + WilfDataPro.ToString(ImageHeader.r, "X2", 64, "0x")+ Environment.NewLine);
            Msg.Append("s:"  + Environment.NewLine + WilfDataPro.ToString(ImageHeader.s, "X2", 64, "0x")+ Environment.NewLine);
            OutMsg = Msg.ToString();
            return result;
        }

    }
}
