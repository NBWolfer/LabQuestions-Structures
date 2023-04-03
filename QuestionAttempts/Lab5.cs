using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAttempts
{
    public class Lab5
    {

        // Doubly Linked List
        class Node
        {
            public int val;
            public Node prev;
            public Node next;
            public Node(int data)
            {
                val = data;
                prev = null;
                next = null;
            }
        }

        class DLList
        {
            internal Node head;
        }
        // Sona ekleme
        static void Insertlast(DLList node, int data)
        {
            Node newnode = new Node(data);
            if (node.head == null)
            {
                newnode.prev = null;
                node.head = newnode;
                return;
            }
            Node lastnode = Getlastnode(node);
            lastnode.next = newnode;
            newnode.prev = lastnode;
        }
        // listeyi yazdırma
        static void Printlist(DLList node)
        {
            Node temp = node.head;
            while (temp != null)
            {

                Console.Write(" " + temp.val);
                temp = temp.next;
            }
            Console.WriteLine("\n");
        }
        // son düğüme ulaşmak için
        static Node Getlastnode(DLList node)
        {
            Node temp = node.head;
            while (temp.next != null)
            {
                temp = temp.next;
            }
            return temp;
        }
        // liste uzunluğu için
        static int Getlenght(DLList node)
        {
            Node temp = node.head;
            int count = 0;
            while (temp != null)
            {
                count++;
                temp = temp.next;
            }
            return count;
        }
        static Node GetNodeatIndex(DLList node,int index)
        {
            Node temp = node.head;
            int iter = 0;
            while(temp != null && iter<=index)
            {
                iter++;
                temp=temp.next;
            }
            return temp;
        }
        //////////////////////////////////////

        // Sortlar 
        static void SelectionSort(DLList node)
        {
            if (node.head == null || node.head.next == null)
                return;

            Node current = node.head;

            while (current != null)
            {
                Node min = FindMin(current);
                SwappingNodes(node, current, min);
                current = current.next;
            }
        }

        static Node FindMin(Node start)
        {
            Node min = start;
            Node current = start.next;

            while (current != null)
            {
                if (current.val < min.val)
                    min = current;

                current = current.next;
            }

            return min;
        }


        static void RecursiveInsertionSort(DLList node)
        {
            if (node.head == null || node.head.next == null)
                return;

            node.head = RecursiveInsertionSort(node.head);
        }

        static Node RecursiveInsertionSort(Node node)
        {
            if (node == null || node.next == null)
                return node;

            Node sorted = RecursiveInsertionSort(node.next);

            node.next = null;
            node.prev = null;

            sorted = SortedInsert(sorted, node);

            return sorted;
        }

        static Node SortedInsert(Node sorted, Node newNode)
        {
            if (sorted == null)
            {
                newNode.next = null;
                newNode.prev = null;
                sorted = newNode;
                return sorted;
            }

            if (newNode.val <= sorted.val)
            {
                newNode.next = sorted;
                sorted.prev = newNode;
                sorted = newNode;
                return sorted;
            }

            Node current = sorted;
            while (current.next != null && current.next.val < newNode.val)
            {
                current = current.next;
            }

            newNode.next = current.next;
            if (current.next != null)
                current.next.prev = newNode;

            current.next = newNode;
            newNode.prev = current;

            return sorted;
        }
    
        static void SwappingNodes(DLList node, Node node1, Node node2)
        {
            if (node1 == node2)
                return;

            Node temp = node1.prev;
            node1.prev = node2.prev;
            node2.prev = temp;

            if (node1.prev != null)
                node1.prev.next = node1;

            if (node2.prev != null)
                node2.prev.next = node2;

            temp = node1.next;
            node1.next = node2.next;
            node2.next = temp;

            if (node1.next != null)
                node1.next.prev = node1;

            if (node2.next != null)
                node2.next.prev = node2;

            if (node.head == node1)
                node.head = node2;
            else if (node.head == node2)
                node.head = node1;
        }
        static void BubbleSort(DLList node)
        {
            if(node.head == null)
            {
                return;
            }
            Node temp1;
            Node temp2 = null;
            bool swap;
            do
            {
                swap = false;
                temp1 = node.head;
                while (temp1.next != temp2)
                {
                    if (temp1.val > temp1.next.val)
                    {
                        SwappingNodes(node, temp1, temp1.next);
                        swap = true;
                    }
                    else
                    {
                        temp1 = temp1.next;

                    }
                }
                temp2 = temp1;
            } while (swap);
        }

        // Sayı kontrolleri
        static int TAUCounter(DLList node)
        {
            Node temp = node.head;
            int counter = 0;
            while (temp != null)
            {
                if (IsTAU(temp.val))
                {
                    counter++;
                }
                temp = temp.next;
            }
            return counter;
        }

        static int LASACounter(DLList node)
        {
            Node temp = node.head;
            int counter = 0;
            while (temp != null)
            {
                if(IsLASA(temp.val))
                {
                    counter++;
                }
                temp = temp.next;
            }
            return counter;
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

        public static bool IsTAU(int cont)
        {
            int count = 0;
            for(int i =1; i <= cont; i++)
            {
                if (cont % i == 0)
                {
                    count++;
                }
            }
            return (cont % count == 0);
        }

        public static bool IsLASA(int cont)
        {
            if (cont < 10)
            {
                return false;
            }
            string scont = cont.ToString();
            int reversecont = 0 ;
            for(int i= scont.Length - 1;i>= 0; i--)
            {
                reversecont = reversecont * 10 + Convert.ToInt32(scont[i].ToString());
            }
            return (IsPrime(Convert.ToInt32(reversecont)) && IsPrime(cont));
        }

        public static void Main()
        {
            DLList dLList = new DLList();
            StreamReader streamReader = new StreamReader(@"C:\Users\Mahmut Enes\Desktop\Coding\C#\QuestionAttempts\QuestionAttempts\Sayilar.txt");
            string line = streamReader.ReadLine();
            while(line!= null)
            {
                Insertlast(dLList,Convert.ToInt32(line));
                line = streamReader.ReadLine();
            }
            streamReader.Close();
            Printlist(dLList);
            int lasaCount = LASACounter(dLList);
            int tauCount = TAUCounter(dLList);
            
            if(lasaCount> tauCount)
            {
                BubbleSort(dLList);
            }
            else if(tauCount> lasaCount)
            {
                RecursiveInsertionSort(dLList);
            }
            else
            {
                SelectionSort(dLList);
            }
            Printlist(dLList);
        }
    }
}
