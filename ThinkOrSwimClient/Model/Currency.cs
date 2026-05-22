namespace ThinkOrSwimClient.Model;

public class Currency
{
    private string _name = string.Empty;
    private string _description = string.Empty;

    public string Name { get { return _name; } }
    public string Description { get { return _description; } }

    public Currency(string name, string description)
    {
        _name = name;
        _description = description;
    }
}

