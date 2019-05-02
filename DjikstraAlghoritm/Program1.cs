using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using two_way_list;


    class Program
    {
        static void Main(string[] args)
        {
        /*      WeightedGraphMatrix.WeightedGraphMatrix graf = new WeightedGraphMatrix.WeightedGraphMatrix(@"C:\Users\Student241580\source\repos\PriorityQueue\Graph_test.txt");

              DjikstraAlgoritm1 djikstra = new DjikstraAlgoritm1(graf);
              weighted_graph.Branch E =djikstra.FindTheShortestWayTo(4);
              Console.WriteLine(E.vertice);
              Console.WriteLine(E.Weight);
              djikstra.saveToFile(@"C:\Users\Student241580\source\repos\PriorityQueue\paths1.txt");*/
        WeightedGraphMatrix.WeightedGraphMatrix graf = new WeightedGraphMatrix.WeightedGraphMatrix(3, 6);

        Console.ReadKey();
        }
    }

