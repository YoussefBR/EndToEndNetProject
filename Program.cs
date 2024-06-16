using System;
using Microsoft.VisualBasic;
using NameGenerator.Generators;

namespace HelloWorld{
    class Program{
        static void Main(string[] args){
            Console.WriteLine("Enter some number separated by spaces:");
            string line = Console.ReadLine();
            // Method 1
            // List<int> s_nums = line.Split(" ").ToList().Select(s => int.Parse(s)).ToList();
            // foreach(int i in s_nums){
            //     PrintAge(i);
            // }
            // Method 2
            string[] s_nums = line.Split(" ");
            foreach(string s in s_nums){
                PrintAge(int.Parse(s));
            }
        }

        static void PrintAge(int x){
            Student student = new Student(x);
            Console.WriteLine(student.Name + "'s age is " + student.Age);
        }
    }
    class Student{
        public string Name;
        public int Age;
        RealNameGenerator nameGenerator = new RealNameGenerator();
        public Student(int age){
            Age = age;
            Name = nameGenerator.Generate();
        }
    }
}