using System;
using System.ComponentModel.Design;
using Microsoft.VisualBasic;
using NameGenerator.Generators;

namespace HelloWorld{
    class Program{
        static void Main(string[] args){
            List<Student> students = new List<Student>();
            while(true){
                Console.WriteLine("Enter command (enter h to see list of commands):");
                string[] command = Console.ReadLine().Split(" ");
                switch(command[0]){
                    case "h":
                        if(command.Length != 1) goto InputError;
                        Console.WriteLine("l - list students\nd <NAME> - delete students that match <NAME>\na <FULL NAME> <AGE> - add a student with the name <FULL NAME> and age <AGE>\nq - quit program");
                        break;
                    case "l":
                        if(command.Length != 1) goto InputError;
                        if(students.Count == 0) Console.WriteLine("No students");
                        foreach(Student s in students){
                            Console.WriteLine(s.Name + "'s age is " + s.Age);
                        }
                        break;
                    case "a":
                        if(command.Length != 3) goto InputError;
                        Student student = new Student(command[1], int.Parse(command[2]));
                        students.Add(student);
                        Console.WriteLine(student.Name + " has been added");
                        break;
                    InputError:
                        Console.WriteLine("Incorrect amount of args for command type");
                        break;
                    case "d":
                        if(command.Length != 2) goto InputError;
                        int count = 0;
                        for(int i = 0; i < students.Count; i++){
                            if(students[i].Name.Contains(command[1])){
                                students.Remove(students[i]);
                                i--; // account for cahnge in List size
                                count++;
                            }
                        }
                        Console.WriteLine(count + " students removed");
                        break;
                    case "q":
                        return;
                    default:
                        Console.WriteLine("Unrecognized command, try again");
                        break;
                }
            }
        }

        static void parseAges(string ages){
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
        public Student(string name, int age){
            Age = age;
            Name = name;
        }
    }
}