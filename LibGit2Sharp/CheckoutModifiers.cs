using System;

namespace LibGit2Sharp
{
    /// <summary>
    /// Options controlling Checkout behavior.
    /// </summary>
    [Flags]
    public enum CheckoutModifiers
    {
        /// <summary>
        /// No checkout flags - use default behavior.
        /// </summary>
        Safe = 1 << 0,

        /// <summary>
        /// Proceed with checkout even if the index or the working tree differs from HEAD.
        /// This will throw away local changes.
        /// </summary>
        Force = 1 << 1,
        CleanIgnored = 1 << 2,
        CleanUntracked = 1 << 3
    }
}
