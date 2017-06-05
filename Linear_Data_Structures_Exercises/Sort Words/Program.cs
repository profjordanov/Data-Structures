using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort_Words
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split();
            List<string> list = input.Select(word => word).ToList();
            list.Sort();
            foreach (var word in list)
            {
                Console.Write(word + " ");
            }
            Console.WriteLine();
        }
    }
}
