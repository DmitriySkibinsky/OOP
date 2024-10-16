using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик события PreviewTextInput
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверяем вводимый символ
            bool isValid = IsValidInput(NumericTextBox.Text, e.Text);

            // Если ввод некорректный, предотвращаем его добавление в TextBox
            e.Handled = !isValid;

            // Выводим сообщение о корректности ввода
            if (isValid)
            {
                ResultLabel.Content = "Корректное вещественное число";
            }
            else
            {
                ResultLabel.Content = "Некорректный ввод!";
            }
        }

        // Метод для проверки, является ли вводимый символ допустимым
        private bool IsValidInput(string currentText, string newInput)
        {
            // Объединяем текущий текст с новым вводимым символом
            string fullText = currentText + newInput;

            // Регулярное выражение для вещественного числа со знаком
            string pattern = @"^[-]?(\d+)?([.,]\d*)?$";

            return Regex.IsMatch(fullText, pattern);
        }
    }
}
