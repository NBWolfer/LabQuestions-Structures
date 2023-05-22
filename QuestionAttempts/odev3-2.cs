using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAttempts
{
    public class odev3_2
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

        public class MinHeap
        {
            public int[] arr;
            public int iter;
            public MinHeap(int iter, int count)
            {
                this.iter = iter;
                arr = new int[count];
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
                if (node == null)
                { return; }
                else
                {
                    PrintHelper(node.left);
                    Console.Write(node.data + " ");
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
                    Console.Write(node.data + " ");
                    PrintPreHelper(node.left);
                    PrintPreHelper(node.right);
                }
            }

            public void Heap()
            {
                Heap(this.root);
            }

            private void Heap(BSTNode node)
            {

            }

            private int GetParentIndex(int i)
            {
                return (i - 1) / 2;
            }

            private int GetLeftIndex(int i)
            {
                return (2 * i + 1);
            }

            private int GetRightIndex(int i)
            {
                return (2 * i + 2);
            }


            private MinHeap InsertHeap(MinHeap heap, int el)
            {
                heap.iter++;
                heap.arr[heap.iter-1]=el;

                int curr = heap.iter-1;

                while(curr > 0 && heap.arr[GetParentIndex(curr)] > heap.arr[curr])
                {
                    int temp = heap.arr[curr];
                    heap.arr[GetParentIndex(curr)] = heap.arr[curr];
                    heap.arr[curr] = temp;
                    curr = GetParentIndex(curr);
                }
                return heap;
            }
            
            private MinHeap Heapify(MinHeap heap,int index)
            {
                if(heap.iter <= 1)
                {
                    return heap;
                }
                
                int left = GetLeftIndex(index);
                int right = GetRightIndex(index);

                int small = index;
                if(left < heap.iter && heap.arr[left]< heap.arr[index])
                {
                    small = left;
                }

                if(right < heap.iter && heap.arr[right]< heap.arr[small])
                {
                    small = right;
                }

                if(small != index)
                {
                    int temp = heap.arr[index];
                    heap.arr[index] = heap.arr[small];
                    heap.arr[small] = temp;
                    heap = Heapify(heap,small);
                }
                return heap;
            }
            
           

        }
        //public static void Main()
        //{

        //}
    }
}
