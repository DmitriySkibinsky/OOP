using System;
using System.Linq;
using System.Windows;

namespace MatrixMultiplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MultiplyMatrices(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = ""; // Очищаем предыдущие ошибки

            try
            {
                // Пытаемся распарсить матрицы из текстовых полей
                var matrixA = ParseMatrix(MatrixA.Text);
                var matrixB = ParseMatrix(MatrixB.Text);

                // Проверка возможности умножения матриц
                if (matrixA[0].Length != matrixB.Length)
                {
                    throw new InvalidOperationException("Матрицы нельзя умножить: число столбцов первой матрицы не совпадает с числом строк второй матрицы.");
                }

                // Выполнение умножения матриц
                var result = Multiply(matrixA, matrixB);
                Result.Text = MatrixToString(result);
            }
            catch (FormatException ex)
            {
                // Ошибка при преобразовании строк в числа
                ErrorTextBlock.Text = $"Ошибка формата данных: \n{ex.Message}\nПроверьте, что все элементы матрицы — это числа.";
            }
            catch (InvalidOperationException ex)
            {
                // Ошибка при неправильной размерности матриц
                ErrorTextBlock.Text = $"Ошибка в операциях с матрицами: \n{ex.Message}";
            }
            catch (IndexOutOfRangeException ex)
            {
                // Ошибка доступа к элементам массивов (например, если одна из строк пустая)
                ErrorTextBlock.Text = $"Ошибка доступа к элементам матрицы: \n{ex.Message}\nПроверьте структуру введенных данных.";
            }
            catch (Exception ex)
            {
                // Ловим любые другие необработанные исключения
                ErrorTextBlock.Text = $"Произошла неизвестная ошибка: \n{ex.Message}";
            }
        }

        private double[][] ParseMatrix(string input)
        {
            try
            {
                // Преобразуем строки в числа
                return input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(line => line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                                .Select(double.Parse).ToArray())
                            .ToArray();
            }
            catch (FormatException ex)
            {
                throw new FormatException("Некорректный формат ввода матрицы. Убедитесь, что используете только числа и пробелы для разделения элементов.", ex);
            }
        }

        private double[][] Multiply(double[][] a, double[][] b)
        {
            int aRows = a.Length;
            int aCols = a[0].Length;
            int bCols = b[0].Length;

            var result = new double[aRows][];
            for (int i = 0; i < aRows; i++)
            {
                result[i] = new double[bCols];
                for (int j = 0; j < bCols; j++)
                {
                    for (int k = 0; k < aCols; k++)
                    {
                        result[i][j] += a[i][k] * b[k][j];
                    }
                }
            }
            return result;
        }

        private string MatrixToString(double[][] matrix)
        {
            // Преобразуем матрицу в строку для отображения
            return string.Join(Environment.NewLine, matrix.Select(row => string.Join(" ", row)));
        }
    }
}