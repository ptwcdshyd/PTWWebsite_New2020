using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PTW.DBAccess
{
    public static class UtilityHelper
    {
        public static string Encrypt(string EncryptText)
        {

            try
            {

                string EncryptionKey = "PTW@123";
                byte[] clearBytes = Encoding.Unicode.GetBytes(EncryptText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        EncryptText = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return EncryptText;
            }
            catch { throw; }
        }

        /// <method>
        /// Decrypt text using key
        /// </method>
        public static string Decrypt(string DecryptText)
        {
            try
            {
                string EncryptionKey = "PTW@123";
                DecryptText = DecryptText.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(DecryptText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        DecryptText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return DecryptText;
            }
            catch { throw; }
        }


    }

}