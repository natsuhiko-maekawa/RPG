﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

namespace Utility
{
    public static class MyJsonUtility
    {
        private const string Key = "6c147f6ddb0943cfb92e4b279bc71dba";
        private const string Iv = "b8d8f4826b854d30bf724cee87fceb55";
        private const string Create = "新規にファイルを作成しました。";
        private const string FailLoad = "ファイルの読み込みに失敗ました。";

        public static void Save(object save, string path)
        {
            var json = JsonUtility.ToJson(save);
            using var aes = Aes.Create();
            using var encryptor = aes.CreateEncryptor(StringToBytes(Key), StringToBytes(Iv));
            if (!File.Exists(path))
            {
                using var _ = File.Create(path);
                MyDebug.Log(Create);
            }

            using var fileStream = File.Open(path, FileMode.Open);
            using var cryptoStream = new CryptoStream(fileStream, encryptor, CryptoStreamMode.Write);
            using var streamWriter = new StreamWriter(cryptoStream);
            streamWriter.Write(json);
        }

        public static void Save(IReadOnlyList<(object save, string path)> tupleList)
        {
            foreach (var tuple in tupleList)
                Save(tuple.save, tuple.path);
        }

        public static T Load<T>(string path)
        {
            using var aes = Aes.Create();
            var key = StringToBytes(Key);
            var iv = StringToBytes(Iv);
            using var decryptor = aes.CreateDecryptor(key, iv);
            try
            {
                using var fileStream = File.Open(path, FileMode.Open);
                using var cryptoStream = new CryptoStream(fileStream, decryptor, CryptoStreamMode.Read);
                using var streamReader = new StreamReader(cryptoStream);
                var json = streamReader.ReadToEnd();
                return JsonUtility.FromJson<T>(json);
            }
            catch (IOException)
            {
                MyDebug.LogError(FailLoad);
                throw;
            }
        }

        public static IReadOnlyList<T> Load<T>(IReadOnlyList<string> pathList)
        {
            return pathList
                .Select(Load<T>)
                .ToList();
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