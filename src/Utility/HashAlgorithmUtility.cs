// Copyright (C) 2020, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.Security.Cryptography;

namespace Tuckfirtle.Core.Utility
{
    /// <summary>
    /// Utility class that contains all hash algorithm functions.
    /// </summary>
    public static class HashAlgorithmUtility
    {
        /// <summary>
        /// Computes the <see cref="SHA256" /> hash value for the specified byte array.
        /// </summary>
        /// <param name="buffer">The input to compute the hash code for.</param>
        /// <returns>The computed hash code.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="buffer" /> is null.</exception>
        public static byte[] Sha256ComputeHash(byte[] buffer)
        {
            try
            {
                using var sha = new SHA256Managed();
                return sha.ComputeHash(buffer);
            }
            catch (InvalidOperationException)
            {
                using var sha = new SHA256CryptoServiceProvider();
                return sha.ComputeHash(buffer);
            }
        }

        /// <summary>
        /// Computes the <see cref="SHA256" /> hash value for the specified byte array.
        /// </summary>
        /// <param name="buffer">The input to compute the hash code for.</param>
        /// <param name="destArray">The output to copy the computed hash code into.</param>
        /// <param name="destOffset">The zero-based byte offset into <paramref name="destArray" />.</param>
        /// <exception cref="ArgumentNullException"><paramref name="buffer" /> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="destArray" /> is null.</exception>
        /// <exception cref="ArgumentException">The number of bytes in <paramref name="destArray" /> is less than <paramref name="destOffset" /> plus 32.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="destOffset" /> is less than 0.</exception>
        public static void Sha256ComputeHash(byte[] buffer, byte[] destArray, int destOffset = 0)
        {
            try
            {
                using var sha = new SHA256Managed();
                Buffer.BlockCopy(sha.ComputeHash(buffer), 0, destArray, destOffset, 32);
            }
            catch (InvalidOperationException)
            {
                using var sha = new SHA256CryptoServiceProvider();
                Buffer.BlockCopy(sha.ComputeHash(buffer), 0, destArray, destOffset, 32);
            }
        }

        /// <summary>
        /// Computes the <see cref="SHA256" /> hash value for the specified byte array.
        /// </summary>
        /// <param name="buffer">The input to compute the hash code for.</param>
        /// <param name="destArrayPtr">The output pointer to copy the computed hash code into.</param>
        /// <param name="destOffset">The zero-based byte offset into <paramref name="destArrayPtr" />.</param>
        /// <exception cref="ArgumentNullException"><paramref name="buffer" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="destOffset" /> is less than 0.</exception>
        public static unsafe void Sha256ComputeHash(byte[] buffer, byte* destArrayPtr, int destOffset = 0)
        {
            try
            {
                using var sha = new SHA256Managed();

                fixed (byte* resultPtr = sha.ComputeHash(buffer))
                {
                    Buffer.MemoryCopy(resultPtr, destArrayPtr + destOffset, 32, 32);
                }
            }
            catch (InvalidOperationException)
            {
                using var sha = new SHA256CryptoServiceProvider();

                fixed (byte* resultPtr = sha.ComputeHash(buffer))
                {
                    Buffer.MemoryCopy(resultPtr, destArrayPtr + destOffset, 32, 32);
                }
            }
        }

        /// <summary>
        /// Computes the <see cref="SHA384" /> hash value for the specified byte array.
        /// </summary>
        /// <param name="buffer">The input to compute the hash code for.</param>
        /// <returns>The computed hash code.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="buffer" /> is null.</exception>
        public static byte[] Sha384ComputeHash(byte[] buffer)
        {
            try
            {
                using var sha = new SHA384Managed();
                return sha.ComputeHash(buffer);
            }
            catch (InvalidOperationException)
            {
                using var sha = new SHA384CryptoServiceProvider();
                return sha.ComputeHash(buffer);
            }
        }

        /// <summary>
        /// Computes the <see cref="SHA384" /> hash value for the specified byte array.
        /// </summary>
        /// <param name="buffer">The input to compute the hash code for.</param>
        /// <param name="destArray">The output to copy the computed hash code into.</param>
        /// <param name="destOffset">The zero-based byte offset into <paramref name="destArray" />.</param>
        /// <exception cref="ArgumentNullException"><paramref name="buffer" /> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="destArray" /> is null.</exception>
        /// <exception cref="ArgumentException">The number of bytes in <paramref name="destArray" /> is less than <paramref name="destOffset" /> plus 48.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="destOffset" /> is less than 0.</exception>
        public static void Sha384ComputeHash(byte[] buffer, byte[] destArray, int destOffset = 0)
        {
            try
            {
                using var sha = new SHA384Managed();
                Buffer.BlockCopy(sha.ComputeHash(buffer), 0, destArray, destOffset, 48);
            }
            catch (InvalidOperationException)
            {
                using var sha = new SHA384CryptoServiceProvider();
                Buffer.BlockCopy(sha.ComputeHash(buffer), 0, destArray, destOffset, 48);
            }
        }

