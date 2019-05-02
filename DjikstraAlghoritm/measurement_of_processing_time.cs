using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueue
{
    class measurement_of_processing_time
    {
        Graph[] listGraphs;
        int numberOfVertices;
        int numberOfGraphs;
        int numberOfBranches;
        measurement_of_processing_time(int numberOfVertices,int numberOfGraphs,double density, Type type)
        {
            numberOfBranches = Convert.ToInt32(numberOfVertices * (numberOfVertices - 1) * density);
            ///There would be better to use factory pattern. taki patencik
            if (type == typeof(weighted_graph.Weighted_graph))
            {


            }
            if (type == typeof(WeightedGraphMatrix.WeightedGraphMatrix))
            {


            }


        }
    }
}
