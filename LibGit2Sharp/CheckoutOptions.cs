using LibGit2Sharp.Core;
using LibGit2Sharp.Handlers;

namespace LibGit2Sharp
{
    /// <summary>
    /// Collection of parameters controlling Checkout behavior.
    /// </summary>
    public sealed class CheckoutOptions : IConvertableToGitCheckoutOpts
    {
        /// <summary>
        /// Options controlling checkout behavior.
        /// </summary>
        public CheckoutModifiers CheckoutModifiers { get; set; }

        /// <summary>
        /// The flags specifying what conditions are
        /// reported through the OnCheckoutNotify delegate.
        /// </summary>
        public CheckoutNotifyFlags CheckoutNotifyFlags { get; set; }

        /// <summary>
        /// Delegate to be called during checkout for files that match
        /// desired filter specified with the NotifyFlags property.
        /// </summary>
        public CheckoutNotifyHandler OnCheckoutNotify { get; set; }

        /// Delegate through which checkout will notify callers of
        /// certain conditions. The conditions that are reported is
        /// controlled with the CheckoutNotifyFlags property.
        public CheckoutProgressHandler OnCheckoutProgress { get; set; }

        CheckoutStrategy IConvertableToGitCheckoutOpts.CheckoutStrategy
        {
            get
            {
                var checkoutStragegyFlag = CheckoutStrategy.GIT_CHECKOUT_NONE;
                var modifier = CheckoutModifiers;

                if (modifier.HasFlag(CheckoutModifiers.Safe) && modifier.HasFlag(CheckoutModifiers.Force))
                {
                    throw new LibGit2SharpException("Checkout strategy cannot set 'force' and 'safe' together");
                }

                if (modifier.HasFlag(CheckoutModifiers.Force))
                {
                    checkoutStragegyFlag |= CheckoutStrategy.GIT_CHECKOUT_FORCE;
                }

                if (modifier.HasFlag(CheckoutModifiers.Safe) || modifier == 0)
                {
                    checkoutStragegyFlag |= CheckoutStrategy.GIT_CHECKOUT_SAFE;
                }

                if (modifier.HasFlag(CheckoutModifiers.CleanIgnored))
                {
                    checkoutStragegyFlag |= CheckoutStrategy.GIT_CHECKOUT_REMOVE_IGNORED;
                }

                if (modifier.HasFlag(CheckoutModifiers.CleanUntracked))
                {
                    checkoutStragegyFlag |= CheckoutStrategy.GIT_CHECKOUT_REMOVE_UNTRACKED;
                }

                return checkoutStragegyFlag;
            }
        }

        /// <summary>
        /// Generate a <see cref="CheckoutCallbacks"/> object with the delegates
        /// hooked up to the native callbacks.
        /// </summary>
        /// <returns></returns>
        CheckoutCallbacks IConvertableToGitCheckoutOpts.GenerateCallbacks()
        {
            return CheckoutCallbacks.From(OnCheckoutProgress, OnCheckoutNotify);
        }
    }
}
