using System;
using System.Collections.Generic;

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

    // Метод для представления числа как суммы степеней двойки (битовая маска)
    static void PrintBinaryRepresentation(int number)
    {
        Console.WriteLine($"{number} в двоичной системе: {Convert.ToString(number, 2)}");
        Console.WriteLine($"Представление с использованием степеней двойки:");

        int bitPosition = 0;
        while (number > 0)
        {
            if ((number & 1) == 1) // Проверяем, если младший бит равен 1
            {
                Console.WriteLine($"2^{bitPosition}");
            }
            number >>= 1; // Побитовый сдвиг вправо
            bitPosition++;
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
            PrintBinaryRepresentation(largestPrime);
        }
        else
        {
            Console.WriteLine("Простых чисел меньше N не существует.");
        }
    }
}
