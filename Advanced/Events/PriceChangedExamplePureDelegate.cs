namespace Events;

public class PriceChangedExamplePureDelegate
{
    public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);

    public class Stock
    {
        string _symbol;
        decimal _price;
        public Stock(string symbol) => this._symbol = symbol;

        public event PriceChangedHandler? PriceChanged;

        public decimal Price
        {
            get => _price;
            set
            {
                if (_price == value) return; // Exit if nothing has changed
                decimal oldPrice = _price;
                _price = value;
                PriceChanged?.Invoke(oldPrice, _price); // empty, fire event.
            }
        }
    }
}