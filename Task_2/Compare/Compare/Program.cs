using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        int a = 20;
        int b = 50;

        long totalEvklidTicks = 0;
        long totalSteinTicks = 0;
        int iterations = 10;

        for (int i = 0; i < iterations; i++)
        {
            // Убедимся, что a и b остаются положительными
            int currentA = Math.Max(a - i, 1); // минимальное значение 1
            int currentB = Math.Max(b - i, 1); 

            Stopwatch sw1 = Stopwatch.StartNew();
            int evklid_gcd = EuclideanAlgorithm(currentA, currentB);
            sw1.Stop();
            totalEvklidTicks += sw1.ElapsedTicks;

            Stopwatch sw2 = Stopwatch.StartNew();
            int steinn_gcd = SteinAlgorithm(currentA, currentB);
            sw2.Stop();
            totalSteinTicks += sw2.ElapsedTicks;
        }

        double evklidTime = totalEvklidTicks / (double)Stopwatch.Frequency; // переводим тики в секунды
        double steinTime = totalSteinTicks / (double)Stopwatch.Frequency;

        Console.WriteLine($"Время, затраченное методом Евклида: {evklidTime * 1000 / iterations} миллисекунд");
        Console.WriteLine($"Время, затраченное методом Штейна: {steinTime * 1000 / iterations} миллисекунд");

        if (evklidTime > steinTime)
        {
            Console.WriteLine("Эффективнее метод Штейна");
        }
        else
        {
            Console.WriteLine("Эффективнее метод Евклида");
        }
    }

    static int SteinAlgorithm(int a, int b)
    {
        if (a == 0) return b;
        if (b == 0) return a;

        int shift;
        for (shift = 0; ((a | b) & 1) == 0; shift++)
        {
            a >>= 1;
            b >>= 1;
        }

        while ((a & 1) == 0) a >>= 1; // a нечетное
        do
        {
            while ((b & 1) == 0) b >>= 1; // b нечетное
            if (a > b)
            {
                int temp = a;
                a = b;
                b = temp; // Обмен a и b
            }
            b -= a; // b - a
        } while (b != 0);

        return a << shift; // Возвращаем результат с учетом общего множителя 2
    }

    static int EuclideanAlgorithm(int a, int b)
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
