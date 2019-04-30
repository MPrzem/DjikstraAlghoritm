using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weighted_graph;

namespace WeightedGraphMatrix
{
    public class WeightedGraphMatrix : Igraph
    {

        public int[][] incidencesMatrix { get; private set; }
        const char Separator = '\t';
        public int NumberOfVertices { get; private set; }
        public int NumberOfBranches { get; private set; }
        public int StartingVertice { get; private set; }
        public WeightedGraphMatrix(string file_Name)
        {
            try
            {
                FileStream gs = new FileStream(file_Name, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(gs);
                string tmp;
                if (!sr.EndOfStream)
                {
                    decodeFirstLine(sr.ReadLine());
                    setGraph();
                }
                while (!sr.EndOfStream)
                {
                    decodeNormalLine(tmp = sr.ReadLine());
                }
                sr.Close();
            }
            catch (FormatException)
            {
                Console.WriteLine("Zły format pliku, to nie liczba");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void decodeFirstLine(string line)
        {
            string[] tmp_strings;
            tmp_strings = line.Split(Separator);
            if (tmp_strings.GetLength(0) != 3)
                throw (new IOException("zły format pliku"));
            NumberOfBranches = Int32.Parse(tmp_strings[0]);
            NumberOfVertices = Int32.Parse(tmp_strings[1]);
            StartingVertice = Int32.Parse(tmp_strings[2]);
        }
        private void decodeNormalLine(string line)
        {
            string[] tmp_strings;
            tmp_strings = line.Split(Separator);
            if (tmp_strings.GetLength(0) != 3)
                throw (new IOException("zły format pliku"));
            int startingVertice = Int32.Parse(tmp_strings[0]);
            int endingVertice = Int32.Parse(tmp_strings[1]);
            int branchWeight = Int32.Parse(tmp_strings[2]);
            incidencesMatrix[startingVertice][endingVertice] = branchWeight;
        }
        private void setGraph()
        {
            /// Tylko tyle?
            incidencesMatrix = new int[NumberOfVertices][];
            for (int i = 0; i < incidencesMatrix.Length; i++)
            {
                incidencesMatrix[i] = new int[NumberOfVertices];
                for (int j = 0; j < incidencesMatrix[i].Length; j++)
                {
                    incidencesMatrix[i][j] = int.MaxValue;

                }
            }
        }
        /// <summary>
        /// Zwraca wektor galezi dla danego wierzcholka
        /// </summary>
        /// <param name="which_vertice"></param>
        /// <returns></returns>
        public Branch[] GiveArrayOfBranches(int which_vertice)
        {
            if (which_vertice >= NumberOfVertices)
                throw new ArgumentOutOfRangeException("Wrong index value for GiveFirstOf method");
            Branch[] tmp = new Branch[NumberOfVertices];
            for (int i = 0; i < incidencesMatrix[which_vertice].Length; i++)
            {
                tmp[i] = new Branch(i, incidencesMatrix[which_vertice][i]);
            }
            return tmp;
        }
    }
}
