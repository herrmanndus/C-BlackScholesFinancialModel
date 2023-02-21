using System;

namespace OptionPricing
{
    public interface IEquityOption
    {
        /// <summary>
        /// When does the option expire
        /// </summary>
        /// <returns></returns>
        DateTimeOffset ExpiryDate();

        /// <summary>
        /// What is the strike price of the option
        /// </summary>
        /// <returns></returns>
        double Strike();

        /// <summary>
        /// Is this a call or a put option
        /// </summary>
        /// <returns></returns>
        OptionType OptionType();

        /// <summary>
        /// What is the value of the option, using a model (e.g Black-Scholes)
        /// </summary>
        double Price(MarketInformation marketInformation);
    }
}
