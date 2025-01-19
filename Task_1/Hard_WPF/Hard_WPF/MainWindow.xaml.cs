using System;
using System.Windows;
using System.Windows.Controls;

namespace MyApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            string input = InputTextBox.Text;
            string type = ((ComboBoxItem)TypeComboBox.SelectedItem).Content.ToString();
            string convertTo = ((ComboBoxItem)ConvertToComboBox.SelectedItem).Content.ToString();

            Converter converter = new Converter();
            dynamic convertedValue = converter.Convert_input_Explicited(input, GetChoice(type));

            string explicitResult = convertedValue is string ? convertedValue : "Успешно";
            dynamic implicitValue = converter.Convert_input_Implicit(convertedValue, GetChoice(convertTo));
            string implicitResult = implicitValue is string ? implicitValue : "Успешно";

            ResultTextBlock.Text = $"Явное преобразование: {explicitResult}\nНеявное преобразование: {implicitResult}";
        }

        private int GetChoice(string type)
        {
            switch (type)
            {
                case "bool": return 1;
                case "sbyte": return 2;
                case "int": return 3;
                case "uint": return 4;
                case "long": return 5;
                case "ulong": return 6;
                case "float": return 7;
                case "double": return 8;
                case "decimal": return 9;
                case "char": return 10;
                case "string": return 11;
                case "object": return 12;
                case "byte": return 13;
                default: return -1;
            }
        }
    }

    class Converter
    {
        public dynamic Convert_input_Explicited(dynamic input, int choice)
        {
            if (input == null)
                return "Входное значение не может быть null.";

            switch (choice)
            {
                case 1: // bool
                    bool boolResult;
                    return bool.TryParse(input.ToString(), out boolResult) ? boolResult : "Не удалось преобразовать в bool.";
                case 2: // sbyte
                    sbyte sbyteResult;
                    return sbyte.TryParse(input.ToString(), out sbyteResult) ? sbyteResult : "Не удалось преобразовать в sbyte.";
                case 3: // int
                    int intResult;
                    return int.TryParse(input.ToString(), out intResult) ? intResult : "Не удалось преобразовать в int.";
                case 4: // uint
                    uint uintResult;
                    return uint.TryParse(input.ToString(), out uintResult) ? uintResult : "Не удалось преобразовать в uint.";
                case 5: // long
                    long longResult;
                    return long.TryParse(input.ToString(), out longResult) ? longResult : "Не удалось преобразовать в long.";
                case 6: // ulong
                    ulong ulongResult;
                    return ulong.TryParse(input.ToString(), out ulongResult) ? ulongResult : "Не удалось преобразовать в ulong.";
                case 7: // float
                    float floatResult;
                    return float.TryParse(input.ToString(), out floatResult) ? floatResult : "Не удалось преобразовать в float.";
                case 8: // double
                    double doubleResult;
                    return double.TryParse(input.ToString(), out doubleResult) ? doubleResult : "Не удалось преобразовать в double.";
                case 9: // decimal
                    decimal decimalResult;
                    return decimal.TryParse(input.ToString(), out decimalResult) ? decimalResult : "Не удалось преобразовать в decimal.";
                case 10: // char
                    return (input.ToString().Length == 1) ? input.ToString()[0] : "Не удалось преобразовать в char.";
                case 11: // string
                    return (string)input; 
                case 12: // object
                    return (object)input; // всегда успешно
                case 13: // byte
                    byte byteResult;
                    return byte.TryParse(input.ToString(), out byteResult) ? byteResult : "Не удалось преобразовать в byte.";
                default:
                    return "Неверный выбор.";
            }
        

    




}

        public dynamic Convert_input_Implicit(dynamic input, int choice)
        {
            switch (choice)
            {
                case 1: // bool
                    return input is bool ? "Успешно" : "Не удалось преобразовать в bool.";
                case 2: // sbyte
                    return input is sbyte ? "Успешно" : "Не удалось преобразовать в sbyte.";
                case 3: // int
                    return input is int ? "Успешно" : "Не удалось преобразовать в int.";
                case 4: // uint
                    return input is uint ? "Успешно" : "Не удалось преобразовать в uint.";
                case 5: // long
                    return input is long ? "Успешно" : "Не удалось преобразовать в long.";
                case 6: // ulong
                    return input is ulong ? "Успешно" : "Не удалось преобразовать в ulong.";
                case 7: // float
                    return input is float ? "Успешно" : "Не удалось преобразовать в float.";
                case 8: // double
                    return input is double ? "Успешно" : "Не удалось преобразовать в double.";
                case 9: // decimal
                    return input is decimal ? "Успешно" : "Не удалось преобразовать в decimal.";
                case 10: // char
                    return input is char ? "Успешно" : "Не удалось преобразовать в char."; // Неявное преобразование не должно быть успешным
                case 11: // string
                    return "Не удалось преобразовать в string."; // Прямое преобразование не должно быть успешным
                case 12: // object
                    return "Успешно"; // всегда успешно
                case 13: // byte
                    return input is byte ? "Успешно" : "Не удалось преобразовать в byte.";
                default:
                    return "Неверный выбор.";
            }
        }


    }
}


