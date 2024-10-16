using System;
using System.Globalization;
using System.Windows;

namespace NewtonMethodWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ru-RU");
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            // Заменяем запятую на точку в текстовом поле
            NumberTextBox.Text = NumberTextBox.Text.Replace(",", ".");

            decimal number;
            int rootDegree;

            // Проверяем корректность ввода
            if (!decimal.TryParse(NumberTextBox.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out number) ||
                !int.TryParse(RootDegreeTextBox.Text, out rootDegree))
            {
                MessageBox.Show("Вы ввели неверные значения");
                return;
            }

            double initialGuess = GetInitialApproximation((double)number);
            ResultTextBlock3.Text = $"Первичное приближение: {initialGuess}";

            decimal epsilon = 1E-10m; // Точность
            var (result, iter) = NewtonMethod(number, rootDegree, (decimal)initialGuess, epsilon);

            double standardResult = Math.Pow((double)number, 1.0 / rootDegree);

            ResultTextBlock1.Text = $"Корень {rootDegree}-й степени из {number} (метод Ньютона) равен {result}";
            ResultTextBlock2.Text = $"Понадобилось {iter} итераций";
            ResultTextBlock4.Text = $"Корень {rootDegree}-й степени из {number} (стандартный метод) равен {standardResult}";
        }


        static (decimal result, int iterations) NewtonMethod(decimal number, int rootDegree, decimal initialGuess, decimal epsilon)
        {
            decimal guess = initialGuess;
            decimal previousGuess;
            int iter = 0;

            Application.Current.Dispatcher.Invoke(() =>
            {
                ((MainWindow)Application.Current.MainWindow).IterationsTextBlock.Text = string.Empty;
            });

            do
            {
                previousGuess = guess;
                guess = ((rootDegree - 1) * guess + number / Pow(guess, rootDegree - 1)) / rootDegree;
                iter++;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    ((MainWindow)Application.Current.MainWindow).IterationsTextBlock.Text +=
                        $"Итерация {iter}: Приближение = {guess}\n";
                });

                // Отладочные сообщения
                Console.WriteLine($"Итерация {iter}: guess = {guess}, previousGuess = {previousGuess}");

            } while (Math.Abs(guess - previousGuess) > epsilon);

            return (guess, iter);
        }

        static decimal Pow(decimal baseValue, int exponent)
        {
            decimal result = 1;
            for (int i = 0; i < exponent; i++)
            {
                result *= baseValue;
            }
            return result;
        }

        public static double GetInitialApproximation(double y)
        {
            // Заменяем десятичный разделитель в строковом представлении с '.' на ','
            // Это нужно, чтобы соответствовать текущей культуре, где запятая используется как разделитель
            string yString = y.ToString(CultureInfo.InvariantCulture).Replace('.', ',');

            if (!double.TryParse(yString, NumberStyles.Any, CultureInfo.CurrentCulture, out double yAdjusted))
            {
                throw new FormatException("Не удалось распознать число с заданным десятичным разделителем.");
            }

            int D; // Переменная для хранения количества цифр перед десятичной точкой

            // Если число больше или равно 1
            if (yAdjusted >= 1)
            {
                // Определяем количество цифр перед десятичной точкой
                D = (int)Math.Floor(Math.Log10(yAdjusted)) + 1;
                int n = D / 2; // Делим количество цифр на 2 для дальнейших вычислений

                // Возвращаем первичное приближение в зависимости от четности D
                return D % 2 == 0 ? 6 * Math.Pow(10, n - 1) : 2 * Math.Pow(10, n);
            }
            else
            {
                // Если число меньше 1, определяем количество нулей после десятичной точки
                string decimals = yString.Substring(yString.IndexOf(',') + 1);
                int zeroCount = decimals.Count(c => c == '0');
                D = -zeroCount; // Устанавливаем D со знаком минус

                int n = Math.Abs(D / 2); // Берем абсолютное значение для дальнейших вычислений
                return 2 * Math.Pow(10, n); // Возвращаем первичное приближение
            }
        }

    }
}
