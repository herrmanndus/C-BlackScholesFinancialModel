using OptionPricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace StartupProgram
{
    public class OptionPricer : IEquityOption
    {

        private double strike;
        private OptionType optiontype;
        private DateTimeOffset expirydate;

        public OptionPricer(double strike, OptionType optiontype, DateTimeOffset expirydate)
        {
            this.strike = strike;
            this.optiontype = optiontype;
            this.expirydate = expirydate;
        }

        public DateTimeOffset ExpiryDate()
        {
            return expirydate;
        }

        public OptionType OptionType()
        {
            return optiontype;
        }

        public double Price(MarketInformation marketInformation)
        {
            Console.WriteLine("Implementing Black-Scholes");

            // compute time remaining on option (fraction of years)
            TimeSpan timetoexpiry = expirydate - marketInformation.PricingDate;
            double daystoexpiry = timetoexpiry.Days;
            double yearstoexpiry = daystoexpiry / 365;
            //Console.WriteLine(yearstoexpiry);

            if (yearstoexpiry < 0) //exception for querying negative option durations
            {
                throw new ArgumentOutOfRangeException("Remaining option duration must be ≥ 0");
            }
            if ((marketInformation.EquitySpotPrice <= 0) | (strike <= 0))
            {
                throw new ArgumentOutOfRangeException("Spot and strike prices should be positive and nonzero");
            }

            // compute cdf queries d1&d2
            double d1 = (Math.Log(marketInformation.EquitySpotPrice / strike) +
                        (marketInformation.InterestRate + (marketInformation.Volatility * marketInformation.Volatility / 2) *
                        yearstoexpiry)) / (marketInformation.Volatility * Math.Sqrt(yearstoexpiry));
            double d2 = d1 - (marketInformation.Volatility * Math.Sqrt(yearstoexpiry));

            // implement black-scholes Call model
            if (optiontype == OptionPricing.OptionType.Call)
            {
                Console.WriteLine("Call model");

                double cost = (dustinsMath.Ncum(d1)*marketInformation.EquitySpotPrice)-
                    dustinsMath.Ncum(d2)*strike*Math.Pow(Math.E,(-1*marketInformation.InterestRate*yearstoexpiry));

                return cost;
            }
            else if (optiontype == OptionPricing.OptionType.Put)
            {
                Console.WriteLine("Put model");

                double cost = (dustinsMath.Ncum(-1 * d2) * strike * Math.Pow(Math.E, (-1 * marketInformation.InterestRate * yearstoexpiry))) -
                    (dustinsMath.Ncum(-1 * d1) * marketInformation.EquitySpotPrice);
                    
                return cost;
            }
            else
            {
                throw new ArgumentException("Can only implement Call or Put option model");
            }

            // implement black-scholes put model 
        }

        public double Strike()
        {
            //Console.WriteLine("Strike Implemented");
            return strike;
        }
    }
}
