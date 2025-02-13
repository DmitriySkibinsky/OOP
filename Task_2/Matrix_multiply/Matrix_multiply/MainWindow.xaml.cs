﻿using System;
using System.Linq;
using System.Windows;

namespace MatrixMultiply
{
    public partial class MainWindow : Window
    {
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FillRandomNumbers(object sender, RoutedEventArgs e)
        {
            MatrixA.Text = GenerateRandomMatrix(3, 3);
            MatrixB.Text = GenerateRandomMatrix(3, 3);
        }

        private string GenerateRandomMatrix(int rows, int cols)
        {
            double[][] matrix = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                matrix[i] = new double[cols];
                for (int j = 0; j < cols; j++)
                {
                    matrix[i][j] = random.Next(1, 10);
                }
            }
            return MatrixToString(matrix);
        }

        private void MultiplyMatrices(object sender, RoutedEventArgs e)
        {
            try
            {
                var matrixA = ParseMatrix(MatrixA.Text);
                var matrixB = ParseMatrix(MatrixB.Text);

                if (matrixA[0].Length != matrixB.Length)
                {
                    Result.Text = "Матрицы нельзя \nумножить!";
                    return;
                }

                var result = Multiply(matrixA, matrixB);
                Result.Text = MatrixToString(result);
            }
            catch (Exception ex)
            {
                Result.Text = "Ошибка: " + ex.Message;
            }
        }

        private double[][] ParseMatrix(string input)
        {
            return input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(line => line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                            .Select(double.Parse).ToArray())
                        .ToArray();
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
            return string.Join(Environment.NewLine, matrix.Select(row => string.Join(" ", row)));
        }
    }
}