// Copyright (C) 2019, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

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

        public static string Encode(byte[] value)
        {
            var result = new char[EncodeLength(value)];
            var index = 0;

            foreach (var byteValue in value)
            {
                if (byteValue > 58)
                {
                    result[index] = Characters[byteValue / Characters.Length - (byteValue % Characters.Length == 0 ? 1 : 0)];
                    result[index + 1] = Characters[byteValue % Characters.Length];
                    index++;
                }
                else
                    result[index] = Characters[byteValue % Characters.Length];

                index++;
            }

            return new string(result);
        }

        private static int EncodeLength(byte[] value)
        {
            var length = 0;

            foreach (var byteValue in value)
            {
                if (byteValue <= 58)
                    length++;
                else
                    length += 2;
            }

            return length;
        }
    }
}