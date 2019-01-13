using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace fmCommon
{
    public class fmSecurity
    {
        public bool enable { get; set; }
        readonly string aeskey = "*7.Qa#82/-+bR";
        readonly byte[] deskey = ASCIIEncoding.ASCII.GetBytes("nO-1As/+");

        //AES128
        public string EncryptAES128(string target, string key = null)
        {
            if (string.IsNullOrEmpty(key)) key = aeskey;
            if (!enable) return target;

            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(target);
            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
        }

        public string DecryptAES128(string enc, string key = null)
        {
            if (string.IsNullOrEmpty(key)) key = aeskey;
            if (!enable) return enc;

            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] encryptedData = Convert.FromBase64String(enc);
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }

        public string EncryptDES(string p_data, string key = null)
        {
            if (!enable) return p_data;
            if (string.IsNullOrEmpty(key)) key = aeskey;
            if (deskey.Length != 8)
                throw (new Exception("Invalid key. Key length must be 8 byte."));

            DESCryptoServiceProvider rc2 = new DESCryptoServiceProvider();
            rc2.Key = deskey;
            rc2.IV = deskey;

            MemoryStream ms = new MemoryStream();
            CryptoStream cryStream = new CryptoStream(ms, rc2.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] data = Encoding.UTF8.GetBytes(p_data.ToCharArray());

            cryStream.Write(data, 0, data.Length);
            cryStream.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        public string DecryptDES(string p_data, string key = null)
        {
            if (!enable) return p_data;
            if (string.IsNullOrEmpty(key)) key = aeskey;
            if (String.IsNullOrEmpty(p_data))
                throw new ArgumentNullException("The string which needs to be decrypted can not be null.");

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(p_data));
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(deskey, deskey), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }
    }
}
