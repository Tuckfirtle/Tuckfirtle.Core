// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System.IO;
using System.Security.Cryptography;

namespace Tuckfirtle.Core.Utility
{
    public static class AesUtility
    {
        public static byte[] EncryptBytes(byte[] keyBytes, byte[] dataBytes)
        {
            using (var aes = new AesManaged { Key = keyBytes })
            {
                aes.GenerateIV();

                using (var memoryStream = new MemoryStream())
                {
                    memoryStream.Write(aes.IV, 0, aes.IV.Length);

                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(dataBytes, 0, dataBytes.Length);
                        cryptoStream.FlushFinalBlock();
                    }

                    return memoryStream.ToArray();
                }
            }
        }
    }
}