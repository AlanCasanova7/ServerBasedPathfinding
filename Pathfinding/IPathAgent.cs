using System.Collections.Generic;

namespace Pathfinding_2._0
{
    public interface IPathAgent
    {
        Vector3 PathFindingStart { get; set; }
        Vector3 PathFindingEnd { get; set; }
        List<Node> Path { get; }
        void ReceivePath(List<Node> path);
    }
}