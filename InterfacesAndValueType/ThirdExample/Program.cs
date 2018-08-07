using System;

namespace ThirdExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Point p = new Point(1 , 1);

            Console.WriteLine(p);

            p.Change(2 , 2);
            Console.WriteLine(p);

            Object o = p;
            Console.WriteLine(o);

            ((Point)o).Change(3 , 3);
            Console.WriteLine(o);

            // Boxes p, changes the boxed object and discards it
            ((IChangeBoxedPoint) p).Change(4 , 4);

            Console.WriteLine(p);
            // Changes the boxed object and shows it
            ((IChangeBoxedPoint) o).Change(5 , 5);
            Console.WriteLine(o);
        }
    }
}
