using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheTrasure
{
    class Graph
    {
        private Node _rootNode;

        private static List<Node> _nodes;
        public List<Node> Nodes
        {
            get { return _nodes; }
        }

        //Edges, connections or the lines that connect the nodes together will be represented as adjacency Matrix
        //   A B C 
        // A 0 1 0
        // B 1 0 1
        // C 1 1 0
        public static int[,] AdjMatrix;
        
        public Graph(int size)
        {
            //set the capacity to 1000 nodes as stated in the instructions
            if (size > 1000)
            {
                throw new ArgumentException("Maximum size for the graph is 1000.");
            }

            _nodes = new List<Node>(size);
            AdjMatrix = new int[size, size];
        }

        private void SetRootNode(Node n)
        {
            if (_rootNode != null)
            {
                throw new Exception("Root node is already set.");
            }
            _rootNode = n;
        }

        public Node GetRootNode()
        {
            return _rootNode;
        }

        public void AddNode(Node node)
        {
            _nodes.Add(node); 
            
            if (node.IsRoot)
            {
                SetRootNode(node);
            }
        }

        public static Node GetTopNeighbor(Node node)
        {
            //edge for top neghbouring node
            return  _nodes.FirstOrDefault(c => c.Column == node.Column && c.Row == node.Row - 1);
        }

        public static Node GetLeftNeighbor(Node node)
        {
            return _nodes.FirstOrDefault(c => c.Column == node.Column - 1 && c.Row == node.Row);
        }

        //use this to connect two nodes
        //all nodes must first be added to the graph
        //in order to create a matrix which can represent all the nodes
        public static void ConnectNode(Node start, Node end)
	    {
		    int startIndex = _nodes.IndexOf(start);
		    int endIndex = _nodes.IndexOf(end);

		    AdjMatrix[startIndex , endIndex] = 1;
		    AdjMatrix[endIndex , startIndex] = 1;
	    }

        private Node GetUnvisitedChildNode(Node n)
        {

            int index = _nodes.IndexOf(n);
            int i = 0;
            while (i < _nodes.Count)
            {
                if (AdjMatrix[index,i] == 1 && _nodes[i].Visited == false)
                {
                    return _nodes[i];
                }
                i++;
            }
            return null;
        }
        
        
        public SearchResult DepthFirstSearch(List<Node> nodesToFind)
        {
            if (!nodesToFind.Any())
            {
                throw new Exception("Please provide a list of nodes to search for.");
            }

            var stack = new Stack<Node>();

            var res = new SearchResult();

            stack.Push(_rootNode);

            _rootNode.Visited = true;
            res.History.Add(_rootNode.Label);

            while (stack.Count > 0)
            {
                var node = stack.Peek();
                var child = GetUnvisitedChildNode(node);
                if (child != null)
                {
                    child.Visited = true;
                    res.History.Add(child.Label);

                    if (nodesToFind.Contains(child))
                    {
                        res.Node = child;
                        res.Found = true;
                        break;
                    }
                    res.Steps++;
                    stack.Push(child);
                }
                else
                {
                    stack.Pop();
                }
            }
            return res;
        }
    }
}
