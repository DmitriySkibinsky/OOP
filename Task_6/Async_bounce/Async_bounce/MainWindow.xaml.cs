using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MultithreadingApp
{
    public partial class MainWindow : Window
    {
        private List<Ball> balls = new List<Ball>();
        private bool isRunning = false;
        private bool isDragging = false;
        private Ellipse draggedEllipse;
        private Point dragStartPoint;
        private Mutex mutex = new Mutex(); // Мьютекс для синхронизации

        public MainWindow()
        {
            InitializeComponent();
            InitializeBalls();
        }

        private void InitializeBalls()
        {
            var ball1 = new Ball(100, 100, 2, 2, Brushes.Red);
            var ball2 = new Ball(600, 100, 2, 2, Brushes.Blue);
            balls.Add(ball1);
            balls.Add(ball2);

            foreach (var ball in balls)
            {
                var ellipse = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = ball.Color
                };
                Canvas.SetLeft(ellipse, ball.X);
                Canvas.SetTop(ellipse, ball.Y);
                ellipse.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
                ellipse.MouseMove += Ellipse_MouseMove;
                ellipse.MouseLeftButtonUp += Ellipse_MouseLeftButtonUp;
                canvas.Children.Add(ellipse);
            }
        }

        private Semaphore semaphore = new Semaphore(2, 2); // Семафор с максимум 2 потоками

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(X1TextBox.Text, out double x1) || !double.TryParse(Y1TextBox.Text, out double y1) ||
                !double.TryParse(X2TextBox.Text, out double x2) || !double.TryParse(Y2TextBox.Text, out double y2) ||
                !double.TryParse(SpeedX1TextBox.Text, out double speedX1) || !double.TryParse(SpeedY1TextBox.Text, out double speedY1) ||
                !double.TryParse(SpeedX2TextBox.Text, out double speedX2) || !double.TryParse(SpeedY2TextBox.Text, out double speedY2))
            {
                MessageBox.Show("Please enter valid numeric coordinates and speeds.");
                return;
            }

            isRunning = true;
            var ball1 = new Ball(x1, y1, speedX1, speedY1, Brushes.Red);
            var ball2 = new Ball(x2, y2, speedX2, speedY2, Brushes.Blue);
            balls.Clear();
            balls.Add(ball1);
            balls.Add(ball2);

            foreach (var ball in balls)
            {
                var ellipse = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = ball.Color
                };
                Canvas.SetLeft(ellipse, ball.X);
                Canvas.SetTop(ellipse, ball.Y);
                ellipse.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
                ellipse.MouseMove += Ellipse_MouseMove;
                ellipse.MouseLeftButtonUp += Ellipse_MouseLeftButtonUp;
                canvas.Children.Add(ellipse);
            }

            // Запуск движения шариков с использованием Parallel.For и Semaphore
            var task = Task.Run(() => MoveBallsWithParallelFor());
            await task;
        }

        private void MoveBallsWithParallelFor()
        {
            while (isRunning)
            {
                // Используем Parallel.For для параллельного выполнения движения шариков
                Parallel.For(0, balls.Count, i =>
                {
                    semaphore.WaitOne(); // Захватываем семафор
                    try
                    {
                        mutex.WaitOne(); // Захватываем мьютекс
                        try
                        {
                            balls[i].Move(balls, mutex); // Двигаем шарик
                        }
                        finally
                        {
                            mutex.ReleaseMutex(); // Освобождаем мьютекс
                        }
                    }
                    finally
                    {
                        semaphore.Release(); // Освобождаем семафор
                    }
                });

                // Обновляем позиции шариков в UI
                Dispatcher.Invoke(() =>
                {
                    foreach (var ball in balls)
                    {
                        UpdateBallPosition(ball);
                    }
                });

                Thread.Sleep(30); // Задержка для имитации движения
            }
        }

        private void UpdateBallPosition(Ball ball)
        {
            var ellipse = canvas.Children.OfType<Ellipse>().FirstOrDefault(el => el.Fill == ball.Color);
            if (ellipse != null)
            {
                Canvas.SetLeft(ellipse, ball.X);
                Canvas.SetTop(ellipse, ball.Y);
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            isRunning = false;
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            draggedEllipse = sender as Ellipse;
            if (draggedEllipse != null)
            {
                isDragging = true;
                dragStartPoint = e.GetPosition(canvas);
                Canvas.SetZIndex(draggedEllipse, 1);
                draggedEllipse.CaptureMouse();
            }
        }

        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && draggedEllipse != null)
            {
                Point currentPoint = e.GetPosition(canvas);
                double offsetX = currentPoint.X - dragStartPoint.X;
                double offsetY = currentPoint.Y - dragStartPoint.Y;

                double newX = Canvas.GetLeft(draggedEllipse) + offsetX;
                double newY = Canvas.GetTop(draggedEllipse) + offsetY;

                if (newX >= 0 && newX <= 680 && newY >= 2 && newY <= 330)
                {
                    Canvas.SetLeft(draggedEllipse, newX);
                    Canvas.SetTop(draggedEllipse, newY);
                    dragStartPoint = currentPoint;

                    var ball = balls.Find(b => b.Color == draggedEllipse.Fill);
                    if (ball != null)
                    {
                        ball.X = newX;
                        ball.Y = newY;
                        UpdateTextBoxes(ball);
                    }
                }
            }
        }

        private void UpdateTextBoxes(Ball ball)
        {
            if (ball.Color == Brushes.Red)
            {
                X1TextBox.Text = ball.X.ToString();
                Y1TextBox.Text = ball.Y.ToString();
            }
            else if (ball.Color == Brushes.Blue)
            {
                X2TextBox.Text = ball.X.ToString();
                Y2TextBox.Text = ball.Y.ToString();
            }
        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging && draggedEllipse != null)
            {
                isDragging = false;
                Canvas.SetZIndex(draggedEllipse, 0);
                draggedEllipse.ReleaseMouseCapture();
                draggedEllipse = null;
            }
        }
    }

    public class Ball
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double SpeedX { get; set; }
        public double SpeedY { get; set; }
        public Brush Color { get; set; }
        public double Radius { get; } = 10; // Радиус шарика
        public double Mass { get; } = 1; // Масса шарика

        public Ball(double x, double y, double speedX, double speedY, Brush color)
        {
            X = x;
            Y = y;
            SpeedX = speedX;
            SpeedY = speedY;
            Color = color;
        }

        public void Move(List<Ball> balls, Mutex mutex)
        {
            mutex.WaitOne(); // Блокировка доступа к balls
            try
            {
                X += SpeedX;
                Y += SpeedY;

                // Отражение от границ квадрата
                if (X < 0 || X > 680) SpeedX = -SpeedX;
                if (Y < 2 || Y > 330) SpeedY = -SpeedY;

                // Отталкивание от других шариков
                foreach (var otherBall in balls)
                {
                    if (otherBall != this)
                    {
                        double dx = otherBall.X - X;
                        double dy = otherBall.Y - Y;
                        double distance = Math.Sqrt(dx * dx + dy * dy);

                        if (distance < Radius * 2) // Расстояние между центрами шариков должно быть меньше суммы их радиусов
                        {
                            double angle = Math.Atan2(dy, dx);
                            double overlap = Radius * 2 - distance;

                            // Перемещаем шарики, чтобы они не пересекались
                            X -= overlap * Math.Cos(angle) / 2;
                            Y -= overlap * Math.Sin(angle) / 2;
                            otherBall.X += overlap * Math.Cos(angle) / 2;
                            otherBall.Y += overlap * Math.Sin(angle) / 2;

                            // Вычисляем новые скорости после столкновения
                            double v1 = Math.Sqrt(SpeedX * SpeedX + SpeedY * SpeedY);
                            double v2 = Math.Sqrt(otherBall.SpeedX * otherBall.SpeedX + otherBall.SpeedY * otherBall.SpeedY);
                            double theta1 = Math.Atan2(SpeedY, SpeedX);
                            double theta2 = Math.Atan2(otherBall.SpeedY, otherBall.SpeedX);
                            double phi = Math.Atan2(dy, dx);

                            double v1x = v1 * Math.Cos(theta1 - phi);
                            double v1y = v1 * Math.Sin(theta1 - phi);
                            double v2x = v2 * Math.Cos(theta2 - phi);
                            double v2y = v2 * Math.Sin(theta2 - phi);

                            double u1x = (v1x * (Mass - otherBall.Mass) + 2 * otherBall.Mass * v2x) / (Mass + otherBall.Mass);
                            double u2x = (v2x * (otherBall.Mass - Mass) + 2 * Mass * v1x) / (Mass + otherBall.Mass);

                            SpeedX = u1x * Math.Cos(phi) + v1y * Math.Cos(phi + Math.PI / 2);
                            SpeedY = u1x * Math.Sin(phi) + v1y * Math.Sin(phi + Math.PI / 2);
                            otherBall.SpeedX = u2x * Math.Cos(phi) + v2y * Math.Cos(phi + Math.PI / 2);
                            otherBall.SpeedY = u2x * Math.Sin(phi) + v2y * Math.Sin(phi + Math.PI / 2);
                        }
                    }
                }
            }
            finally
            {
                mutex.ReleaseMutex(); // Освобождаем мьютекс
            }
        }
    }
}