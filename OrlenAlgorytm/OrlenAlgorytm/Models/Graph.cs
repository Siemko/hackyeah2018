using System;
using System.Collections.Generic;

namespace OrlenAlgorytm.Models
{
    public class Graph
    {
        private List<Node> _intersections = new List<Node>() {

            new Node {Name = "G1", DistanceDict = new Dictionary<string,List<string>>(){
                    { "B1", new List<string>(){"B1"}} },
                Visited = false},

            new Node {Name = "G2", DistanceDict = new Dictionary<string,List<string>>(){
                    { "C1", new List<string>(){"B1"}} },
                Visited = false},

            new Node {Name = "A1", DistanceDict = new Dictionary<string,List<string>>(){
                    { "B1", new List<string>(){"B1"}}, { "A2", new List<string>(){"A2"}} },
                Visited = false},

            new Node {Name = "A2", DistanceDict = new Dictionary<string,List<string>>(){
                    { "A3", new List<string>(){"A3"}} },
                Visited = false},

            new Node {Name = "A3", DistanceDict = new Dictionary<string,List<string>>(),
                Visited = false},

            new Node {Name = "B1", DistanceDict = new Dictionary<string,List<string>>(){
                    { "C1", new List<string>(){"C1"}}, { "A1", new List<string>(){"A1"}}, { "B2", new List<string>(){"B2"}} },
                Visited = false},

            new Node {Name = "B2", DistanceDict = new Dictionary<string,List<string>>(){
                    { "C2", new List<string>(){"C2"}}, { "A2", new List<string>(){"A2"}} },
                Visited = false},

            new Node {Name = "C2", DistanceDict = new Dictionary<string,List<string>>(){
                    { "B2", new List<string>(){"B2"}}, { "C3", new List<string>(){"C3"}} },
                Visited = false},

            new Node {Name = "B3", DistanceDict = new Dictionary<string,List<string>>(){
                    { "A3", new List<string>(){"A3"}}, { "C3", new List<string>(){"C3"}} },
                Visited = false},

            new Node {Name = "C3", DistanceDict = new Dictionary<string,List<string>>(){
                    { "B3", new List<string>(){"B3"}}},
                Visited = false},

            new Node {Name = "C1", DistanceDict = new Dictionary<string,List<string>>(){
                    { "C2", new List<string>(){"C2"}}, { "B1", new List<string>(){"B1"}} },
                Visited = false},

            new Node {Name = "C2", DistanceDict = new Dictionary<string,List<string>>(){
                    { "B2", new List<string>(){"B2"}}, { "C3", new List<string>(){"C3"}} },
                Visited = false},

            new Node {Name = "C3", DistanceDict = new Dictionary<string,List<string>>() {
                    { "B3", new List<string>(){"B3"}} },
                Visited = false},
        };

        public Node Root
        {
            get
            {
                return _intersections[0];
            }
        }

        public void InitializeNeighbors()
        {
            foreach (var intersection in _intersections)
            {
                intersection.Neighbors = new List<Neighbor>();
                foreach (KeyValuePair<string, List<string>> neighbor in intersection.DistanceDict)
                {
                    var newNeightbor = new Neighbor();
                    foreach (var intersectionNode in _intersections)
                    {
                        if (intersectionNode.Name == neighbor.Key)
                        {
                            newNeightbor.Node = intersectionNode;
                            newNeightbor.Distance = neighbor.Value.Count;
                            intersection.Neighbors.Add(newNeightbor);
                            break;
                        }
                    }
                }
            }
        }

        public void TransverNode(Node node)
        {
            if (!node.Visited)
            {
                node.Visited = true;
                foreach (Neighbor neighbor in node.Neighbors)
                {
                    TransverNode(neighbor.Node);
                    string neighborName = neighbor.Node.Name;
                    int neighborDistance = neighbor.Distance;

                    foreach (string key in neighbor.Node.DistanceDict.Keys)
                    {
                        if (key != node.Name)
                        {
                            int neighborKeyDistance = neighbor.Node.DistanceDict[key].Count;
                            if (node.DistanceDict.ContainsKey(key))
                            {
                                int currentDistance = node.DistanceDict[key].Count;
                                if (neighborKeyDistance + neighborDistance < currentDistance)
                                {
                                    List<string> nodeList = new List<string>();
                                    nodeList.AddRange(neighbor.Node.DistanceDict[key].ToArray());
                                    nodeList.Insert(0, neighbor.Node.Name);
                                    node.DistanceDict[key] = nodeList;
                                }
                            }
                            else
                            {
                                List<string> nodeList = new List<string>();
                                nodeList.AddRange(neighbor.Node.DistanceDict[key].ToArray());
                                nodeList.Insert(0, neighbor.Node.Name);
                                node.DistanceDict.Add(key, nodeList);
                            }
                        }
                    }
                }
            }
        }
    }
}
