using System;

public class Matrix
{
    private readonly int[,] data;  // Двумерный массив для хранения данных матрицы
    public int Rows { get; }       // Количество строк
    public int Columns { get; }    // Количество столбцов

    // Конструктор для инициализации матрицы с передачей двумерного массива данных
    public Matrix(int[,] initialData)
    {
        Rows = initialData.GetLength(0);   // Количество строк
        Columns = initialData.GetLength(1); // Количество столбцов
        data = initialData;                 // Копируем данные в матрицу
    }

    // Индексатор для доступа к элементам матрицы
    public int this[int row, int column]
    {
        get { return data[row, column]; }
        set { data[row, column] = value; }
    }

    // Перегрузка оператора умножения для матриц
    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (a.Columns != b.Rows)
        {
            throw new InvalidOperationException("Невозможно умножить матрицы: количество столбцов первой матрицы должно быть равно количеству строк второй матрицы.");
        }

        Matrix result = new Matrix(new int[a.Rows, b.Columns]);

        // Умножение матриц
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < b.Columns; j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < a.Columns; k++)
                {
                    result[i, j] += a[i, k] * b[k, j];
                }
            }
        }

        return result;
    }

    // Метод для вывода матрицы на консоль
    public void Print()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Console.Write(data[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main()
    {
        // Создание первой матрицы с помощью двумерного массива
        int[,] array1 = {
            { 1, 2, 3 },
            { 4, 5, 6 }
        };
        Matrix matrix1 = new Matrix(array1);

        // Создание второй матрицы с помощью двумерного массива
        int[,] array2 = {
            { 7, 8 },
            { 9, 10 },
            { 11, 12 }
        };
        Matrix matrix2 = new Matrix(array2);

        // Умножение матриц
        Matrix result = matrix1 * matrix2;

        // Вывод результата
        Console.WriteLine("Результат умножения матриц:");
        result.Print();
    }
}
