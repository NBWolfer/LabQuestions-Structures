using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAttempts
{
    public class Lab4
    {
        public class Node
        {
            public int value;
            public Node left;
            public Node right;
            public Node(int value)
            {
                this.value = value;
                left = null;
                right = null;
            }
        }

        public class Tree
        {
            internal Node root;
        }


        private static int stepcounter = 0;
        public static Node AddNode(Node node, int val)
        {
            if (node == null)
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

        // hem DFS hem de BFS için çıkış değeri vermediğimiz için bütün ağacı dolaşarak istediğimiz koşulu kontrol ediyor.
        public static void BFSIter(Node root, int val)
        {
            if (root == null)
            {
                return;
            }
            stepcounter = 1;

            list queue = new list();
            PushQueue(queue, root);

            while (GetLength(queue) > 0)
            {
                Node currentNode = PopQueue(queue);

                if (IsPrime(XOR(currentNode.value, val)))
                {
                    Console.WriteLine("BFS--> " + currentNode.value + "\t" + stepcounter + " Adım");
                }

                if (currentNode.left != null)
                {
                    PushQueue(queue, currentNode.left);
                }

                if (currentNode.right != null)
                {
                    PushQueue(queue, currentNode.right);
                }
                stepcounter++;
            }
            return;
        }

        public static void DFSIter(Node root, int val)
        {
            if (root == null)
            {
                return;
            }
            stepcounter = 1;
            list stack = new list();
            PushStack(stack, root);
            while (GetLength(stack) > 0)
            {
                Node currentNode = PopStack(stack);
                if (IsPrime(XOR(currentNode.value, val)))
                {
                    Console.WriteLine("DFS--> " + currentNode.value + "\t" + stepcounter + " Adım");
                }
                if (currentNode.left != null)
                {
                    PushStack(stack, currentNode.left);
                }
                if (currentNode.right != null)
                {
                    PushStack(stack, currentNode.right);
                }
                stepcounter++;
            }
            return;
        }

        public static void PrintTree(Node root)
        {
            if (root == null)
            {
                return;
            }
            PrintTree(root.left);
            Console.Write(root.value + " ");
            PrintTree(root.right);
        }

        public static int GetLength(list node)
        {
            if (node.head == null)
            {
                return 0;
            }
            int length = 0;
            NodeList temp = node.head;
            while (temp != null)
            {
                length++;
                temp = temp.next;
            }
            return length;
        }

        public static int XOR(int a, int b)
        {
            int result = 0;
            int bitIndex = 0;

            while (a > 0 || b > 0)      // Burada bitwise bakarak iki sayının karşılıklı bitlerini karşılaştırıyoruz.
            {
                int aBit = a % 2;       // burada her iki sayının o andaki bitleri tutuluyor.
                int bBit = b % 2;

                if (aBit != bBit)      // Eğer bitleri aynı ise 0, farklı ise 1 yerleştiriliyor.
                {
                    result += (1 << bitIndex);
                }

                a /= 2;                // Sonraki basamağa geçmek için
                b /= 2;
                bitIndex++;
            }

            return result;
        }

        public static bool IsPrime(int cont)
        {
            if (cont <= 1) return false;

            for (int i = 2; i < Math.Sqrt(cont); i++) // Girilen sayının kareköküne kadar giderek asallığı kontrol ediyoruz.
            {
                if (cont % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        // Stack ve Queue için
        public class NodeList
        {
            public Node value;
            public NodeList next;
            public NodeList(Node value)
            {
                this.value = value;
                next = this;
            }
        }

        public class list
        {
            internal NodeList head;
        }

        public static void PushQueue(list node, Node val)
        {
            NodeList newnode = new NodeList(val);
            newnode.next = node.head;
            node.head = newnode;
        }

        public static Node PopQueue(list node)
        {
            if (node.head == null)
            {
                return null;
            }
            NodeList temp = node.head;
            if (node.head.next == null)
            {
                temp = node.head;
                node.head = null;
                return temp.value;
            }
            NodeList tempPrev = null;
            while (temp.next != null)
            {
                tempPrev = temp;
                temp = temp.next;
            }
            temp = tempPrev.next;
            tempPrev.next = null;
            return temp.value;
        }

        public static void PushStack(list node, Node val)
        {
            NodeList newnode = new NodeList(val);
            newnode.next = node.head;
            node.head = newnode;
        }

        public static Node PopStack(list node)
        {
            if (node.head == null)
            {
                return null;
            }
            Node temp = node.head.value;
            node.head = node.head.next;
            return temp;
        }

        //public static void Main()
        //{
        //    Tree tree = new Tree();
        //    int num=1;

        //    // A)
        //    while (num != 0)
        //    {
        //        Console.Write("Eklemek istediğiniz değeri girin: ");
        //        num = Convert.ToInt32(Console.ReadLine());
        //        if(num != 0)
        //            tree.root = AddNode(tree.root, num);
        //    }

        //    // B)
        //    Console.Write("\nBir sayı girin:");
        //    int contNum = Convert.ToInt32(Console.ReadLine());

        //    // B-1
        //    BFSIter(tree.root, contNum);
        //    Console.WriteLine();
        //    // B-2
        //    DFSIter(tree.root, contNum);

        //}
    }
}
