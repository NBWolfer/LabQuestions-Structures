using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAttempts
{
    public class Huffmann
    {

        // Huffman düğümünü temsil eden sınıf
        class HuffmanNode
        {
            public char Symbol { get; set; }
            public int Frequency { get; set; }
            public HuffmanNode Left { get; set; }
            public HuffmanNode Right { get; set; }
        }

        // Karakter frekanslarını hesaplayan ve Huffman ağacını oluşturan sınıf
        class HuffmanTree
        {
            private Dictionary<char, int> frequencyTable;

            public HuffmanTree(string text)
            {
                frequencyTable = new Dictionary<char, int>();

                // Karakter frekanslarını hesapla
                foreach (char c in text)
                {
                    if (frequencyTable.ContainsKey(c))
                        frequencyTable[c]++;
                    else
                        frequencyTable[c] = 1;
                }
            }

            // Huffman ağacını oluştur
            public HuffmanNode BuildTree()
            {
                // Tüm harfleri içeren yaprak düğümlerini oluştur
                List<HuffmanNode> leafNodes = frequencyTable
                    .Select(pair => new HuffmanNode { Symbol = pair.Key, Frequency = pair.Value })
                    .ToList();

                // Huffman ağacını oluştur
                while (leafNodes.Count > 1)
                {
                    // En düşük frekansa sahip iki düğümü birleştir
                    List<HuffmanNode> sortedNodes = leafNodes.OrderBy(node => node.Frequency).ToList();
                    HuffmanNode left = sortedNodes[0];
                    HuffmanNode right = sortedNodes[1];

                    // Yeni düğümü oluştur ve frekansını ayarla
                    HuffmanNode parent = new HuffmanNode
                    {
                        Symbol = '\0', // iç düğüm için sembol yok
                        Frequency = left.Frequency + right.Frequency,
                        Left = left,
                        Right = right
                    };

                    // Birleştirilen düğümleri listeden çıkar ve yeni düğümü ekler
                    leafNodes.Remove(left);
                    leafNodes.Remove(right);
                    leafNodes.Add(parent);
                }

                // Oluşan ağacın kök düğümünü döndür
                return leafNodes.FirstOrDefault();
            }

            // Karakterlere karşılık gelen Huffman kodlarını döndür
            public Dictionary<char, string> GetHuffmanCodes()
            {
                Dictionary<char, string> huffmanCodes = new Dictionary<char, string>();
                HuffmanNode root = BuildTree();
                BuildHuffmanCodes(root, "", huffmanCodes);
                return huffmanCodes;
            }

            // Huffman kodlarını oluştur
            private void BuildHuffmanCodes(HuffmanNode node, string code, Dictionary<char, string> huffmanCodes)
            {
                if (node.Left == null && node.Right == null)
                {
                    // Yaprak düğüm: Karaktere karşılık gelen kodu ekle
                    huffmanCodes[node.Symbol] = code;
                    return;
                }

                // Sol dalı gez
                BuildHuffmanCodes(node.Left, code + "0", huffmanCodes);

                // Sağ dalı gez
                BuildHuffmanCodes(node.Right, code + "1", huffmanCodes);
            }
        }

        class HuffmanCompression
        {
            // Metni Huffman kodlamasıyla sıkıştır
            public static string Compress(string text)
            {
                HuffmanTree tree = new HuffmanTree(text);
                Dictionary<char, string> huffmanCodes = tree.GetHuffmanCodes();
                string compressedText = "";

                // Metindeki her karakteri Huffman koduyla değiştir
                foreach (char c in text)
                {
                    compressedText += huffmanCodes[c];
                }

                return compressedText;
            }

            // Sıkıştırılmış metni Huffman kodlamasını kullanarak aç
            public static string Decompress(string compressedText, HuffmanNode root)
            {
                string decompressedText = "";
                HuffmanNode currentNode = root;

                // Sıkıştırılmış metindeki her biti gez
                foreach (char bit in compressedText)
                {
                    if (bit == '0')
                    {
                        // Sol dalı takip et
                        currentNode = currentNode.Left;
                    }
                    else if (bit == '1')
                    {
                        // Sağ dalı takip et
                        currentNode = currentNode.Right;
                    }

                    if (currentNode.Left == null && currentNode.Right == null)
                    {
                        // Yaprak düğüm: Karakteri bulundu
                        decompressedText += currentNode.Symbol;
                        currentNode = root; // Yeni karakter için kök düğüme geri dön
                    }
                }

                return decompressedText;
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                string originalText = "aabbccddeeff";

                // Metni sıkıştır
                string compressedText = HuffmanCompression.Compress(originalText);
                Console.WriteLine("Sıkıştırılmış metin: " + compressedText);

                // Metni aç
                HuffmanTree tree = new HuffmanTree(originalText);
                HuffmanNode root = tree.BuildTree();
                string decompressedText = HuffmanCompression.Decompress(compressedText, root);
                Console.WriteLine("Açılmış metin: " + decompressedText);
            }
        }
    }
}
