using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.Common
{
    /// <summary>
    /// RSA加解密帮助类
    /// </summary>
    public class RsaHelper
    {

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="xmlPublicKey">私钥</param>
        /// <param name="encryptString">需要加密的数据</param>
        /// <returns>RSA公钥加密后的数据</returns>
        public static string RSAEncrypt(string xmlPublicKey, string encryptString)
        {
            string result;
            try
            {
                RSACryptoServiceProvider.UseMachineKeyStore = true;
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlPublicKey);
                byte[] bytes = new UnicodeEncoding().GetBytes(encryptString);
                result = Convert.ToBase64String(provider.Encrypt(bytes, false));
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return result;
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="xmlPrivateKey">公钥</param>
        /// <param name="decryptString">需要解密的数据</param>
        /// <returns>解密后的数据</returns>
        public static string RSADecrypt(string xmlPrivateKey, string decryptString)
        {
            string result;
            try
            {
                RSACryptoServiceProvider.UseMachineKeyStore = true;
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlPrivateKey);
                byte[] rgb = Convert.FromBase64String(decryptString);
                byte[] buffer2 = provider.Decrypt(rgb, false);
                result = new UnicodeEncoding().GetString(buffer2);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return result;
        }

        /// <summary>
        /// 生成公钥、私钥
        /// </summary>
        /// <param name="PrivateKeyPath">私钥文件保存路径，包含文件名</param>
        /// <param name="PublicKeyPath">公钥文件保存路径，包含文件名</param>
        public static void CreateKey(out string privateKey, out string publicKey)
        {
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            privateKey = provider.ToXmlString(true);
            publicKey = provider.ToXmlString(false);
        }

    }
}
