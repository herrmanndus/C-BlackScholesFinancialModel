using System;

namespace OptionPricing
{
    /// <summary>
    /// Market information required to price an equity option
    /// This is the minimum set of information needed for a simple black-scholes type pricer, other approaches may need different market data
    /// </summary>
    public struct MarketInformation
    {
        /// <summary>
        /// pricing date
        /// </summary>
        public DateTimeOffset PricingDate;

        /// <summary>
        /// The spot price of the equity on which the option exists
        /// </summary>
        public double EquitySpotPrice;

        /// <summary>
        /// The prevailing interest rate
        /// </summary>
        public double InterestRate;

        /// <summary>
        /// The volatility of the equity price
        /// </summary>
        public double Volatility;
    }
}
