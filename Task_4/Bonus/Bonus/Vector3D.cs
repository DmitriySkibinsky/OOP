namespace Vector3D
{
    public struct Vector3<T> where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        
        //                             Отрицательное число, форматировать свое строковое представление, int, double, CompareTo, сравниваемыми 
        // автосвойства
        public T X { get; set; }
        public T Y { get; set; }
        public T Z { get; set; }

        // Конструктор для инициализации свойств
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
}