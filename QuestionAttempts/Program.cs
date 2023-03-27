using QuestionAttempts;
using System;
using static QuestionAttempts.Trees.Func;

namespace Questions
{
    public class mainclass{
        public class Node
        {
            public int value;
            public Node next;
            public Node(int value)
            { 
                this.value=value;
                next = this;
            }
        }

        public class list
        {
            internal Node head;
        }

        public class Funcs
        {

            public static void Push(list node, int val) // Liste ve Queue için pushlama fonksiyonu
            {
                Node newnode = new Node(val);

                if (node.head == null)
                {
                    node.head = newnode;
                    return;
                }
                Node temp = node.head;
                while (temp.next != null)
                {
                    temp = temp.next;
                }
                temp.next = newnode;
            }
            public static void PushStack(list node, int val) // Stack için pushlama fonksiyonu
            {
                Node newnode = new Node(val);
                newnode.next = node.head;
                node.head = newnode;
            }
            public static int Pop(list node) 
            {
                if (node.head == null)
                {
                    return 0;
                }
                int temp = node.head.value;
                node.head = node.head.next;
                return temp;
            }

            public static void Print(list node) // Verilen bağlı yapıyı yazdırma
            {
                if (node.head == null)
                {
                    return;
                }
                Node temp = node.head;
                while (temp != null)
                {
                    Console.Write(temp.value + " ");
                    temp = temp.next;
                }
                Console.WriteLine("\n");
            }

            public static int GetLength(list node)  // Kolaylık sağlaması için yapıldı
            {
                if (node.head == null)
                {
                    return 0;
                }
                int length = 0;
                Node temp = node.head;
                while (temp != null)
                {
                    length++;
                    temp = temp.next;
                }
                return length;
            }

            public static void TersKuyruk(list node,list queue) // Listeyi kuyruğa tersten pushlamak için
            {
                list stack = new list();
                Node temp = node.head;
                for(int i = 0; i < GetLength(node); i++)
                {
                    PushStack(stack, temp.value);          // Burada listeyi stack e pushluyoruz
                    temp= temp.next;
                }
                for(int i = 0; i < GetLength(node); i++)
                {
                    Push(queue, stack.head.value);        // Burada da stack i queue e pushluyoruz
                    stack.head = stack.head.next;
                }
            }
            public static void TekCiftFark(list stack,list queue) // C şıkkının cevabı
            {
                int tekcount = 0;
                int ciftcount = 0;
                Node temp = stack.head;
                while (temp != null)
                {
                    if(temp.value%2 == 0)
                    {
                        ciftcount++;
                    }
                    temp = temp.next;
                }
                temp = queue.head;
                while (temp != null)
                {
                    if(temp.value%2 != 0) {
                        tekcount++;
                    }
                    temp= temp.next;
                }
                if (tekcount > ciftcount)
                {
                    Console.WriteLine("Kuyruktaki tek sayılar yığıttaki çift sayılardan fazla !");
                }
                else if (ciftcount > tekcount)
                {
                    Console.WriteLine("Kuyruktaki tek sayılar yığıttaki çift sayılardan eksik !");
                }
                else
                {
                    Console.WriteLine("Kuyruktaki tek sayılar ve yığıttaki çift sayılar eşit sayıda !");
                }
            }
        }


        //public static void Main()
        //{
        //    int[] dizi = { 1, 4, 5, 8, 3, 2, 7, 10 };
        //    // Eğer n elemanlı istediğiniz bir dizi kullanılmak istenirse
        //    Console.WriteLine("Dizinin eleman sayısını girin: ");
        //    int countdizi = Convert.ToInt32(Console.ReadLine());
        //    int[] dizi2 = new int[countdizi];
        //    for (int i = 0; i < countdizi; i++)
        //    {
        //        dizi2[i] = Convert.ToInt32(Console.ReadLine()); // dizi yazan yerlere dizi2 yazılmalı eğer kullanılacaksa
        //    }

        //    list liste = new list();
        //    for(int i = 0;i< dizi2.Length; i++)
        //    {
        //        Funcs.Push(liste, dizi2[i]);
        //    }
        //    Console.Write("\nListe: ");
        //    Funcs.Print(liste);
        //    list queue = new list();
            
        //    Funcs.TersKuyruk(liste, queue);
        //    Console.Write("\nQueue: ");
        //    Funcs.Print(queue);

        //    list stack = new list();

        //    Console.WriteLine("Kaç elemanı stack e eklemek istersiniz: ");
        //    int count = Convert.ToInt32(Console.ReadLine());
        //    for(int i = 0; i < count; i++)
        //    {
        //        Funcs.PushStack(stack, Funcs.Pop(queue));
        //    }
        //    Console.Write("Stack: ");
        //    Funcs.Print(stack);
        //    Console.Write("\nQueue: ");
        //    Funcs.Print(queue);

        //    Funcs.TekCiftFark(stack, queue);

        //}
    }
}