using System;
using System.IO;

namespace Project
{
    //ListGraph dziedziczy po Graphie, zawiera tablice wierzchołków,
    //której indeksy określają również żródło krawędzi zawartych w liście.
    public class ListGraph : Graph
    {
        ListEdge[] vertices; //Wierzcholki z czym sasiaduja

        public ListEdge[] ListVertices { get => vertices; }

        public ListGraph() : base() {}
        public ListGraph(int aVertices, double aDensity) : base(aVertices, (int)Math.Floor(aDensity*aVertices*(aVertices-1)/2), aDensity)
        {
            CreateList(aVertices);
        }

   
        private void CreateList(int aVertices)
        {
            vertices= new ListEdge [aVertices]; // liczba krawedzi dla określonej gęstości
            for (int i = 0; i < aVertices; i++)
            {
                vertices[i] = new ListEdge();
            }
        }
       
        

        public override void FillGraph(int maxWeight = maxW)
        {
            Random rnd = new Random();
            //losowanie wag z danego przedziału dla grafu spójnego bez krawedzi na samych siebie
            if(Density == 1) 
            {
                for (int i = 0; i < NumVertices; i++)
                {
                    for (int x = i + 1; x < NumVertices; x++)
                    {
                        int weight = rnd.Next() % maxWeight + changeInterval;
                        while(weight == 0)
                            weight = rnd.Next() % maxWeight + changeInterval;
                        vertices[i].AddLast(x, weight);
                        
                    }
                }
            }
            else
            {   
                //dla gestości różnej od 1 algorytm losuje 2 wierzchołki a nastepnie sprawdza
                //czy istnieje krawedz między nimi, jeśli tak to losuje ponownie.
                //kiedy krawędź nieistnieje losuje wage różną od 0 z określonego przedziału.
                int edges = NumEdges;
                while(edges > 0)
                {
                    int V1 = rnd.Next() % NumVertices;//losowanie wierzchołków pomiędzy
                    int V2 = rnd.Next() % NumVertices;//którymi będzie krawędź
                    // If(existEdge(V1,V2))
                    if(V1 != V2 && !IfExist(V1,V2) && !IfExist(V2,V1) ) 
                    {
                        int weight = rnd.Next() % maxWeight + changeInterval;
                        while(weight == 0)
                            weight = rnd.Next() % maxWeight + changeInterval;
                        vertices[V1].AddLast(V2, weight);
                        --edges;
                    }      
                }
            }
        }
        public override void PrintGraph()
        {
            for (int i = 0; i < NumVertices; i++)
            {
                Console.WriteLine("{0} : ",i);
                vertices[i].printAllNodes();
            }
        }
        public override int ReadFromFile()
        {
             //5 prob podania właściwego adresu.
            for (int i = 0; i < 5; i++)
            {
                try{
                    string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    
                    Console.Write("Dokoncz sciezke pliku zawierajacego graf\n {0}\\",docPath);
                    string path = Console.ReadLine();

                    using( StreamReader sr = new StreamReader(Path.Combine(docPath + path)))
                    {

                        string line = sr.ReadLine();
                        //dzielenie na pojedyncze liczby w przypadku gdy wystepuje conajmniej 1 spacja
                        string[] splitFirst = System.Text.RegularExpressions.Regex.Split( line, @"\s{1,}");
                        //wczytanie pierwszej linii
                        
                        NumEdges = int.Parse(splitFirst[0]); 
                        NumVertices = int.Parse(splitFirst[1]); 
                        int startNode = int.Parse(splitFirst[2]); 
                        CreateList(NumVertices);

                        for(int x = 0; x < NumEdges; x++)
                        {
                            line = sr.ReadLine();
                            string[] split = System.Text.RegularExpressions.Regex.Split( line, @"\s{1,}");
                            vertices[int.Parse(split[0])].AddLast(int.Parse(split[1]), int.Parse(split[2]));
                            
                        }
                        return startNode;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return 1;
        }

        public override int[] Edge(int source, int which)
        {   
            int index = 0;
            EdgeNode current = vertices[source].Head;
            while(current != null)
            {   
                if(index == which) return new int[] {source, current.Destination, current.Weight};
                current = current.Next;
                ++index;
            }
            return null;   
        }

        bool IfExist(int V1, int V2)
        {
            try
            {
                EdgeNode current = vertices[V1].Head;
                while(current != null)
                {
                    if(current.Destination == V2) return true;
                    current = current.Next;
                }
                return false;
            }
            catch(Exception)
            {
                return false;
            }
        }

        


    }
}
