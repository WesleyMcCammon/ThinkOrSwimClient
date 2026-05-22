namespace ThinkOrSwimClient.Model;

public class ThinkOrSwimQuoteMessage
{
    private DateTime _date = DateTime.MinValue;
    private string _symbol = string.Empty;
    private string _displaySymbol = string.Empty;
    private string _quoteType = string.Empty;
    private decimal _value = 0;

    public DateTime Date { get { return _date; } }
    public string Symbol { get { return _symbol; } }
    public string DisplaySymbol { get { return _displaySymbol; } }
    public string QuoteType { get { return _quoteType; } }
    public decimal Value { get { return _value; } }

    public ThinkOrSwimQuoteMessage(DateTime date, string symbol, string quoteType, decimal value)
    {
        _date= date;
        _symbol= symbol;
        _quoteType = quoteType;
        _value = value;
        _displaySymbol = symbol.IndexOf(':') < 0 ? _symbol : symbol.Substring(1, symbol.IndexOf(':') - 1);
    }
}
