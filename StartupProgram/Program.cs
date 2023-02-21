// See https://aka.ms/new-console-template for more information
using OptionPricing;
using StartupProgram;
using System.Text;

//Console.WriteLine("Hello, World!");

class Startprogram
{
    public static void Main()
    { 
        Console.WriteLine("main executing:");

        //set marketinformation by implementing new MarketInformation instance;
        MarketInformation themarketsays = new MarketInformation();

        DateTimeOffset pricingdate = new DateTimeOffset(new DateTime(2021, 10, 31),
                                        TimeSpan.Zero);

        themarketsays.EquitySpotPrice = 300;
        themarketsays.Volatility = 0.2;
        themarketsays.InterestRate = 0.03;
        themarketsays.PricingDate = pricingdate;

        // make a new option pricer instance for a Call option
        DateTimeOffset expiresdate = new DateTimeOffset(new DateTime(2022, 10, 31),
                                        TimeSpan.Zero); // 1 year to expiry
        OptionPricer thisPricer = new OptionPricer(250, OptionType.Call,expiresdate);
        
            //Console.WriteLine(thisPricer.Strike());
            //Console.WriteLine(thisPricer.OptionType());
            //Console.WriteLine(thisPricer.ExpiryDate());

        Console.WriteLine(thisPricer.Price(themarketsays));

        // try implementing a put option under same conditions 
        OptionPricer thisPricerPut = new OptionPricer(350, OptionType.Put, expiresdate);
        Console.WriteLine(thisPricerPut.Price(themarketsays));

    }
}
