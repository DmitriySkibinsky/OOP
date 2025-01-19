namespace StoreApp
{
    public class ShoppingCart
    {
        private static ShoppingCart _instance;
        private List<CartItem> _cartItems;

        private ShoppingCart()
        {
            _cartItems = new List<CartItem>();
        }

        public static ShoppingCart Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ShoppingCart();
                }
                return _instance;
            }
        }

        public List<CartItem> CartItems => _cartItems;

        public void AddToCart(Product product)
        {
            var existingItem = _cartItems.FirstOrDefault(item => item.Product.ProductID == product.ProductID);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                _cartItems.Add(new CartItem { Product = product, Quantity = 1 });
            }
        }

        public void ClearCart()
        {
            _cartItems.Clear();
        }
    }
}