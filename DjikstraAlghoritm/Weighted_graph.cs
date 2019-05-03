using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using two_way_list;

namespace weighted_graph
{
    public class Weighted_graph:Graph
    {
                                 
        public My_list<Branch>[] Incidences_lists { get; private set; }



        /// <summary>
        /// There is also inicialization
        /// </summary>
        /// <param name="file_Name"></param>
        public Weighted_graph(string file_Name)
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

        public Weighted_graph(int numberOfVertices, int numberOfBranches, int startingVertice = 0)
        {
            if (numberOfBranches > numberOfVertices * (numberOfVertices - 1))
                throw new InvalidDataException("Podano zbyt dużą liczbę gałęzi");

            this.NumberOfVertices = numberOfVertices;
            this.NumberOfBranches = numberOfBranches;
            setGraph();
            Random rand = new Random();
            int weight;
            int father_vertice;
            int son_vertice;
            int i = 0;
            while (numberOfBranches > i)
            {
                //// TO TRZEBA PRZEROBIC POD TEN TYP GRAFU
                father_vertice = RandomFather();
                son_vertice = FindEmptySon(father_vertice);
                if (son_vertice != father_vertice)
                {
                    weight = rand.Next(5, 100);
                    Incidences_lists[father_vertice].InsertLast(new Branch(son_vertice, weight));
                    i++;
                }

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
        ////***************************************************************************** TO TRZEBA PRZETESTOWAĆ
        /// <summary>
        /// It gives array of branches for chosed vertice
        /// </summary>
        /// <param name="which_vertice"></param>
        /// <returns></returns>
        public override Branch[] GiveArrayOfBranches(int which_vertice)
        {
            if (which_vertice >= NumberOfVertices)
                throw new ArgumentOutOfRangeException("Wrong index value for GiveFirstOf method");
            return Incidences_lists[which_vertice].ToArray();
        }
        protected override bool isThisSonEmpty(int father_vertice,int son_vertice) {
            Branch[] array = GiveArrayOfBranches(father_vertice);
            for (int i = 0; i <array.Length; i++)
            {
                if (array[i].vertice == son_vertice)
                    return false;
            }
            return true;
        }
        protected override bool wasEverySonDrawn(int starting_vertice)
        {
            if (GiveArrayOfBranches(starting_vertice).Length == NumberOfVertices)
                return true;
            else
                return false;
        }

    }
    public class Branch : IComparable<Branch>
    {
        public int vertice { get; set; }
        private int weight;
        public int Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if(value<0)
                    throw new Exception("a weight has to be positive");
                else
                weight = value;
            }
        }
        public Branch(int vertice, int weight)
        {
            this.vertice = vertice;
            this.Weight = weight;
        }
        public Branch(Branch tmp)
        {
            this.vertice = tmp.vertice;
            this.Weight = tmp.weight;
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
            if (this.Weight < other.Weight)
                return -1;
            if (this.Weight.Equals(other.Weight))
                return 0;
            else
                return 1;
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Branch)) { 

                Branch tmp = (Branch)obj;
                if (tmp.weight == this.weight && tmp.vertice == this.vertice)
                    return true;
                else return false;

            }
            return base.Equals(obj);
        }
        
    }
}
