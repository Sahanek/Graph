using System;
namespace Project
{
    //Lista krawedzi posiada swój kierunek oraz wage. Sama lista określa swoje żródło.
    //dalszy opis w ListGraph.cs
      public class EdgeNode
    {
        int  destination, weight;
        EdgeNode next;

        public EdgeNode()
        {   
        }
         public EdgeNode(int destination, int weight)
        {   
            Destination = destination;
            Weight = weight;
            Next = null;
        }

        public EdgeNode Next { get => next; set => next = value; }
        public int Destination { get => destination; set => destination = value; }
        public int Weight { get => weight; set => weight = value; }
    }
    public class ListEdge
    {
        private EdgeNode head;

        public EdgeNode Head { get => head; set => head = value; }

        public void printAllNodes() {
            EdgeNode current = Head;
             while (current != null) 
            {
                Console.WriteLine("{0} {1}",current.Destination, current.Weight);
                current = current.Next;
            }
        }
        public void AddFirst(int destination, int weight)
        {
            EdgeNode toAdd = new EdgeNode(destination, weight);
            toAdd.Next=Head;

            Head = toAdd;
        }

        public void AddLast(int destination, int weight)
        {
            if (Head == null)
            {
                Head = new EdgeNode(destination, weight);
            }
            else
            {
                EdgeNode toAdd = new EdgeNode(destination, weight);
            
                EdgeNode current = Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = toAdd;
            }
        }

    }
}
