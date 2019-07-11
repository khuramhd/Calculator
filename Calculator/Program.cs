using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Domains;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Correct format: 'Program <equation>'");
            }

            var calc = new Calc();
            try
            {
                Console.WriteLine($"Input: {args[0]}");
                var result = calc.ProcessEquation(args[0]);
                Console.WriteLine($"Result: {result}"); 
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

        }
    }
}
