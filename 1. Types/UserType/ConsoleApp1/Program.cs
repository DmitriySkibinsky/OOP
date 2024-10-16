using System;

namespace ConsoleApp1
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public static explicit operator Car(Person person)
        {
            return new Car($"{person.Name}'s car", DateTime.Now.Year - person.Age);
        }
    }

    public class Car
    {
        public string Model { get; set; }
        public int Year { get; set; }

        public Car(string model, int year)
        {
            Model = model;
            Year = year;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создаем объект класса Person
            Person person = new Person("John Doe", 35);

            // Преобразовываем объект Person в объект Car
            Car car = (Car)person;

            Console.WriteLine($"Model: {car.Model}, Year: {car.Year}");
        }
    }
}
