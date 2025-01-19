public interface IOrder
{
    void SetCustomerAddress(string address);
    void PlaceOrder();
    void CancelOrder();
    void ConfirmOrder();
}