using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace Utility
{
    public class XmlSerializer : IXmlSerializer
    {
        private const string Key = "6c147f6ddb0943cfb92e4b279bc71dba";
        private const string Iv = "b8d8f4826b854d30bf724cee87fceb55";

        public void Save<T>(T obj)
        {
            var path = typeof(T).FullName;
            var serializer = new DataContractSerializer(typeof(T));

            using var aes = Aes.Create();
            using var encryptor = aes.CreateEncryptor(StringToBytes(Key), StringToBytes(Iv));
            if (!File.Exists(path))
            {
                using var _ = File.Create(path);
            }

            using var fileStream = File.Open(path, FileMode.Open);
            using var cryptoStream = new CryptoStream(fileStream, encryptor, CryptoStreamMode.Write);
            serializer.WriteObject(cryptoStream, obj);
        }

        public T Load<T>()
        {
            var path = typeof(T).FullName;
            var serializer = new DataContractSerializer(typeof(T));

            using var aes = Aes.Create();
            var key = StringToBytes(Key);
            var iv = StringToBytes(Iv);
            using var decryptor = aes.CreateDecryptor(key, iv);
            using var fileStream = File.Open(path, FileMode.Open);
            using var cryptoStream = new CryptoStream(fileStream, decryptor, CryptoStreamMode.Read);
            return (T)serializer.ReadObject(cryptoStream);
        }

        private static byte[] StringToBytes(string str)
        {
            return Enumerable.Range(0, str.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(str.Substring(x, 2), 16))
                .ToArray();
        }
    }
}