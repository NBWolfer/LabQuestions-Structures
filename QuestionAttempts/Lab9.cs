using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAttempts
{
    public class Lab9
    {
        public class CLinkedlist
        {
            public class CNode
            {
                public string Key { get; }
                public string Value { get; }
                public CNode Next { get; set; }

                public CNode(string key, string value)
                {
                    Key = key;
                    Value = value;
                    Next = null;
                }
            }

            private CNode head;

            public void Add(string key, string value) 
            { 
                CNode newnode = new CNode(key, value);
                if(head == null)
                {
                    head = newnode;
                }
                else
                {
                    CNode temp = head;
                    while(temp.Next != null)
                    { 
                        temp = temp.Next;
                    }
                    temp.Next = newnode;
                }
            }

            public string Find(string key)
            {
                CNode temp = head;
                while(temp != null)
                {
                    if(temp.Key == key)
                    {
                        return temp.Value;
                    }
                    temp = temp.Next;
                }

                return null;
            }
        }

        public class HashTable
        {
            private const int size = 8;
            private CLinkedlist[] buckets;

            public HashTable()
            {
                buckets = new CLinkedlist[size];
            }

            private int GetIndex(string key)
            {
                int hash = HashF(key);
                int index = hash % size;
                return index;
            }

            private int HashF(string key)
            {
                int hash = 0;
                foreach(char c in key)
                {
                    hash += (int)c;
                }
                return hash;
            }

            public void Add(string key, string value)
            {
                int index = GetIndex(key);
                if (buckets[index] == null)
                {
                    buckets[index] = new CLinkedlist();
                }
                CLinkedlist bucket = buckets[index];
                bucket.Add(key, value);
            }

            public string Get(string key)
            {
                int index = GetIndex(key);
                CLinkedlist bucket = buckets[index];
                if(bucket != null)
                {
                    return bucket.Find(key);
                }
                return null;
            }
        }
        public static int[] freq = { 0, 0, 0, 0, 0, 0, 0, 0 };
        public class Huffman
        {
            public class HuffmanNode
            {
                public char Character { get; set; }
                public int Frequency { get; set; }
                public HuffmanNode Left { get; set; }
                public HuffmanNode Right { get; set; }
            }

            private static HuffmanNode root;
            private static HashTable encodingTable;

            public static string Compress(string text)
            {
                int[] frequencyTable = BuildFrequencyTable(text);
                root = BuildHuffmanTree(frequencyTable);
                encodingTable = new HashTable();
                GenerateEncodingTable(root, "");

                StringBuilder compressedText = new StringBuilder();
                foreach (char c in text)
                {
                    compressedText.Append(encodingTable.Get(c.ToString()) == null ? encodingTable.Get(c.ToString()) : encodingTable.Get(c.ToString()).ToCharArray()[0]);
                }

                return compressedText.ToString();
            }

            public static string Decompress(string compressedText, HuffmanNode root)
            {
                StringBuilder decompressedText = new StringBuilder();

                HuffmanNode currentNode = root;
                foreach (char c in compressedText)
                {
                    if (c == '0')
                    {
                        currentNode = currentNode.Left;
                    }
                    else if (c == '1')
                    {
                        currentNode = currentNode.Right;
                    }

                    if (currentNode.Character != '\0')
                    {
                        decompressedText.Append(currentNode.Character);
                        currentNode = root;
                    }
                }

                return decompressedText.ToString();
            }

            private static int[] BuildFrequencyTable(string text)
            {
                return freq;
            }

            private static HuffmanNode BuildHuffmanTree(int[] frequencyTable)
            {
                PriorityQueue<HuffmanNode> queue = new PriorityQueue<HuffmanNode>();
                for (int i = 0; i < frequencyTable.Length; i++)
                {
                    if (frequencyTable[i] > 0)
                    {
                        HuffmanNode node = new HuffmanNode
                        {
                            Character = (char)i,
                            Frequency = frequencyTable[i]
                        };
                        queue.Enqueue(node, node.Frequency);
                    }
                }

                while (queue.Size > 1)
                {
                    HuffmanNode left = queue.Dequeue();
                    HuffmanNode right = queue.Dequeue();

                    HuffmanNode parent = new HuffmanNode
                    {
                        Character = '\0',
                        Frequency = left.Frequency + right.Frequency,
                        Left = left,
                        Right = right
                    };

                    queue.Enqueue(parent, parent.Frequency);
                }

                return queue.Dequeue();
            }

            private static void GenerateEncodingTable(HuffmanNode node, string code)
            {
                if (node == null)
                    return;

                if (node.Character != '\0')
                {
                    encodingTable.Add(node.Character.ToString(), code);
                }

                GenerateEncodingTable(node.Left, code + "0");
                GenerateEncodingTable(node.Right, code + "1");
            }

            public void PrintHuffmannTree()
            {
                PrintHuffmannTreeH(root);
            }

            private void PrintHuffmannTreeH(HuffmanNode node)
            {
                if (node != null)
                {
                    PrintHuffmannTreeH(node.Left);
                    Console.Write(node.Character+":"+node.Frequency);
                    PrintHuffmannTreeH(node.Right);
                }
            }

        }

        public class PriorityQueue<T>
        {
            private SortedDictionary<int, Queue<T>> dictionary;

            public int Size { get; private set; }

            public PriorityQueue()
            {
                dictionary = new SortedDictionary<int, Queue<T>>();
                Size = 0;
            }

            public void Enqueue(T item, int priority)
            {
                if (!dictionary.ContainsKey(priority))
                {
                    dictionary[priority] = new Queue<T>();
                }

                dictionary[priority].Enqueue(item);
                Size++;
            }

            public T Dequeue()
            {
                if (Size == 0)
                    throw new InvalidOperationException("Priority queue is empty.");

                foreach (var queue in dictionary)
                {
                    if (queue.Value.Count > 0)
                    {
                        Size--;
                        return queue.Value.Dequeue();
                    }
                }
                return default(T);
            }
        }


        //public static void Main()
        //{
        //    //char[] chars = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };


        //    //Console.WriteLine("Dosya kaç adet harften oluşacak?");
        //    //int n = Convert.ToInt32(Console.ReadLine());
        //    //Random rnd = new Random();
        //    //StreamWriter writer = new StreamWriter("normal.txt"); // kaynak dosyasının dizinine değil, metin dosyasını debug klasörü altında çıktılar kısmında oluşturuyor
        //    //using (writer)
        //    //{
        //    //    for (int i = 0; i < n; i++)
        //    //    {
        //    //        int index = rnd.Next(chars.Length);
        //    //        freq[index]++;
        //    //        writer.Write(chars[index]);
        //    //    }
        //    //}

        //    //for(int i = 0; i < freq.Length; i++)
        //    //{
        //    //    Console.Write(chars[i]+": "+freq[i]+" ");
        //    //}

        //    //string textt = File.ReadAllText("normal.txt");
        //    //Console.WriteLine("\n"+textt);
        //    //string compressedText = Huffman.Compress(textt);
        //    //Console.WriteLine(compressedText);

        //}
    }
}
