using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAttempts
{
    public class Lab7
    {
        public class Queue
        {
            public class Dugum
            {
                public Node dugum;
                public Dugum Next;
                public Dugum(Node dugum)
                {
                    this.dugum = dugum;
                    this.Next = null;
                }
            }
            private Dugum head;
            public void Enqueue(Node node)
            {
                if (head == null)
                {
                    head = new Dugum(node);
                }

                Dugum temp = head;
                while(temp.Next != null)
                {
                    temp = temp.Next;
                }
                temp.Next = new Dugum(node);
            }
            public Node Dequeue()
            {
                Dugum temp = head;
                if(temp.Next != null)
                {
                    return temp.dugum;
                }
                while(temp.Next.Next != null)
                {
                    temp = temp.Next;
                }
                Node node = temp.Next.dugum;
                temp.Next = null;
                return node;
            }
            public int Count()
            {
                Dugum temp = head;
                int t = 0;
                while( temp.Next != null)
                {
                    t++;
                    temp = temp.Next;
                    
                }
                return t;
            }
        }

        public class Node
        {
            public int value;
            public Node left;
            public Node right;
            public Node parent;

            public Node(int value)
            {
                this.value = value;
                this.left = null;
                this.right = null;
            }
        }

        public class MinHeap
        {
            private Node root;

            public void Insert(int val)         // bfs algoritmasına benzer şekilde min-heap yapısına uyulması için düğümleri kuyruk kullanarak ağaca levele göre ekliyor
            {
                Node newnode = new Node(val);
                newnode.parent = null;
                if(root == null)
                {
                    root = newnode;
                    return;
                }

                Queue queue = new Queue();
                queue.Enqueue(root);
                while (queue.Count() > 0)
                {
                    Node node = queue.Dequeue();
                    ConvertToMinHeap(node);
                    if(node.left == null)
                    {
                        node.left = newnode;
                        newnode.parent = node;
                    }
                    if(node.right == null)
                    {
                        node.right = newnode;
                        newnode.parent = node;
                    }
                    queue.Enqueue(node.left);
                    queue.Enqueue(node.right);
                }
            }

            public void ConvertToMinHeap(Node node) // post order gezerek ağacın elemanlarını min-heap yapısına uygun değiştirme
            {
                if (node == null)
                {
                    return;
                }

                ConvertToMinHeap(node.left);
                ConvertToMinHeap(node.right);

                if (node.left != null && node.left.value < node.value)
                {
                    int temp = node.value;
                    node.value = node.left.value;
                    node.left.value = temp;
                    ConvertToMinHeap(node.left);
                }
                else if (node.right != null && node.right.value < node.value)
                {
                    int temp = node.value;
                    node.value = node.right.value;
                    node.right.value = temp;
                    ConvertToMinHeap(node.right);
                }
            }

            public void Print()
            {
                PrintH(root);
            }
            public void PrintH(Node node)
            {
                if (node != null)
                {
                    PrintH(node.left);
                    Console.Write(node.value+" ");
                    PrintH(node.right);
                }
            }
        }
        //public static void Main()
        //{
        //    MinHeap minHeap = new MinHeap();
        //    Console.WriteLine("Enter the number of elements:");
        //    int n = Convert.ToInt32(Console.ReadLine());
        //    for(int i = 0; i < n; i++)
        //    {
        //        minHeap.Insert(Convert.ToInt32(Console.ReadLine()));
        //    }
        //    Console.WriteLine();
        //    minHeap.Print();
        //}
    }
}
