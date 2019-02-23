using System;
using MyProject;
using MyProject.Folder1;
using MyProject.Folder2;
using MyProject.Folder2.Folder3;

namespace SomeProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Class1 class1 = new Class1();
            class1.Print();
            Class2 class2 = new Class2();
            class2.Print();
            Class3 class3 = new Class3();
            class3.Print();
            Class4 class4 = new Class4();
            class4.Print();
            Class5 class5 = new Class5();
            class5.Print();
            Console.WriteLine("Hello World!");
        }
    }
}
