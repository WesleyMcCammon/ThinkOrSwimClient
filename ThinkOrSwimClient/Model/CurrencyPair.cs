namespace ThinkOrSwimClient.Model;

public class CurrencyPair
{
    private readonly Currency _quoteCurrency = default!;
    private readonly Currency _baseCurrency = default!;
    private string _symbol = string.Empty;
    private string _description = string.Empty;
    private string _pairtype = string.Empty;

    public string Symbol { get { return _symbol; } }
    public string Description { get { return _description; } }
    public string PairType { get { return _pairtype; } }

    public CurrencyPair(Currency quoteCurrency, Currency baseCurrency, string pairtype)
    {
        _quoteCurrency = quoteCurrency;
        _baseCurrency = baseCurrency;
        _pairtype = pairtype;
        _symbol = $"{quoteCurrency.Name}/{baseCurrency.Name}";
        _description = $"{quoteCurrency.Description}/{baseCurrency.Description}";
    }
}
