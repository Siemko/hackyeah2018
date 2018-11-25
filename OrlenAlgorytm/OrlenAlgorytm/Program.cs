using OrlenAlgorytm.Models;
using System;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph();

            graph.InitializeNeighbors();
            graph.TransverNode(graph.Root);

            Console.WriteLine("Gate : {0}", graph.Root.Name);
            foreach (string key in graph.Root.DistanceDict.Keys.OrderBy(x => x))
            {
                Console.WriteLine(" Path to node {0} is {1}", key, string.Join(",", graph.Root.DistanceDict[key].ToArray()));
            }

            Console.ReadKey();
        }
    }
}