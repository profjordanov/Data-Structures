using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var leaves = GetLeafNodes();
            WriteLine($"Leaf nodes: {string.Join(" ", leaves)}");
            WriteLine("-----------------------------");

            /* 04. Middle Nodes */
            var middleNodes = GetMiddleNodes();
            WriteLine($"Middle nodes: {string.Join(" ", middleNodes)}");
            WriteLine("-----------------------------");

            /* 05. Deepest Node */
            WriteLine($"Deepest node: {DeepestNode}");
            WriteLine("-----------------------------");

            /* 06. Longest Path */
            var path = GetLongestPath();
            WriteLine($"Longest path: {string.Join(" ", path)}");
            WriteLine("-----------------------------");

            /* 07. All Paths With a Given Sum */
            WriteLine("Insert Path Sum: ");
            var targetSum = int.Parse(ReadLine());
            PrintPathsByGivenSum(targetSum);
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
        private static IOrderedEnumerable<int> GetLeafNodes() =>
            NodeByValue
                .Values
                .Where(tree => tree.Children.Count == 0)
                .Select(tree => tree.Value)
                .OrderBy(val => val);

        private static Task<IOrderedEnumerable<int>> GetLeafNodesAsync() =>
            Task.Run(() => GetLeafNodes());

        // Finds all middle nodes (in increasing order)
        private static IOrderedEnumerable<int> GetMiddleNodes() =>
            NodeByValue
                .Values
                .Where(tree => tree.Parent != null &&
                               tree.Children.Count > 0)
                .Select(tree => tree.Value)
                .OrderBy(val => val);

        // Finds tree's leftmost deepest node
        private static int DeepestNode => GetLeafNodes().ToArray().FirstOrDefault();

        // Finds the longest path in the tree
        // (the leftmost if several paths have the same longest length)
        private static IEnumerable<int> GetLongestPath()
        {
            var current = GetTreeNodeByValue(DeepestNode);

            var path = new Stack<int>();

            while (current != null)
            {
                path.Push(current.Value);
                current = current.Parent;
            }

            return path;
        }

        // Finds all paths in the tree with given sum
        // of their nodes (from the leftmost to the rightmost)
        // and prints them on the console 
        private static void PrintPathsByGivenSum(int targetSum)
        {
            var nodes = new List<Tree<int>>();
            var root = GetRootNode();
            Dfs(root, nodes);
            WriteLine($"Paths of sum {targetSum}:");
            foreach (var tree in nodes)
            {
                var (path, sum) = PathAndSumByTree(tree);
                if (sum == targetSum)
                {
                    WriteLine(path);
                }
            }
        }

        private static (string path, int sum) PathAndSumByTree(Tree<int> tree)
        {
            var sum = 0;
            var path = new Stack<int>();
            while (tree != null)
            {
                sum += tree.Value;
                path.Push(tree.Value);
                tree = tree.Parent;
            }

            return (string.Join(" ", path),sum);
        }

        private static void Dfs(Tree<int> node, ICollection<Tree<int>> nodes)
        {
            foreach (var child in node.Children)
            {
                Dfs(child, nodes);
            }

            nodes.Add(node);
        }
    }
}

