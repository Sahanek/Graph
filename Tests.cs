using System;

namespace Project
{
    class Tests
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //int[] verticesTab = {5}; //liczba wierzchołków do testów
		    //double[] densityTab = {0.5}; // gęstości do testów
		    //int loops = 1;
            int[] verticesTab = {10, 50, 100, 500, 1000}; //liczba wierzchołków do testów
		    double[] densityTab = {0.25, 0.5, 0.75, 1}; // gęstości do testów
		    int loops = 1;		// Ilość powtórzeń dla danej gęstości i liczby wierzchołków.

            Test(verticesTab, densityTab, loops);
            // ListGraph graphList = new ListGraph();
            // int source = graphList.ReadFromFile();
            // graphList.PrintGraph();
            // BellmanAlgorithm.FindTheShortestPath(graphList, source,true);
            // graphList.PrintGraph();


        }

        static void Test(int [] verticesTab, double[] densityTab, int loops, bool printolution = false)
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            long timeList, timeMatrix;
            int start;
            Random rnd = new Random();
            foreach (int vertices in verticesTab)
            {
                timeList = 0; timeMatrix = 0;
                foreach (double density in densityTab)
                {
                    timeList=0;
                    timeMatrix=0;
                    for (int i = 1; i <= loops; i++)
                    {   
                        GC.Collect();//wywołanie GarbageCollector, aby poprzednie wywołania zostały usunięte z pamięci.
                        start = rnd.Next() % vertices;
                       
                        ListGraph graphList = new ListGraph(vertices, density);
                        graphList.FillGraph();
                        watch.Start();
                        BellmanAlgorithm.FindTheShortestPath(graphList, start);
                        watch.Stop();
                        timeList += watch.ElapsedMilliseconds;
                        if(i!=10 && i % 10 == 0) timeList /= 11; //Co dziesieć uśrednia czas aby long nie przekroczył swego maksa
                        if(i % 10 == 0) timeList /= 10; //po pierwszym przebiegu dzieli przez 10, za każdym następnym jest 10 elementów + uśredniony

                        GC.Collect();//wywołanie GarbageCollector, aby poprzednie wywołania zostały usunięte z pamięci.

                        MatrixGraph graphMatrix = new MatrixGraph(vertices, density);
                        graphMatrix.createMatrixFromList(graphList.ListVertices);
                        //graphMatrix.PrintGraph();
                        watch.Start();
                        BellmanAlgorithm.FindTheShortestPat(graphMatrix, start);
                        watch.Stop();
                        timeMatrix += watch.ElapsedMilliseconds;
                        if(i != 10 && i % 10 == 0) timeMatrix /= 11;
                        if(i % 10 == 0) timeMatrix /= 10;
                    }
                    Console.WriteLine("Lista: {0}, {1} czas: {2} ms",vertices , density, timeList);
                    Console.WriteLine("Macierz: {0}, {1} czas: {2} ms",vertices , density, timeMatrix);
                }
            }

        }
    }
}
