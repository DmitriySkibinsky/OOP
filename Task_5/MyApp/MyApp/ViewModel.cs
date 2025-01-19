using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using YourNamespace;

public class ProductViewModel : INotifyPropertyChanged
{
    private readonly IProductRepository _repository;
    private readonly IOrder _orderService;

    public ProductViewModel(IProductRepository repository, IOrder orderService)
    {
        _repository = repository;
        _orderService = orderService;

        Products = new ObservableCollection<Product>();
        CartItems = new ObservableCollection<Product>();

        LoadProducts();

        AddProductCommand = new RelayCommand(AddProduct);
        AddToCartCommand = new RelayCommand<Product>(AddToCart);
        RemoveFromCartCommand = new RelayCommand<Product>(RemoveFromCart);
        BuyCommand = new RelayCommand(Buy, CanBuy);
        SetCustomerAddressCommand = new RelayCommand<string>(SetCustomerAddress);
        PlaceOrderCommand = new RelayCommand(PlaceOrder);
        CancelOrderCommand = new RelayCommand(CancelOrder);
        ConfirmOrderCommand = new RelayCommand(ConfirmOrder);
        NextPageCommand = new RelayCommand(NextPage, CanNextPage);
        PreviousPageCommand = new RelayCommand(PreviousPage, CanPreviousPage);
        OpenAddProductWindowCommand = new RelayCommand(OpenAddProductWindow);
    }

    private ObservableCollection<Product> _products;
    public ObservableCollection<Product> Products
    {
        get => _products;
        set
        {
            _products = value;
            OnPropertyChanged();
        }
    }

    private string _newProductName;
    public string NewProductName
    {
        get => _newProductName;
        set
        {
            _newProductName = value;
            OnPropertyChanged();
        }
    }

    private string _newProductDescription;
    public string NewProductDescription
    {
        get => _newProductDescription;
        set
        {
            _newProductDescription = value;
            OnPropertyChanged();
        }
    }

    private double _newProductPrice;
    public double NewProductPrice
    {
        get => _newProductPrice;
        set
        {
            _newProductPrice = value;
            OnPropertyChanged();
        }
    }

    private int _newProductQuantity;
    public int NewProductQuantity
    {
        get => _newProductQuantity;
        set
        {
            _newProductQuantity = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Product> _cartItems;
    public ObservableCollection<Product> CartItems
    {
        get => _cartItems;
        set
        {
            _cartItems = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(TotalPrice));
        }
    }

    public double TotalPrice => CartItems.Sum(p => p.Price * p.Quantity);

    private string _customerAddress;
    public string CustomerAddress
    {
        get => _customerAddress;
        set
        {
            _customerAddress = value;
            OnPropertyChanged();
            ((RelayCommand)BuyCommand).RaiseCanExecuteChanged();
        }
    }

    private int _currentPage = 1;
    public int CurrentPage
    {
        get => _currentPage;
        set
        {
            _currentPage = value;
            OnPropertyChanged();
            LoadProducts();
            ((RelayCommand)PreviousPageCommand).RaiseCanExecuteChanged();
            ((RelayCommand)NextPageCommand).RaiseCanExecuteChanged();
        }
    }

    private int _totalPages;
    public int TotalPages
    {
        get => _totalPages;
        set
        {
            _totalPages = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddProductCommand { get; }
    public ICommand AddToCartCommand { get; }
    public ICommand RemoveFromCartCommand { get; }
    public ICommand BuyCommand { get; }
    public ICommand SetCustomerAddressCommand { get; }
    public ICommand PlaceOrderCommand { get; }
    public ICommand CancelOrderCommand { get; }
    public ICommand ConfirmOrderCommand { get; }
    public ICommand NextPageCommand { get; }
    public ICommand PreviousPageCommand { get; }
    public ICommand OpenAddProductWindowCommand { get; }

    private void LoadProducts()
    {
        var allProducts = _repository.GetAllProducts();
        TotalPages = (int)Math.Ceiling(allProducts.Count / 6.0);

        var pagedProducts = allProducts.Skip((CurrentPage - 1) * 6).Take(6).ToList();
        Products.Clear();
        foreach (var product in pagedProducts)
        {
            Products.Add(product);
        }
    }

    private void AddProduct()
    {
        var newProduct = new Product
        {
            Name = NewProductName,
            Description = NewProductDescription,
            Price = NewProductPrice,
            Quantity = NewProductQuantity
        };

        _repository.AddProduct(newProduct);
        LoadProducts();
    }

    private void AddToCart(Product product)
    {
        var cartItem = CartItems.FirstOrDefault(p => p.Id == product.Id);
        if (cartItem != null)
        {
            cartItem.Quantity++;
        }
        else
        {
            var newCartItem = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = 1
            };
            CartItems.Add(newCartItem);
        }
        OnPropertyChanged(nameof(CartItems));
        OnPropertyChanged(nameof(TotalPrice));
    }

    private void RemoveFromCart(Product product)
    {
        var cartItem = CartItems.FirstOrDefault(p => p.Id == product.Id);
        if (cartItem != null)
        {
            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
            }
            else
            {
                CartItems.Remove(cartItem);
            }
        }
        OnPropertyChanged(nameof(CartItems));
        OnPropertyChanged(nameof(TotalPrice));
    }

    private void Buy()
    {
        if (string.IsNullOrEmpty(CustomerAddress))
        {
            MessageBox.Show("Please enter a customer address before buying.", "Address Required", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        CartItems.Clear();
        OnPropertyChanged(nameof(CartItems));
        OnPropertyChanged(nameof(TotalPrice));
    }

    private bool CanBuy()
    {
        return !string.IsNullOrEmpty(CustomerAddress);
    }

    private void SetCustomerAddress(string address)
    {
        _orderService.SetCustomerAddress(address);
        CustomerAddress = address;
    }

    private void PlaceOrder()
    {
        _orderService.PlaceOrder();
    }

    private void CancelOrder()
    {
        _orderService.CancelOrder();
    }

    private void ConfirmOrder()
    {
        _orderService.ConfirmOrder();
        MessageBox.Show("Your order has been successfully placed!", "Order Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void NextPage()
    {
        if (CurrentPage < TotalPages)
        {
            CurrentPage++;
        }
    }

    private bool CanNextPage()
    {
        return CurrentPage < TotalPages;
    }

    private void PreviousPage()
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;
        }
    }

    private bool CanPreviousPage()
    {
        return CurrentPage > 1;
    }

    private void OpenAddProductWindow()
    {
        var addProductWindow = new AddProductWindow(this);
        addProductWindow.ShowDialog();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool> _canExecute;

    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute();
    }

    public void Execute(object parameter)
    {
        _execute();
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler CanExecuteChanged;
}

public class RelayCommand<T> : ICommand
{
    private readonly Action<T> _execute;
    private readonly Func<T, bool> _canExecute;

    public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute((T)parameter);
    }

    public void Execute(object parameter)
    {
        _execute((T)parameter);
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
}