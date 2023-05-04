using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAttempts
{
    public class Lab6
    {
        public class Node
        {
            public int value;
            public Node next;

            public Node(int value)
            {
                this.value = value;
                this.next = null;
            }
        }

        public class list
        {
            public Node head;
            public list()
            {
                this.head = null;
            }

            public void AddNode(int data)
            {
                Node newNode = new Node(data);

                if (this.head == null)
                {
                    this.head = newNode;
                }
                else
                {
                    Node currentNode = this.head;
                    while (currentNode.next != null)
                    {
                        currentNode = currentNode.next;
                    }
                    currentNode.next = newNode;
                }
            }

            public void PrintList()
            {
                Node currentNode = this.head;
                while (currentNode != null)
                {
                    Console.Write(currentNode.value + " ");
                    currentNode = currentNode.next;
                }
                Console.WriteLine();
            }

            public Node GetNodeAtIndex(int index)
            {
                Node currentNode = this.head;
                int currentIndex = 0;

                while (currentNode != null && currentIndex < index)
                {
                    currentNode = currentNode.next;
                    currentIndex++;
                }

                if (currentNode != null && currentIndex == index)
                {
                    return currentNode;
                }
                else
                {
                    return null;
                }
            }

            public list Evenlist()
            {
                list even = new list();
                Node temp = this.head;
                while (temp != null)
                {
                    if (GetIndex(temp.value) != -1 && GetIndex(temp.value) % 2 == 0)
                    {
                        even.AddNode(temp.value);
                    }
                    temp = temp.next;
                }
                return even;
            }
            public list Oddlist()
            {
                list odd = new list();
                Node temp = this.head;
                while (temp != null)
                {
                    if (GetIndex(temp.value) != -1 && GetIndex(temp.value) % 2 != 0)
                    {
                        odd.AddNode(temp.value);
                    }
                    temp = temp.next;
                }
                return odd;
            }
            public int GetLenght(Node head)
            {
                int count = 0;
                Node temp = head;
                while (temp != null)
                {
                    count++;
                    temp = temp.next;
                }
                return count;
            }

            public int GetIndex(int data)
            {
                Node currentNode = this.head;
                int index = 0;

                while (currentNode != null)
                {
                    if (currentNode.value == data)
                    {
                        return index;
                    }
                    currentNode = currentNode.next;
                    index++;
                }

                // aranan değer listede yok
                return -1;
            }


            public void MergeSort(bool sortCont)
            {
                this.head = MergeSort(this.head, sortCont);
            }

            private Node MergeSort(Node head, bool sortCont) // sortCont true ise ascending order, false ise descending order oluyor
            {
                if (head == null || head.next == null)
                {
                    return head;
                }

                Node middle = FindMiddleNode(head);
                Node secondHalf = middle.next;
                middle.next = null;

                Node left = MergeSort(head, sortCont);
                Node right = MergeSort(secondHalf, sortCont);
                if (sortCont == true)
                {
                    return MergeAs(left, right);
                }
                else
                {
                    return MergeDes(left, right);
                }

            }

            private Node MergeAs(Node left, Node right)
            {
                Node result = null;
                if (left == null)
                {
                    return right;
                }
                if (right == null)
                {
                    return left;
                }
                if (left.value <= right.value)
                {
                    result = left;
                    result.next = MergeAs(left.next, right);
                }
                else
                {
                    result = right;
                    result.next = MergeAs(left, right.next);
                }
                return result;
            }
            private Node MergeDes(Node left, Node right)
            {
                Node result = null;
                if (left == null)
                {
                    return right;
                }
                if (right == null)
                {
                    return left;
                }
                if (left.value > right.value)
                {
                    result = left;
                    result.next = MergeDes(left.next, right);
                }
                else
                {
                    result = right;
                    result.next = MergeDes(left, right.next);
                }
                return result;
            }

            public Node FindMiddleNode(Node head)
            {
                Node slow = head;
                Node fast = head;

                while (fast.next != null && fast.next.next != null)
                {
                    slow = slow.next;
                    fast = fast.next.next;
                }
                return slow;
            }
        }

        public static list MergeLists(list even, list odd)
        {
            list mergedList = new list();
            Node iter1 = even.head;
            Node iter2 = odd.head;
            if (even.head == null)
            {
                return odd;
            }
            if (odd.head == null)
            {
                return even;
            }
            while (iter1 != null && iter2 != null)
            {
                if (iter1.value > iter2.value)
                {
                    mergedList.AddNode(iter2.value);
                    mergedList.AddNode(iter1.value);
                }
                else
                {
                    mergedList.AddNode(iter1.value);
                    mergedList.AddNode(iter2.value);
                }
                iter1 = iter1.next;
                iter2 = iter2.next;
            }
            if (iter1 != null)
            {
                while (iter1 != null)
                {
                    mergedList.AddNode(iter1.value);
                    iter1 = iter1.next;
                }
            }
            if (iter2 != null)
            {
                while (iter2 != null)
                {
                    mergedList.AddNode(iter2.value);
                    iter2 = iter2.next;
                }
            }
            return mergedList;
        }



        //public static void Main()
        //{
        //    list list = new list();
        //    list odd = new list();
        //    list even = new list();
        //    int cont = 1;
        //    while (cont == 1)
        //    {
        //        Console.WriteLine("Listeye ekle: ");
        //        int val = Convert.ToInt32(Console.ReadLine());
        //        if (val == 0)
        //        {
        //            break;
        //        }
        //        list.AddNode(val);
        //    }
        //    list.PrintList();
        //    even = list.Evenlist();
        //    odd = list.Oddlist();
        //    Console.WriteLine("\nSırasıyla çift poziyondaki ve tek pozisyondakilerin sırasız durumu;");
        //    even.PrintList();
        //    odd.PrintList();
        //    odd.MergeSort(false); // descending
        //    even.MergeSort(true); // ascending
        //    Console.WriteLine("\nSırasıyla çift pozisyondaki ve tek pozisyondakilerin sıralı durumu;");
        //    even.PrintList();
        //    odd.PrintList();
        //    list mergedList = new list();
        //    mergedList = MergeLists(even, odd);
        //    Console.WriteLine("\nListelerin karşılaştırılarak birleşmiş hali;");
        //    mergedList.PrintList();
        //}
    }
}
