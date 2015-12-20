using System;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.IO;
namespace JieYiGuang.Common.Utils
{
    /// <summary>
    /// 密码密钥
    /// </summary>
    public partial class KeyHelper
    {

        #region 加解密算法 DES

        /// <summary>
        /// DES加密偏移量，必须是>=8位长的字符串
        /// </summary>
        private static string DES_IV = "HAIYABTX";
        /// <summary>
        /// DES加密的私钥，必须是8位长的字符串
        /// </summary>
        private static string DES_KEY = "HAIYABTX";

        /// <summary>
        /// 对字符串进行DES加密
        /// </summary>
        /// <param name="sourceString">待加密的字符串</param>
        /// <returns>加密后的BASE64编码的字符串</returns>
        public static string DES_Encrypt(string sourceString)
        {

            byte[] btKey = Encoding.Default.GetBytes(DES_KEY);
            byte[] btIV = Encoding.Default.GetBytes(DES_IV);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.Default.GetBytes(sourceString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    throw;

                }
            }
        }

        /// <summary>
        /// 对DES加密后的字符串进行解密
        /// </summary>
        /// <param name="encryptedString">待解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string DES_Decrypt(string encryptedString)
        {

            byte[] btKey = Encoding.Default.GetBytes(DES_KEY);
            byte[] btIV = Encoding.Default.GetBytes(DES_IV);
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
                    return Encoding.Default.GetString(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
        }

        #endregion

        #region 加解密算法 MD5

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="code">加密位数16/32</param>
        /// <returns></returns>
        public static string Md5(string str, int code)
        {
            #region "MD5加密"
            string strEncrypt = string.Empty;
            if (code == 16)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
            }

            if (code == 32)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }

            return strEncrypt;
            #endregion
        }
        #endregion

        #region 加解密算法 字符串

        /// <summary>
        /// 字符串加密  进行位移操作
        /// </summary>
        /// <param name="str">待加密数据</param>
        /// <returns>加密后的数据</returns>
        public static string String_Encrypt(string Input)
        {
            #region 字符串加密  进行位移操作
            string _temp = "";
            int _inttemp;
            char[] _chartemp = Input.ToCharArray();
            for (int i = 0; i < _chartemp.Length; i++)
            {
                _inttemp = _chartemp[i] + 1;
                _chartemp[i] = (char)_inttemp;
                _temp += _chartemp[i];
            }
            return _temp;
            #endregion
        }

        /// <summary>
        /// 字符串解密 进行位移操作
        /// </summary>
        /// <param name="str">待解密数据</param>
        /// <returns>解密成功后的数据</returns>
        public static string String_Decrypt(string Input)
        {
            #region 字符串解密 进行位移操作
            string _temp = "";
            int _inttemp;
            char[] _chartemp = Input.ToCharArray();
            for (int i = 0; i < _chartemp.Length; i++)
            {
                _inttemp = _chartemp[i] - 1;
                _chartemp[i] = (char)_inttemp;
                _temp += _chartemp[i];
            }
            return _temp;
            #endregion
        }

        #endregion

        #region 加解密算法 RSA

        /// <summary>
        /// RSA加密函数
        /// </summary>
        /// <param name="xmlPublicKey">说明KEY必须是XML的行式,返回的是字符串</param>
        /// <param name="EncryptString"></param>
        /// <returns></returns>
        public static string RSA_Encrypt(string xmlPublicKey, string EncryptString)
        {
            byte[] PlainTextBArray;
            byte[] CypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            PlainTextBArray = (new UnicodeEncoding()).GetBytes(EncryptString);
            CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
            Result = Convert.ToBase64String(CypherTextBArray);
            return Result;
        }

        /// <summary>
        /// RSA解密函数
        /// </summary>
        /// <param name="xmlPrivateKey"></param>
        /// <param name="DecryptString"></param>
        /// <returns></returns>
        public static string RSA_Decrypt(string xmlPrivateKey, string DecryptString)
        {
            byte[] PlainTextBArray;
            byte[] DypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            PlainTextBArray = Convert.FromBase64String(DecryptString);
            DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
            Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
            return Result;
        }

        /// <summary>
        /// 产生RSA的密钥
        /// </summary>
        /// <param name="xmlKeys">私钥</param>
        /// <param name="xmlPublicKey">公钥</param>
        public void RSA_Key(out string xmlKeys, out string xmlPublicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            xmlKeys = rsa.ToXmlString(true);
            xmlPublicKey = rsa.ToXmlString(false);
        }
        #endregion

        #region 加解密算法 SHA1

        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="input">加密字符</param>
        /// <returns></returns>
        public static string SHA1_Encrypt(string input)
        {
            SHA1 sha1Hasher = SHA1.Create();

            byte[] data = sha1Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        #endregion

    }

}
