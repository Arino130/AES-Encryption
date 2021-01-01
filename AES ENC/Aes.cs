using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace AES_ENC
{
    public class AES
    {
        //Giải mã
        public AES() { }
        //Giải mã
        public string DecryptAes(byte[] encrypted, string keyText)
        {
            try
            {
                return Decrypt(encrypted, ASCIIEncoding.ASCII.GetBytes(keyText.PadLeft(32)), ASCIIEncoding.ASCII.GetBytes(keyText.PadLeft(16)));
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Mã hóa
        public byte[] EncryptAes(string source, string keyText)
        {
            try
            {
                byte[] keyBytes = ASCIIEncoding.ASCII.GetBytes(keyText.PadLeft(32));
                byte[] IVBytes = ASCIIEncoding.ASCII.GetBytes(keyText.PadLeft(16));
                //Tạo khóa aes.Key và Tọa độ aes.IV
                using (AesManaged aes = new AesManaged())
                {
                    //byte[] encrypted = Encrypt(raw,aes.Key,aes.IV);
                    //Mã hóa
                    byte[] encrypted = Encrypt(source, keyBytes, IVBytes);
                    //Giải mã
                    //string decrypted = Decrypt(encrypted,aes.Key,aes.IV);
                    return encrypted;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        private byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                //Tạo luồng dữ liệu
                using (MemoryStream ms = new MemoryStream())
                {
                    //Sử dụng lớp CryptoStream để mã hóa qua 1 luồng dữ liệu nhất định
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        //Tạo luồng StreamWriter để ghi dữ liệu
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            //Trả về chuổi đã giải mã
            return encrypted;
        }     
        private string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream(cipherText))
                { 
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    { 
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
    }

}
