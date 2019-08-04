namespace Tuckfirtle.Core
{
    /// <summary>
    /// Core settings class that contains settings for this blockchain.
    /// </summary>
    public static class CoreSettings
    {
        /// <summary>
        /// Coin full name.
        /// </summary>
        public const string CoinFullName = "Tuckfirtle";

        /// <summary>
        /// Coin ticker.
        /// </summary>
        public const string CoinTicker = "TF";

        /// <summary>
        /// Coin atomic unit.
        /// </summary>
        public const ulong CoinAtomicUnit = 100000000;

        /// <summary>
        /// Current block version.
        /// </summary>
        public const int BlockVersion = 1;

        /// <summary>
        /// Genesis block difficulty.
        /// </summary>
        public const int GenesisBlockDifficulty = 1000;

        /// <summary>
        /// Tuckfirtle pow scratchpad size.
        /// </summary>
        public const int TuckfirtlePowScratchpadSize = 2 * 32 * 1024;

        /// <summary>
        /// Tuckfirtle pow memory loop round.
        /// </summary>
        public const int TuckfirtlePowMemoryLoopRound = 1;
    }
}