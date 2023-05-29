using System;

public class Graph
{
    private int V; // Düğüm sayısı
    private LinkedList[] adjList; // Komşuluk listesi

    public Graph(int vertices)
    {
        V = vertices;
        adjList = new LinkedList[V];

        for (int i = 0; i < V; i++)         // Her düğüm kendi komşuluk listesine sahip olmalı
        {
            adjList[i] = new LinkedList();
        }
    }

    // Düğümleri birleştirmek için yardımcı bir fonksiyon
    public void AddEdge(int source, int destination, int weight) // Düğümleri eklerken yönsüz bir grafta olduğu için iki yönde de eklenir
    {                                                            // Ayrıca ağırlıklar da eklenir
        adjList[source].AddLast(new Node(destination, weight));
        adjList[destination].AddLast(new Node(source, weight));
    }

    // Dijkstra algoritması
    public void Dijkstra(int startNode)
    {
        int[] distances = new int[V]; // Başlangıç düğümünden diğer düğümlere olan en kısa mesafeler
        bool[] visited = new bool[V]; // Ziyaret edilen düğümler

        for (int i = 0; i < V; i++)
        {
            distances[i] = int.MaxValue; // Başlangıçta tüm mesafeler sonsuz
            visited[i] = false; // Başta gezilen düğüm olmadığı için hepsi false ayarlanır
        }

        distances[startNode] = 0; // Başlangıç düğümünün mesafesi 0 olarak ayarlanır

        for (int i = 0; i < V - 1; i++)
        {
            int minDistance = FindMinDistance(distances, visited);
            visited[minDistance] = true;                     // Gezilen düğümler true olarak ayarlanır

            Node current = adjList[minDistance].Head;        // İlk komşudan başlanır

            while (current != null)
            {
                int neighbor = current.Vertex;

                if (!visited[neighbor] && distances[minDistance] != int.MaxValue && // Ziyaret edilmemiş ve mesafesi şu anki düğüme sonsuz olmayan bir yol varsa çalışır
                    distances[minDistance] + current.Weight < distances[neighbor])  // Ayrıca direkt gitmek yerine şu anki düğümden gidildiğinde daha kısa bir yol varsa çalışır
                {
                    distances[neighbor] = distances[minDistance] + current.Weight;  // Yeni mesafe ayarlanır
                }

                current = current.Next;
            }

            // Her adımda sonuçları ekranda gösterme
            Console.WriteLine("Adım {0} Sonuçları:", i + 1);
            for (int j = 0; j < V; j++)
            {
                if (distances[j] == int.MaxValue)
                {
                    Console.WriteLine("Düğüm {0}: En Kısa Uzaklık = Sonsuz", j);
                }
                else
                {
                    Console.WriteLine("Düğüm {0}: En Kısa Uzaklık = {1}", j, distances[j]);
                }
            }
            Console.WriteLine("--------------------------");
        }

        // Sonuçları küçükten büyüğe sıralayarak yazdırma
        Console.WriteLine("Sonuçlar:");
        Console.WriteLine("Düğüm\t\tEn Kısa Uzaklık");
        int[,] sortedList = new int[V,2];
        for (int i = 0; i < V; i++)
        {
            sortedList[i,0] = i;
            sortedList[i,1] = distances[i];
            //Console.WriteLine(i + "\t\t" + distances[i]); // düğümlerin sırasına göre basar
        }
        SortList(sortedList);
        for(int i = 0; i < V; i++)
        {
            Console.WriteLine($"{startNode} --> {sortedList[i, 0]}\t\t{sortedList[i,1]}");
        }
    }

    // Küçükten büyüğe sıralama işlemi
    private void SortList(int[,] sortedList)
    {
        for (int i = 0; i < V - 1; i++)
        {
            for (int j = i + 1; j < V; j++)
            {
                if (sortedList[i, 1] > sortedList[j, 1])
                {
                    int tempNode = sortedList[i, 0];
                    int tempDistance = sortedList[i, 1];
                    sortedList[i, 0] = sortedList[j, 0];
                    sortedList[i, 1] = sortedList[j, 1];
                    sortedList[j, 0] = tempNode;
                    sortedList[j, 1] = tempDistance;
                }
            }
        }
    }


    // En kısa mesafeyi bulan yardımcı fonksiyon
    private int FindMinDistance(int[] distances, bool[] visited)
    {
        int min = int.MaxValue;
        int minIndex = -1;

        for (int i = 0; i < V; i++)                             // Komşuluk listesinde gezerken en yakındaki düğümü arıyor
        {
            if (visited[i] == false && distances[i] <= min)
            {
                min = distances[i];
                minIndex = i;
            }
        }

        return minIndex;
    }

    public class Node
    {
        public int Vertex { get; }
        public int Weight { get; }
        public Node Next { get; set; }

        public Node(int vertex, int weight)
        {
            Vertex = vertex;
            Weight = weight;
            Next = null;
        }
    }

    public class LinkedList
    {
        public Node Head { get; set; }

        public void AddLast(Node newNode)
        {
            if (Head == null)
            {
                Head = newNode;
            }
            else
            {
                Node current = Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }
    }
}

public class Program
{
    public static void Main()
    {
        // Örnek bir grafiği oluşturalım
        int numNodes = 9;
        Graph graph = new(numNodes);

        graph.AddEdge(0, 1, 4);
        graph.AddEdge(0, 7, 8);
        graph.AddEdge(1, 7, 11);
        graph.AddEdge(1, 2, 8);
        graph.AddEdge(7, 6, 1);
        graph.AddEdge(7, 8, 7);
        graph.AddEdge(2, 8, 2);
        graph.AddEdge(8, 6, 6);
        graph.AddEdge(2, 3, 7);
        graph.AddEdge(2, 5, 4);
        graph.AddEdge(6, 5, 2);
        graph.AddEdge(3, 5, 14);
        graph.AddEdge(3, 4, 9);
        graph.AddEdge(5, 4, 10);

        int startNode = 0;
        graph.Dijkstra(startNode);
    }
}
