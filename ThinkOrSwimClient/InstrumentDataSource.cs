using ThinkOrSwimClient.Model;

namespace ThinkOrSwimClient;

internal class InstrumentDataSource
{
    private IList<Currency> _currencies = new List<Currency>();
    private IList<CurrencyPair> _currencyPairs = new List<CurrencyPair>();
    private IList<Futures> _futures = new List<Futures>();


    public InstrumentDataSource()
    {
        CreateCurrencyList();
        CreateCurrencyPairs();
        CreateFuturesList();
    }

    private void CreateFuturesList()
    {
        _futures.Add(new Futures("/ES:XCME", "E-Mini S&P 500", "", "Index"));
        _futures.Add(new Futures("/NQ:XCME", "E-Mini NASDAQ 100", "", "Index"));
        _futures.Add(new Futures("/YM:XCBT", "E-Mini Dow", "", "Index")); ;
        _futures.Add(new Futures("/RTY:XCME", "E-Mini Russell 2000", "", "Index"));
        _futures.Add(new Futures("/CL:XNYM", "Crude Oil", "", "Energy"));
        _futures.Add(new Futures("/GC:XCEC", "Gold", "", "Metals"));
    }

    private string GetDisplaySymbol(string symbol)
    {
        return _futures.Where(f => f.Symbol == symbol).Select(f => f.DisplaySymbol).First();
    }
    private void CreateCurrencyList()
    {
        _currencies.Add(new Currency("EUR", "Euro"));
        _currencies.Add(new Currency("USD", "United States Dollar"));
        _currencies.Add(new Currency("JPY", "Japanese Yen"));
        _currencies.Add(new Currency("GBP", "British Pound"));
        _currencies.Add(new Currency("AUD", "Australian Dollar"));
        _currencies.Add(new Currency("CAD", "Canadian Dollar"));
        _currencies.Add(new Currency("CHF", "Swiss Franc"));
        _currencies.Add(new Currency("NZD", "New Zealand Dollar"));
    }

    private void CreateCurrencyPairs()
    {
        var euro = _currencies.Where(c => c.Name == "EUR").First();
        var usd = _currencies.Where(c => c.Name == "USD").First();
        var jpy = _currencies.Where(c => c.Name == "JPY").First();
        var gpb = _currencies.Where(c => c.Name == "GBP").First();
        var aud = _currencies.Where(c => c.Name == "AUD").First();
        var cad = _currencies.Where(c => c.Name == "CAD").First();
        var chf = _currencies.Where(c => c.Name == "CHF").First();
        var nzd = _currencies.Where(c => c.Name == "NZD").First();

        // majors
        _currencyPairs.Add(new CurrencyPair(euro, usd, "Major"));
        _currencyPairs.Add(new CurrencyPair(usd, jpy, "Major"));
        _currencyPairs.Add(new CurrencyPair(gpb, usd, "Major"));
        _currencyPairs.Add(new CurrencyPair(aud, usd, "Major"));
        _currencyPairs.Add(new CurrencyPair(usd, cad, "Major"));
        _currencyPairs.Add(new CurrencyPair(usd, chf, "Major"));
        _currencyPairs.Add(new CurrencyPair(nzd, usd, "Major"));

        // some minors
        _currencyPairs.Add(new CurrencyPair(euro, gpb, "Minor"));
        _currencyPairs.Add(new CurrencyPair(euro, aud, "Minor"));
        _currencyPairs.Add(new CurrencyPair(euro, cad, "Minor"));
        _currencyPairs.Add(new CurrencyPair(euro, chf, "Minor"));
        _currencyPairs.Add(new CurrencyPair(euro, jpy, "Minor"));
        _currencyPairs.Add(new CurrencyPair(gpb, jpy, "Minor"));
        _currencyPairs.Add(new CurrencyPair(gpb, chf, "Minor"));
        _currencyPairs.Add(new CurrencyPair(gpb, cad, "Minor"));
        _currencyPairs.Add(new CurrencyPair(gpb, aud, "Minor"));
    }

    public IList<CurrencyPair> GetCurrencyPairs(string pairtype = "all")
    {
        return _currencyPairs.Where(c => c.PairType == pairtype || pairtype == "all").ToList();
    }

    public IList<Futures> GetFutures(string category = "all")
    {
        return _futures.Where(f => f.Category ==  category || category == "all").ToList();
    }
}