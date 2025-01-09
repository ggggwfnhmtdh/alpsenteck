
using System;
using System.Numerics;
using System.Collections.Generic;
using System.IO;
using EllipticCurve;
using System.Security.Cryptography;
using System.Text;

namespace SOM_DEVELOP_TOOL
{
    /// <summary>
    /// ECDsa签名验证算法
    /// </summary>
    public static class SE
    {

        static SE()
        {

        }

        public static void GenerateKeyPair(out byte[] sk, out byte[] pk, string cure = "secp256r1")
        {
            PrivateKey privateKey;
            PublicKey publicKey;
            byte[] pk_x, pk_y;
            privateKey = new PrivateKey(cure);
            publicKey = privateKey.publicKey();
            sk = BigToByte(privateKey.secret,true);
            pk_x = BigToByte(publicKey.point.x, true);
            pk_y = BigToByte(publicKey.point.y, true);
            pk = new byte[pk_x.Length + pk_y.Length];
            Array.Copy(pk_x,0,pk,0,pk_x.Length);
            Array.Copy(pk_y, 0, pk, pk_x.Length, pk_y.Length);
        }
        
        public static void GenerateKeyPair(out byte[] sk, out byte[] pk,out byte[] k,string cure = "secp256r1")
        {
            PrivateKey privateKey;
            PublicKey publicKey;
            byte[] pk_x, pk_y;
            privateKey = new PrivateKey(cure);
            publicKey = privateKey.publicKey();
            sk = BigToByte(privateKey.secret, true);
            pk_x = BigToByte(publicKey.point.x, true);
            pk_y = BigToByte(publicKey.point.y, true);
            pk = new byte[pk_x.Length + pk_y.Length];
            Array.Copy(pk_x, 0, pk, 0, pk_x.Length);
            Array.Copy(pk_y, 0, pk, pk_x.Length, pk_y.Length);
            BigInteger randNum;
            randNum = GetK(privateKey);
            k = BigToByte(randNum, true);
        }

        public static byte[] GetDataFormHexStr(string InStr,bool BigEnd = false)
        {
            List<byte> lst = new List<byte>();
            for(int i=0;i<InStr.Length;i+=2)
            {
                string str;
                if(BigEnd == true)
                    str = InStr.Substring(i, 2);
                else
                    str = InStr.Substring(InStr.Length - i-2, 2);
                byte data = Convert.ToByte(str,16);
                lst.Add(data);
            }
            return lst.ToArray();
        }

        public static void LoadKey(string FileName, out byte[] PublicKey, out byte[] PrivateKey, out byte[] k)
        {
            string[] Instr = File.ReadAllLines(FileName);
            byte[] x=null, y=null;
            PublicKey = null;
            PrivateKey = null;
            k = null;
            for (int i = 0; i < Instr.Length; i++)
            {
                string str = Instr[i].Replace(" ","");
                str = str.Replace("0x", "");
                string[] str_array = str.Split('=');
                str_array[1] = str_array[1].Replace("_", "");
                if (str_array[0] == "kpub_x")
                {
                    x = GetDataFormHexStr(str_array[1]);
                }
                else if (str_array[0] == "kpub_y")
                {
                    y = GetDataFormHexStr(str_array[1]);
                }
                else if (str_array[0] == "kpriv")
                {
                    PrivateKey = GetDataFormHexStr(str_array[1]);
                }
                else if (str_array[0] == "k")
                {
                    k = GetDataFormHexStr(str_array[1]);
                }
            }
            PublicKey = new byte[x.Length+y.Length];
            Array.Copy(x,0,PublicKey,0,x.Length);
            Array.Copy(y,0,PublicKey,x.Length,y.Length);
        }

