using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using Vector3D;

namespace Vector3DVisualizer
{
    public partial class MainWindow : Window
    {
        private Vector3<double> vector1;
        private Vector3<double> vector2;

        public MainWindow()
        {
            InitializeComponent();
            // Initialize vectors with default values (0,0,0)
            vector1 = new Vector3<double>(0, 0, 0);
            vector2 = new Vector3<double>(0, 0, 0);
        }

        private void Vector1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateVectors();
        }

        private void Vector2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateVectors();
        }

        private void UpdateVectors()
        {
            try
            {
                // Считывание координат векторов
                vector1 = new Vector3<double>(
                    string.IsNullOrEmpty(Vector1X.Text) ? 0 : double.Parse(Vector1X.Text),
                    string.IsNullOrEmpty(Vector1Y.Text) ? 0 : double.Parse(Vector1Y.Text),
                    string.IsNullOrEmpty(Vector1Z.Text) ? 0 : double.Parse(Vector1Z.Text));

                vector2 = new Vector3<double>(
                    string.IsNullOrEmpty(Vector2X.Text) ? 0 : double.Parse(Vector2X.Text),
                    string.IsNullOrEmpty(Vector2Y.Text) ? 0 : double.Parse(Vector2Y.Text),
                    string.IsNullOrEmpty(Vector2Z.Text) ? 0 : double.Parse(Vector2Z.Text));

                // Очистка старых визуальных элементов
                HelixView.Children.Clear();
                HelixView.Children.Add(new GridLinesVisual3D
                {
                    Length = 10,
                    Width = 10,
                    MinorDistance = 1,
                    Thickness = 0.01
                });

                // Отображаем векторы в реальном времени
                DrawVector(vector1, Colors.Red);  // Исходный вектор 1 (красный)
                DrawVector(vector2, Colors.Blue);  // Исходный вектор 2 (синий)
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Выбор операции
                string operation = (OperationSelector.SelectedItem as ComboBoxItem)?.Content.ToString();
                Vector3<double> resultVector;
                string resultText = "";

                // Отображаем результат операции
                switch (operation)
                {
                    case "Addition":
                        resultVector = vector1 + vector2;
                        DrawVector(resultVector, Colors.Green);  // Результат сложения (зелёный)
                        resultText = $"Result: {resultVector}";
                        break;

                    case "Subtraction":
                        resultVector = vector1 - vector2;
                        DrawVector(resultVector, Colors.Green);  // Результат вычитания (зелёный)
                        resultText = $"Result: {resultVector}";
                        break;

                    case "Dot Product":
                        var dotProduct = Vector3<double>.Dot(vector1, vector2);
                        resultText = $"Dot Product: {dotProduct}";
                        break;

                    case "Cross Product":
                        resultVector = Vector3<double>.Cross(vector1, vector2);
                        DrawVector(resultVector, Colors.Green);  // Результат векторного произведения (зелёный)
                        resultText = $"Result: {resultVector}";
                        break;

                    default:
                        MessageBox.Show("Please select a valid operation.");
                        return;
                }

                // Вывод результата
                MessageBox.Show(resultText, "Operation Result");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DrawVector(Vector3<double> vector, Color color)
        {
            var arrow = new ArrowVisual3D
            {
                Point1 = new Point3D(0, 0, 0),
                Point2 = new Point3D(vector.X, vector.Y, vector.Z),
                Diameter = 0.1,
                Fill = new SolidColorBrush(color) // Ensure the SolidColorBrush is used correctly
            };

            HelixView.Children.Add(arrow);
        }
    }
}
