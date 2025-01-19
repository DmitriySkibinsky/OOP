using StoreApp;

public class ViewModel
{
    private readonly DatabaseHelper _dbHelper;

    public List<Category> Categories { get; set; }
    public List<Product> Products { get; set; }

    public ViewModel()
    {
        // Укажите путь к вашей базе данных SQLite
        _dbHelper = new DatabaseHelper("C:\\Users\\dmitr\\Desktop\\University\\OOP\\Task_7\\Porajdaushie\\db.db");

        // Загрузка данных из базы данных
        Categories = _dbHelper.GetCategories();
    }

    public List<Product> GetProductsByCategory(int categoryID)
    {
        return _dbHelper.GetProductsByCategory(categoryID);
    }
}