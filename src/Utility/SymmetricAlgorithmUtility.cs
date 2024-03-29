﻿// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.Security.Cryptography;

namespace Tuckfirtle.Core.Utility
{
    /// <summary>
    /// Utility class that contains all symmetric algorithm functions.
    /// </summary>
    public static class SymmetricAlgorithmUtility
    {
        /// <summary>
        /// Encrypt using <see cref="Aes" /> algorithm with 128 key size.
        /// </summary>
        /// <param name="iv">The initialization vector (IV) to use for the <see cref="Aes" /> algorithm.</param>
        /// <param name="key">The secret key to use for the <see cref="Aes" /> algorithm.</param>
        /// <param name="cryptoTransformAction">Delegate for <see cref="ICryptoTransform" /> action.</param>
        /// <param name="mode">The mode for operation of the <see cref="Aes" /> algorithm.</param>
        /// <param name="padding">The padding mode used in the <see cref="Aes" /> algorithm.</param>
        /// <exception cref="CryptographicException">The Windows security policy setting for FIPS is enabled.</exception>
        /// <exception cref="InvalidOperationException">This implementation is not part of the Windows Platform FIPS-validated cryptographic algorithms.</exception>
        public static void Aes128Encrypt(byte[] iv, byte[] key, Action<ICryptoTransform> cryptoTransformAction, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
        {
            AesEncrypt(128, iv, key, mode, padding, cryptoTransformAction);
        }

        /// <summary>
        /// Encrypt using <see cref="Aes" /> algorithm with 128 key size.
        /// </summary>
        /// <param name="iv">The initialization vector (IV) to use for the <see cref="Aes" /> algorithm.</param>
        /// <param name="key">The secret key to use for the <see cref="Aes" /> algorithm.</param>
        /// <param name="state">The variable to pass into <paramref name="cryptoTransformAction" />.</param>
        /// <param name="cryptoTransformAction">Delegate for <see cref="ICryptoTransform" /> action.</param>
        /// <param name="mode">The mode for operation of the <see cref="Aes" /> algorithm.</param>
        /// <param name="padding">The padding mode used in the <see cref="Aes" /> algorithm.</param>
        /// <exception cref="CryptographicException">The Windows security policy setting for FIPS is enabled.</exception>
        /// <exception cref="InvalidOperationException">This implementation is not part of the Windows Platform FIPS-validated cryptographic algorithms.</exception>
        public static void Aes128Encrypt<TState>(byte[] iv, byte[] key, TState state, Action<ICryptoTransform, TState> cryptoTransformAction, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
        {
            AesEncrypt(128, iv, key, mode, padding, state, cryptoTransformAction);
        }

        /// <summary>
        /// Encrypt using <see cref="Aes" /> algorithm with 192 key size.
        /// </summary>
        /// <param name="iv">The initialization vector (IV) to use for the <see cref="Aes" /> algorithm.</param>
        /// <param name="key">The secret key to use for the <see cref="Aes" /> algorithm.</param>
        /// <param name="cryptoTransformAction">Delegate for <see cref="ICryptoTransform" /> action.</param>
        /// <param name="mode">The mode for operation of the <see cref="Aes" /> algorithm.</param>
        /// <param name="padding">The padding mode used in the <see cref="Aes" /> algorithm.</param>
        /// <exception cref="CryptographicException">The Windows security policy setting for FIPS is enabled.</exception>
        /// <exception cref="InvalidOperationException">This implementation is not part of the Windows Platform FIPS-validated cryptographic algorithms.</exception>
        public static void Aes192Encrypt(byte[] iv, byte[] key, Action<ICryptoTransform> cryptoTransformAction, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
        {
            AesEncrypt(192, iv, key, mode, padding, cryptoTransformAction);
        }

        /// <summary>
        /// Encrypt using <see cref="Aes" /> algorithm with 128 key size.
        /// </summary>
        /// <param name="iv">The initialization vector (IV) to use for the <see cref="Aes" /> algorithm.</param>
        /// <param name="key">The secret key to use for the <see cref="Aes" /> algorithm.</param>
        /// <param name="state">The variable to pass into <paramref name="cryptoTransformAction" />.</param>
        /// <param name="cryptoTransformAction">Delegate for <see cref="ICryptoTransform" /> action.</param>
        /// <param name="mode">The mode for operation of the <see cref="Aes" /> algorithm.</param>
        /// <param name="padding">The padding mode used in the <see cref="Aes" /> algorithm.</param>
        /// <exception cref="CryptographicException">The Windows security policy setting for FIPS is enabled.</exception>
        /// <exception cref="InvalidOperationException">This implementation is not part of the Windows Platform FIPS-validated cryptographic algorithms.</exception>
        public static void Aes192Encrypt<TState>(byte[] iv, byte[] key, TState state, Action<ICryptoTransform, TState> cryptoTransformAction, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
        {
            AesEncrypt(192, iv, key, mode, padding, state, cryptoTransformAction);
        }

        /// <summary>
        /// Encrypt using <see cref="Aes" /> algorithm with 192 key size.
        /// </summary>
        /// <param name="iv">The initialization vector (IV) to use for the <see cref="Aes" /> algorithm.</param>
        /// <param name="key">The secret key to use for the <see cref="Aes" /> algorithm.</param>
        /// <param name="cryptoTransformAction">Delegate for <see cref="ICryptoTransform" /> action.</param>
        /// <param name="mode">The mode for operation of the <see cref="Aes" /> algorithm.</param>
        /// <param name="padding">The padding mode used in the <see cref="Aes" /> algorithm.</param>
        /// <exception cref="CryptographicException">The Windows security policy setting for FIPS is enabled.</exception>
        /// <exception cref="InvalidOperationException">This implementation is not part of the Windows Platform FIPS-validated cryptographic algorithms.</exception>
        public static void Aes256Encrypt(byte[] iv, byte[] key, Action<ICryptoTransform> cryptoTransformAction, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
        {
            AesEncrypt(256, iv, key, mode, padding, cryptoTransformAction);
        }

        /// <summary>
        /// Encrypt using <see cref="Aes" /> algorithm with 128 key size.
        /// </summary>
        /// <param name="iv">The initialization vector (IV) to use for the <see cref="Aes" /> algorithm.</param>
        /// <param name="key">The secret key to use for the <see cref="Aes" /> algorithm.</param>
        /// <param name="state">The variable to pass into <paramref name="cryptoTransformAction" />.</param>
        /// <param name="cryptoTransformAction">Delegate for <see cref="ICryptoTransform" /> action.</param>
        /// <param name="mode">The mode for operation of the <see cref="Aes" /> algorithm.</param>
        /// <param name="padding">The padding mode used in the <see cref="Aes" /> algorithm.</param>
        /// <exception cref="CryptographicException">The Windows security policy setting for FIPS is enabled.</exception>
        /// <exception cref="InvalidOperationException">This implementation is not part of the Windows Platform FIPS-validated cryptographic algorithms.</exception>
        public static void Aes256Encrypt<TState>(byte[] iv, byte[] key, TState state, Action<ICryptoTransform, TState> cryptoTransformAction, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
        {
            AesEncrypt(256, iv, key, mode, padding, state, cryptoTransformAction);
        }

        private static void AesEncrypt(int keySize, byte[] iv, byte[] key, CipherMode mode, PaddingMode padding, Action<ICryptoTransform> cryptoTransformAction)
        {
            using var aes = new AesManaged
            {
                KeySize = keySize,
                IV = iv,
                Key = key,
                Mode = mode,
                Padding = padding
            };
            using var cryptoTransform = aes.CreateEncryptor();
            cryptoTransformAction(cryptoTransform);
        }

        private static void AesEncrypt<TState>(int keySize, byte[] iv, byte[] key, CipherMode mode, PaddingMode padding, TState state, Action<ICryptoTransform, TState> cryptoTransformAction)
        {
            using var aes = new AesManaged
            {
                KeySize = keySize,
                IV = iv,
                Key = key,
                Mode = mode,
                Padding = padding
            };
            using var cryptoTransform = aes.CreateEncryptor();
            cryptoTransformAction(cryptoTransform, state);
        }
    }
}