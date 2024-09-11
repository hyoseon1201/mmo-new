using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class MapComponent
    {
        public int MinX { get; set; }
        public int MaxX { get; set; }
        public int MinY { get; set; }
        public int MaxY { get; set; }

        public int SizeX { get { return MaxX - MinX + 1; } }
        public int SizeY { get { return MaxY - MinY + 1; } }

        bool[,] _mapCollision;
        BaseObject[,] _objCollision;

        public bool CanGo(BaseObject self, Vector2Int cellPos, bool checkObjects = true)
        {
            if (cellPos.x < MinX || cellPos.x > MaxX)
                return false;
            if (cellPos.y < MinY || cellPos.y > MaxY)
                return false;

            int x = cellPos.x - MinX;
            int y = MaxY - cellPos.y;

            if (_mapCollision[x, y])
                return false;

            if (checkObjects)
            {
                if (_objCollision[x, y] != null && _objCollision[x, y] != self)
                    return false;
            }

            return true;
        }

        public BaseObject FindObjectAt(Vector2Int cellPos)
        {
            if (cellPos.x < MinX || cellPos.x > MaxX)
                return null;
            if (cellPos.y < MinY || cellPos.y > MaxY)
                return null;

            return _objCollision[cellPos.x - MinX, MaxY - cellPos.y];
        }

        public bool ApplyLeave(BaseObject obj)
        {
            if (obj.Room == null)
                return false;
            if (obj.Room.Map != this)
                return false;

            PositionInfo posInfo = obj.PosInfo;
            if (posInfo.PosX < MinX || posInfo.PosX > MaxX)
                return false;
            if (posInfo.PosY < MinY || posInfo.PosY > MaxY)
                return false;

            Zone zone = obj.Room.GetZone(obj.CellPos);
            zone.Remove(obj);

            int x = posInfo.PosX - MinX;
            int y = MaxY - posInfo.PosY;
            _objCollision[x, y] = null;

            return true;
        }

        public bool ApplyMove(BaseObject obj, Vector2Int dest, bool checkObjects = true, bool collision = true)
        {
            if (obj == null || obj.Room == null || obj.Room.Map != this)
                return false;

            PositionInfo posInfo = obj.PosInfo;
            if (CanGo(obj, dest, checkObjects) == false)
                return false;

            if (collision)
            {
                int oldX = posInfo.PosX - MinX;
                int oldY = MaxY - posInfo.PosY;
                _objCollision[oldX, oldY] = null;

                int newX = dest.x - MinX;
                int newY = MaxY - dest.y;
                _objCollision[newX, newY] = obj;
            }

            if (obj is Hero hero)
            {
                Zone oldZone = obj.Room.GetZone(obj.CellPos);
                Zone newZone = obj.Room.GetZone(dest);
                if (oldZone != newZone)
                {
                    oldZone.Heroes.Remove(hero);
                    newZone.Heroes.Add(hero);
                }
            }

            posInfo.PosX = dest.x;
            posInfo.PosY = dest.y;

            return true;
        }

        public void LoadMap()
        {
            string mapName = "Map_001Collision"; // TEMP
            string text = File.ReadAllText($"{ConfigManager.Config.dataPath}/MapData/{mapName}.txt");
            StringReader reader = new StringReader(text);

            MinX = int.Parse(reader.ReadLine());
            MaxX = int.Parse(reader.ReadLine());
            MinY = int.Parse(reader.ReadLine());
            MaxY = int.Parse(reader.ReadLine());

            int xCount = MaxX - MinX + 1;
            int yCount = MaxY - MinY + 1;
            _mapCollision = new bool[xCount, yCount];
            _objCollision = new BaseObject[xCount, yCount];

            for (int y = 0; y < yCount; y++)
            {
                string line = reader.ReadLine();
                for (int x = 0; x < xCount; x++)
                {
                    _mapCollision[x, y] = line[x] == Define.MAP_TOOL_WALL;
                }
            }
        }

        #region A* PathFinding

        public struct PQNode : IComparable<PQNode>
        {
            public int F;
            public Vector2Int CellPos;
            public int G;

            public int CompareTo(PQNode other)
            {
                if (F == other.F)
                    return 0;
                return F < other.F ? 1 : -1;
            }
        }

        List<Vector2Int> _delta = new List<Vector2Int>()
        {
            new Vector2Int(0, 1),  // Up
            new Vector2Int(1, 0),  // Right
            new Vector2Int(0, -1), // Down
            new Vector2Int(-1, 0), // Left
        };

        public List<Vector2Int> FindPath(BaseObject self, Vector2Int startCellPos, Vector2Int destCellPos, int maxDepth = 10)
        {
            int maxSize = SizeX * SizeY;
            Dictionary<Vector2Int, int> best = new Dictionary<Vector2Int, int>();
            Dictionary<Vector2Int, Vector2Int> parent = new Dictionary<Vector2Int, Vector2Int>();
            PriorityQueue<PQNode> pq = new PriorityQueue<PQNode>();

            Vector2Int pos = startCellPos;
            Vector2Int dest = destCellPos;

            // 시작점 발견
            pq.Push(new PQNode() { F = 0, G = 0, CellPos = pos });
            parent[pos] = pos;
            best[pos] = 0;

            while (pq.Count > 0)
            {
                PQNode node = pq.Pop();
                pos = node.CellPos;

                if (pos == dest)
                    break;

                if (best[pos] < node.G)
                    continue;

                if (best.Count > maxSize)
                    break;

                foreach (Vector2Int delta in _delta)
                {
                    Vector2Int next = pos + delta;

                    if (CanGo(self, next) == false)
                        continue;

                    int g = node.G + 1;
                    int h = Manhattan(dest, next);
                    int f = g + h;

                    if (best.ContainsKey(next) == false || best[next] > g)
                    {
                        best[next] = g;
                        pq.Push(new PQNode() { F = f, G = g, CellPos = next });
                        parent[next] = pos;
                    }
                }
            }

            return CalcCellPathFromParent(parent, dest);
        }

        int Manhattan(Vector2Int a, Vector2Int b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        List<Vector2Int> CalcCellPathFromParent(Dictionary<Vector2Int, Vector2Int> parent, Vector2Int dest)
        {
            List<Vector2Int> cells = new List<Vector2Int>();

            if (parent.ContainsKey(dest) == false)
                return cells;

            Vector2Int now = dest;

            while (parent[now] != now)
            {
                cells.Add(now);
                now = parent[now];
            }

            cells.Add(now);
            cells.Reverse();

            return cells;
        }

        #endregion
    }
}