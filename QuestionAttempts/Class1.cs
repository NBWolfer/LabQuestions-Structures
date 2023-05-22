using System;

namespace QuestionAttempts
{
    public class Class1
    {
        public class BSTNode
        {
            public int data;
            public BSTNode left;
            public BSTNode right;
            public BSTNode parent;

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
                    node.left.parent = node;
                }
                else if (data > node.data)
                {
                    node.right = InsertHelper(node.right, data);
                    node.right.parent = node;
                }

                return node;
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

            public void MinorMaxH(bool minormax)
            {
                if (minormax)
                {
                    ConvertToMinHeap(root);
                }
                else
                {
                    ConvertToMaxHeap(root);
                }
            }
            private void ConvertToMinHeap(BSTNode node)
            {
                if (node == null)
                {
                    return;
                }

                ConvertToMinHeap(node.left);
                ConvertToMinHeap(node.right);

                if (node.left != null && node.left.data < node.data)
                {
                    int temp = node.data;
                    node.data = node.left.data;
                    node.left.data = temp;
                    ConvertToMinHeap(node.left);
                }
                else if (node.right != null && node.right.data < node.data)
                {
                    int temp = node.data;
                    node.data = node.right.data;
                    node.right.data = temp;
                    ConvertToMinHeap(node.right);
                }
            }

            private void ConvertToMaxHeap(BSTNode node)
            {
                if (node == null)
                {
                    return;
                }

                ConvertToMaxHeap(node.right);

                if (node.right != null && node.right.data > node.data)
                {
                    int temp = node.data;
                    node.data = node.right.data;
                    node.right.data = temp;
                    ConvertToMaxHeap(node.right);
                }

                else if (node.left != null && node.left.data > node.data)
                {
                    int temp = node.data;
                    node.data = node.left.data;
                    node.left.data = temp;
                    ConvertToMaxHeap(node.left);
                }

                ConvertToMaxHeap(node.left);
            }
        }
        
        //public static void Main()
        //{
        //    BST bST = new BST();
        //    BST bST2 = new BST();
        //    int[] nums = { 6, 4, 8, 1, 5, 7, 10 };
        //    Console.WriteLine("Kaç adet sayı gireceksiniz:");
        //    int n = Convert.ToInt32(Console.ReadLine());
        //    for(int i=0; i<n; i++)
        //    {
        //        int x = Convert.ToInt32(Console.ReadLine());
        //        bST.Insert(x);
        //        bST2.Insert(x);
        //    }
        //    bST.Print();
        //    Console.Write("\nMin-Heap:");
        //    bST.MinorMaxH(true);
        //    bST.Print();
        //    Console.Write("\n\nMax-Heap:");
        //    bST2.MinorMaxH(false);
        //    bST2.Print();

        //}
    }
}
