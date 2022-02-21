using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public interface IPathfindTile
{
    int Index { get; }
    Vector2Int Coordinate { get; }
    bool IsWalkable { get; }
    
}
public interface IPathfindMap
{
    Vector2Int Area { get; }
    float GetHeuristic(IPathfindTile current,IPathfindTile next);
    List<IPathfindTile> GetAround(int radius, IPathfindTile IPathfindTile);
}

public class PathFinder<T> where T : IPathfindTile
{
    public struct PathNode : System.IComparable<PathNode>
    {
        public T Tile;
        public int Index => Tile.Index;
        public Vector2Int Coordinate => Tile.Coordinate;
        public bool IsWalkable => Tile.IsWalkable;
        public float heuristic;
        public float pathCostTotal;
        public PathNode(T Tile, float heuristic, float pathCost)
        {
            this.Tile = Tile;
            this.heuristic = heuristic;
            this.pathCostTotal = pathCost;
        }
        public int CompareTo(PathNode other) => other.heuristic.CompareTo(heuristic);
    }
    protected static List<int> GetPath(IPathfindMap MapSystem,T start, T destination, int range)
    {
        if(start.Index == destination.Index)
        {
            return new List<int>();
        }
        // fast
        int count = MapSystem.Area.x * MapSystem.Area.y;
        var pathPriorityQueue = new PriorityQueue<PathNode>();
        float[] heuristics = Enumerable.Repeat<float>(Mathf.Infinity, count).ToArray<float>();
        bool[] isVisited = new bool[count];
        int[] parents = new int[count];

        pathPriorityQueue.Push(new PathNode(start, 0, 0));
        parents[start.Index] = start.Index;
        heuristics[start.Index] = MapSystem.GetHeuristic(destination,start);
        bool isFindPath = false;
            var node = pathPriorityQueue.Peek();
        while (pathPriorityQueue.Count > 0)
        {
            // 제일 좋은 후보를 찾는다
            node = pathPriorityQueue.Pop();
            Vector2Int popCoordinate = node.Coordinate;
            int nodeIndex = node.Coordinate.ToIndex(MapSystem.Area);
            // 동일한 좌표를 여러 경로로 찾아서, 더 빠른 경로로 인해서 이미 방문(closed)된 경우 스킵
            if (isVisited[nodeIndex])
                continue;

            // 방문한다
            isVisited[nodeIndex] = true;
            // 목적지 도착했으면 바로 종료
            if (node.Coordinate.DistanceHex(destination.Coordinate) <= range)
            {
                isFindPath = true;
                break;
            }
            // 상하좌우 등 이동할 수 있는 좌표인지 확인해서 예약(open)한다
            var aroundTiles = MapSystem.GetAround(1,node.Tile);
            for (int i = 0; i < aroundTiles.Count; i++)
            {
                var nextTile = aroundTiles[i];
                // 막히면 스킵
                if (nextTile.Coordinate != destination.Coordinate &&
                    nextTile.IsWalkable == false)
                    continue;
                // 이미 방문한 곳이면 스킵
                if (isVisited[nextTile.Index])
                    continue;
                // 비용 계산
                float g = node.pathCostTotal + 10;
                //거리 같은 휴리스틱 계산
                float h = MapSystem.GetHeuristic(nextTile,destination);
                // 다른 경로에서 더 빠른 길 이미 찾았으면 스킵
                if (heuristics[nextTile.Index] < g + h)
                    continue;

                // 예약 진행
                heuristics[nextTile.Index] = g + h;
                pathPriorityQueue.Push(new PathNode((T)nextTile, g + h, g));
                parents[nextTile.Index] = popCoordinate.ToIndex(MapSystem.Area);
            }
        }
        if (isFindPath == false)
            throw new System.Exception("No Path");
        var result = new List<int>();

        int indexCheck = node.Index;
        while (parents[indexCheck] != indexCheck)
        {
            result.Add(indexCheck);
            var nextIndex = parents[indexCheck];
            if (nextIndex == indexCheck)
                break;
            indexCheck = nextIndex;
        }
        result.Reverse();
        return result;
    }
}
