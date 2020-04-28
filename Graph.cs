using System;

namespace Project
{
    //graf skierowany spójny z możlwiością krawędzi maksymalnie w jedną stronę
    //bez pętli w wierzchołkach.
    public abstract class Graph 
    {
        protected const int maxW = 40;
        protected const int changeInterval = 0;
        private int numVertices,numEdges;//liczba wierzchołków, krawędzi;
        double density;//gęstość grafu
    
        public Graph(int aVertices, int aEdges, double aDensity)
        {
            NumVertices = aVertices;
            NumEdges = aEdges;
            Density = aDensity;   
        }
        public Graph(){}

        //Gettery,settery
 
     
        public int NumVertices { get => numVertices; set => numVertices = value; }
        public int NumEdges { get => numEdges; set => numEdges = value; }
        public double Density { get => density; set => density = value; }

        public abstract void FillGraph( int maxWeight = maxW );
        public abstract void PrintGraph();
        public abstract int ReadFromFile();


        public abstract int[] Edge(int source, int which);



    }
}
