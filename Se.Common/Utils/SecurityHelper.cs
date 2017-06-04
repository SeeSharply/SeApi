using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SeApi.Common
{
    public static class SecurityHelper
    {
        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string SHA256Encrypt(string content)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            SHA256 sha256 = new SHA256Managed();

            tmpByte = sha256.ComputeHash(GetKeyByteArray(content));
            sha256.Clear();

            return GetStringValue(tmpByte);

        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string content)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(content));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <returns></returns>
        public static string AESEncrypt(string contetn, string key)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(key);
            byte[] bIV = Encoding.UTF8.GetBytes(key.Substring(5, 16));
            byte[] byteArray = Encoding.UTF8.GetBytes(contetn);

            string encrypt = null;
            Rijndael aes = Rijndael.Create();
            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        encrypt = Convert.ToBase64String(mStream.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            aes.Clear();

            return encrypt;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <returns></returns>
        public static string AESDecrypt(string contetn, string key)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(key);
            byte[] bIV = Encoding.UTF8.GetBytes(key.Substring(5, 16));
            byte[] byteArray = Convert.FromBase64String(contetn);

            string decrypt = null;
            Rijndael aes = Rijndael.Create();
            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                    }
                }
            }
            catch { }
            aes.Clear();

            return decrypt;
        }

        public static string RSAEncrypt(string str, string key)
        {
            RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
            rSACryptoServiceProvider.FromXmlString(key);
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            int num = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(bytes.Length) / Convert.ToDecimal(117)));
            List<byte> list = new List<byte>();
            for (int i = 0; i < num; i++)
            {
                int num2 = Math.Min(bytes.Length - 117 * i, 117);
                byte[] array = new byte[num2];
                int num3 = 117 * i;
                for (int j = 0; j < num2; j++)
                {
                    int num4 = num3 + j;
                    array[j] = bytes[num4];
                }
                byte[] collection = rSACryptoServiceProvider.Encrypt(array, false);
                list.AddRange(collection);
            }
            byte[] bytes2 = list.ToArray();
            // return EncryptUtils.ByteArrayToHex(bytes2);
            return null;
        }

        private static byte[] GetKeyByteArray(string strKey)
        {

            ASCIIEncoding Asc = new ASCIIEncoding();

            int tmpStrLen = strKey.Length;
            byte[] tmpByte = new byte[tmpStrLen - 1];

            tmpByte = Asc.GetBytes(strKey);

            return tmpByte;

        }

        private static string GetStringValue(byte[] Byte)
        {
            return BitConverter.ToString(Byte).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 进行dex加密
        /// </summary>
        /// <param name="sourceString">需要加密的字符串</param>
        /// <param name="key">加密key，8位字符串</param>
        /// <param name="iv">向量，8位字符串</param>
        /// <returns></returns>

        public static string DesEncrypt(string sourceString,string key)
        {
            try
            {
                byte[] btKey = Encoding.UTF8.GetBytes(key);

                byte[] btIV = Encoding.UTF8.GetBytes(key);

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] inData = Encoding.UTF8.GetBytes(sourceString);

                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);

                        cs.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
 
            }
            catch (Exception e)
            {
                throw new Exception("加密失败"+e.Message);
            }
        }

        /// <summary>
        /// des解密
        /// </summary>
        /// <param name="encryptedString"></param>
        /// <returns></returns>
        public static string DesDecrypt(string encryptedString,string key)
        {
            byte[] btKey = Encoding.UTF8.GetBytes(key);

            byte[] btIV = Encoding.UTF8.GetBytes(key);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Convert.FromBase64String(encryptedString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);

                        cs.FlushFinalBlock();
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
                catch
                {
                    return "解密失败";
                }
            }
        }
    }
}
