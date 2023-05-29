using System;
using System.Collections.Generic;

public class DijkstraAlgorithm
{
    private int vertexCount;
    private List<Edge>[] adjacencyList;
    private int[] distances;
    private bool[] visited;

    public DijkstraAlgorithm(int count)
    {
        vertexCount = count;
        adjacencyList = new List<Edge>[count];
        distances = new int[count];
        visited = new bool[count];

        for (int i = 0; i < count; i++)
        {
            adjacencyList[i] = new List<Edge>();
            distances[i] = int.MaxValue;
            visited[i] = false;
        }
    }

    public void AddEdge(int source, int destination, int weight)
    {
        adjacencyList[source].Add(new Edge(destination, weight));
        adjacencyList[destination].Add(new Edge(source, weight));
    }

    public void DijkstraShortestPath(int source)
    {
        distances[source] = 0;

        for (int i = 0; i < vertexCount - 1; i++)
        {
            int minDistance = FindMinimumDistance();
            visited[minDistance] = true;

            foreach (Edge edge in adjacencyList[minDistance])
            {
                if (!visited[edge.Destination])
                {
                    int newDistance = distances[minDistance] + edge.Weight;
                    if (newDistance < distances[edge.Destination])
                        distances[edge.Destination] = newDistance;
                }
            }
        }
    }

    private int FindMinimumDistance()
    {
        int min = int.MaxValue;
        int minIndex = 0;

        for (int i = 0; i < vertexCount; i++)
        {
            if (!visited[i] && distances[i] <= min)
            {
                min = distances[i];
                minIndex = i;
            }
        }

        return minIndex;
    }

    public void PrintShortestPaths()
    {
        Console.WriteLine("Dijkstra Algorithm - Shortest Paths from Source");

        for (int i = 0; i < vertexCount; i++)
        {
            Console.WriteLine($"Vertex {i}: Distance = {distances[i]}");
        }
    }
}

public class Edge
{
    public int Destination { get; }
    public int Weight { get; }

    public Edge(int destination, int weight)
    {
        Destination = destination;
        Weight = weight;
    }
}

//public class Program
//{
//    public static void Main(string[] args)
//    {
//        int vertexCount = 6;
//        DijkstraAlgorithm dijkstra = new DijkstraAlgorithm(vertexCount);

//        // Ağırlıklandırılmış grafa kenarları ekleyin
//        dijkstra.AddEdge(0, 1, 4);
//        dijkstra.AddEdge(0, 2, 1);
//        dijkstra.AddEdge(1, 2, 2);
//        dijkstra.AddEdge(1, 3, 5);
//        dijkstra.AddEdge(2, 3, 1);
//        dijkstra.AddEdge(2, 4, 3);
//        dijkstra.AddEdge(3, 4, 2);
//        dijkstra.AddEdge(3, 5, 1);
//        dijkstra.AddEdge(4, 5, 4);

//        int sourceVertex = 0;
//        dijkstra.DijkstraShortestPath(sourceVertex);

//        dijkstra.PrintShortestPaths();
//    }
//}
