// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using Tuckfirtle.OpenQuantumSafe;

namespace Tuckfirtle.Core.Utility
{
    /// <summary>
    /// Utility class that contains all asymmetric algorithm functions.
    /// </summary>
    public static class AsymmetricAlgorithmUtility
    {
        public static void GenerateKeypair(out byte[] publicKey, out byte[] privateKey)
        {
            using (var signatureScheme = new Signature("SPHINCS+-SHA256-128s-simple"))
                signatureScheme.GenerateKeypair(out publicKey, out privateKey);
        }

        public static void Sign(out byte[] signature, in byte[] message, in byte[] secretKey)
        {
            using (var signatureScheme = new Signature("SPHINCS+-SHA256-128s-simple"))
                signatureScheme.Sign(out signature, message, secretKey);
        }

        public static bool Verify(in byte[] message, in byte[] signature, in byte[] publicKey)
        {
            using (var signatureScheme = new Signature("SPHINCS+-SHA256-128s-simple"))
                return signatureScheme.Verify(message, signature, publicKey);
        }
    }
}