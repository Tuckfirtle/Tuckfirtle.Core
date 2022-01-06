// Copyright (C) 2020, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System;
using System.Collections.Generic;
using System.Numerics;

namespace Tuckfirtle.Core.Utility
{
    public static class Base58Utility
    {
        private static readonly char[] Characters =
        {
            '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };

        public static string Encode(byte[] payload)
        {
            var unsignedPayload = new byte[payload.Length + 1];
            unsignedPayload[^1] = 0;

            Buffer.BlockCopy(payload, 0, unsignedPayload, 0, payload.Length);

            var payloadValue = new BigInteger(unsignedPayload);
            var result = new List<char>();
            var characters = Characters;

            while (payloadValue > 0)
            {
                payloadValue = BigInteger.DivRem(payloadValue, 58, out var remainder);
                result.Add(characters[(int) remainder]);
            }

            return new string(result.ToArray());
        }
    }
}