using MarketWatchDataSource;
using ThinkOrSwimClient;
using ThinkOrSwimClient.Model;

public class ThinkOrSwimTest
{
    private ThinkOrSwimService _thinkOrSwimService = default!;
    private MarketWatchDataSourceService _marketDataSource = default!;

    public ThinkOrSwimTest()
    {
        _marketDataSource = new MarketWatchDataSourceService();
        _thinkOrSwimService = new ThinkOrSwimService();
    }

    public void Start(bool useMockData = false)
    {
        var indexFutures = _marketDataSource.GetFutures("Index").Select(f => f.Symbol).ToList();
        var majorForex = _marketDataSource.GetCurrencyPairs("Major").Select(c => c.Symbol).ToList();
        var combined = indexFutures.Concat(majorForex).ToList();

        _thinkOrSwimService.SetTradingSymbols(combined);
        _thinkOrSwimService.ThinkOrSwimEventHandler += _thinkOrSwimService_ThinkOrSwimEventHandler;
        _thinkOrSwimService.Start(useMockData);
    }

    public void Stop()
    {
        _thinkOrSwimService.Stop();
        _thinkOrSwimService.ThinkOrSwimEventHandler -= _thinkOrSwimService_ThinkOrSwimEventHandler;
    }

    private void _thinkOrSwimService_ThinkOrSwimEventHandler(ThinkOrSwimQuoteMessage message)
    {
        var output = $"{message.Symbol} {message.DisplaySymbol} {message.QuoteType} {message.Value}";
        Console.WriteLine(output);
    }
}
