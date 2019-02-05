using System;
using System.Collections.Generic;

/// <summary>
/// Finds the shortest path from a starting point
/// "P" (Start) to a goal point "*" (Goal) on a given grid of squares. 
/// </summary>
public class AStar
{
    // Shortest path => min fCost = gCost + hCost
    // gCost = distance from current to start
    // hCost = distance from current to goal
    private const char Wall = 'W';

    private readonly char[,] _map;

    public AStar(char[,] map)
    {
        _map = map;
    }

    /// <summary>
    ///  H is the approximation of the distance from the current node to the goal.
    /// </summary>
    /// <param name="current"></param>
    /// <param name="goal"></param>
    /// <returns>
    /// Total number of squares moved horizontally and vertically to reach the target,
    /// ignoring diagonal movement, and ignoring any obstacles that may be in the way
    /// </returns>
    public static int GetH(Node current, Node goal)
    {
        // Manhattan distance calc
        var deltaX = Math.Abs(current.Col - goal.Col);
        var deltaY = Math.Abs(current.Row - goal.Row);

        return deltaX + deltaY;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        // priority queue containing START
        var priorityQueue = new PriorityQueue<Node>(); // min priority heap => min fCost
        // storing the node from which we have reached a node (following a path)
        var parents = new Dictionary<Node, Node>();
        // storing cost from the start to a node (following a path)
        var gCost = new Dictionary<Node, int>();

        priorityQueue.Enqueue(start);
        parents[start] = null;
        gCost[start] = 0;

        // while OPEN is not empty
        while (priorityQueue.Count > 0)
        {
            // remove highest priority item from OPEN
            var current = priorityQueue.Dequeue();

            if (current.Equals(goal))
            {
                break;
            }

            // neighbor of current (up, right, down, left)
            var neighbors = AddAdjacentNodes(current);
            var newGCost = gCost[current] + 1;

            foreach (var neighbor in neighbors)
            {
                // if neighbor is not in COST or new cost < COST[neighbor]
                if (gCost.ContainsKey(neighbor) && newGCost >= gCost[neighbor])
                {
                    continue;
                }
                gCost[neighbor] = newGCost;
                neighbor.F = newGCost + GetH(neighbor, goal); // fCost = gCost + hCost

                parents[neighbor] = current;

                priorityQueue.Enqueue(neighbor);
            }
        }

        return ReconstructPath(parents, start, goal);
    }

    private static IEnumerable<Node> ReconstructPath(
        IReadOnlyDictionary<Node, Node> parents,
        Node start,
        Node goal)
    {
        var path = new Stack<Node>();

        if (parents.ContainsKey(goal))
        {
            path.Push(goal);
            var current = parents[goal];

            while (current != start)
            {
                path.Push(current);
                current = parents[current];
            }
        }

        path.Push(start);
        return path;
    }

    private IEnumerable<Node> AddAdjacentNodes(Node current)
    {
        var neighbors = new List<Node>();

        AddNeighbors(neighbors, current.Row - 1, current.Col);
        AddNeighbors(neighbors, current.Row + 1, current.Col);
        AddNeighbors(neighbors, current.Row, current.Col - 1);
        AddNeighbors(neighbors, current.Row, current.Col + 1);

        return neighbors;
    }

    private void AddNeighbors(ICollection<Node> neighbors, int row, int col)
    {
        if (IsInsideMap(row, col) && IsAccessible(row, col))
        {
            var neighbor = new Node(row, col);
            neighbors.Add(neighbor);
        }
    }

    private bool IsInsideMap(int row, int col) =>
        row >= 0 &&
        row < _map.GetLength(0) &&
        col >= 0 &&
        col < _map.GetLength(1);

    private bool IsAccessible(int row, int col) =>
        _map[row, col] != Wall;
}

