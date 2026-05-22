using ThinkOrSwim;
using ThinkOrSwimClient.Model;

namespace ThinkOrSwimClient;

public delegate void ThinkOrSwimEventHandler(ThinkOrSwimQuoteMessage message);

public class ThinkOrSwimService
{
    private readonly List<string> _tradingSymbols = new List<string>();
    public event ThinkOrSwimEventHandler ThinkOrSwimEventHandler = default!;
    private readonly Client _client = new Client();
    private bool _keepGoing = true;

    public ThinkOrSwimService(List<string> tradingSymbols)
    {
        _tradingSymbols.AddRange(tradingSymbols);
    }

    public void Stop()
    {
        _keepGoing = false;
    }

    public void Start(bool useMockData = false) { 
        _tradingSymbols.ForEach(tradingSymbol =>
        {
            _client.Add(tradingSymbol);
        });

        if (useMockData)
            StartMockData();
        else
            RunThread();
    }

    public void StartMockData()
    {
        List<MockFeedData> mockFeedData = new List<MockFeedData>();
        try
        {
            Dictionary<string, decimal> mockSeedData = new Dictionary<string, decimal>();
            mockSeedData.Add("EUR/USD", 1.0762m);
            mockSeedData.Add("USD/JPY", 131.66m);
            mockSeedData.Add("GBP/USD", 1.21336m);
            mockSeedData.Add("USD/CAD", 1.3648m);
            mockSeedData.Add("USD/CHF", 0.92082m);
            mockSeedData.Add("AUD/USD", 0.69085m);
            mockSeedData.Add("NZD/USD", 0.64412m);

            _tradingSymbols.ForEach(instrument =>
            {
                if (mockSeedData.ContainsKey(instrument))
                {
                    decimal offset = instrument.Contains("JPY") ? .21m : .0021m;
                    mockFeedData.Add(new MockFeedData
                    {
                        Symbol = instrument,
                        Bid = mockSeedData[instrument],
                        Ask = mockSeedData[instrument] + offset
                    });
                }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        while (_keepGoing)
        {
            Thread.Sleep(1500);

            mockFeedData.ForEach(mfd =>
            {
                //var bidMessage = new ThinkOrSwimQuoteMessage
                //{
                //    Date = DateTime.Now,
                //    Symbol = mfd.Symbol,
                //    QuoteType = "BID",
                //    Value = mfd.Bid
                //};
                //var askMesage = new ThinkOrSwimQuoteMessage
                //{
                //    Date = DateTime.Now,
                //    Symbol = mfd.Symbol,
                //    QuoteType = "ASK",
                //    Value = mfd.Ask
                //};

                //if (ThinkOrSwimEventHandler != null)
                //{
                //    ThinkOrSwimEventHandler(bidMessage);
                //    ThinkOrSwimEventHandler(askMesage);
                //}

                int priceMove = DateTime.Now.Ticks % 2 == 0 ? 1 : -1;
                decimal priceMoveValue = mfd.Symbol.Contains("JPY") ? .001m : .00001m;
                mfd.Ask += (priceMove * priceMoveValue);
                mfd.Bid += (priceMove * priceMoveValue);
                Thread.Sleep(500);
            });
        }
    }

    private void RunThread()
    {
        while (_keepGoing)
        {
            foreach (var quote in _client.Quotes())
            {
                string message = string.Format("{0} {1} {2}", quote.Symbol,
                    quote.Type.ToString(), quote.Value);

                string symbol = _tradingSymbols.Where(s => s == quote.Symbol)
                    .Select(s => s)
                    .First();

                var thinkOrSwimQuoteMessage = new ThinkOrSwimQuoteMessage(
                    DateTime.Now, 
                    symbol ?? string.Empty, 
                    quote.Type.ToString(), 
                    (decimal)quote.Value);
                //var thinkOrSwimQuoteMessage = new ThinkOrSwimQuoteMessage
                //{
                //    Date = DateTime.Now,
                //    Symbol = symbol ?? String.Empty,
                //    QuoteType = quote.Type.ToString(),
                //    Value = (decimal)quote.Value
                //};

                if (ThinkOrSwimEventHandler != null) ThinkOrSwimEventHandler(thinkOrSwimQuoteMessage);
            }
        }
    }
}

internal class MockFeedData
{
    public string Symbol { get; set; } = string.Empty;
    public decimal Bid { get; set; }
    public decimal Ask { get; set; }
}
