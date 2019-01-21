using System.Collections.Generic;

namespace CsAi
{
    public class Graph<T>
        where T : IGraphNode<T>
    {
        /// <summary>
        /// Creates a new, empty graph.
        /// </summary>
        /// <param name="node"></param>
        public Graph()
        {
            this.Nodes = new List<T>();
            this.Edges = new List<Edge<T>>();
        }

        /// <summary>
        /// Creates a new graph with 1 item on it.
        /// </summary>
        /// <param name="node"></param>
        public Graph(T node)
        {
            this.Nodes = new List<T>();
            this.Edges = new List<Edge<T>>();
            this.Nodes.Add(node);
        }

        public List<T> Nodes { get; }

        public List<Edge<T>> Edges { get; }

        public int GetDistance(Graph<T> otherGraph)
        {
            int min = 10000;
            foreach (var n1 in this.Nodes)
            {
                foreach (var n2 in otherGraph.Nodes)
                {
                    var dist = n1.GetDistance(n2);
                    if (dist < min)
                    {
                        min = dist;
                    }
                }
            }

            return min;
        }

        public override string ToString()
        {
            return string.Join(", ", this.Nodes);
        }
    }

    public interface IGraphNode<T>
    {
        int GetDistance(T other);
    }

    public class Edge<T>
        where T : IGraphNode<T>
    {
        public T Node1; // { get; set; }

        public T Node2; // { get; set; }
    }
}
