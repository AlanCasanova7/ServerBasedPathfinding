using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding_2._0
{
    public class Node
    {
        public Vector3 UnityPosition;
        //public int Weight;
        public List<Node> Neighbours;
        public Node parent;
        public Node(Vector3 position, int weight)
        {
            UnityPosition = position;
            //Weight = weight;
        }

        public void AddNeighbour(Node toAdd)
        {
            if (Neighbours == null)
                Neighbours = new List<Node>();

            Neighbours.Add(toAdd);
        }

        public void SetParent(Node toSet)
        {
            parent = toSet;
        }
    }
}
