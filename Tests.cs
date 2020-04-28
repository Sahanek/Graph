using System;

namespace Project
{
    class Tests
    {
        static void Main(string[] args)
        {
            int switch_on;
            do
            {
                Console.WriteLine(@"Menu:");
                Console.WriteLine("1)   Program testowy");
                Console.WriteLine("2)   Wczytaj graf z pliku");
                Console.WriteLine("3)   Stworz wlasny graf");
                Console.WriteLine("4..) Zakończ działanie");
                Console.Write("Podaj liczbe odpowiadajaca dzialaniu ktore chcesz wykonac: ");
                switch_on = int.Parse(Console.ReadLine());
                int v;
                double d;
                switch (switch_on)
                {
                    case 1: 
                        Console.WriteLine("Uruchomiono program testowy:");
                        int[] verticesTab = {10, 50, 100, 500, 1000}; //liczba wierzchołków do testów
                        double[] densityTab = {0.25, 0.5, 0.75, 1}; // gęstości do testów
                        int loops = 100;
                        Test(verticesTab, densityTab, loops);
                        break;
                    case 2:
                        ListGraph graphList = new ListGraph();
                        int source = graphList.ReadFromFile();
                        Console.WriteLine("Menu:");
                        Console.WriteLine("1)   Uruchom algorytm wyszukiwania sciezki");
                        Console.WriteLine("2)   Uruchom algorytm wyszukiwania sciezki i zapisz wynik do pliku");
                        Console.WriteLine("3)   Wypisz graf");
                        Console.WriteLine("4..) Zakończ działanie");
                        Console.Write("Podaj liczbe odpowiadajaca dzialaniu ktore chcesz wykonac: ");
                        switch_on = int.Parse(Console.ReadLine());
                        switch (switch_on)
                        {
                            case 1: 
                                BellmanAlgorithm.FindTheShortestPat(graphList, source);
                                break;
                            case 2:
                                BellmanAlgorithm.FindTheShortestPat(graphList, source, true);
                                break;
                            case 3: 
                                graphList.PrintGraph();
                                break;
                            default: break;
                        }
                        break;
                    case 3:
                        Console.Write("Podaj liczbe wierzchołków: ");
                        v=int.Parse(Console.ReadLine());
                        do
                        {
                            Console.Write("Podaj gestosc grafu z przedzialu (0,1) przecinek jako separator: ");
                            d=double.Parse(Console.ReadLine());
                            Console.WriteLine(d);
                        } while(d < 0 && d > 1);
                        MatrixGraph mat = new MatrixGraph(v, d);
                        Console.WriteLine("Menu:");
                        Console.WriteLine("1)   Uruchom algorytm wyszukiwania sciezki");
                        Console.WriteLine("2)   Uruchom algorytm wyszukiwania sciezki i zapisz wynik do pliku");
                        Console.WriteLine("3)   Wypisz graf");
                        Console.WriteLine("4..) Zakończ działanie");
                        Console.Write("Podaj liczbe odpowiadajaca dzialaniu ktore chcesz wykonac: ");
                        switch_on = int.Parse(Console.ReadLine());
                        switch (switch_on)
                        {
                            case 1: 
                                Console.WriteLine("Menu:");
                                Console.WriteLine("1)   Losuj wierzcholek startowy");
                                Console.WriteLine("2)   Podaj wierzcholek startowy");
                                Console.WriteLine("3..) Zakończ działanie");
                                Console.Write("Podaj liczbe odpowiadajaca dzialaniu ktore chcesz wykonac: ");
                                switch_on = int.Parse(Console.ReadLine());
                                switch (switch_on)
                                {
                                    case 1:
                                        Random rnd = new Random();
                                        source = rnd.Next() % v;
                                        Console.WriteLine("Wierzcholek startowy: {0}", source);
                                        BellmanAlgorithm.FindTheShortestPat(mat, source);
                                        break;
                                    case 2:
                                        do
                                        {  
                                            Console.Write("Wierzcholek startowy:");
                                            source = int.Parse(Console.ReadLine());
                                        }while(source > v && source < 0);
                                        break;
                                    default: switch_on = 4; break;
                                }

                                
                                break;
                            case 2:
                                Random rn = new Random();
                                source = rn.Next() % v;
                                Console.Write("Wierzcholek startowy: {0}", source);
                                BellmanAlgorithm.FindTheShortestPat(mat, source, true);
                                break;
                            case 3: 
                                mat.PrintGraph();
                                break;
                            default: break;
                        }
                        break;
                    default: break;
                
                }
            }while(switch_on<4);
            //Console.WriteLine("Hello World!");
            //int[] verticesTab = {1000}; //liczba wierzchołków do testów
		    //double[] densityTab = {0.75}; // gęstości do testów
		    //int loops = 1;
            //int[] verticesTab = {10, 50, 100, 500, 1000}; //liczba wierzchołków do testów
		    //double[] densityTab = {0.25, 0.5, 0.75, 1}; // gęstości do testów
		    //int loops = 100;		// Ilość powtórzeń dla danej gęstości i liczby wierzchołków.

            //Test(verticesTab, densityTab, loops);
        //     ListGraph graphList = new ListGraph();
        //     int source = graphList.ReadFromFile();
        //     graphList.PrintGraph();
        //     BellmanAlgorithm.FindTheShortestPat(graphList, source,true);
        //     graphList.PrintGraph();
        //     MatrixGraph graphMatrix = new MatrixGraph(graphList.NumVertices,2*graphList.NumEdges / graphList.NumVertices*(graphList.NumVertices-1));
        //     graphMatrix.createMatrixFromList(graphList.ListVertices);
        //    // BellmanAlgorithm.FindTheShortestPat(graphMatrix, source,true);

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
                    {   watch.Reset();
                        start = rnd.Next() % vertices;
                       
                        ListGraph graphList = new ListGraph(vertices, density);
                        graphList.FillGraph();
                        watch.Start();
                        BellmanAlgorithm.FindTheShortestPat(graphList, start);
                        watch.Stop();
                        timeList += watch.ElapsedMilliseconds;
                      //  if(i!=10 && i % 10 == 0) timeList /= 11; //Co dziesieć uśrednia czas aby long nie przekroczył swego maksa
                       // if(i % 10 == 0) timeList /= 10; //po pierwszym przebiegu dzieli przez 10, za każdym następnym jest 10 elementów + uśredniony
                        watch.Reset();

                        MatrixGraph graphMatrix = new MatrixGraph(vertices, density);
                        graphMatrix.createMatrixFromList(graphList.ListVertices);
                        //graphMatrix.PrintGraph();
                        watch.Start();
                        BellmanAlgorithm.FindTheShortestPat(graphMatrix, start);
                        watch.Stop();
                        timeMatrix += watch.ElapsedMilliseconds;
                        //Console.WriteLine(timeMatrix/i);
                    }
                    Console.WriteLine("Lista: {0}, {1} czas: {2} ms",vertices , density, timeList/loops);
                    Console.WriteLine("Macierz: {0}, {1} czas: {2} ms",vertices , density, timeMatrix/loops);
                }
            }

        }
    }
}
