using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using static Define;

public class MapManager
{
    public GameObject Map { get; private set; }
    public string MapName { get; private set; }
    public Grid CellGrid { get; private set; }
    public Dictionary<Vector3Int, BaseObject> _cells = new Dictionary<Vector3Int, BaseObject>();

    public int MinX { get; private set; }
    public int MaxX { get; private set; }
    public int MinY { get; private set; }
    public int MaxY { get; private set; }

    private ECellCollisionType[,] _collision;

    public Vector3Int World2Cell(Vector3 worldPos)
    {
        return CellGrid.WorldToCell(worldPos);
    }

    public Vector3 Cell2World(Vector3Int cellPos)
    {
        return CellGrid.CellToWorld(cellPos);
    }

    public void LoadMap(string mapName)
    {
        DestroyMap();

        GameObject map = Managers.Resource.Instantiate(mapName);
        map.transform.position = Vector3.zero;
        map.name = $"@Map_{mapName}";

        Map = map;
        MapName = mapName;
        CellGrid = map.GetOrAddComponent<Grid>();

        ParseCollisionData(map, mapName);
    }

    public void DestroyMap()
    {
        _cells.Clear();

        if (Map != null)
        {
            Managers.Resource.Destroy(Map);
            Map = null;
        }
    }

    public bool CanGo(BaseObject self, Vector3 worldPos, bool ignoreObjects = false)
    {
        return CanGo(self, World2Cell(worldPos), ignoreObjects);
    }

    public bool CanGo(BaseObject self, Vector3Int cellPos, bool ignoreObjects = false)
    {
        if (cellPos.x < MinX || cellPos.x > MaxX)
            return false;
        if (cellPos.y < MinY || cellPos.y > MaxY)
            return false;

        if (ignoreObjects == false)
        {
            BaseObject obj = GetObject(cellPos);
            if (obj != null && obj != self)
                return false;
        }

        int x = cellPos.x - MinX;
        int y = MaxY - cellPos.y;
        ECellCollisionType type = _collision[x, y];

        return type == ECellCollisionType.None;
    }

    void ParseCollisionData(GameObject map, string mapName, string tilemap = "Tilemap_Collision")
    {
        GameObject collision = Utils.FindChild(map, tilemap, true);
        if (collision != null)
            collision.SetActive(false);

        TextAsset txt = Managers.Resource.Load<TextAsset>($"{mapName}Collision");
        StringReader reader = new StringReader(txt.text);

        MinX = int.Parse(reader.ReadLine());
        MaxX = int.Parse(reader.ReadLine());
        MinY = int.Parse(reader.ReadLine());
        MaxY = int.Parse(reader.ReadLine());

        int xCount = MaxX - MinX + 1;
        int yCount = MaxY - MinY + 1;
        _collision = new ECellCollisionType[xCount, yCount];

        for (int y = 0; y < yCount; y++)
        {
            string line = reader.ReadLine();
            for (int x = 0; x < xCount; x++)
            {
                switch (line[x])
                {
                    case MAP_TOOL_WALL:
                        _collision[x, y] = ECellCollisionType.Wall;
                        break;
                    case MAP_TOOL_NONE:
                        _collision[x, y] = ECellCollisionType.None;
                        break;
                    default:
                        _collision[x, y] = ECellCollisionType.None;
                        break;
                }
            }
        }
    }

    public bool MoveTo(BaseObject obj, Vector3Int cellPos, bool forceMove = false)
    {
        if (CanGo(obj, cellPos) == false)
            return false;

        RemoveObject(obj);
        AddObject(obj, cellPos);
        obj.SetCellPos(cellPos, forceMove);

        return true;
    }

    public BaseObject GetObject(Vector3Int cellPos)
    {
        _cells.TryGetValue(cellPos, out BaseObject value);
        return value;
    }

    public BaseObject GetObject(Vector3 worldPos)
    {
        Vector3Int cellPos = World2Cell(worldPos);
        return GetObject(cellPos);
    }

    public void RemoveObject(BaseObject obj)
    {
        Vector3Int cellPos = obj.CellPos;
        BaseObject prev = GetObject(cellPos);

        if (prev == obj)
            _cells[cellPos] = null;
    }

    void AddObject(BaseObject obj, Vector3Int cellPos)
    {
        BaseObject prev = GetObject(cellPos);
        if (prev != null && prev != obj)
            Debug.LogWarning($"AddObject ¼ö»óÇÔ");

        _cells[cellPos] = obj;
    }

    public void ClearObjects()
    {
        _cells.Clear();
    }

    public struct PQNode : IComparable<PQNode>
    {
        public int H;
        public Vector3Int CellPos;
        public int Depth;

        public int CompareTo(PQNode other)
        {
            if (H == other.H)
                return 0;
            return H < other.H ? 1 : -1;
        }
    }

    List<Vector3Int> _delta = new List<Vector3Int>()
    {
        new Vector3Int(0, 1, 0), // Up
        new Vector3Int(1, 0, 0), // Right
        new Vector3Int(0, -1, 0), // Down
        new Vector3Int(-1, 0, 0), // Left
    };

    public List<Vector3Int> FindPath(BaseObject self, Vector3Int startCellPos, Vector3Int destCellPos, int maxDepth = 10)
    {
        Dictionary<Vector3Int, int> best = new Dictionary<Vector3Int, int>();
        Dictionary<Vector3Int, Vector3Int> parent = new Dictionary<Vector3Int, Vector3Int>();
        PriorityQueue<PQNode> pq = new PriorityQueue<PQNode>();

        Vector3Int pos = startCellPos;
        Vector3Int dest = destCellPos;

        Vector3Int closestCellPos = startCellPos;
        int closestH = ManhattanDistance(pos, dest);

        {
            int h = ManhattanDistance(pos, dest);
            pq.Push(new PQNode() { H = h, CellPos = pos, Depth = 1 });
            parent[pos] = pos;
            best[pos] = h;
        }

        while (pq.Count > 0)
        {
            PQNode node = pq.Pop();
            pos = node.CellPos;

            if (pos == dest)
                break;

            if (node.Depth >= maxDepth)
                break;

            foreach (Vector3Int delta in _delta)
            {
                Vector3Int next = pos + delta;

                if (CanGo(self, next) == false)
                    continue;

                int h = ManhattanDistance(next, dest);

                if (best.ContainsKey(next) == false)
                    best[next] = int.MaxValue;

                if (best[next] <= h)
                    continue;

                best[next] = h;

                pq.Push(new PQNode() { H = h, CellPos = next, Depth = node.Depth + 1 });
                parent[next] = pos;

                if (closestH > h)
                {
                    closestH = h;
                    closestCellPos = next;
                }
            }
        }

        if (parent.ContainsKey(dest) == false)
            return CalcCellPathFromParent(parent, closestCellPos);

        return CalcCellPathFromParent(parent, dest);
    }

    private int ManhattanDistance(Vector3Int a, Vector3Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    List<Vector3Int> CalcCellPathFromParent(Dictionary<Vector3Int, Vector3Int> parent, Vector3Int dest)
    {
        List<Vector3Int> cells = new List<Vector3Int>();

        if (parent.ContainsKey(dest) == false)
            return cells;

        Vector3Int now = dest;

        while (parent[now] != now)
        {
            cells.Add(now);
            now = parent[now];
        }

        cells.Add(now);
        cells.Reverse();

        return cells;
    }
}