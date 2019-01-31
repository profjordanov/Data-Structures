using System;
using System.Collections.Generic;
using System.Linq;

using static System.Console;

namespace BasicTreeDataStructuresExercise
{
    internal class Program
    {
        // allows to find the tree nodes during the tree construction
        private static readonly Dictionary<int, Tree<int>> NodeByValue = new Dictionary<int, Tree<int>>();

        private static void Main()
        {
            ReadTree();

            /* 01. Root Node */
            var root = GetRootNode();
            WriteLine("-----------------------------");
            WriteLine($"Root node: {root.Value}");
            WriteLine("-----------------------------");

            /* 02. Print Tree */
            PrintTreeIndented(root);
            WriteLine("-----------------------------");

            /* 03. Leaf Nodes */
            var leaves = LeafNodes();
            WriteLine($"Leaf nodes: {string.Join(" ", leaves)}");
            WriteLine("-----------------------------");

            /* 04. Middle Nodes */
            var middleNodes = MiddleNodes();
            WriteLine($"Middle nodes: {string.Join(" ", middleNodes)}");
            WriteLine("-----------------------------");
        }

        private static void ReadTree()
        {
            var nodeCount = int.Parse(ReadLine());
            for (var i = 1; i < nodeCount; i++)
            {
                var edge = ReadLine()?.Split();
                if (edge != null)
                {
                    AddEdge(int.Parse(edge[0]), int.Parse(edge[1]));
                }
            }
        }

        //adds an edge to the tree
        private static void AddEdge(int parent, int child)
        {
            var parentNode = GetTreeNodeByValue(parent);
            var childNode = GetTreeNodeByValue(child);

            parentNode.Children.Add(childNode);
            childNode.Parent = parentNode;
        }

        // finds the tree node by its value or create a new node if it does not exist
        private static Tree<int> GetTreeNodeByValue(int value)
        {
            if (!NodeByValue.ContainsKey(value))
            {
                NodeByValue[value] = new Tree<int>(value);
            }

            return NodeByValue[value];
        }

        private static Tree<int> GetRootNode() =>
            NodeByValue.Values.FirstOrDefault(tree => tree.Parent == null);

        // Recursively prints the tree from given node
        // Format: each level indented + 2 spaces.
        private static void PrintTreeIndented(Tree<int> node, int indent = 0)
        {
            WriteLine(new string(' ', indent * 2) + node.Value);

            foreach (var child in node.Children)
            {
                PrintTreeIndented(child, indent + 1);
            }
        }

        // Finds all leaf nodes (in increasing order)
        private static IOrderedEnumerable<int> LeafNodes() =>
            NodeByValue
                .Values
                .Where(tree => tree.Children.Count == 0)
                .Select(tree => tree.Value)
                .OrderBy(val => val);

        // Find all middle nodes (in increasing order)
        private static IOrderedEnumerable<int> MiddleNodes() =>
            NodeByValue
                .Values
                .Where(tree => tree.Parent != null &&
                               tree.Children.Count > 0)
                .Select(tree => tree.Value)
                .OrderBy(val => val);
    }
}
