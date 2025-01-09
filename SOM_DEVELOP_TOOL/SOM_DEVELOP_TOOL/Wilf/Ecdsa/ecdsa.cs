using System.Security.Cryptography;
using System.Numerics;
using System.Text;
using SOM_DEVELOP_TOOL;
using System;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

namespace EllipticCurve
{

    public static class Ecdsa
    {
        public static BigInteger sign_k;
        public static void GenerateKeyPair( out PrivateKey privateKey,out PublicKey publicKey, string cure = "secp256r1")
        {
            privateKey = new PrivateKey(cure);
            publicKey = privateKey.publicKey();
        }

        public static void GenerateKeyPair(out BigInteger sk, out BigInteger pkx,out BigInteger pky, string cure = "secp256r1")
        {
            PrivateKey privateKey = new PrivateKey(cure);
            PublicKey publicKey = privateKey.publicKey();
            sk = privateKey.secret;
            pkx = publicKey.point.x;
            pky = publicKey.point.y;
        }

        public static BigInteger GetK()
        {
            return sign_k;
        }

        public static BigInteger GetK(PrivateKey sk)
        {
            return Utils.Integer.randomBetween(BigInteger.One, sk.curve.N - 1); ;
        }

        public static Signature Sign(byte[] Data,BigInteger sk,BigInteger k,bool need_calc_hash = false)
        {
            BigInteger randNum;
            CurveFp curve;
            PrivateKey privateKey = new PrivateKey("secp256r1",sk);
            byte[] hashMessage;
            if (need_calc_hash==true)
                hashMessage = sha256(Data);
            else
                hashMessage = Data;
            BigInteger BigHash = SE.ByteToBig(hashMessage,true);
            curve = privateKey.curve;
            if (k == null||k==0)
                randNum = Utils.Integer.randomBetween(BigInteger.One, curve.N - 1);
            else
            {
                randNum = k;
            }
            sign_k = randNum;
            Point randSignPoint = EcdsaMath.multiply(curve.G, randNum, curve.N, curve.A, curve.P);
            BigInteger r = Utils.Integer.modulo(randSignPoint.x, curve.N);
            BigInteger s = Utils.Integer.modulo((BigHash + r * privateKey.secret) * (EcdsaMath.inv(randNum, curve.N)), curve.N);
            return new Signature(r, s);
        }

        public static bool VerifySign(byte[] Data, BigInteger pkx,BigInteger pky, Signature sig, bool need_calc_hash = false)
        {
            byte[] hashMessage;
            PublicKey publicKey = new PublicKey(pkx, pky);
            CurveFp curve = publicKey.curve;
            if (need_calc_hash==true)
                hashMessage = sha256(Data);
            else
                hashMessage = Data;
            BigInteger BigHash = SE.ByteToBig(hashMessage, true);
            BigInteger sigR = sig.r;
            BigInteger sigS = sig.s;

            if (sigR < 1 || sigR >= curve.N)
            {
                return false;
            }
            if (sigS < 1 || sigS >= curve.N)
            {
                return false;
            }

            BigInteger inv = EcdsaMath.inv(sigS, curve.N);

            Point u1 = EcdsaMath.multiply(
                curve.G,
                Utils.Integer.modulo((BigHash * inv), curve.N),
                curve.N,
                curve.A,
                curve.P
            );
            Point u2 = EcdsaMath.multiply(
                publicKey.point,
                Utils.Integer.modulo((sigR * inv), curve.N),
                curve.N,
                curve.A,
                curve.P
            );
            Point v = EcdsaMath.add(
                u1,
                u2,
                curve.A,
                curve.P
            );
            if (v.isAtInfinity())
            {
                return false;
            }
            return Utils.Integer.modulo(v.x, curve.N) == sigR;
        }

  
        public static string sha256(string message)
        {
            byte[] bytes;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(message));
            }

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public static byte[] sha256(byte[] message)
        {
            byte[] bytes;
            SHA256 sha256Hash = SHA256.Create();
            bytes = sha256Hash.ComputeHash(message);
            return bytes;
        }

    }

}
