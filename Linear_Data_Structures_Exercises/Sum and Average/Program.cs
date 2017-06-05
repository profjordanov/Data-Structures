using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sum_and_Average
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split();
            List<double> list = input.Select(number => double.Parse(number)).ToList();
            double sum = list.Sum();
            double avrg = list.Sum() / list.Count();
            Console.WriteLine($"Sum={sum}; Average={avrg}");
        }
    }
}
