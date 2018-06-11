using System;
using System.Security.Cryptography;

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

        private static void EncryptDecryptData()
        {
            var rc4 = new RC4Algorithm(GenerateKey(), "Mary had a little lamb.");
            rc4.Rc4Initialize(768);
            var encrypted = rc4.EnDeCrypt();
            Console.WriteLine("Encrypted: " + encrypted);


            rc4.Text = encrypted;
            var decrypted = rc4.EnDeCrypt();
            Console.WriteLine("Decrypted: " + decrypted);
        }

        private static void GenerateKeyStream()
        {
            var rc4 = new RC4Algorithm(GenerateKey());
            rc4.Rc4Initialize(768);

            for (var i = 0; i < 300000; i++)
            {
                Console.Write(rc4.GetNextKeyByte() + ", ");
            }
        }
    }
}
