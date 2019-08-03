using System.Numerics;

namespace Tuckfirtle.Core.Utility
{
    /// <summary>
    /// Class containing utility function for difficulty.
    /// </summary>
    public static class DifficultyUtility
    {
        private const int MaxDifficultyByteSize = 256 / 8;

        /// <summary>
        /// Get the minimum difficulty value.
        /// </summary>
        public static BigInteger MinDifficultyValue { get; } = BigInteger.One;

        /// <summary>
        /// Get the maximum difficulty value.
        /// </summary>
        public static BigInteger MaxDifficultyValue { get; } 

        static unsafe DifficultyUtility()
        {
            var biggestPowValue = new byte[MaxDifficultyByteSize + 1];

            fixed (byte* biggestPowValuePtr = biggestPowValue)
            {
                *(biggestPowValuePtr + MaxDifficultyByteSize) = 0;

                var biggestPowValueBytePtr = biggestPowValuePtr;

                for (var i = 0; i < MaxDifficultyByteSize; i++)
                    *biggestPowValueBytePtr++ = byte.MaxValue;

                MaxDifficultyValue = new BigInteger(biggestPowValue);
            }
        }

        /// <summary>
        /// Get the target pow value by difficulty.
        /// </summary>
        /// <param name="difficulty">Difficulty value.</param>
        /// <returns>The target pow value.</returns>
        public static BigInteger GetTargetPowValue(BigInteger difficulty)
        {
            return MaxDifficultyValue / difficulty;
        }

        /// <summary>
        /// Get the difficulty value by target pow value.
        /// </summary>
        /// <param name="targetPowValue">Target pow value.</param>
        /// <returns>The difficulty of the target pow value.</returns>
        public static BigInteger GetDifficulty(BigInteger targetPowValue)
        {
            return MaxDifficultyValue / targetPowValue;
        }
    }
}