using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAttempts
{
    public class Trees
    {
        public class Node
        {
            public int value;
            public Node left;
            public Node right;
            public Node(int value) { 
                this.value = value;
                left = null;
                right = null;
            }
        }

        public class Tree
        {
            internal Node root;
        }

        public class Func
        {
            public static Node AddNode(Node node, int val)
            {
                if(node == null)
                {
                    return new Node(val);
                }
                if (node.value > val)
                {
                    node.left = AddNode(node.left, val);
                }
                else
                {
                    node.right = AddNode(node.right, val);
                }
                return node;
            }

            public static Node DeleteNode(Node root, int val)
            {
                if (root == null)
                {
                    return root;
                }

                if (val < root.value)
                {
                    root.left = DeleteNode(root.left, val);
                }
                else if (val > root.value)
                {
                    root.right = DeleteNode(root.right, val);
                }
                else
                {
                    if (root.left == null)
                    {
                        return root.right; // sol alt ağaç yoksa, sağ alt ağaç kök düğüm
                    }
                    else if (root.right == null)
                    {
                        return root.left; // sağ alt ağaç yoksa, sol alt ağaç kök düğüm
                    }

                    Node smallestNode = FindSmallestNode(root.right);
                    root.value = smallestNode.value;
                    root.right = DeleteNode(root.right, smallestNode.value);
                }

                return root;
            }

            private static Node FindSmallestNode(Node node) // Silerken
            {
                Node current = node;

                while (current.left != null)
                {
                    current = current.left;
                }

                return current;
            }

            public static Node FindNode(Node root, int val)
            {
                if(root == null)
                {
                    return null;
                }
                if(val == root.value)
                {
                    return root;
                }
                if(val  < root.value)
                {
                    return FindNode(root.left, val);
                }
                else
                {
                    return FindNode(root.right, val);
                }
            }

            public static Node BFS(Node root, int val)
            {
                if (root == null)
                {
                    return null;
                }

                Queue<Node> queue = new Queue<Node>();
                queue.Enqueue(root);

                while (queue.Count > 0)
                {
                    Node currentNode = queue.Dequeue();

                    if (currentNode.value == val)
                    {
                        return currentNode;
                    }

                    if (currentNode.left != null)
                    {
                        queue.Enqueue(currentNode.left);
                    }

                    if (currentNode.right != null)
                    {
                        queue.Enqueue(currentNode.right);
                    }
                }

                return null;
            }

            public static Node DFS(Node root, int val)
            {
                if (root == null)
                {
                    return null;
                }

                if (root.value == val)
                {
                    return root;
                }

                Node left = DFS(root.left, val);
                if (left != null)
                {
                    return left;
                }

                Node right = DFS(root.right, val);
                if (right != null)
                {
                    return right;
                }

                return null;
            }

            public static Node DFSIter(Node root, int val)
            {
                if (root == null)
                {
                    return null;
                }

                Stack<Node> stack = new Stack<Node>();
                stack.Push(root);
                while (stack.Count > 0)
                {
                    Node currentNode = stack.Pop();
                    if (currentNode.value == val)
                    {
                        return currentNode;
                    }
                    if(currentNode.left != null)
                    {
                        stack.Push(currentNode.left);
                    }
                    if( currentNode.right != null)
                    {
                        stack.Push(currentNode.right);
                    }
                }
                return null;
            }

            public static void PrintTree(Node root)
            {
                if(root == null)
                {
                    return;
                }
                PrintTree(root.left);
                Console.Write(root.value+" ");
                PrintTree(root.right);
            }

            public static int GetDepth(Node root)
            {
                if(root == null)
                {
                    return 0;
                }
                else
                {
                    int leftdepth = GetDepth(root.left);
                    int rightdepth = GetDepth(root.right);

                    if(leftdepth > rightdepth)
                    {
                        return leftdepth+1;
                    }
                    else
                    {
                        return rightdepth+1;
                    }
                }
            }

            public static bool IsBalanced(Node root)
            {
                if (root == null)
                {
                    return true;
                }

                int leftHeight = Height(root.left);
                int rightHeight = Height(root.right);

                if (Math.Abs(leftHeight - rightHeight) <= 1 &&
                    IsBalanced(root.left) && IsBalanced(root.right))
                {
                    return true;
                }

                return false;
            }

            public static int Height(Node node)
            {
                if (node == null)
                {
                    return 0;
                }

                return 1 + Math.Max(Height(node.left), Height(node.right));
            }

            public static Node BalanceTree(Node root)
            {
                if (root == null)
                {
                    return null;
                }

                int balanceFactor = Height(root.left) - Height(root.right);

                if (balanceFactor > 1) // Ağaç sola eğilimli
                {
                    if (Height(root.left.left) >= Height(root.left.right))
                    {
                        root = RotateRight(root); // Sağa dönüşüm
                    }
                    else
                    {
                        root.left = RotateLeft(root.left); // Sol düğüm için sola dönüşüm
                        root = RotateRight(root); // Sağa dönüşüm
                    }
                }
                else if (balanceFactor < -1) // Ağaç sağa eğilimli
                {
                    if (Height(root.right.right) >= Height(root.right.left))
                    {
                        root = RotateLeft(root); // Sola dönüşüm
                    }
                    else
                    {
                        root.right = RotateRight(root.right); // Sağ düğüm için sağa dönüşüm
                        root = RotateLeft(root); // Sola dönüşüm
                    }
                }
                root.left = BalanceTree(root.left);
                root.right = BalanceTree(root.right);

                return root;
            }

            public static Node RotateRight(Node node)
            {
                Node newRoot = node.left;
                node.left = newRoot.right;
                newRoot.right = node;
                return newRoot;
            }

            public static Node RotateLeft(Node node)
            {
                Node newRoot = node.right;
                node.right = newRoot.left;
                newRoot.left = node;
                return newRoot;
            }
        }

    //    public static void Main()
    //    {
    //        Stopwatch stopwatch = new Stopwatch();
    //        stopwatch.Start();
    //        Tree tree = new Tree();

    //        tree.root = Func.AddNode(tree.root, 1);
    //        tree.root = Func.AddNode(tree.root, 2);
    //        tree.root = Func.AddNode(tree.root, 3);
    //        tree.root = Func.AddNode(tree.root, 4);

    //        Random rnd = new Random();
    //        int[] test = new int[100];
    //        for (int i = 0; i < 100; i++)             // Ağacı rastgele değerlerle doldurmak için
    //        {
    //            test[i] = rnd.Next(100);
    //            tree.root = Func.AddNode(tree.root, test[i]);
    //        }

    //        Func.PrintTree(tree.root);
    //        Console.WriteLine();
    //        Console.Write("Aranan değeri yazın:");
    //        int find = Convert.ToInt32(Console.ReadLine());
    //        Node foundNode = Func.DFSIter(tree.root, find);
    //        if (foundNode == null)
    //        {
    //            Console.WriteLine("Bulunamadı.");
    //        }
    //        else
    //        {
    //            Console.WriteLine("Bulundu: " + foundNode.value);
    //        }

    //        Console.WriteLine();
    //        Console.WriteLine("Derinlik: " + Func.GetDepth(tree.root));
    //        if (Func.IsBalanced(tree.root))
    //        {
    //            Console.WriteLine("\nDenge: Dengeli");
    //        }
    //        else
    //        {
    //            Console.WriteLine("\nDenge: Dengesiz");
    //            while (!Func.IsBalanced(tree.root))
    //            {
    //                tree.root = Func.BalanceTree(tree.root);
    //            }
    //            Func.PrintTree(tree.root);
    //            if (Func.IsBalanced(tree.root))
    //            {
    //                Console.WriteLine("\nDengeli hale geldi.");
    //                Console.WriteLine("Derinlik: " + Func.GetDepth(tree.root));
    //            }
    //            else
    //            {
    //                Console.WriteLine("\nHala dengesiz");
    //            }
    //        }
    //        stopwatch.Stop();
    //        TimeSpan timeSpan = stopwatch.Elapsed;
    //        Console.WriteLine("\n" + timeSpan.TotalMilliseconds + " ms");
    //    }
    }
}
