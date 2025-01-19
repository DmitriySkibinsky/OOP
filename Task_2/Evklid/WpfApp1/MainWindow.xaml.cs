using System;
using System.Linq;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GCDApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FindGCDButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int a = int.Parse(FirstNumberTextBox.Text);
                int b = int.Parse(SecondNumberTextBox.Text);
                int c = int.TryParse(ThirdNumberTextBox.Text, out int cValue) ? cValue : 0;
                int d = int.TryParse(FourthNumberTextBox.Text, out int dValue) ? dValue : 0;
                int g = int.TryParse(FifthNumberTextBox.Text, out int eValue) ? eValue : 0;

                var numbers = new[] { a, b, c, d, g }.Where(x => x != 0).ToArray();

                if (numbers.Length < 2)
                {
                    ResultLabel.Content = "Введите как минимум два числа.";
                    return;
                }

                int gcd = numbers.Aggregate(GCDAlg.FindGCD);
                ResultLabel.Content = $"НОД({string.Join(", ", numbers)}) = {gcd}";
            }
            catch (FormatException)
            {
                ResultLabel.Content = "Введите корректные числа.";
            }
        }
    }

    public static class GCDAlg
    {
        public static int FindGCD(int a, int b)
        {
            {
                if (a == 0) return b;
                while (b != 0)
                {
                    if (a > b)
                    {
                        a -= b;
                    }
                    else
                    {
                        b -= a;
                    }
                }
                return a;
            }
        }
    }
}
