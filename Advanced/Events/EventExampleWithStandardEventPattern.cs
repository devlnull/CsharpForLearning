namespace Events;

public class EventExampleWithStandardEventPattern
{
    public void Execute()
    {
        Stock stock = new Stock("THPW");
        stock.Price = 27.10M;
        // Register with the PriceChanged event
        stock.PriceChanged += stock_PriceChanged;
        stock.Price = 31.59M;
    }

    void stock_PriceChanged(object sender, PriceChangedEventArgs e)
    {
        if ((e.NewPrice - e.LastPrice) / e.LastPrice > 0.1M)
            Console.WriteLine("Alert, 10% stock price increase!");
    }
}

public class Stock
{
    string _symbol;
    decimal _price;
    public Stock(string symbol) => this._symbol = symbol;
    public event EventHandler<PriceChangedEventArgs>? PriceChanged;

    protected virtual void OnPriceChanged(PriceChangedEventArgs e)
    {
        PriceChanged?.Invoke(this, e);
    }

    public decimal Price
    {
        get => _price;
        set
        {
            if (_price == value) return;
            decimal oldPrice = _price;
            _price = value;
            OnPriceChanged(new PriceChangedEventArgs(oldPrice, _price));
        }
    }
}

public class PriceChangedEventArgs : EventArgs
{
    public readonly decimal LastPrice;
    public readonly decimal NewPrice;

    public PriceChangedEventArgs(decimal lastPrice, decimal newPrice)
    {
        LastPrice = lastPrice;
        NewPrice = newPrice;
    }
}