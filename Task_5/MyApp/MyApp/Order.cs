public class OrderService : IOrder
{
    private string _customerAddress;

    public void SetCustomerAddress(string address)
    {
        _customerAddress = address;
    }

    public void PlaceOrder()
    {
        // Логика размещения заказа
        Console.WriteLine($"Order placed with address: {_customerAddress}");
    }

    public void CancelOrder()
    {
        // Логика отмены заказа
        Console.WriteLine("Order canceled");
    }

    public void ConfirmOrder()
    {
        // Логика подтверждения заказа
        Console.WriteLine("Order confirmed");
    }
}