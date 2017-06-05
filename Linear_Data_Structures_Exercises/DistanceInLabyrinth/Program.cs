﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistanceInLabyrinth
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            string[,] lab = ReadLab(n);

            bool[,] visited = new bool[lab.GetLength(0),lab.GetLength(1)];

            int row = 0;
            int col = 0;

            bool found = false;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (lab[i,j] == "*")
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }

            Queue<Cell> queue = new Queue<Cell>();
            queue.Enqueue(new Cell(row,col,true,0));

            while (queue.Count != 0)
            {
                Cell current = queue.Dequeue();
                visited[current.Row, current.Col] = true;

                row = current.Row;
                col = current.Col;

                if (lab[row,col] != "*")
                {
                    lab[row, col] = current.Moves.ToString();
                }
                //up
                if (row - 1 >= 0 && lab[row - 1, col] != "x" && !visited[row - 1, col])
                {
                    queue.Enqueue(new Cell(row - 1, col, false, current.Moves + 1));
                }

                //right
                if (col + 1 < lab.GetLength(1) && lab[row, col + 1] != "x" && !visited[row, col + 1])
                {
                    queue.Enqueue(new Cell(row, col + 1, false, current.Moves + 1));
                }

                //down
                if (row + 1 < lab.GetLength(0) && lab[row + 1, col] != "x" && !visited[row + 1, col])
                {
                    queue.Enqueue(new Cell(row + 1, col, false, current.Moves + 1));
                }

                //left
                if (col - 1 >= 0 && lab[row, col - 1] != "x" && !visited[row, col - 1])
                {
                    queue.Enqueue(new Cell(row, col - 1, false, current.Moves + 1));
                }
            }
        }

        private static string[,] ReadLab(int n)
        {
            string[,] lab = new string[n,n];
            for (int i = 0; i < n; i++)
            {
                char[] line = Console.ReadLine().ToCharArray();
                for (int j = 0; j < n; j++)
                {
                    lab[i, j] = line[j].ToString();
                }
            }
            return lab;

        }
    }
}
