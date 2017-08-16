using System;

namespace GenericDataAcessLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            new DataAcessUtil().Search<Person>(new object());

            Console.ReadKey();
        }
    }
}