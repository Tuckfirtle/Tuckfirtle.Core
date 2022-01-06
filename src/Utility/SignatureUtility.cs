// Copyright (C) 2020, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using Tuckfirtle.OpenQuantumSafe;

namespace Tuckfirtle.Core.Utility
{
    /// <summary>
    /// Utility class that contains all the signature functions.
    /// </summary>
    public static class SignatureUtility
    {
        public static void GenerateKeypair(out ReadOnlySpan<byte> publicKey, out ReadOnlySpan<byte> privateKey)
        {
            using var signatureScheme = new Signature("SPHINCS+-SHA256-128s-simple");
            signatureScheme.GenerateKeypair(out publicKey, out privateKey);
        }

        public static void Sign(out ReadOnlySpan<byte> signature, ReadOnlySpan<byte> message, ReadOnlySpan<byte> secretKey)
        {
            using var signatureScheme = new Signature("SPHINCS+-SHA256-128s-simple");
            signatureScheme.Sign(out signature, message, secretKey);
        }

        public static bool Verify(ReadOnlySpan<byte> message, ReadOnlySpan<byte> signature, ReadOnlySpan<byte> publicKey)
        {
            using var signatureScheme = new Signature("SPHINCS+-SHA256-128s-simple");
            return signatureScheme.Verify(message, signature, publicKey);
        }
    }
}