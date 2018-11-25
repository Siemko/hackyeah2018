using System.Collections.Generic;

namespace Orlen.Services.RouteService.Models
{
    public class Graph
    {
        private List<Node> _intersections { get; set; }

        public Graph(List<Node> intersections, int rootId)
        {
            _intersections = intersections;
            Root = _intersections[rootId];
        }

        public Node Root { get; }

        public void InitializeNeighbors()
        {
            foreach (var intersection in _intersections)
            {
                intersection.Neighbors = new List<Neighbor>();
                foreach (var neighbor in intersection.DistanceDict)
                {
                    var newNeightbor = new Neighbor();
                    foreach (var intersectionNode in _intersections)
                    {
                        if (intersectionNode.Id == neighbor.Key)
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
                foreach (var neighbor in node.Neighbors)
                {
                    TransverNode(neighbor.Node);
                    var neighborName = neighbor.Node.Id;
                    int neighborDistance = neighbor.Distance;

                    foreach (var key in neighbor.Node.DistanceDict.Keys)
                    {
                        if (key != node.Id)
                        {
                            int neighborKeyDistance = neighbor.Node.DistanceDict[key].Count;
                            if (node.DistanceDict.ContainsKey(key))
                            {
                                int currentDistance = node.DistanceDict[key].Count;
                                if (neighborKeyDistance + neighborDistance > currentDistance)
                                {
                                    var nodeList = new List<int>();
                                    nodeList.AddRange(neighbor.Node.DistanceDict[key].ToArray());
                                    nodeList.Insert(0, neighbor.Node.Id);
                                    node.DistanceDict[key] = nodeList;
                                }
                            }
                            else
                            {
                                var nodeList = new List<int>();
                                nodeList.AddRange(neighbor.Node.DistanceDict[key].ToArray());
                                nodeList.Insert(0, neighbor.Node.Id);
                                node.DistanceDict.Add(key, nodeList);
                            }
                        }
                    }
                }
            }
        }
    }
}
