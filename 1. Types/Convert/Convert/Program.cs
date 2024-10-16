using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //// Преобразование с использованием Convert
            //string input1 = "42";
            //string input2 = "3.14";
            //string input3 = "true";

            //int result1 = Convert.ToInt32(input1);
            //double result2 = Convert.ToDouble(input2);
            //bool result3 = Convert.ToBoolean(input3);

            //Console.WriteLine($"Преобразование int: {result1}");
            //Console.WriteLine($"Преобразование double: {result2}");
            //Console.WriteLine($"Преобразование bool: {result3}");


            string input = "hello";

            // Преобразование с использованием Convert
            try
            {
                // Преобразование с использованием Convert
                int result = Convert.ToInt32(input);
                Console.WriteLine($"Преобразование int: {result}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка преобразования: неверный формат");
            }
 
        }
    }
}
