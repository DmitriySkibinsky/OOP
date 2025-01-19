using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DecimalToAnyBaseConverter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtDecimal.Text, out int decimalNumber))
            {
                string selectedBase = (cmbBase.SelectedItem as ComboBoxItem)?.Content.ToString();
                if (selectedBase == "Римские")
                {
                    txtResult.Text = DecimalToRoman(decimalNumber);
                }
                else if (int.TryParse(selectedBase, out int baseNumber) && baseNumber >= 2 && baseNumber <= 16)
                {
                    if (decimalNumber < 0)
                    {
                        MessageBox.Show("Decimal number must be positive");
                    }
                    else
                    {
                        string result = DecimalToAnyBase(decimalNumber, baseNumber);
                        txtResult.Text = result;
                    }
                }
                else
                {
                    MessageBox.Show("Please select a valid base");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid decimal number");
            }
        }


        private string DecimalToAnyBase(int decimalNumber, int baseNumber)
        {
            if (decimalNumber == 0)
                return "0";

            StringBuilder result = new StringBuilder();
            while (decimalNumber > 0)
            {
                int remainder = decimalNumber % baseNumber;
                char digit = GetDigit(remainder);
                result.Insert(0, digit);
                decimalNumber /= baseNumber;
            }

            return result.ToString();
        }

        private char GetDigit(int remainder)
        {
            if (remainder < 10)
                return (char)('0' + remainder);
            else
                return (char)('A' + remainder - 10);
        }
        private string DecimalToRoman(int num)
        {
            if (num < 1 || num > 3999) return "Invalid input";
            StringBuilder result = new StringBuilder();
            var romanNumerals = new (int value, string numeral)[]
            {
        (1000, "M"), (900, "CM"), (500, "D"), (400, "CD"),
        (100, "C"), (90, "XC"), (50, "L"), (40, "XL"),
        (10, "X"), (9, "IX"), (5, "V"), (4, "IV"),
        (1, "I")
            };

            foreach (var (value, numeral) in romanNumerals)
            {
                while (num >= value)
                {
                    result.Append(numeral);
                    num -= value;
                }
            }
            return result.ToString();
        }

    }
}
