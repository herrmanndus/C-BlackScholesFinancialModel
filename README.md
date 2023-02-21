# OptionPricing - Solution notes Dustin 

My implementation of the Black-Scholes option pricing model for call and put options & unit testing using the NUnit framework.

Implementation notes by Dustin: 
- Added new startup program project to the solution to keep my implementation separate from the provided IEquityOption inferface and MarketInformation struct ("Startup Program")
  - implemented a new OptionPricer class against the provided IEquityOption interface, adding a constructor, as well as method bodies
  - implemented the OptionPricer.Price method for Black-Scholes Call and Put options, depending on the OptionType provided when instantiating a new OptionPricer
  - defined a custom dustinsMath project to implement the CDF of the Black-Scholes model 
  - set a Main method in Program.cs from where to call the model implementation after setting market information parameters of a new instance of MarketInformation 

![image](https://user-images.githubusercontent.com/31996025/197507534-a9e3c85b-20e1-47ee-84ea-d64970849492.png)

- Added a set of currently 5 (as of 2022-10-23) unit tests 
  - compare returned call option price to an externally calculated result 
  - compare returned put option price to an externally calculated result 
  - ensure that negative times to expiry throw exception
  - ensure that negative spot prices or strike prices throw exception
  - check that with zero volatility and interest rate, a call option should return spot price - strike price 
  
![image](https://user-images.githubusercontent.com/31996025/197504963-11cd7a01-4db1-4a11-b21d-e80b353c5e9d.png)












/// ORIGINAL TASK PROVIDED BY FINBOURNE 
The goal is to implement some code in C-sharp to price an equity option. This should be done by creating a class EquityOption, for example, which inherits from the provided IEquityOption interface and implements the methods specified there. 


Some background reading on options and the pricing of them can be found here:

https://en.wikipedia.org/wiki/Option_(finance)

https://www.investopedia.com/terms/s/stockoption.asp

https://www.investopedia.com/articles/optioninvestor/07/options_beat_market.asp


Visual Studio Community is available free if you need a C-sharp IDE

https://visualstudio.microsoft.com/free-developer-offers/

And a guide on using Visual Studio to create a C-sharp hello world program can be found here:

https://docs.microsoft.com/en-us/visualstudio/ide/quickstart-csharp-console?view=vs-2019
