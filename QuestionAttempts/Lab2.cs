using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAttempts
{
    public class Lab2
    {
        public class Node
        {
            public int val;
            public Node next;

            public Node(int value)
            {
                val = value;
                next = null;
            }
        }

        public class LinkedListe
        {
            public Node head;
           
            public LinkedListe()
            {
                head = null;
            }

            public void AddLast(int value)
            {
                Node newNode = new Node(value);
                if (head == null)
                {
                    head = newNode;
                    return;
                }
                Node lastNode = head;
                while (lastNode.next != null)
                {
                    lastNode = lastNode.next;
                }
                lastNode.next = newNode;
            }

            public void Print()
            {
                Node currNode = head;
                while (currNode != null)
                {
                    Console.Write(currNode.val + " ");
                    currNode = currNode.next;
                }
                Console.WriteLine();
            }
        }
        public static void PrintPrimes(Node node)
        {
            Node temp = node;
            while (temp != null)
            {
                if (IsPrime(temp.val))              // IsPrime fonksiyonuna göre ekrana yazdırma yapıyoruz
                {
                    Console.Write(temp.val + " ");
                }
                temp = temp.next;
            }
        }
        
        public static bool IsPrime(int cont)
        {
            if(cont <=1) return false;

            for(int i = 2; i < Math.Sqrt(cont); i++) // Girilen sayının kareköküne kadar giderek asallığı kontrol ediyoruz.
            {                                        
                if(cont % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsSortedRec(Node node)
        {
            if(node == null || node.next == null)   // Stack sonunda burası yine çalışır ve sıralı olduğunu anlarız
            {
                return true;
            }
            if(node.val > node.next.val)        // listede o anki eleman, bir sonraki elemandan daha büyükse liste sıralı değil
            {
                return false;
            }
            return IsSortedRec(node.next);      // listede ilerlemek için next ile çağırıyoruz.
        }

        public static bool IsSortedIter(Node node)
        {
            if(node == null)
            {
                return true;
            }   
            Node iter1 = node;                      // 2 iteratör ile listeyi baştan sona giderken kontrol yapmak için
            Node iter2 = node.next;
            while(iter2!= null)
            {
                if(iter1.val > iter2.val)
                {
                    return false;
                }
                iter1 = iter2;
                iter2 = iter2.next;
            }
            return true;
        }

        public static void PrintHarshad(Node node)
        {
            Node temp = node;
            int control = 0;
            while(temp != null)
            {
                int sum = SumofDigits(temp.val);
                if (temp.val % sum ==0)             // Basamaklar toplamına kalansız bölünüyorsa ekrana yazdır
                {
                    control = 1;
                    Console.Write(temp.val+" ");
                }
                temp = temp.next;
            }
            if(control == 0)
            {
                Console.Write("Boş");
            }
        }

        public static int SumofDigits(int num) // Burada basamaklar toplamını bulmak için
        {
            int sum = 0;
            while (num > 0)
            {
                sum += num % 10;
                num /= 10;
            }
            return sum;
        }

        //public static void Main()
        //{
        //    LinkedListe list = new LinkedListe();
            
        //    Console.Write("Kaç elemenlı bir liste istiyorsunuz:");
        //    int count = Convert.ToInt32(Console.ReadLine());

        //    for(int i = 1; i <= count; i++)
        //    {
        //        Console.Write(i+". elemanı girin:");
        //        list.AddLast(Convert.ToInt32(Console.ReadLine()));
        //    }

        //    Console.Write("\nListenin sıralı olup olmadığını rekürsif kontrol etmek için 1, iteratif kontrol etmek için 2 yazın:");
        //    int secim=Convert.ToInt32(Console.ReadLine());
        //    switch(secim)
        //    {
        //        case 1: 
        //            if (IsSortedRec(list.head))
        //            {
        //                Console.WriteLine("\nListe sıralı !");
        //                Console.WriteLine("Listedeki asallar;");
        //                PrintPrimes(list.head);
        //                Console.WriteLine();
        //            }
        //            else
        //            {
        //                Console.WriteLine("\nSıralı değil");
        //                Console.Write("Listedeki harshad sayılar: ");
        //                PrintHarshad(list.head);
        //                Console.WriteLine(); 
        //            }
        //            break;
        //        case 2: 
        //            if (IsSortedIter(list.head))
        //            {
        //                Console.WriteLine("\nListe sıralı !");
        //                Console.WriteLine("Listedeki asallar;");
        //                PrintPrimes(list.head);
        //            }
        //            else
        //            {
        //                Console.WriteLine("\nSıralı değil");
        //                Console.Write("Listedeki harshad sayılar: ");
        //                PrintHarshad(list.head);
        //                Console.WriteLine();
        //            }
        //            break;
        //        default:
        //            Console.WriteLine("Geçerli bir seçim yapmadınız.");
        //            break;
        //    }
        //}

    }
}
