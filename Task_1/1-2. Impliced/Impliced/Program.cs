﻿using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TypeConversionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TypeConverter converter = new TypeConverter();

            Console.WriteLine("Неявное преобразование:");
            //converter.PerformImplicitConversion();
            //converter.PerformExplicitConversion();
            converter.PerformUserType();




        }
    }

    class TypeConverter
    {
        public void PerformImplicitConversion()
        {



            byte bbyte = oobject;
            sbyte ssbyte = oobject;
            short sshort = oobject;
            ushort uushort = oobject;
            int iint = oobject;
            uint uuint = oobject;
            long llong = oobject;
            ulong uulong = oobject;

            float ffloat = oobject;
            double ddouble = oobject;
            decimal ddecimal = oobject;

            bool bbool = oobject;

            char cchar = oobject;
            string sstring = oobject;

            object oobject = 123; 

            //Console.WriteLine($" Byte -> short = {sshort} \n Byte -> ushort = {uushort} \n Byte -> int = {iint} \n Byte -> uint = {uuint} \n Byte -> long = {llong} \n Byte -> ulong = {uulong} \n");
            //Console.WriteLine($" Byte -> float = {ffloat} \n Byte -> double = {ddouble} \n Byte -> float = {ddecimal}");
            //Console.WriteLine($" Byte -> object = {oobject}");

        }
        /*
_____________________________________________________________________________________________________________________________________
       | byte  | sbyte | short | ushort  |  int  |  uint |  long | ulong | float | double| decimal |  bool |  char | string| object |
------------------------------------------------------------------------------------------------------------------------------------|
byte   |#######|   -   |   +   |    +    |   +   |   +   |   +   |   +   |   +   |   +   |    +    |   -   |   -   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
sbyte  |   -   |#######|   +   |    -    |   +   |   -   |   +   |   -   |   +   |   +   |    +    |   -   |   -   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
short  |   -   |   -   |#######|    -    |   +   |   -   |   +   |   -   |   +   |   +   |    +    |   -   |   -   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
ushort |   -   |   -   |   -   |#########|   +   |   +   |   +   |   +   |   +   |   +   |    +    |   -   |   -   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
int    |   -   |   -   |   -   |    -    |#######|   -   |   +   |   -   |   +   |   +   |    +    |   -   |   -   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
uint   |   -   |   -   |   -   |    -    |   -   |#######|   +   |   +   |   +   |   +   |    +    |   -   |   -   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
long   |   -   |   -   |   -   |    -    |   -   |   -   |#######|   -   |   +   |   +   |    +    |   -   |   -   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
ulong  |   -   |   -   |   -   |    -    |   -   |   -   |   -   |#######|   +   |   +   |    +    |   -   |   -   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
float  |   -   |   -   |   -   |    -    |   -   |   -   |   -   |   -   |#######|   +   |    -    |   -   |   -   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
double |   -   |   -   |   -   |    -    |   -   |   -   |   -   |   -   |   -   |#######|    -    |   -   |   -   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
decimal|   -   |   -   |   -   |    -    |   -   |   -   |   -   |   -   |   -   |   -   |#########|   -   |   -   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
bool   |   -   |   -   |   -   |    -    |   -   |   -   |   -   |   -   |   -   |   -   |    -    |#######|   -   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
char   |   -   |   -   |   -   |    +    |   +   |   +   |   +   |   +   |   +   |   +   |    +    |   -   |#######|   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
string |   -   |   -   |   -   |    -    |   -   |   -   |   -   |   -   |   -   |   -   |    -    |   -   |   -   |#######|   +    |
------------------------------------------------------------------------------------------------------------------------------------|
object |   -   |   -   |   -   |    -    |   -   |   -   |   -   |   -   |   -   |   -   |    -    |   -   |   -   |   -   |########|
------------------------------------------------------------------------------------------------------------------------------------|
*/

        public void PerformExplicitConversion()
        {
            object oobject = 12;

            byte bbyte = (byte)oobject;
            sbyte ssbyte = (sbyte)oobject;
            short sshort = (short)oobject;
            ushort uushort = (ushort)oobject;
            int iint = (int)oobject;
            uint uuint = (uint)oobject;
            long llong = (long)oobject;
            ulong uulong = (ulong)oobject;
            
            float ffloat = (float)oobject;
            double ddouble = (double)oobject;
            decimal ddecimal = (decimal)oobject;

            bool bbool = (bool)oobject;

            char cchar = (char)oobject;
            string sstring =(string)oobject;

            

            Console.WriteLine($"double -> float: {b}, float -> long: {c}, long -> int: {d}, int -> short: {z}, short -> byte: {f}");
            /*
_____________________________________________________________________________________________________________________________________
       | byte  | sbyte | short | ushort  |  int  |  uint |  long | ulong | float | double| decimal |  bool |  char | string| object |
------------------------------------------------------------------------------------------------------------------------------------|
byte   |#######|   +   |   +   |    +    |   +   |   +   |   +   |   +   |   +   |   +   |    +    |   -   |   +   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
sbyte  |   +   |#######|   +   |    +    |   +   |   +   |   +   |   +   |   +   |   +   |    +    |   -   |   +   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
short  |   +   |   +   |#######|    +    |   +   |   +   |   +   |   +   |   +   |   +   |    +    |   -   |   +   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
ushort |   +   |   +   |   +   |#########|   +   |   +   |   +   |   +   |   +   |   +   |    +    |   -   |   +   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
int    |   +   |   +   |   +   |    +    |#######|   +   |   +   |   +   |   +   |   +   |    +    |   -   |   +   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
uint   |   +   |   +   |   +   |    +    |   +   |#######|   +   |   +   |   +   |   +   |    +    |   -   |   +   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
long   |   +   |   +   |   +   |    +    |   +   |   +   |#######|   +   |   +   |   +   |    +    |   -   |   +   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
ulong  |   +   |   +   |   +   |    +    |   +   |   +   |   +   |#######|   +   |   +   |    +    |   -   |   +   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
float  |   +   |   +   |   +   |    +    |   +   |   +   |   +   |   +   |#######|   +   |    +    |   -   |   +   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
double |   +   |   +   |   +   |    +    |   +   |   +   |   +   |   +   |   +   |#######|    +    |   -   |   +   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
decimal|   +   |   +   |   +   |    +    |   +   |   +   |   +   |   +   |   +   |   +   |#########|   -   |   +   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
bool   |   -   |   -   |   -   |    -    |   -   |   -   |   -   |   -   |   -   |   -   |    -    |#######|   -   |   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
char   |   +   |   +   |   +   |    +    |   +   |   +   |   +   |   +   |   +   |   +   |    +    |   -   |#######|   -   |   +    |
------------------------------------------------------------------------------------------------------------------------------------|
string |   -   |   -   |   -   |    -    |   -   |   -   |   -   |   -   |   -   |   -   |    -    |   -   |   -   |#######|   +    |
------------------------------------------------------------------------------------------------------------------------------------|
object |   +   |   +   |   +   |    +    |   +   |   +   |   +   |   +   |   +   |   +   |    +    |   +   |   +   |   +   |########|
------------------------------------------------------------------------------------------------------------------------------------|
           */
        }
        public void PerformUserType() {
            
            Person person = new Person("John Doe", 35);
            // Преобразовываем объект Person в объект Car
            Car car = (Car)person;
            Console.WriteLine($"Model: {car.Model}, Year: {car.Year}");

        }
    }
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







}


