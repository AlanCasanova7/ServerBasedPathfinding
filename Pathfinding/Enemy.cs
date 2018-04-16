using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding_2._0
{
    public class Enemy : IPathAgent
    {
        public Vector3 PathFindingStart { get { return pathFindingStart; } set { pathFindingStart = value; } }
        private Vector3 pathFindingStart;
        public Vector3 PathFindingEnd { get { return pathFindingEnd; } set { pathFindingEnd = value; } }
        private Vector3 pathFindingEnd;
        public List<Node> Path { get { return path; } }
        private List<Node> path;

        public Enemy()
        {
            path = new List<Node>();
        }

        public void ReceivePath(List<Node> path)
        {
            this.path = path;
            //Console.WriteLine("received path");
        }

        public void DebugPath()
        {
            Console.WriteLine(path.Count);
        }
    }
}
