using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Calculator;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ICalculator<BigInteger> calculator = new SequentialCalculator(SequentialCalculator.Mode.REPLACE);

            string exp = "";

            while (exp != "exit")
            {
                exp = Console.ReadLine();

                if (exp == "clear")
                {
                    Console.Clear();
                    continue;
                }

                try
                {
					BigInteger value = calculator.Calculate(exp);
                    Console.WriteLine("value: {0}", value.ToString("R"));
                }
                catch (EvalException e)
                {
                    Console.Error.WriteLine(e.Message);
                }
                finally
                {
                    Console.WriteLine("---------");
                }
            }
        }

    }
}
