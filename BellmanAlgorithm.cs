using System;
using System.IO;
namespace Project
{   //zamień vertices na vertices
    public class BellmanAlgorithm
    {
        public static void FindTheShortestPath(Graph graph, int source, bool printSolution = false)
        {
            int vertices = graph.NumVertices;
            int edges = graph.NumEdges;
            int[] distance = new int [vertices];
            string[] pathToVertice = new string [vertices];
            for (int i = 0; i < vertices; i++) // inicjowanie wszystkich odległości jako max
                distance[i] = int.MaxValue - 10000;
 
                 distance[source] = 0; //źródla na 0
 
            for (int i = 1; i <= vertices - 1; ++i) // Najkrótsza droga z żródła może mieć maksymalnie V-1 krawędzi.
            {
                for (int j = 0; j < vertices; ++j) 
                {
                    for (int k = 0; k < vertices; k++)
                    {
                        /*
                        edge[0] = Source
                        edge[1] = Destination
                        edge[2] = Weight
                        */   
                        int[] edge = graph.Edge(j,k);
                        if(edge == null ) break; //skończyły się krawędzie
    
                        if (distance[edge[0]] != int.MaxValue - 10000 && distance[edge[0]] + edge[2] < distance[edge[1]])
                        {
                            distance[edge[1]] = distance[edge[0]] + edge[2]; 
                             if (printSolution)
                                pathToVertice[edge[1]] = pathToVertice[edge[0]] + edge[0].ToString() + "->";
                            
                        }
                    }
                    
                }
            }

            // Sprawdzenie czy nie wystapił cykl ujemny
            
            // for (int j = 0; j < vertices; ++j) 
            // {
            //     for (int k = 0; k < vertices; k++)
            //     {
            //         /*
            //        edge[0] = Source
            //         edge[1] = Destination
            //         edge[2] = Weight
            //         */ 
            //         int[] edge = graph.Edge(j,k);
            //         if(edge == null ) break; //skończyły się krawędzie
    
            //         if (distance[edge[0]] != int.MaxValue - 10000 && distance[edge[0]] + edge[2] < distance[edge[1]])
            //             Console.WriteLine("Graph contains negative weight cycle.");
            //     }
            // }

            if(printSolution)
                PrintSolution(pathToVertice, distance, vertices, source);
        
        }
        
        private static void PrintSolution(string[] pathToVertice, int[] distance, int numVertices, int startVertice)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    
            // Console.Write("Dokoncz sciezke pliku zawierajacego graf\n {0}",docPath);
            string path = @"\Output.txt";
            // StreamWriter sr = new StreamWriter(Path.Combine(docPath + path));
            using (StreamWriter stream = new StreamWriter(Path.Combine(docPath + path)) ) 
            {
                for (int i = 0; i < numVertices; i++)
                {
                    if(distance[i] != int.MaxValue - 10000)
                        stream.WriteLine("{0}->{1} Cost: {2}, Path:{3}{1}",startVertice,i ,distance[i] , pathToVertice[i]);
                    else if (distance[i] == int.MaxValue - 10000)
                        stream.WriteLine("{0}->{1} Cost: Infinity, Path: Don't exist.", startVertice, i);
                }
            }
            
        }

        public static void FindTheShortestPat(MatrixGraph graph, int source, bool printSolution = false)
        {
            int vertices = graph.NumVertices;
            int edges = graph.NumEdges;
            int[] distance = new int [vertices];
            string[] pathToVertice = new string [vertices];
            for (int i = 0; i < vertices; i++) // inicjowanie wszystkich odległości jako max
                distance[i] = int.MaxValue - 10000;
 
                 distance[source] = 0; //źródla na 0
 
            for (int i = 1; i <= vertices - 1; ++i) // Najkrótsza droga z żródła może mieć maksymalnie V-1 krawędzi.
            {
                for (int j = 0; j < vertices; ++j) 
                {
                    for (int k = 0; k < vertices; k++)
                    {

                        if (distance[j] != int.MaxValue - 10000 && distance[j] + graph.getEdgeWeight(j,k) < distance[k])
                        {
                            distance[k] = distance[j] + graph.getEdgeWeight(j,k); 
                             if (printSolution)
                                pathToVertice[k] = pathToVertice[j] + j.ToString() + "->";
                            
                        }
                    }
                    
                }
            }

            // Sprawdzenie czy nie wystapił cykl ujemny
            
            // for (int j = 0; j < vertices; ++j) 
            // {
            //     for (int k = 0; k < vertices; k++)
            //     {
            //         /*
            //        edge[0] = Source
            //         edge[1] = Destination
            //         edge[2] = Weight
            //         */ 
            //         int[] edge = graph.Edge(j,k);
            //         if(edge == null ) break; //skończyły się krawędzie
    
            //         if (distance[edge[0]] != int.MaxValue - 10000 && distance[edge[0]] + edge[2] < distance[edge[1]])
            //             Console.WriteLine("Graph contains negative weight cycle.");
            //     }
            // }

            if(printSolution)
                PrintSolution(pathToVertice, distance, vertices, source);
        
        }

        public static void FindTheShortestPat(ListGraph graph, int source, bool printSolution = false)
        {
            int vertices = graph.NumVertices;
            int edges = graph.NumEdges;
            int[] distance = new int [vertices];
            string[] pathToVertice = new string [vertices];
            int destination, weight;
            EdgeNode current;
            for (int i = 0; i < vertices; i++) // inicjowanie wszystkich odległości jako max
                distance[i] = int.MaxValue - 10000;
 
                 distance[source] = 0; //źródla na 0
 
            for (int i = 1; i <= vertices - 1; ++i) // Najkrótsza droga z żródła może mieć maksymalnie V-1 krawędzi.
            {
                for (int j = 0; j < vertices; ++j) 
                {
                    current = graph.ListVertices[j].Head;
                    while(current!=null)  
                    { 
                        destination = current.Destination;
                        weight = current.Weight; 
                        if (distance[j] != int.MaxValue - 10000 && distance[j] + weight < distance[destination])
                        {
                            distance[destination] = distance[j] + weight; 
                             if (printSolution)
                                pathToVertice[weight] = pathToVertice[j] + j.ToString() + "->";
                            
                        }
                        current = current.Next;
                    }
                    
                }
            }

            //Sprawdzenie czy nie wystapił cykl ujemny
            
            for (int j = 0; j < vertices; ++j) 
            {
                for (int k = 0; k < vertices; k++)
                {
                    /*
                   edge[0] = Source
                    edge[1] = Destination
                    edge[2] = Weight
                    */ 
                    int[] edge = graph.Edge(j,k);
                    if(edge == null ) break; //skończyły się krawędzie
    
                    if (distance[edge[0]] != int.MaxValue - 10000 && distance[edge[0]] + edge[2] < distance[edge[1]])
                        Console.WriteLine("Graph contains negative weight cycle.");
                }
            }

            if(printSolution)
                PrintSolution(pathToVertice, distance, vertices, source);
        
        }
    }
}