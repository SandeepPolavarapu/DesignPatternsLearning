using DesignPatternsLearning.Behavioral;
using System;

namespace DesignPatternsLearning
{
    class Program
    {
        delegate double func(int num);
        static void Main(string[] args)
        {
            ObserverPattern.Run();
            Console.Read();

        }

    }



}
