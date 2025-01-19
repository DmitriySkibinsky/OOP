using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GCDApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddDefaultTextBoxes(3); // Добавляем три текстовых поля по умолчанию
        }

        private void AddDefaultTextBoxes(int count)
        {
            for (int i = 0; i < count; i++)
            {
                AddNumberTextBox();
            }
        }

        private void AddNumberTextBox()
        {
            var textBox = new TextBox
            {
                Width = 75,
                Height = 50,
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 16,
                TextAlignment = TextAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            NumbersWrapPanel.Children.Add(textBox);
        }

        private void AddNumberButton_Click(object sender, RoutedEventArgs e)
        {
            AddNumberTextBox(); // Добавляем новое текстовое поле
        }

        private void FindGCDButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var numbers = NumbersWrapPanel.Children.OfType<TextBox>()
                    .Select(tb => int.TryParse(tb.Text, out int value) ? value : 0)
                    .Where(x => x != 0)
                    .ToArray();

                if (numbers.Length < 2)
                {
                    ResultLabel.Content = "Введите как минимум два числа.";
                    return;
                }

                int gcd = FindGCD(numbers);
                int mincd = -1; // Инициализируем по умолчанию

                for (int i = 2; i < gcd; i++) // Изменено на i < gcd, чтобы исключить gcd
                {
                    if (gcd % i == 0) // Проверяем, делит ли i на gcd
                    {
                        mincd = i;
                        break;
                    }
                }

                // Если mincd не найден, сообщаем об этом
                if (mincd == -1)
                {
                    ResultLabel.Content = $"НОД({string.Join(", ", numbers)}) = {gcd} \nНаименьший делитель не найден (равен 1).";
                }
                else
                {
                    ResultLabel.Content = $"НОД({string.Join(", ", numbers)}) = {gcd} \nНаименьший делитель = {mincd}";
                }
            }
            catch (FormatException)
            {
                ResultLabel.Content = "Введите корректные числа.";
            }
        }

        private int FindGCD(params int[] values)
        {
            return values.Aggregate((a, b) => GCD(a, b));
        }

        private int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}
