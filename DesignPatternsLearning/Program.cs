using DesignPatternsLearning.Structural;
using System;

namespace DesignPatternsLearning
{
    class Program
    {
        delegate double func(int num);
        static void Main(string[] args)
        {
            //FactoryMethod.Run();
            //AbstractFactoryPattern.Run();
            DecoratorPattern.Run();
            Console.Read();

        }

    }



}
