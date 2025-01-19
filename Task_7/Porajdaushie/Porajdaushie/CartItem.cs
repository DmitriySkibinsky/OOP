namespace StoreApp
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{Product.Name} x {Quantity} - {Product.Price * Quantity:C}";
        }
    }
}