        /// <summary>
        /// Computes the <see cref="SHA384" /> hash value for the specified byte array.
        /// </summary>
        /// <param name="buffer">The input to compute the hash code for.</param>
        /// <param name="destArrayPtr">The output pointer to copy the computed hash code into.</param>
        /// <param name="destOffset">The zero-based byte offset into <paramref name="destArrayPtr" />.</param>
        /// <exception cref="ArgumentNullException"><paramref name="buffer" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="destOffset" /> is less than 0.</exception>
        public static unsafe void Sha384ComputeHash(byte[] buffer, byte* destArrayPtr, int destOffset = 0)
        {
            try
            {
                using var sha = new SHA384Managed();

                fixed (byte* resultPtr = sha.ComputeHash(buffer))
                {
                    Buffer.MemoryCopy(resultPtr, destArrayPtr + destOffset, 48, 48);
                }
            }
            catch (InvalidOperationException)
            {
                using var sha = new SHA384CryptoServiceProvider();

                fixed (byte* resultPtr = sha.ComputeHash(buffer))
                {
                    Buffer.MemoryCopy(resultPtr, destArrayPtr + destOffset, 48, 48);
                }
            }
        }

        /// <summary>
        /// Computes the <see cref="SHA512" /> hash value for the specified byte array.
        /// </summary>
        /// <param name="buffer">The input to compute the hash code for.</param>
        /// <returns>The computed hash code.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="buffer" /> is null.</exception>
        public static byte[] Sha512ComputeHash(byte[] buffer)
        {
            try
            {
                using var sha = new SHA512Managed();
                return sha.ComputeHash(buffer);
            }
            catch (InvalidOperationException)
            {
                using var sha = new SHA512CryptoServiceProvider();
                return sha.ComputeHash(buffer);
            }
        }

        /// <summary>
        /// Computes the <see cref="SHA512" /> hash value for the specified byte array.
        /// </summary>
        /// <param name="buffer">The input to compute the hash code for.</param>
        /// <param name="destArray">The output to copy the computed hash code into.</param>
        /// <param name="destOffset">The zero-based byte offset into <paramref name="destArray" />.</param>
        /// <exception cref="ArgumentNullException"><paramref name="buffer" /> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="destArray" /> is null.</exception>
        /// <exception cref="ArgumentException">The number of bytes in <paramref name="destArray" /> is less than <paramref name="destOffset" /> plus 64.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="destOffset" /> is less than 0.</exception>
        public static void Sha512ComputeHash(byte[] buffer, byte[] destArray, int destOffset = 0)
        {
            try
            {
                using var sha = new SHA512Managed();
                Buffer.BlockCopy(sha.ComputeHash(buffer), 0, destArray, destOffset, 64);
            }
            catch (InvalidOperationException)
            {
                using var sha = new SHA512CryptoServiceProvider();
                Buffer.BlockCopy(sha.ComputeHash(buffer), 0, destArray, destOffset, 64);
            }
        }

        /// <summary>
        /// Computes the <see cref="SHA512" /> hash value for the specified byte array.
        /// </summary>
        /// <param name="buffer">The input to compute the hash code for.</param>
        /// <param name="destArrayPtr">The output pointer to copy the computed hash code into.</param>
        /// <param name="destOffset">The zero-based byte offset into <paramref name="destArrayPtr" />.</param>
        /// <exception cref="ArgumentNullException"><paramref name="buffer" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="destOffset" /> is less than 0.</exception>
        public static unsafe void Sha512ComputeHash(byte[] buffer, byte* destArrayPtr, int destOffset = 0)
        {
            try
            {
                using var sha = new SHA512Managed();

                fixed (byte* resultPtr = sha.ComputeHash(buffer))
                {
                    Buffer.MemoryCopy(resultPtr, destArrayPtr + destOffset, 64, 64);
                }
            }
            catch (InvalidOperationException)
            {
                using var sha = new SHA512CryptoServiceProvider();

                fixed (byte* resultPtr = sha.ComputeHash(buffer))
                {
                    Buffer.MemoryCopy(resultPtr, destArrayPtr + destOffset, 64, 64);
                }
            }
        }
    }
}