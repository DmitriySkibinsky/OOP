using System;
using System.Text;

namespace MyApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Устанавливаем кодировку вывода консоли
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Начало программы");
            Console.WriteLine("Введите число");
            string val1 = Console.ReadLine();
            Console.WriteLine("Вы ввели: " + val1);
            Console.WriteLine("Определите тип введенной переменной");
            Console.WriteLine(" 1. bool \n 2. sbyte \n 3. int \n 4. uint \n 5. long \n 6. ulong \n 7. float \n 8. double \n 9. decimal \n 10. char \n 11. string \n 12. object \n 13. byte");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Неверный выбор.");
                return;
            }

            Converter converter = new Converter();
            dynamic answ = converter.Convert_input_Expliced(val1, choice);

            if (answ is string && answ.StartsWith("Не удалось преобразовать в "))
            {
                Console.WriteLine(answ);
                return;
            }
            else
            {
                Console.WriteLine("В какой тип проебразовать?");
                Console.WriteLine(" 1. bool \n 2. sbyte \n 3. int \n 4. uint \n 5. long \n 6. ulong \n 7. float \n 8. double \n 9. decimal \n 10. char \n 11. string \n 12. object \n 13. byte");

                if (!int.TryParse(Console.ReadLine(), out int choice2))
                {
                    Console.WriteLine("Неверный выбор.");
                    return;
                }

                Console.WriteLine("Каким преобразованием воспользоваться \n 1. Явное \n 2. Неявное");
                if (!int.TryParse(Console.ReadLine(), out int choice_type))
                {
                    Console.WriteLine("Неверный выбор.");
                    return;
                }

                Converter converter2 = new Converter();

                if (choice_type == 1)
                {
                    dynamic fin = converter2.Convert_input_Expliced(answ, choice2);

                    if (fin is string && fin.StartsWith("Не удалось преобразовать в "))
                    {
                        Console.WriteLine(fin);
                    }
                    else
                    {
                        Console.WriteLine("Преобразование удалось");
                    }
                }
                else if (choice_type == 2)
                {
                    dynamic fin = converter2.Convert_input_Implicit(answ, choice2);

                    if (fin is string && fin.StartsWith("Не удалось преобразовать в "))
                    {
                        Console.WriteLine(fin);
                    }
                    else
                    {
                        Console.WriteLine("Преобразование удалось");
                    }
                }
                else
                {
                    Console.WriteLine("Неверный выбор.");
                }
            }
        }
    }

    class Converter
    {
        public dynamic Convert_input_Expliced(dynamic input, int choice)
        {
            string inputStr = input.ToString(); // Преобразуем dynamic в string

            switch (choice)
            {
                case 1:
                    if (bool.TryParse(inputStr, out bool boolValue))
                    {
                        return boolValue;
                    }
                    else
                    {
                        return "Не удалось преобразовать в bool.";
                    }
                case 2:
                    if (sbyte.TryParse(inputStr, out sbyte sbyteValue))
                    {
                        return sbyteValue;
                    }
                    else
                    {
                        return "Не удалось преобразовать в sbyte.";
                    }
                case 3:
                    if (int.TryParse(inputStr, out int intValue))
                    {
                        return intValue;
                    }
                    else
                    {
                        return "Не удалось преобразовать в int.";
                    }
                case 4:
                    if (uint.TryParse(inputStr, out uint uintValue))
                    {
                        return uintValue;
                    }
                    else
                    {
                        return "Не удалось преобразовать в uint.";
                    }
                case 5:
                    if (long.TryParse(inputStr, out long longValue))
                    {
                        return longValue;
                    }
                    else
                    {
                        return "Не удалось преобразовать в long.";
                    }
                case 6:
                    if (ulong.TryParse(inputStr, out ulong ulongValue))
                    {
                        return ulongValue;
                    }
                    else
                    {
                        return "Не удалось преобразовать в ulong.";
                    }
                case 7:
                    if (float.TryParse(inputStr, out float floatValue))
                    {
                        return floatValue;
                    }
                    else
                    {
                        return "Не удалось преобразовать в float.";
                    }
                case 8:
                    if (double.TryParse(inputStr, out double doubleValue))
                    {
                        return doubleValue;
                    }
                    else
                    {
                        return "Не удалось преобразовать в double.";
                    }
                case 9:
                    if (decimal.TryParse(inputStr, out decimal decimalValue))
                    {
                        return decimalValue;
                    }
                    else
                    {
                        return "Не удалось преобразовать в decimal.";
                    }
                case 10:
                    if (char.TryParse(inputStr, out char charValue))
                    {
                        return charValue;
                    }
                    else
                    {
                        return "Не удалось преобразовать в char.";
                    }
                case 11:
                    return inputStr;
                case 12:
                    object objectValue = inputStr;
                    return objectValue;
                case 13:
                    if (byte.TryParse(inputStr, out byte byteValue))
                    {
                        return byteValue;
                    }
                    else
                    {
                        return "Не удалось преобразовать в byte.";
                    }

                default:
                    return "Неверный выбор.";
            }
        }

        public dynamic Convert_input_Implicit(dynamic input, int choice)
        {
            switch (choice)
            {
                case 1:
                    if (input is bool)
                    {
                        return input;
                    }
                    else
                    {
                        return "Не удалось преобразовать в bool.";
                    }
                case 2:
                    if (input is sbyte)
                    {
                        return input;
                    }
                    else
                    {
                        return "Не удалось преобразовать в sbyte.";
                    }
                case 3:
                    if (input is int)
                    {
                        return input;
                    }
                    else
                    {
                        return "Не удалось преобразовать в int.";
                    }
                case 4:
                    if (input is uint)
                    {
                        return input;
                    }
                    else
                    {
                        return "Не удалось преобразовать в uint.";
                    }
                case 5:
                    if (input is long)
                    {
                        return input;
                    }
                    else
                    {
                        return "Не удалось преобразовать в long.";
                    }
                case 6:
                    if (input is ulong)
                    {
                        return input;
                    }
                    else
                    {
                        return "Не удалось преобразовать в ulong.";
                    }
                case 7:
                    if (input is float)
                    {
                        return input;
                    }
                    else
                    {
                        return "Не удалось преобразовать в float.";
                    }
                case 8:
                    if (input is double)
                    {
                        return input;
                    }
                    else
                    {
                        return "Не удалось преобразовать в double.";
                    }
                case 9:
                    if (input is decimal)
                    {
                        return input;
                    }
                    else
                    {
                        return "Не удалось преобразовать в decimal.";
                    }
                case 10:
                    if (input is char)
                    {
                        return input;
                    }
                    else
                    {
                        return "Не удалось преобразовать в char.";
                    }
                case 11:
                    return input.ToString();
                case 12:
                    object objectValue = input;
                    return objectValue;
                case 13:
                    if (input is byte)
                    {
                        return input;
                    }
                    else
                    {
                        return "Не удалось преобразовать в byte.";
                    }

                default:
                    return "Неверный выбор.";
            }
        }
    }
}