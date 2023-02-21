using StartupProgram;
using System;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;

namespace OptionPricing.Tests
{
    /// <summary>
    /// Add some unit testing for your option
    /// </summary>
    /// 
    public class Tests
    {
        //could have added a [SetUp] method here, but decided against it for now...

        [Test]
        public void No_Volatility_No_Interest()
        {
            Console.WriteLine("Unit testing, 1: option price should be [spot - strike] for zero volatility and interest");
            // Goal: check if Pricer returns option price of spot - strike price if r and sigma are 0 (sanity check for Call model)

            // ARRANGE
            // set market information 
            MarketInformation themarketsays2 = new MarketInformation();

            DateTimeOffset pricingdate = new DateTimeOffset(new DateTime(2021, 10, 31),
                                            TimeSpan.Zero);

            themarketsays2.EquitySpotPrice = 300;
            themarketsays2.Volatility = 0.0;
            themarketsays2.InterestRate = 0.00;
            themarketsays2.PricingDate = pricingdate;

            // make a new option pricer instance for a Call option
            DateTimeOffset expiresdate = new DateTimeOffset(new DateTime(2023, 10, 31),
                                            TimeSpan.Zero); // 1 year to expiry
            OptionPricer thisPricer2 = new OptionPricer(250, OptionType.Call, expiresdate);

            // ACT 
            double option_price = thisPricer2.Price(themarketsays2);

            // ASSERT
            Assert.IsTrue(option_price == themarketsays2.EquitySpotPrice-250);
        }

        [Test]
        public void Negative_Time_To_Expiry_Throws()
        {
            Console.WriteLine("Unit testing, 2: negative option duration throws exception");
            // Goal: check if Pricer throws exception if option duration is negative

            // ARRANGE
            // set market information 
            MarketInformation themarketsays2 = new MarketInformation();

            DateTimeOffset pricingdate = new DateTimeOffset(new DateTime(2022, 10, 31),
                                            TimeSpan.Zero);

            themarketsays2.EquitySpotPrice = 300;
            themarketsays2.Volatility = 0.2;
            themarketsays2.InterestRate = 0.03;
            themarketsays2.PricingDate = pricingdate;

            // make a new option pricer instance for a Call option
            DateTimeOffset expiresdate = new DateTimeOffset(new DateTime(2021, 10, 31),
                                            TimeSpan.Zero); // 1 year to expiry
            OptionPricer thisPricer2 = new OptionPricer(250, OptionType.Call, expiresdate);

            // ACT + ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => thisPricer2.Price(themarketsays2));

            // need the '() =>' because thisPricer2.Price tries to return double but needs to be "TestDelegate"
        }

        [Test]
        public void Spot_Or_Strike_EqualZero_Throws()
        {
            Console.WriteLine("Unit testing, 3: should not compute option price for spot or strike prices <= 0");
            // Goal: check if accidentally inputing a negative or zero value price correctly throws an exception

            // ARRANGE
            MarketInformation themarketsays2 = new MarketInformation();
            DateTimeOffset pricingdate = new DateTimeOffset(new DateTime(2021, 10, 31),
                                            TimeSpan.Zero);
            themarketsays2.EquitySpotPrice = -300;
            themarketsays2.Volatility = 0.2;
            themarketsays2.InterestRate = 0.03;
            themarketsays2.PricingDate = pricingdate;

            // make a new option pricer instance for a Call option
            DateTimeOffset expiresdate = new DateTimeOffset(new DateTime(2023, 10, 31),
                                            TimeSpan.Zero); // 1 year to expiry
            OptionPricer thisPricer2 = new OptionPricer(250, OptionType.Call, expiresdate);

            // ACT / ASSERT
            // assert exception for negative EquitySpotPrice
            Assert.Throws<ArgumentOutOfRangeException>(() => thisPricer2.Price(themarketsays2));

            // assert exception for Strike price == 0
            OptionPricer thisPricer3 = new OptionPricer(0, OptionType.Call, expiresdate);
            themarketsays2.EquitySpotPrice = 300;
            Assert.Throws<ArgumentOutOfRangeException>(() => thisPricer3.Price(themarketsays2));

        }

        [Test]
        public void Compare_To_External_Result_Call()
        {
            Console.WriteLine("Unit testing, 4: compare my B-S model implementation to external calculator: Call");
            // Goal: check if computed call option price matches an online calculator implementing the same B-S model 
            // https://goodcalculators.com/black-scholes-calculator/
            // spot price: 100, strike price: 95, time to exp: 1 year, volatility: 15%, interest rate: 3%
            // Call Price: $10.47

            // ARRANGE
            // set market information 
            MarketInformation themarketsays2 = new MarketInformation();

            DateTimeOffset pricingdate = new DateTimeOffset(new DateTime(2021, 10, 31),
                                            TimeSpan.Zero);

            themarketsays2.EquitySpotPrice = 100;
            themarketsays2.Volatility = 0.15;
            themarketsays2.InterestRate = 0.03;
            themarketsays2.PricingDate = pricingdate;

            // make a new option pricer instance for a Call option
            DateTimeOffset expiresdate = new DateTimeOffset(new DateTime(2022, 10, 31),
                                            TimeSpan.Zero); // 1 year to expiry
            OptionPricer thisPricer2 = new OptionPricer(95, OptionType.Call, expiresdate);

            // ACT / ASSERT
            double call_price = Math.Round(thisPricer2.Price(themarketsays2),2);
            Assert.IsTrue(call_price == 10.47);
        }

        [Test]
        public void Compare_To_External_Result_Put()
        {
            Console.WriteLine("Unit testing, 5: compare my B-S model implementation to external calculator: Put");
            // Goal: check if computed put option price matches an online calculator implementing the same B-S model 
            // https://goodcalculators.com/black-scholes-calculator/
            // spot price: 100, strike price: 95, time to exp: 1 year, volatility: 15%, interest rate: 3%
            // --> Put Price: $2.66

            // ARRANGE
            // set market information 
            MarketInformation themarketsays2 = new MarketInformation();

            DateTimeOffset pricingdate = new DateTimeOffset(new DateTime(2021, 10, 31),
                                            TimeSpan.Zero);

            themarketsays2.EquitySpotPrice = 100;
            themarketsays2.Volatility = 0.15;
            themarketsays2.InterestRate = 0.03;
            themarketsays2.PricingDate = pricingdate;

            DateTimeOffset expiresdate = new DateTimeOffset(new DateTime(2022, 10, 31),
                                            TimeSpan.Zero); // 1 year to expiry

            // ACT / ASSERT
            OptionPricer thisPricer3 = new OptionPricer(95, OptionType.Put, expiresdate);
            double put_price = Math.Round(thisPricer3.Price(themarketsays2), 2);
            Assert.IsTrue(put_price == 2.66);
        }
    }

}

//public class Program
//{
//    static void Main()
//    {
//        Tests newTest = new Tests();
//        newTest.TestTest();
//         Console.WriteLine("XX");
//        }
//    }
//}
