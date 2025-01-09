using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class AES
{
    public static string key = "ggggwfnhmtdh2010";
    public static string iv = "eAFmBHVilJgOTqlO";

    public static string Encrypt(string data)
    {
        return EnCode(data, key, iv);
    }

    public static string Decrypt(string data)
    {
        return DeCode(data, key, iv);
    }
    private static string EnCode(string data, string key, string iv)
    {
        try
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            byte[] ivArray = Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(data);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        catch 
        {
            return null;
        }
    }

    /// <summary>
    /// 解密数据
    /// </summary>
    /// <param name="data">加密内容</param>
    /// <param name="key">密钥</param>
    /// <param name="iv">偏移</param>
    /// <returns></returns>
    private static string DeCode(string data, string key, string iv)
    {
        try
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            byte[] ivArray = Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = Convert.FromBase64String(data);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }
        catch
        {
            return null;
        }
    }
}