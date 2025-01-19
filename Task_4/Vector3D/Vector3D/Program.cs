using System;

namespace Vector3D
{
    public struct Vector3<T> where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        //                             Отрицательное число, форматировать свое строковое представление, int, double, CompareTo, сравниваемыми 
        public T X { get; set; }
        public T Y { get; set; }
        public T Z { get; set; }

        public Vector3(T x, T y, T z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3<T> operator +(Vector3<T> a, Vector3<T> b)
        {
            dynamic dx = a.X, dy = a.Y, dz = a.Z;
            dynamic bx = b.X, by = b.Y, bz = b.Z;
            return new Vector3<T>(dx + bx, dy + by, dz + bz);
        }

        public static Vector3<T> operator +(Vector3<T> a, T b)
        {
            dynamic dx = a.X, dy = a.Y, dz = a.Z;
            dynamic scalar = b;
            return new Vector3<T>(dx + scalar, dy + scalar, dz + scalar);
        }

        public static Vector3<T> operator +(T a, Vector3<T> b)
        {
            dynamic dx = b.X, dy = b.Y, dz = b.Z;
            dynamic scalar = a;
            return new Vector3<T>(dx + scalar, dy + scalar, dz + scalar);
        }

        public static Vector3<T> operator -(Vector3<T> a, Vector3<T> b)
        {
            dynamic dx = a.X, dy = a.Y, dz = a.Z;
            dynamic bx = b.X, by = b.Y, bz = b.Z;
            return new Vector3<T>(dx - bx, dy - by, dz - bz);
        }

        public static Vector3<T> operator -(Vector3<T> a, T b)
        {
            dynamic dx = a.X, dy = a.Y, dz = a.Z;
            dynamic scalar = b;
            return new Vector3<T>(dx - scalar, dy - scalar, dz - scalar);
        }

        public static Vector3<T> operator -(T a, Vector3<T> b)
        {
            dynamic bx = b.X, by = b.Y, bz = b.Z;
            dynamic scalar = a;
            return new Vector3<T>(bx - scalar, by - scalar, bz - scalar);
        }

        public static Vector3<T> operator *(Vector3<T> a, T scalar)
        {
            dynamic dx = a.X, dy = a.Y, dz = a.Z;
            dynamic s = scalar;
            return new Vector3<T>(dx * s, dy * s, dz * s);
        }

        public static Vector3<T> operator *(T a, Vector3<T> b)
        {
            dynamic dx = b.X, dy = b.Y, dz = b.Z;
            dynamic s = a;
            return new Vector3<T>(dx * s, dy * s, dz * s);
        }

        public static T Dot(Vector3<T> a, Vector3<T> b) // скалярное
        {
            dynamic ax = a.X, ay = a.Y, az = a.Z;
            dynamic bx = b.X, by = b.Y, bz = b.Z;
            return ax * bx + ay * by + az * bz;
        }

        public static Vector3<T> Cross(Vector3<T> a, Vector3<T> b) // векторное
        {
            dynamic ax = a.X, ay = a.Y, az = a.Z;
            dynamic bx = b.X, by = b.Y, bz = b.Z;
            return new Vector3<T>(ay * bz - az * by, az * bx - ax * bz, ax * by - ay * bx);
        }

        public static Vector3<T> operator /(Vector3<T> a, T scalar)
        {
            dynamic dx = a.X, dy = a.Y, dz = a.Z;
            dynamic s = scalar;
            return new Vector3<T>(dx / s, dy / s, dz / s);
        }

        public static Vector3<T> operator /(T scalar, Vector3<T> b)
        {
            dynamic dx = b.X, dy = b.Y, dz = b.Z;
            dynamic s = scalar;
            return new Vector3<T>(dx / s, dy / s, dz / s);
        }

        public static bool operator ==(Vector3<T> a, Vector3<T> b)
        {
            return a.X.Equals(b.X) && a.Y.Equals(b.Y) && a.Z.Equals(b.Z);
        }

        public static bool operator !=(Vector3<T> a, Vector3<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode() // сравнение в словарях коллекциях
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }

        public double Magnitude
        {
            get
            {
                dynamic dx = X, dy = Y, dz = Z;
                return Math.Sqrt((double)(dx * dx + dy * dy + dz * dz));
            }
        }

        public double GetMagnitude()
        {
            return Magnitude;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Vector3<int> vector1 = new Vector3<int>(1, 2, 3);
            Vector3<int> vector2 = new Vector3<int>(4, 5, 6);
            Vector3<int> vector3 = new Vector3<int>(1, 2, 3);

            // Сложение векторов
            Vector3<int> sum = vector1 + vector2;
            Console.WriteLine($"Сумма векторов: {sum}"); // Вывод: Сумма векторов: (5, 7, 9)

            // Вычитание векторов
            Vector3<int> difference = vector1 - vector2;
            Console.WriteLine($"Разность векторов: {difference}"); // Вывод: Разность векторов: (-3, -3, -3)

            // Умножение вектора на скаляр
            Vector3<int> scaled = vector1 * 2;
            Console.WriteLine($"Умножение вектора на скаляр: {scaled}"); // Вывод: Умножение вектора на скаляр: (2, 4, 6)

            // Деление вектора на скаляр
            Vector3<int> divided = vector1 / 2;
            Console.WriteLine($"Деление вектора на скаляр: {divided}"); // Вывод: Деление вектора на скаляр: (0, 1, 1)

            // Скалярное произведение
            int dotProduct = Vector3<int>.Dot(vector1, vector2);
            Console.WriteLine($"Скалярное произведение: {dotProduct}"); // Вывод: Скалярное произведение: 32

            // Векторное произведение
            Vector3<int> crossProduct = Vector3<int>.Cross(vector1, vector2);
            Console.WriteLine($"Векторное произведение: {crossProduct}"); // Вывод: Векторное произведение: (-3, 6, -3)

            // Модуль вектора
            Console.WriteLine($"Модуль вектора1: {vector1.Magnitude}"); // Вывод: Модуль вектора1: 3.7416573867739413
            Console.WriteLine($"Модуль вектора2: {vector2.Magnitude}"); // Вывод: Модуль вектора2: 8.774964387392123

            // Сравнение векторов
            Console.WriteLine($"vector1 == vector2: {vector1 == vector2}"); // Вывод: vector1 == vector2: False
            Console.WriteLine($"vector1 == vector3: {vector1 == vector3}"); // Вывод: vector1 == vector3: True
            Console.WriteLine($"vector1 != vector2: {vector1 != vector2}"); // Вывод: vector1 != vector2: True
            Console.WriteLine($"vector1 != vector3: {vector1 != vector3}"); // Вывод: vector1 != vector3: False

            // Масса студента
            double mass = 70;

            // Скорость студента
            Vector3<double> velocity = new Vector3<double>(0, -7, 0); // Направление на юг

            // Угловая скорость Земли
            double omega = 7.27e-5; // рад/с
            double latitude = 45 * Math.PI / 180; // Широта Симферополя в радианах
            Vector3<double> earthAngularVelocity = new Vector3<double>(0, 0, omega * Math.Sin(latitude));

            // Вычисление силы Кориолиса
            Vector3<double> coriolisForce = -2 * mass * Vector3<double>.Cross(earthAngularVelocity, velocity);

            Console.WriteLine($"Сила Кориолиса: {coriolisForce}"); // Вывод: Сила Кориолиса: (0.0504, 0, 0)
        }
    }
}