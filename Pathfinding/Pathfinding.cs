using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding_2._0
{
    public static class Pathfinding
    {
        private static Map                  map;
        private static Queue<IPathAgent>    waitingForPath;
        private static int maxDispatchedAgent;

        public static void Init(int MaxDispatchedAgent = 50)
        {
            waitingForPath = new Queue<IPathAgent>();
            map = new Map("Assets/Grid.txt");
            maxDispatchedAgent = MaxDispatchedAgent;
        }
        public static void RequestPath(IPathAgent agent, Vector3 start, Vector3 end)
        {
            waitingForPath.Enqueue(agent);
            agent.PathFindingStart = start;
            agent.PathFindingEnd = end;
        }
        //NEEDED IF USING THE QUEUE DISPATCH SYSTEM.
        public static void Update()
        {
            for (int i = 0; i < maxDispatchedAgent; i++)
            {
                if (waitingForPath.Count == 0)
                    break;
                IPathAgent agent = waitingForPath.Dequeue();
                agent.ReceivePath(GetPath(agent.PathFindingStart, agent.PathFindingEnd));
            }
        }
        public static List<Node> GetPath(Vector3 start, Vector3 end)
        {
            List<Node> path = new List<Node>();
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();
            Node startingNode = map.GetBestNodeFromVectors(start, end);
            Node endingNode = map.GetBestNodeFromVectors(end, start);

            startingNode.SetParent(new Node(start, 1));

            openList.Add(startingNode);

            while(openList.Count > 0)
            {
                Node node = GetLowestNode(openList, endingNode);
                closedList.Add(node);
                openList.Remove(node);

                if (node == endingNode) break;

                foreach (Node neighbour in node.Neighbours)
                {
                    if (openList.Contains(neighbour) || closedList.Contains(neighbour)) continue;
                    neighbour.SetParent(node);
                    openList.Add(neighbour);
                }
            }

            path.Add(endingNode);
            while (true)
            {
                Node node = path[path.Count - 1].parent;
                if (node == startingNode)
                    break;
                path.Add(node);
            }

            string toSend = null;
            for (int i = path.Count - 1; i >= 0; i--)
            {
                toSend += path[i].UnityPosition.X + "," + path[i].UnityPosition.Y + "," + path[i].UnityPosition.Z + "\n";
            }
            return path;
        }
        private static Node GetLowestNode(List<Node> nodes, Node end)
        {
            Node lowestNode = null;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (lowestNode == null)
                {
                    lowestNode = nodes[i];
                    continue;
                }
                float newNodeWeight = /*nodes[i].Weight +*/ Heuristic(nodes[i], end);
                float oldNodeWeight = /*lowestNode.Weight +*/ Heuristic(lowestNode, end);
                if (newNodeWeight < oldNodeWeight)
                    lowestNode = nodes[i];
            }
            return lowestNode;
        }
        private static float Heuristic(Node start, Node end)
        {
            return Math.Abs(start.UnityPosition.X - end.UnityPosition.X) + Math.Abs(start.UnityPosition.Z - end.UnityPosition.Z);
        }
    }
}
