using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encrypt
{
    public class Aes_Encrypt
    {
        public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            byte[] saltBytes = {1, 2, 3, 4, 5, 6, 7, 8};

            using (var ms = new MemoryStream())
            {
                using (var AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize/8);
                    AES.IV = key.GetBytes(AES.BlockSize/8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
            byte[] saltBytes = {1, 2, 3, 4, 5, 6, 7, 8};

            using (var ms = new MemoryStream())
            {
                using (var AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize/8);
                    AES.IV = key.GetBytes(AES.BlockSize/8);
                    AES.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        public static void EncryptFile(string file, string password)
        {
            var bytesToBeEncrypted = File.ReadAllBytes(file);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            var bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);
            File.WriteAllBytes(file, bytesEncrypted);
        }

        public static void DecryptFile(string fileEncrypted, string password)
        {
            var bytesToBeDecrypted = File.ReadAllBytes(fileEncrypted);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            var bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);
            File.WriteAllBytes(fileEncrypted, bytesDecrypted);
        }
    }
}