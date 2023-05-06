using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAttempts
{
    public class Odev_3
    {
        public class BSTNode
        {
            public int data;
            public BSTNode left;
            public BSTNode right;

            public BSTNode(int data)
            {
                this.data = data;
                this.left = null;
                this.right = null;
            }
        }

        public class BST
        {
            private BSTNode root;

            public BST()
            {
                this.root = null;
            }

            public void Insert(int data)
            {
                this.root = InsertHelper(this.root, data);
            }

            private BSTNode InsertHelper(BSTNode node, int data)
            {
                if (node == null)
                {
                    node = new BSTNode(data);
                }
                else if (data < node.data)
                {
                    node.left = InsertHelper(node.left, data);
                }
                else if (data > node.data)
                {
                    node.right = InsertHelper(node.right, data);
                }

                return node;
            }

            public void Delete(int data)
            {
                this.root = DeleteHelper(this.root, data);
            }

            private BSTNode DeleteHelper(BSTNode node, int data)
            {
                if (node == null)
                {
                    return node;
                }

                if (data < node.data)
                {
                    node.left = DeleteHelper(node.left, data);
                }
                else if (data > node.data)
                {
                    node.right = DeleteHelper(node.right, data);
                }
                else
                {
                    if (node.left == null)
                    {
                        return node.right;
                    }
                    else if (node.right == null)
                    {
                        return node.left;
                    }

                    node.data = FindMinValue(node.right);

                    node.right = DeleteHelper(node.right, node.data);
                }

                return node;
            }

            private int FindMinValue(BSTNode node)
            {
                int minValue = node.data;

                while (node.left != null)
                {
                    minValue = node.left.data;
                    node = node.left;
                }

                return minValue;
            }
            
            public void Print()
            {
                PrintHelper(this.root);
            }

            private void PrintHelper(BSTNode node)
            {
                if(node == null)
                { return; }
                else
                {
                    PrintHelper(node.left);
                    Console.Write(node.data+" ");
                    PrintHelper(node.right);
                }
            }

            public void PrintPre()
            {
                PrintPreHelper(this.root);
            }

            private void PrintPreHelper(BSTNode node)
            {
                if (node == null)
                { return; }
                else
                {
                    Console.Write(node.data+" ");
                    PrintPreHelper(node.left);
                    PrintPreHelper(node.right);
                }
            }
            private int[] minheapfiedarr;
            public void ConvertToHeap(bool minormax)
            {
                int[] arr = InorderTraversal();
                MinHeapify(arr, arr.Length);
                minheapfiedarr = arr;
                if (minormax)
                {
                    Array.Reverse(minheapfiedarr);
                    ApplyHeap(minheapfiedarr, this.root, 0);
                    return;
                }
                ApplyHeap( arr,this.root, 0);
            }

            private void MinHeapify(int[] arr, int n)
            {
                for (int i = n / 2 - 1; i >= 0; i--)
                {
                    Heapify(arr, n, i);
                }

                for (int i = n - 1; i > 0; i--)
                {
                    (arr[i], arr[0]) = (arr[0], arr[i]);
                    Heapify(arr, i, 0);
                }
            }

            private void Heapify(int[] arr, int n, int i)
            {
                int smallest = i; // En küçük elemanın index'i
                int left = 2 * i + 1; // Sol çocuğun index'i
                int right = 2 * i + 2; // Sağ çocuğun index'i

                // Sol çocuk root elemandan daha küçük ise
                if (left < n && arr[left] < arr[smallest])
                {
                    smallest = left;
                }

                // Sağ çocuk root elemandan daha küçük ise
                if (right < n && arr[right] < arr[smallest])
                {
                    smallest = right;
                }

                // En küçük eleman root eleman değil ise
                if (smallest != i)
                {
                    int temp = arr[i];
                    arr[i] = arr[smallest];
                    arr[smallest] = temp;

                    // Alt ağaçları heapify işlemine sokma
                    Heapify(arr, n, smallest);
                }
            }

            private void ApplyHeap(int[] arr, BSTNode node, int i) // heapfied olmuş dizi ile ağacı istenen heap yapısına güncellemek için
            {
                if (node == null)
                {
                    return;
                }
                node.data = arr[i];
                if (2 * i + 1 < arr.Length)
                {
                    node.left.data = arr[2*i+1];
                    ApplyHeap(arr, node.left, 2 * i + 1);
                }
                if (2 * i + 2 < arr.Length)
                {
                    node.right.data = arr[2*i+2];
                    ApplyHeap(arr, node.right, 2 * i + 2);
                }
            }

            private int[] InorderTraversal()
            {
                List<int> list = new List<int>();
                InorderTraversalHelper(root, list);
                return list.ToArray();
            }

            private void InorderTraversalHelper(BSTNode node, List<int> list)
            {
                if (node == null)
                {
                    return;
                }
                InorderTraversalHelper(node.left, list);
                list.Add(node.data);
                InorderTraversalHelper(node.right, list);
            }

        }
        public static void Main()
        {
            BST bst = new BST();
            Console.WriteLine("Sayı sayısını yazın:");
            int count = Convert.ToInt32(Console.ReadLine());
            for(int i= 0; i < count; i++)
            {
                bst.Insert(Convert.ToInt32(Console.ReadLine()));
            }
            Console.WriteLine("\nInorder dolaşarak ağacın içindeki değerler:");
            bst.Print();
            bool minormax = false; // false ise max-heap yapısına uygun ağacı güncelliyor, true ise min-heap
            Console.WriteLine("\n\nMax-heap yapısına uygun değiştirilen ağacın inorder dolaşarak değerleri:");
            bst.ConvertToHeap(minormax);
            bst.Print();
            Console.WriteLine("\n\nMin-heap yapısına uygun değiştirilen ağacın inorder dolaşarak değerleri:");
            minormax = true;
            bst.ConvertToHeap(minormax);
            bst.Print();
        }
    }
}
