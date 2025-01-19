using System;
using System.Collections.Generic;
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
        private object lockObject = new object();
        private Mutex mutex = new Mutex();
        private Semaphore semaphore = new Semaphore(1, 1);

        public MainWindow()
        {
            InitializeComponent();
            InitializeBalls();
        }

        private void InitializeBalls()
        {
            var ball1 = new Ball(100, 100, 2, 2, Brushes.Red);
            var ball2 = new Ball(700, 100, 2, 2, Brushes.Blue);
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

            await Task.WhenAll(balls.Select(ball => MoveBallAsync(ball)).ToArray());
        }

        private async Task MoveBallAsync(Ball ball)
        {
            while (isRunning)
            {
                ball.Move();
                Dispatcher.Invoke(() => UpdateBallPosition(ball));
                await Task.Delay(30);
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

        public Ball(double x, double y, double speedX, double speedY, Brush color)
        {
            X = x;
            Y = y;
            SpeedX = speedX;
            SpeedY = speedY;
            Color = color;
        }

        public void Move()
        {
            X += SpeedX;
            Y += SpeedY;

            // Отражение от границ квадрата
            if (X < 0 || X > 780) SpeedX = -SpeedX;
            if (Y < 2 || Y > 325) SpeedY = -SpeedY;
        }
    }
}