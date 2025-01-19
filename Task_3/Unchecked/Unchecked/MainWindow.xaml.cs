using System;
using System.Windows;

namespace IntegerOverflow
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int number1 = int.Parse(Number1TextBox.Text);
                int number2 = int.Parse(Number2TextBox.Text);

                checked
                {
                    int result = number1 * number2;
                    ResultTextBlock.Text = result.ToString();
                }
            }
            catch (OverflowException ex)
            {
                ResultTextBlock.Text = "Переполнение!";
                ResultTextBlock.Foreground = System.Windows.Media.Brushes.Red;
            }
            catch (FormatException ex)
            {
                ResultTextBlock.Text = "Неверный формат числа!";
                ResultTextBlock.Foreground = System.Windows.Media.Brushes.Red;
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = "Произошла ошибка: " + ex.Message;
                ResultTextBlock.Foreground = System.Windows.Media.Brushes.Red;
            }
        }
    }
}