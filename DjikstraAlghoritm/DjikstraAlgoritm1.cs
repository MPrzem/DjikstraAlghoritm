﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weighted_graph;
using PriorityQueue;
using System.IO;

public class DjikstraAlgoritm1
    {
    public readonly int infinity = int.MaxValue;
    int target;
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
    public Branch FindTheShortestWayTo(int trg)
    {
        target = trg;
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
    public void saveToFile(string filePath)
    {
        if (vertices == null)///it means that graph hadn't been procesed
            return;
        FileStream fs = new FileStream(filePath,FileMode.OpenOrCreate, FileAccess.Write);

        try
        {
            StreamWriter file = new StreamWriter(fs);
            for (int i = 0; i < vertices.Length; i++)
            {
                string weightinfo = String.Format("Koszt najkrótszej drogi do {0} z {1} wynosi: ", procesedGraph.StartingVertice, i); ;
                file.Write(weightinfo);
                if (vertices[i].Weight >= infinity)
                    file.WriteLine("Między wierzchołkami nie ma ścieżki");
                else
                {
                    if (vertices[i].vertice == procesedGraph.StartingVertice)
                        file.Write("Wierzchołki są sąsiadami");
                    else
                    {
                        file.WriteLine(vertices[i].Weight);
                        file.Write("Kolejnymi wierzchołkami na tej drodze są: ");

                        int j = i;/// nei wiem czy to zadziałą
                                  ///if son is exist and if they are not a neighbors
                        while (vertices[j].vertice != infinity)
                        {

                            file.Write(vertices[j].vertice.ToString() + " ");
                            j = vertices[j].vertice;
                        }
                    }
                    
                    file.WriteLine();
                }
            }

            file.WriteLine();
            file.WriteLine("Bye!");
            file.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
    }

