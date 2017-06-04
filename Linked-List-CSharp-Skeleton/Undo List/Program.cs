using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Undo_List
{
    class Program
    {
        static void Main(string[] args)
        {
            var command = Console.ReadLine();
            Stack<string> stack = new Stack<string>();
            string previous = String.Empty;
            while (command != "exit")
            {
                if (command == "back")
                {
                    if (stack.Count != 0)
                    {
                        Console.WriteLine(stack.Pop());
                    }
                    previous = null;

                }
                else
                {
                    if (previous != null)
                    {
                        stack.Push(previous);
                    }
                    previous = command;
                }

                command = Console.ReadLine();
            }
        }
    }
}
