using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalkulator
{
    static class MathematicalBackground
    {
        public static double Calculate(double x, string operation, double y)
        {
            switch (operation)
            {
                case "+":
                    return x + y;
                case "-":
                    return x - y;
                case "*":
                    return x * y;
                case "/":
                    return x / y;
                default:
                    return x % y;
            }
        }
        public static double PerformCalculations(string calculations)
        {
            string[] values = calculations.Split();
            List<double> numbers = new List<double>();
            List<string> operations = new List<string>();
            for (int i = 0; i < values.Length; i += 2)
            {
                numbers.Add(Convert.ToDouble(values[i]));
            }
            for (int i = 1; i < values.Length; i += 2)
            {
                operations.Add(values[i]);
            }
            for (int i = 0; i < operations.Count; i++)
            {
                numbers[i + 1] = MathematicalBackground.Calculate(numbers[i], operations[i], numbers[i + 1]);
            }
            return numbers[numbers.Count - 1];
        }
        public static double Perform1ArgumentFunction(string number, string operation)
        {
            double x = Convert.ToDouble(number);
            if (operation=="x^2")
            {
                return Math.Pow(x, 2);
            }
            else if (operation == "1/x" &&x!=0)
            {
                return 1 / x;
            }
            else if (operation== "SQRT(x)" && x >= 0)
            {
                return Math.Sqrt(x);
            }
            else
            {
                return x;
            }
           
        }
    }
}
