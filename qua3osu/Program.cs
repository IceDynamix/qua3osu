using System;
using System.Collections.Generic;

namespace qua3osu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a path to a .qp file");
            var inputPath = Console.ReadLine();
            Conversion.ConvertMapset(inputPath, new Arguments());
        }
    }
}