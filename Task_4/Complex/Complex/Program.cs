using System;
using System.Numerics;

namespace ComplexNumbers
{
    public struct Complex<T> where T : struct, INumber<T> // int, double, float, decimal, short, long, byte
    {
        public T Real { get; set; }
        public T Imaginary { get; set; }

        public Complex(T real, T imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public T Magnitude
        {
            get
            {
                if (typeof(T) == typeof(double))
                {
                    double realPart = Convert.ToDouble(Real);
                    double imaginaryPart = Convert.ToDouble(Imaginary);
                    double magnitude = Math.Sqrt(realPart * realPart + imaginaryPart * imaginaryPart);
                    return (T)(object)magnitude;
                }
                else if (typeof(T) == typeof(float))
                {
                    float realPart = Convert.ToSingle(Real);
                    float imaginaryPart = Convert.ToSingle(Imaginary);
                    float magnitude = (float)Math.Sqrt(realPart * realPart + imaginaryPart * imaginaryPart);
                    return (T)(object)magnitude;
                }
                else
                {
                    throw new NotSupportedException("Magnitude calculation is only supported for float and double.");
                }
            }
        }

        public T Argument
        {
            get
            {
                if (typeof(T) == typeof(double))
                {
                    double realPart = Convert.ToDouble(Real);
                    double imaginaryPart = Convert.ToDouble(Imaginary);
                    return (T)(object)Math.Atan2(imaginaryPart, realPart);
                }
                else if (typeof(T) == typeof(float))
                {
                    float realPart = Convert.ToSingle(Real);
                    float imaginaryPart = Convert.ToSingle(Imaginary);
                    return (T)(object)MathF.Atan2(imaginaryPart, realPart);
                }
                else
                {
                    throw new NotSupportedException("Argument calculation is only supported for float and double.");
                }
            }
        }

        public static Complex<T> operator +(Complex<T> a, Complex<T> b)
        {
            return new Complex<T>(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }

        public static Complex<T> operator +(Complex<T> a, T b)
        {
            return new Complex<T>(a.Real + b, a.Imaginary);
        }

        public static Complex<T> operator +(T a, Complex<T> b)
        {
            return new Complex<T>(a + b.Real, b.Imaginary);
        }

        public static Complex<T> operator -(Complex<T> a, Complex<T> b)
        {
            return new Complex<T>(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }

        public static Complex<T> operator -(Complex<T> a, T b)
        {
            return new Complex<T>(a.Real - b, a.Imaginary);
        }

        public static Complex<T> operator -(T a, Complex<T> b)
        {
            return new Complex<T>(a - b.Real, b.Imaginary);
        }

        public static Complex<T> operator *(Complex<T> a, Complex<T> b)
        {
            return new Complex<T>(a.Real * b.Real - (a.Imaginary * b.Imaginary), a.Real * b.Imaginary + a.Imaginary * b.Real);
        }

        public static Complex<T> operator *(Complex<T> a, T b)
        {
            return new Complex<T>(a.Real * b, a.Imaginary * b);
        }

        public static Complex<T> operator *(T a, Complex<T> b)
        {
            return new Complex<T>(a * b.Real, a * b.Imaginary);
        }

        public static Complex<T> operator /(Complex<T> a, Complex<T> b)
        {
            T denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;
            T realPart = (a.Real * b.Real + a.Imaginary * b.Imaginary) / denominator;
            T imaginaryPart = (a.Imaginary * b.Real - a.Real * b.Imaginary) / denominator;
            return new Complex<T>(realPart, imaginaryPart);
        }

        public static Complex<T> operator /(Complex<T> a, T b)
        {
            return new Complex<T>(a.Real / b, a.Imaginary / b);
        }

        public static Complex<T> operator /(T a, Complex<T> b)
        {
            T denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;
            T realPart = (a * b.Real) / denominator;
            T imaginaryPart = (-a * b.Imaginary) / denominator;
            return new Complex<T>(realPart, imaginaryPart);
        }

        public static bool operator ==(Complex<T> a, Complex<T> b)
        {
            return a.Real.Equals(b.Real) && a.Imaginary.Equals(b.Imaginary);
        }

        public static bool operator !=(Complex<T> a, Complex<T> b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Complex<T> other)
            {
                return this == other;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Real.GetHashCode() ^ Imaginary.GetHashCode();
        }

        public override string ToString()
        {
            if (Imaginary.CompareTo(T.Zero) > 0)
            {
                return $"{Real} + {Imaginary}i";
            }
            else if (Imaginary.CompareTo(T.Zero) < 0)
            {
                return $"{Real} - {T.Abs(Imaginary)}i";
            }
            else
            {
                return $"{Real}";
            }
        }

        // Прямоугольная -> Экспоненциальная форма
        public (T Magnitude, T Argument) ToExponential()
        {
            return (Magnitude, Argument);
        }

        // Экспоненциальная -> Прямоугольная форма
        public static Complex<T> FromExponential(T magnitude, T argument)
        {
            if (typeof(T) == typeof(double))
            {
                double r = Convert.ToDouble(magnitude);
                double theta = Convert.ToDouble(argument);
                T real = (T)(object)(r * Math.Cos(theta));
                T imaginary = (T)(object)(r * Math.Sin(theta));
                return new Complex<T>(real, imaginary);
            }
            else if (typeof(T) == typeof(float))
            {
                float r = Convert.ToSingle(magnitude);
                float theta = Convert.ToSingle(argument);
                T real = (T)(object)(r * MathF.Cos(theta));
                T imaginary = (T)(object)(r * MathF.Sin(theta));
                return new Complex<T>(real, imaginary);
            }
            else
            {
                throw new NotSupportedException("Conversion is only supported for float and double.");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Complex<double> c1 = new Complex<double>(2, 3);
            Complex<double> c2 = new Complex<double>(7, -1);
            Complex<double> c3 = new Complex<double>(2, 3);

            // Сложение
            Complex<double> sum = c1 + c2;
            Console.WriteLine($"Сумма c1 + c2: {sum}");
            Complex<double> sum1 = c1 + 4;
            Console.WriteLine($"Сумма с1 + 4: {sum1}");
            Complex<double> sum2 = 4 + c2;
            Console.WriteLine($"Сумма 4 + c1: {sum2}\n");

            // Вычитание
            Complex<double> difference = c1 - c2;
            Console.WriteLine($"Разность c1 - c2: {difference}");
            Complex<double> diff1 = c1 - 3;
            Console.WriteLine($"Разность c1 - 3: {diff1}");
            Complex<double> diff2 = 3 - c1;
            Console.WriteLine($"Разность 3 - с1: {diff2}\n");

            // Умножение
            Complex<double> product = c1 * c2;
            Console.WriteLine($"Произведение c1 * c2: {product}");
            Complex<double> mul1 = c1 * 2;
            Console.WriteLine($"Произведение c1 * 2 : {mul1}");
            Complex<double> mul2 = 2 * c1;
            Console.WriteLine($"Произведение 2 * c1 : {mul2}\n");


            // Деление
            Complex<double> quotient = c1 / c2;
            Console.WriteLine($"Частное c1 / c2: {quotient}");
            Complex<double> quotien1 = c1 / 3;
            Console.WriteLine($"Частное c1 / 3 : {quotien1}");
            Complex<double> quotient2 = 3 / c1;
            Console.WriteLine($"Частное 3 / c1: {quotient2}\n");

            // Модуль
            Console.WriteLine($"Модуль c1: {c1.Magnitude}");
            Console.WriteLine($"Модуль c2: {c2.Magnitude}\n");

            // Аргумент
            //Console.WriteLine($"Аргумент c1: {c1.Argument}");
            //Console.WriteLine($"Аргумент c2: {c2.Argument}");

            // Экспоненциальная форма
            Console.WriteLine($"Экспоненциальная форма c1: {c1.Magnitude} * e^({c1.Argument}i)");
            Console.WriteLine($"Экспоненциальная форма c2: {c2.Magnitude} * e^({c2.Argument}i)\n");

            // Демонстрация == и !=
            Console.WriteLine($"c1 == c2: {c1 == c2}");
            Console.WriteLine($"c1 == c3: {c1 == c3}");
            Console.WriteLine($"c1 != c2: {c1 != c2}");
            Console.WriteLine($"c1 != c3: {c1 != c3}");
        }
    }
}