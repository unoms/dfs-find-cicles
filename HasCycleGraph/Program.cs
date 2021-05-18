using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HasCycleGraph
{
    class Program
    {
        class Node
        {
            public readonly List<Node> IncidentsNodes = new List<Node>();

            public int Number { get; set; }

            public Node(int num)
            {
                Number = num;
            }

            public void Connect(Node another)
            {
                another.IncidentsNodes.Add(this);
                IncidentsNodes.Add(another);
            }
        }

        class Graph
        {
            public readonly List<Node> Nodes = new List<Node>();
        }

        //true - a graph has cycle
        static bool HasCycle(Graph graph)
        {
            HashSet<Node> grey = new HashSet<Node>();//List of visited nodes
            HashSet<Node> black = new HashSet<Node>();
            Stack<Node> stack = new Stack<Node>();
            stack.Push(graph.Nodes.First());
            grey.Add(graph.Nodes.First());

            while(stack.Count !=0)
            {
                var node = stack.Pop();
                foreach(var nextNode in node.IncidentsNodes)
                {
                    if (black.Contains(nextNode)) continue;//We need to skip black nodes
                    if (!grey.Add(nextNode)) return true; //Yes, there's a cycle
                    stack.Push(nextNode);
                }
                //Add to black if we checked all incidents nodes
                black.Add(node);
            }
            
            return false;
        }

        static void Main(string[] args)
        {
            /*
                    0
               2        1

               4        3
             
             */

            var n0 = new Node(0);
            var n1 = new Node(1);
            var n2 = new Node(2);
            var n3 = new Node(3);
            var n4 = new Node(4);

            n0.Connect(n1);
            n0.Connect(n2);
            n1.Connect(n3);
            n2.Connect(n4);

            //To make a cycle
            //n4.Connect(n3);

            Graph graph = new Graph();
            graph.Nodes.Add(n0);
            graph.Nodes.Add(n1);
            graph.Nodes.Add(n2);
            graph.Nodes.Add(n3);
            graph.Nodes.Add(n4);

            Console.WriteLine(HasCycle(graph));
        }
    }
}
