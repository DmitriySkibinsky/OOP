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
                MessageBox.Show($"Ошибка формата данных: {ex.Message}\nПроверьте, что все элементы матрицы — это числа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException ex)
            {
                // Ошибка при неправильной размерности матриц
                MessageBox.Show($"Ошибка в операциях с матрицами: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (IndexOutOfRangeException ex)
            {
                // Ошибка доступа к элементам массивов (например, если одна из строк пустая)
                MessageBox.Show($"Ошибка доступа к элементам матрицы: {ex.Message}\nПроверьте структуру введенных данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Ловим любые другие необработанные исключения
                MessageBox.Show($"Произошла неизвестная ошибка: {ex.Message}", "Неизвестная ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
