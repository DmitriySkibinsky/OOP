namespace ShopApp
{
    public interface IIterator<T>
    {
        T Current { get; }
        bool MoveNext();
        void Reset();
    }

    public class ProductIterator : IIterator<Product>
    {
        private List<Product> _products;
        private int _position;

        public ProductIterator(List<Product> products)
        {
            _products = products;
            _position = -1;
        }

        public Product Current => _products[_position];

        public bool MoveNext()
        {
            _position++;
            return _position < _products.Count;
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}