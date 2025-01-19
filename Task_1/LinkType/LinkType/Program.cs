using System;

namespace ConsoleApp1
{
    class Animal { }
    class Dog : Animal { }
    class Cat : Animal { }

    class Program
    {
        static void Main(string[] args)
        {
            float f = (float)1.0e70;
            float f2 = f * 2;

        }
    }
}















/*using System;

namespace ConsoleApp1
{
    class Animal { }
    class Dog : Animal { }
    class Cat : Animal { }

    class Program
    {
        static void Main(string[] args)
        {
            Animal animal = new Animal();

            if (animal is Dog)
            {
                Dog dog = (Dog)animal;
                // Выполнить действия с dog
                Console.WriteLine("Это собака");
            }
            else if (animal is Cat)
            {
                Cat cat = (Cat)animal;
                // Выполнить действия с cat
                Console.WriteLine("Это кошка");
            }
            else
            {
                // Объект animal не является ни Dog, ни Cat
                Console.WriteLine("Это не собака и не кошка");
            }
        }
    }
}

*/
