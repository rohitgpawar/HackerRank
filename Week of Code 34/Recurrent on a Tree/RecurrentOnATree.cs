
/*
 
 PROBLEM STATEMENT : https://www.hackerrank.com/contests/w34/challenges/recurrent-on-tree
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{

    public class Node
    {
        int number;
        public List<Node> Children = new List<Node>();
        public Node(int num)
        {
            number = num;
        }

        
    }

    
    public class RecurrentOnATree
    {
        static int getSumOfAllPaths(Node rootNode, int[] vertexNumber)
        {

            return 0;
        }
        static public void AddEgde(Node n1, Node n2)
        {
            n1.Children.Add(n2);
        }

        static void RecurrentOnATreeMain()
        {
            /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */
            int n = Convert.ToInt32(Console.ReadLine());//No. of vertices
            Node rootNode = null;
            for (int i = 0; i < n - 1; i++)
            {
                string[] vertices = Console.ReadLine().Split(' ');
                int u = Convert.ToInt32(vertices[0]);
                int v = Convert.ToInt32(vertices[1]);
                if(rootNode == null)
                {
                    rootNode = new Node(u);
                }
                AddEgde(new Node(u), new Node(v));
            }
            string[] vertexNum = Console.ReadLine().Split(' ');
            int[] vertexNumber = Array.ConvertAll(vertexNum, Int32.Parse);

            getSumOfAllPaths(rootNode,vertexNumber);
        }
    }
}
