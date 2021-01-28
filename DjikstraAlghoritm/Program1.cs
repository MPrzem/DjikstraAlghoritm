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
        WeightedGraphMatrix.WeightedGraphMatrix graph = new WeightedGraphMatrix.WeightedGraphMatrix(@"..\..\..\Graph.txt");
        weighted_graph.Weighted_graph graph1 = new weighted_graph.Weighted_graph(@"..\..\..\Graph.txt");
        // WeightedGraphMatrix.WeightedGraphMatrix graf = new WeightedGraphMatrix.WeightedGraphMatrix(100,100*99 );
        DjikstraAlgoritm djikstra = new DjikstraAlgoritm(graph);
        weighted_graph.Branch E = djikstra.FindTheShortestWayTo(4);
        Console.WriteLine(E.vertice);
        Console.WriteLine(E.Weight);
        djikstra.saveToFile(@"..\..\..\paths1.txt");

   Console.ReadKey();
        }
    }

