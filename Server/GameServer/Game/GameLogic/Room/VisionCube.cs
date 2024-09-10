using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class VisionCubeComponent
    {
        public Hero Owner { get; private set; }
        public HashSet<BaseObject> PreviousObjects { get; private set; } = new HashSet<BaseObject>();
        public IJob UpdateJob;

        public VisionCubeComponent(Hero owner)
        {
            Owner = owner;
        }

        public HashSet<BaseObject> GatherObjects()
        {
            if (Owner == null || Owner.Room == null)
                return null;

            HashSet<BaseObject> objects = new HashSet<BaseObject>();

            List<Zone> zones = Owner.Room.GetAdjacentZones(Owner.CellPos);

            Vector2Int cellPos = Owner.CellPos;
            foreach (Zone zone in zones)
            {
                foreach (Hero hero in zone.Heroes)
                {
                    int dx = hero.CellPos.x - cellPos.x;
                    int dy = hero.CellPos.y - cellPos.y;
                    if (Math.Abs(dx) > GameRoom.VisionCells)
                        continue;
                    if (Math.Abs(dy) > GameRoom.VisionCells)
                        continue;
                    objects.Add(hero);
                }
            }

            return objects;
        }

        public void Update()
        {
            if (Owner == null || Owner.Room == null)
                return;

            HashSet<BaseObject> currentObjects = GatherObjects();
        }
    }
}
