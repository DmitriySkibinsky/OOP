using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;

public class Product : INotifyPropertyChanged
{
    private int _quantity;
    private ImageSource _imageSource;

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }

    public int Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            OnPropertyChanged();
        }
    }

    public ImageSource ImageSource
    {
        get => _imageSource;
        set
        {
            _imageSource = value;
            OnPropertyChanged();
        }
    }

    public Product()
    {
        // Пример загрузки изображения из файла
        ImageSource = new BitmapImage(new Uri("./img.png", UriKind.Relative));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}