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
        weighted_graph.Weighted_graph graph1= new weighted_graph.Weighted_graph();
        graph1.download_Graph(@"C:\Users\Student241580\source\repos\PriorityQueue\Graph_test.txt");

        DjikstraAlgoritm1 djikstra = new DjikstraAlgoritm1(graph1);

        }
    }

