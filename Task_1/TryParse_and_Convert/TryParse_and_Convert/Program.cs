namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input1 = "42";
            string input2 = "hello";

            // Преобразование с использованием Parse
            try
            {
                int result1 = int.Parse(input1);
                Console.WriteLine($"Успешное преобразование: {result1}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка преобразования");
            }

            try
            {
                int result2 = int.Parse(input2);
                Console.WriteLine($"Успешное преобразование: {result2}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка преобразования");
            }
        }
    }
}


/*using System;

namespace ConsoleApp1
{
    class Program
    {   
        static void Main(string[] args)
        {
            string input1 = "42";
            string input2 = "hello";

            // Преобразование с использованием TryParse
            if (int.TryParse(input1, out int result1))
            {
                Console.WriteLine($"Успешное преобразование: {result1}");
            }
            else
            {
                Console.WriteLine("Ошибка преобразования");
            }

            if (int.TryParse(input2, out int result2))
            {
                Console.WriteLine($"Успешное преобразование: {result2}");
            }
            else
            {
                Console.WriteLine("Ошибка преобразования");
            }
        }
    }
}

*/
