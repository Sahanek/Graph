using System;
using System.IO;

namespace Project
{
    //Macierz sąsiedztwa dziedziczy po Graph zbudowana jest na tablicy dwuwymiarowej,
    //na miejscu x,y , gdzie x to żródło, a y kierunek znajduje się waga danej krawędzi.
    public class MatrixGraph : Graph
    {
        int[][] Matrix;//Macierz sasiedztwa

        public MatrixGraph(int aVertices, double aDensity) : base(aVertices, (int)Math.Floor(aDensity*aVertices*(aVertices-1)/2), aDensity)
        {
            createMatrix(aVertices);
        }
        
        public override void FillGraph(int maxWeight = maxW)
        {
            Random rnd = new Random();
            if(Density == 1) //losowanie wag dla grafu spójnego bez krawedzi na samych siebie
            {
                for (int i = 0; i < NumVertices; i++)
                {
                    for (int x = i + 1; x < NumVertices; x++)
                    {   
                        if(i != x)
                        {
                            int weight = rnd.Next()%maxWeight + changeInterval;
                            while(weight == 0)
                                weight = rnd.Next()%maxWeight + changeInterval;
                        
                            Matrix[i][x] = weight;
                        }
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
                    if(V1 != V2 && Matrix[V1][V2] == int.MaxValue - 10000 && Matrix[V2][V1] != int.MaxValue - 10000)
                    {
                        int weight = rnd.Next()%maxWeight + changeInterval;
                        while(weight == 0)
                            weight = rnd.Next()%maxWeight + changeInterval;
                        Matrix[V1][V2] = weight;
                        --edges;
                    }      
                }
            }

        }
        public override void PrintGraph()
        {
            for (int i = 0; i < NumVertices; i++)
            {
             
                for (int x = 0; x < NumVertices; x++)
                {   
                    Console.Write("{0}  ", Matrix[i][x]);
                }
                Console.WriteLine("");
            }
            
          
        }
        public override int ReadFromFile()
        {
            //5 prob podania właściwego adresu.
            for (int i = 0; i < 5; i++)
            {
                try{
                    string docPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    
                    Console.Write("Dokoncz sciezke pliku zawierajacego graf\n {0}",docPath);
                    string path = Console.ReadLine();
                    using(StreamReader sr = new StreamReader(Path.Combine(docPath + path)))
                    {

                        string line = sr.ReadLine();
                        //dzielenie na pojedyncze liczby w przypadku gdy wystepuje conajmniej 1 spacja
                        string[] splitStart = System.Text.RegularExpressions.Regex.Split( line, @"\s{1,}");
                        //wczytanie pierwszej linii
                        NumEdges = int.Parse(splitStart[0]); 
                        NumVertices = int.Parse(splitStart[1]); 
                        int startNode = int.Parse(splitStart[2]); 
                        
                        createMatrix(NumVertices);//tworzenie macierzy.

                        for(int x = 0; x < NumEdges; x++)
                        {
                            line = sr.ReadLine();
                            string[] split = System.Text.RegularExpressions.Regex.Split( line, @"\s{1,}");
                            Matrix[int.Parse(split[0])][int.Parse(split[1])] = int.Parse(split[2]);
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

        private void createMatrix(int aVertices)
        {
            Matrix = new int[aVertices][]; 
            for (int i = 0; i < aVertices; i++)
            {
                Matrix[i] = new int[aVertices];
                for (int x = 0; x < aVertices; x++)
                {   
                
                    Matrix[i][x]=int.MaxValue - 10000;
                  
                }
            }
        }

        public override int[] Edge(int source, int which)
        {
            if(which >= NumVertices) return null; 
            return new int[] {source, which, Matrix[source][which]};
        }

        public int getEdgeWeight(int V1, int V2)
        {
            return Matrix[V1][V2];
        }
        public void setEdgeWeight(int V1, int V2, int weight)
        {
            Matrix[V1][V2]=weight;
        }

        public void createMatrixFromList(ListEdge[] list)
        {
            EdgeNode current;
            for (int i = 0; i < NumVertices; i++)
            {
                
                current = list[i].Head;
                while(current != null)
                {   
                    Matrix[i][current.Destination]=current.Weight;
                    current = current.Next;
                }
            }
            //PrintGraph();
        }
    }
}