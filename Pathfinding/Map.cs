using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pathfinding_2._0
{
    public class Map
    {
        private List<Node> trimmedGrid;
        private Vector3 startingGridPosition;
        private float gridSpacing;
        public Map(string gridPath)
        {
            string stringMap = File.ReadAllText(gridPath);
            char[] splitter = "\n".ToCharArray();
            string header = stringMap.Split(splitter)[0];
            startingGridPosition = new Vector3(float.Parse(header.Split(',')[0]), float.Parse(header.Split(',')[1]), float.Parse(header.Split(',')[2]));
            gridSpacing = float.Parse(header.Split(',')[3]);
            int xPoints = stringMap.Split(splitter)[1].Length;
            int yPoints = stringMap.Split(splitter).Length - 1;

            Node[] fullGrid = new Node[xPoints * yPoints];

            for (int i = 0; i < fullGrid.Length; i++)
            {
                int x = i % xPoints;
                int y = i / yPoints;

                Node node = getNodeFromGridString(x, y, xPoints, yPoints, stringMap, splitter);
                if (node != null)
                    fullGrid[i] = node;
            }

            for (int y = 0; y < yPoints; y++)
            {
                for (int x = 0; x < xPoints; x++)
                {
                    int index = y * xPoints + x;
                    if (fullGrid[index] == null)
                        continue;
            /*top*/         CheckNeighbour(fullGrid, index, x, y - 1, xPoints, yPoints);
            /*bottom*/      CheckNeighbour(fullGrid, index, x, y + 1, xPoints, yPoints);
            /*left*/        CheckNeighbour(fullGrid, index, x - 1, y, xPoints, yPoints);
            /*right*/       CheckNeighbour(fullGrid, index, x + 1, y, xPoints, yPoints);
            /*top-left*/    CheckNeighbour(fullGrid, index, x - 1, y - 1, xPoints, yPoints);
            /*top-right*/   CheckNeighbour(fullGrid, index, x + 1, y - 1, xPoints, yPoints);
            /*bottom-left*/ CheckNeighbour(fullGrid, index, x - 1, y + 1, xPoints, yPoints);
            /*bottom-right*/CheckNeighbour(fullGrid, index, x + 1, y + 1, xPoints, yPoints);
                }
            }

            trimmedGrid = new List<Node>();
            for (int i = 0; i < fullGrid.Length; i++)
            {
                if (fullGrid[i] != null)
                {
                    trimmedGrid.Add(fullGrid[i]);
                }
            }
        }
        public Node GetBestNodeFromVectors(Vector3 startingPosition, Vector3 endingPosition)
        {
            Node bestNode = null;
            Node[] closest4Nodes = new Node[4] { null, null, null, null };
            float[] closest4Distances = new float[4] { float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue };
            for (int i = 0; i < trimmedGrid.Count; i++)
            {
                float distanceToStart = Math.Abs(trimmedGrid[i].UnityPosition.X - startingPosition.X) + Math.Abs(trimmedGrid[i].UnityPosition.Z - startingPosition.Z);
                float max = 0;
                int maxIndex = 0;
                for (int j = 0; j < closest4Distances.Length; j++)
                {
                    if (max < closest4Distances[j])
                    {
                        max = closest4Distances[j];
                        maxIndex = j;
                    }
                }
                if (max > distanceToStart)
                {
                    closest4Distances[maxIndex] = distanceToStart;
                    closest4Nodes[maxIndex] = trimmedGrid[i];
                }
            }
            float distanceToEnd = float.MaxValue;
            for (int i = 0; i < closest4Nodes.Length; i++)
            {
                float dist = Math.Abs(closest4Nodes[i].UnityPosition.X - endingPosition.X) + Math.Abs(closest4Nodes[i].UnityPosition.Z - endingPosition.Z);
                if (distanceToEnd > dist)
                {
                    distanceToEnd = dist;
                    bestNode = closest4Nodes[i];
                }
            }
            return bestNode;
        }
        private void CheckNeighbour(Node[] fullGrid, int currentIndex, int x, int y, int maxX, int maxY)
        {
            if (x < 0 || x >= maxX - 1)
            {
                return;
            }
            if (y < 0 || y >= maxY - 1)
            {
                return;
            }
            int index = y * maxX + x;
            Node neighbour = fullGrid[index];
            if (neighbour != null)
            {
                fullGrid[currentIndex].AddNeighbour(neighbour);
            }
        }
        private Node getNodeFromGridString(int x, int y, int maxX, int maxY, string gridAsAString, char[] splitter)
        {
            Node node = null;

            if (x < 0 || x > maxX)
                return node;
            if (y < 0 || y > maxY)
                return node;

            int weight = int.Parse(gridAsAString.Split(splitter)[y + 1].ToCharArray()[x].ToString());

            if (weight < 1)
                return node;

            Vector3 position = new Vector3(startingGridPosition.X + (x * gridSpacing), startingGridPosition.Y, startingGridPosition.Z - (y * gridSpacing));

            node = new Node(position, weight);

            return node;
        }
    }
}
