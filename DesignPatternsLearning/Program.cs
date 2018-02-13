using DesignPatternsLearning.Creational;
using System;

namespace DesignPatternsLearning
{
    class Program
    {
        delegate double func(int num);
        static void Main(string[] args)
        {
            FactoryMethod.Run();
            AbstractFactoryPattern.Run();
            Console.Read();

        }

    }



}
