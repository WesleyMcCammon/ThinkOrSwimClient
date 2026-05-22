using ThinkOrSwimClient;
using ThinkOrSwimClient.Model;

public class ThinkOrSwimTest
{
    private ThinkOrSwimService _thinkOrSwimService = default!;

    public ThinkOrSwimTest()
    {
        var dataSource = new InstrumentDataSource();
        var indexFutures = dataSource.GetFutures("Index").Select(f => f.Symbol).ToList();
        var majorForex = dataSource.GetCurrencyPairs("Major").Select(c => c.Symbol).ToList();
        var combined = indexFutures.Concat(majorForex).ToList();
        _thinkOrSwimService = new ThinkOrSwimService(combined);
        _thinkOrSwimService.ThinkOrSwimEventHandler += this._thinkOrSwimService_ThinkOrSwimEventHandler;
    }

    private void _thinkOrSwimService_ThinkOrSwimEventHandler1(ThinkOrSwimQuoteMessage message) => throw new NotImplementedException();

    public void Start(bool useMockData = false)
    {
        _thinkOrSwimService.Start(useMockData);
        //_thinkOrSwimService.StartMockData();
    }

    public void Stop()
    {
        _thinkOrSwimService.Stop();        
    }

    private void _thinkOrSwimService_ThinkOrSwimEventHandler(ThinkOrSwimQuoteMessage message)
    {
        var output = $"{message.Symbol} {message.DisplaySymbol} {message.QuoteType} {message.Value}";
        Console.WriteLine(output);
    }

    public void TestDataSource()
    {
        var instrumentDataSource = new InstrumentDataSource();
        var majors = instrumentDataSource.GetCurrencyPairs("Major");
        var minors = instrumentDataSource.GetCurrencyPairs("Minor");
        var all = instrumentDataSource.GetCurrencyPairs();
        var futures = instrumentDataSource.GetFutures();
    }
}
