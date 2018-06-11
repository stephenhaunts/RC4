using System;
using System.Security.Cryptography;
using System.Text;

namespace RC4
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //GenerateKeyStream();
            EncryptDecryptData();
        }

        public static byte[] GenerateKey()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[256];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }

        static void EncryptDecryptData()
        {
            var rc4 = new RC4Algorithm(GenerateKey(), Encoding.ASCII.GetBytes("This is a very top secret message that can only be ready by very important people."));
            rc4.Rc4Initialize(768);

            var encrypted = rc4.EnDeCrypt();

            Console.WriteLine("Encrypted: " + Convert.ToBase64String(encrypted));

            rc4.Data = encrypted;
            var decrypted = rc4.EnDeCrypt();
            Console.WriteLine("Decrypted: " + System.Text.Encoding.Default.GetString(decrypted));
        }

        static void GenerateKeyStream()
        {
            var rc4 = new RC4Algorithm(GenerateKey());
            rc4.Rc4Initialize(768);

            for (var i = 0; i < 300000; i++)
            {
                Console.Write(rc4.GetNextKeyByte() + ",");
            }
        }
    }
}
