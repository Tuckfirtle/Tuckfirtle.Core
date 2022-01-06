// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.Security.Cryptography;

namespace Tuckfirtle.Core.Utility
{
    public static class AesUtility
    {
        public static byte[] Encrypt(byte[] key, byte[] plainText)
        {
            using var aes = Aes.Create();
            aes.Key = key;

            using var cryptoTransform = aes.CreateEncryptor();
            var plainTextLength = plainText.Length;
            var outputBuffer = new byte[plainTextLength + (16 - plainTextLength % 16) + 16];

            Buffer.BlockCopy(aes.IV, 0, outputBuffer, 0, 16);

            var numberOfBlocksForTransform = plainTextLength / 16;
            var plainTextTotalCountForTransform = numberOfBlocksForTransform * 16;
            var totalWritten = 16;

            if (cryptoTransform.CanTransformMultipleBlocks)
            {
                totalWritten += cryptoTransform.TransformBlock(plainText, 0, plainTextTotalCountForTransform, outputBuffer, totalWritten);
            }
            else
            {
                var inputBufferOffset = 0;

                do
                {
                    totalWritten += cryptoTransform.TransformBlock(plainText, inputBufferOffset, 16, outputBuffer, totalWritten);
                    inputBufferOffset += 16;
                } while (inputBufferOffset < plainTextTotalCountForTransform);
            }

            var finalBlock = cryptoTransform.TransformFinalBlock(plainText, plainTextTotalCountForTransform, plainTextLength - plainTextTotalCountForTransform);
            Buffer.BlockCopy(finalBlock, 0, outputBuffer, totalWritten, finalBlock.Length);

            return outputBuffer;
        }

        public static byte[] Decrypt(byte[] key, byte[] cipherText)
        {
            using var aes = Aes.Create();
            aes.Key = key;

            var iv = new byte[16];
            Buffer.BlockCopy(cipherText, 0, iv, 0, 16);
            aes.IV = iv;

            using var cryptoTransform = aes.CreateDecryptor();
            var cipherTextLength = cipherText.Length - 16;
            var outputBuffer = new byte[cipherTextLength];

            var numberOfBlocksForTransform = cipherTextLength / 16;
            var cipherTextTotalCountForTransform = numberOfBlocksForTransform * 16;
            var totalWritten = 0;

            if (cryptoTransform.CanTransformMultipleBlocks)
            {
                totalWritten += cryptoTransform.TransformBlock(cipherText, 16, cipherTextTotalCountForTransform, outputBuffer, totalWritten);
            }
            else
            {
                var inputBufferOffset = 16;

                do
                {
                    totalWritten += cryptoTransform.TransformBlock(cipherText, inputBufferOffset, 16, outputBuffer, totalWritten);
                    inputBufferOffset += 16;
                } while (inputBufferOffset < cipherTextTotalCountForTransform);
            }

            var finalBlock = cryptoTransform.TransformFinalBlock(cipherText, 16 + cipherTextTotalCountForTransform, cipherTextLength - cipherTextTotalCountForTransform);
            var finalBlockLength = finalBlock.Length;

            if (finalBlockLength > 0)
            {
                Buffer.BlockCopy(finalBlock, 0, outputBuffer, totalWritten, finalBlockLength);
                totalWritten += finalBlockLength;
            }

            var result = new byte[totalWritten];
            Buffer.BlockCopy(outputBuffer, 0, result, 0, totalWritten);

            return result;
        }
    }
}