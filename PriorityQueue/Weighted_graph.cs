using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using two_way_list;

namespace weighted_graph
{
    public class Weighted_graph
    {
                                 
        public My_list<Branch>[] Incidences_lists { get; private set; }
        const char Separator = '\t';
        public int NumberOfVertices { get;private set; }
        public int NumberOfBranches { get;private set; }
        public int StartingVertice { get;private set; }


        /// <summary>
        /// There is also inicialization
        /// </summary>
        /// <param name="file_Name"></param>
        public void download_Graph(string file_Name)
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
            int branchWeight= Int32.Parse(tmp_strings[2]);
            Branch tmpBranch = new Branch(endingVertice, branchWeight);
            Incidences_lists[startingVertice].InsertLast(tmpBranch);
        }
        private void setGraph() {
        Incidences_lists = new My_list<Branch>[NumberOfVertices];
            // Inicjalization of every list
            for (int i = 0; i < Incidences_lists.Length; i++)
            {
                Incidences_lists[i] = new My_list<Branch>();

            }
        }
    }
    public class Branch : IComparable<Branch>
    {
        public int vertice { get; set; }
        public int weight { get; set; }
        public Branch(int vertice, int weight)
        {
            this.vertice = vertice;
            this.weight = weight;
        }
        /// <summary>
        /// It's needed for priority queue. It's setting priority
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Branch other)
        {
            if (other == null)
                return -1;
            if (this.weight < other.weight)
                return -1;
            if (this.weight.Equals(other.weight))
                return 0;
            else
                return 1;
        }
    }
}
