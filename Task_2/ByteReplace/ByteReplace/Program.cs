using System;

class Program
{
    // Метод для нахождения самого большого простого числа меньше N
    static int FindLargestPrime(int N)
    {
        // Метод для проверки, является ли число простым
        bool IsPrime(int num)
        {
            if (num < 2) return false;
            for (int i = 2; i * i <= num; i++)
            {
                if (num % i == 0)
                    return false;
            }
            return true;
        }

        // Поиск самого большого простого числа меньше N
        for (int i = N - 1; i > 1; i--)
        {
            if (IsPrime(i))
                return i;
        }
        return -1; // Если простых чисел нет
    }

    // Рекурсивный метод для генерации выражения сдвига
    private static string RepresentWithTwoAndShift(int number)
    {
        if (number == 0) return "2";
        if (number == 1) return "2 >> ";
        if (number == 2) return "2";
        if (number == 3) return "2 + (2 >> )";
        if (number == 4) return "2 << ";
        if (number == 5) return "(2 << ) + (2 >> )";

        int highestBit = 0;
        while ((1 << (highestBit + 1)) <= number)
        {
            highestBit++;
        }

        int remainder = number - (1 << highestBit);
        if (remainder != 0)
        {
            if (highestBit == 0)
            {
                return $"2 + {RepresentWithTwoAndShift(remainder)}";
            }
            else
            {
                if (highestBit % 2 == 0) 
                    return $"2 << ({RepresentWithTwoAndShift(highestBit)}) + {RepresentWithTwoAndShift(remainder)}";
                else
                {
                    if (highestBit > 3) 
                        return $"2 << ({RepresentWithTwoAndShift(highestBit - 1)}) + 2 << ({RepresentWithTwoAndShift(highestBit - 1)}) + {RepresentWithTwoAndShift(remainder)}";
                    else 
                        return $"2 << {RepresentWithTwoAndShift(highestBit - 1)} + 2 << {RepresentWithTwoAndShift(highestBit - 1)} + {RepresentWithTwoAndShift(remainder)}";
                }
            }
        }
        else
        {
            if (highestBit == 0)
            {
                return $"2";
            }
            else
            {
                return $"2 << ({RepresentWithTwoAndShift(highestBit)})";
            }
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Введите число N:");
        int N = int.Parse(Console.ReadLine());

        int largestPrime = FindLargestPrime(N);

        if (largestPrime != -1)
        {
            Console.WriteLine($"Самое большое простое число меньше {N}: {largestPrime}");
            string expression = RepresentWithTwoAndShift(largestPrime);
            Console.WriteLine($"Выражение для {largestPrime}: {expression}");
        }
        else
        {
            Console.WriteLine("Простых чисел меньше N не существует.");
        }
    }
}