using ThinkOrSwimClient;
using ThinkOrSwimClient.Model;

public class ThinkOrSwimTest
{
    private ThinkOrSwimService _thinkOrSwimService = default!;

    public ThinkOrSwimTest()
    {
        _thinkOrSwimService = new ThinkOrSwimService();
    }

    public void Start(bool useMockData = false)
    {
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
