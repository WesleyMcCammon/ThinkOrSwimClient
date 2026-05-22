namespace ThinkOrSwimClient.Model;

public class Futures
{
    private string _baseSymbol = string.Empty;
    private string _displaySymbol = string.Empty;
    private string _description = string.Empty;
    private string _frontMonth = string.Empty;
    private string _category = string.Empty;

    public string Symbol { get { return _baseSymbol; } }
    public string DisplaySymbol { get { return _displaySymbol; } }
    public string Description { get { return _description; } }
    public string FrontMonth { get { return _frontMonth; } }
    public string Category { get { return _category; } }

    public Futures(string baseSymbol, string description, string frontMonth, string category)
    {
        this._baseSymbol = baseSymbol;
        this._description = description;
        this._frontMonth = frontMonth;
        this._category = category;

        this._displaySymbol = baseSymbol.Substring(1, baseSymbol.IndexOf(':') - 1);
    }
}