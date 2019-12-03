// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System.Collections.Generic;
using System.Numerics;

namespace Tuckfirtle.Core.Utility
{
    public static class Base58Utility
    {
        private static char[] Characters { get; } =
        {
            '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };

        public static string Encode(byte[] payload)
        {
            var unsignedPayload = new byte[payload.Length + 1];
            unsignedPayload[unsignedPayload.Length - 1] = 0;

            for (var i = 0; i < payload.Length; i++)
                unsignedPayload[i] = payload[i];

            var payloadValue = new BigInteger(unsignedPayload);
            var result = new List<char>();

            while (payloadValue > 0)
            {
                payloadValue = BigInteger.DivRem(payloadValue, 58, out var remainder);
                result.Add(Characters[(int) remainder]);
            }

            return new string(result.ToArray());
        }
    }
}