        public static bool  CheckKey(byte[] privatekey, byte[] publickey)
        {
            PrivateKey sk;
            PublicKey ref_pk, calc_pk;
            sk = new PrivateKey("secp256r1", new BigInteger(SE.AddZero(privatekey)));
            calc_pk = sk.publicKey();

            byte[] pkx, pky;
            pkx = new byte[32];
            pky = new byte[32];
            Array.Copy(publickey, 0, pkx, 0, 32);
            Array.Copy(publickey, 32, pky, 0, 32);
            ref_pk = new PublicKey(SE.AddZero(pkx), SE.AddZero(pky));
            byte[] x0 = BigToByte(calc_pk.point.x);
            byte[] x1 = BigToByte(ref_pk.point.x);
            byte[] y0 = BigToByte(calc_pk.point.y);
            byte[] y1 = BigToByte(ref_pk.point.y);
            return WilfDataPro.CmparaArray(x0, x1) && WilfDataPro.CmparaArray(y0, y1);

        }
        public static EllipticCurve.Signature SignFw(byte[] bin_data, byte[] privateKey, byte[] k)
        {
            EllipticCurve.Signature signature;
            PrivateKey sk;
            PublicKey pk;
            sk = new PrivateKey("secp256r1", SE.ByteToBig(privateKey));
            pk = sk.publicKey();
            signature = Ecdsa.Sign(bin_data,sk.secret, SE.ByteToBig(k),true);
            return signature;
        }

        public static bool  VerifyFw(byte[] bin_data, EllipticCurve.Signature signature, byte[] publickey)
        {
            byte[] pkx, pky;
            BigInteger BigPkx, BigPky;
            pkx = new byte[32];
            pky = new byte[32];
            Array.Copy(publickey, 0, pkx, 0, 32);
            Array.Copy(publickey, 32, pky, 0, 32);
            BigPkx = SE.ByteToBig(pkx);
            BigPky = SE.ByteToBig(pky);
            bool ret = Ecdsa.VerifySign(bin_data, BigPkx, BigPky, signature,true);
            
            return ret;
        }

        public static EllipticCurve.Signature SignFw(byte[] bin_data, PrivateKey privateKey, BigInteger k)
        {
            EllipticCurve.Signature signature;
            PublicKey pk;
            pk = privateKey.publicKey();
            signature = Ecdsa.Sign(bin_data, privateKey.secret, k, true);
            return signature;
        }

        public static bool VerifyFw(byte[] bin_data, EllipticCurve.Signature signature, PublicKey publickey)
        {
            bool ret = Ecdsa.VerifySign(bin_data, publickey.point.x, publickey.point.y, signature, true);

            return ret;
        }

        public static byte[] SHA256(byte[] data)
        {
            return Ecdsa.sha256(data);
        }

        public static string SHA256_String(byte[] data)
        {
            string str = "";
            byte[] OutData = Ecdsa.sha256(data);
            for (int i=0; i< data.Length; i++)
            {
                str += data[i].ToString("X2"); 
            }
            return str;  
        }

        public static byte[] AddZero(byte[] data)
        {
            byte[] out_data = new byte[data.Length+1];
            Array.Copy(data, out_data, data.Length);
            out_data[data.Length] = 0;
            return out_data;
        }

        public static byte[] RemoveZero(byte[] data)
        {
            byte[] out_data;
            if (data[data.Length-1] == 0)
            {
                out_data = new byte[data.Length-1];
                Array.Copy(data, out_data, data.Length-1);
                return out_data;
            }
            else
            {
                return data;
            }
            
        }

        public static BigInteger ByteToBig(byte[] data,bool Revert=false)
        {
            if(Revert == true)
            {
                data = WilfDataPro.Revert(data);    
            }
            BigInteger big = new BigInteger(AddZero(data));
            return big;
        }

        public static  byte[] BigToByte(BigInteger data,bool NeedRemoveZero=false)
        {

            byte[] out_data =  data.ToByteArray();
            if(NeedRemoveZero == true) 
            {
                return RemoveZero(out_data);
            }
            else
            return out_data;
        }

        public static BigInteger GetK()
        {
            return Ecdsa.GetK();
        }

        public static BigInteger GetK(PrivateKey sk)
        {
            return Ecdsa.GetK(sk);
        }
    }
}
