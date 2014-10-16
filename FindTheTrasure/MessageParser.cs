using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheTrasure
{
    static class MessageParser
    {
        private static readonly List<char> _chars = new List<char>();

        public static int MessageCount 
        {
            get { return _chars.Count; }
        }

        public static void ProcessMessage(string m)
        {
            var chars = m.Replace(" ", "").ToLower().ToList();

            if (chars.Except(new char[]{'s','o','g','p','e'}).Any())
            {
                throw new Exception("Only these characters are allowed, s p g o e .");
            }
            //add new line for processing purposes
            chars.Add('\n');
            
            _chars.AddRange(chars);
        }

        public static void SearchGraph()
        {
            var graph = new Graph(_chars.Count(c => c != '\n'));
            
            int column = 0, row = 0;
            foreach (var c in _chars)
            {
                Node n;

                if (c == '\n')
                {
                    row++;
                    column = 0;
                }
                else
                {
                    n = new Node(c, column, row, false);

                    if (c == 's')
                    {
                        n.IsRoot = true;
                        graph.AddNode(n);
                    }
                    else
                    {
                        graph.AddNode(n);
                    }

                    ConnectNodes(n);

                    column++;
                }

            }

            FindShortestPath(graph);
            _chars.Clear();
        }

        private static void FindShortestPath(Graph graph)
        {
            var nodesToFind = graph.Nodes.Where(c => c.Label == 'g' || c.Label == 'p').ToList();


            var foundNode = graph.DepthFirstSearch(nodesToFind);

            if (foundNode.Found)
            {
                Console.WriteLine(string.Format("{0}{1}", foundNode.Node.Label, foundNode.Steps));
            }
            else
            {
                Console.WriteLine("Could not find a route to nodes g or p.");
            }
        }

        private static void ConnectNodes(Node node)
        {
            if (node.Row > 0)
            {
                //edge for top neghbouring node
                var topNeighbor = Graph.GetTopNeighbor(node);
                if (topNeighbor != null && topNeighbor.Label != 'o')
                {
                    Graph.ConnectNode(topNeighbor, node);
                }
            }

            //edge for left neghbouring node
            var leftNeighbor = Graph.GetLeftNeighbor(node);
            if (leftNeighbor != null && leftNeighbor.Label != 'o')
            {
                Graph.ConnectNode(leftNeighbor, node);
            }
        }
    }
}
