using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weighted_graph;
using PriorityQueue;


    public class DjikstraAlgoritm1
    {
    public readonly int infinity = int.MaxValue;
    Weighted_graph procesedGraph; ////TUtaj dodać brak edycji
    /// <summary>
    /// Branch in this context means vertice with cost of way form start to him
    /// </summary>
    public PriorityQueue<Branch> Q { get; private set; }
    /// <summary>
    /// There branch.vertice means father of this vertice and weight means total weight
    /// </summary>
    public Branch[] vertices { get; private set; }// a moze tutaj referencje? Jednak nie, edycja kluczy poza kolejka to slaby pomysl. wierzcholek poprzedni czym wypelnić? Null?
    public DjikstraAlgoritm1(Weighted_graph downloadedGraph)///Napisać rano do tego test jednostkowy
    {
        procesedGraph = downloadedGraph;
        Q = new PriorityQueue<Branch>();
        vertices = new Branch[downloadedGraph.NumberOfVertices];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Branch(infinity,infinity);///infinity dla wierzchołka tylko tymczasowo,znaczy to ze poprzedni nie istnieje. Pozniej nullable zrob
        
        }

            vertices[procesedGraph.StartingVertice].Weight = 0; /// Do wagi dopisz wyjatek jesli jest ujemna
        Q.Enqueue(new Branch(procesedGraph.StartingVertice,0));//Dodajemy pierwszy wierzcholek z 0 waga
    }
    /// <summary>
    /// It will return weight of the shortest way, and vertice before target, this allows to see whole way.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public Branch FindTheShortestWayTo(int target)
    {
        Branch processedVertice;
        Branch[] SonsOfProcesedVertice;
        while (Q.Count() > 0)
        {
            processedVertice = Q.Dequeue();
            // Real weights from original graph
            SonsOfProcesedVertice= procesedGraph.Incidences_lists[processedVertice.vertice].ToArray();
            for (int i = 0; i < SonsOfProcesedVertice.Length; i++)
            {
                if(SonsOfProcesedVertice[i].Weight+processedVertice.Weight<vertices[SonsOfProcesedVertice[i].vertice].Weight)
                {
                    vertices[SonsOfProcesedVertice[i].vertice].Weight = SonsOfProcesedVertice[i].Weight + processedVertice.Weight;
                    vertices[SonsOfProcesedVertice[i].vertice].vertice = processedVertice.vertice;
                    Q.Enqueue(new Branch(SonsOfProcesedVertice[i].vertice,vertices[SonsOfProcesedVertice[i].vertice].Weight));

                }

            }

        }
        return vertices[target];
    }
    }

