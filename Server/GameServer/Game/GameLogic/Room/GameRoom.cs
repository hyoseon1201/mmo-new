using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class GameRoom
    {
        public int RoomId { get; set; }
        object _lock = new object();
        List<Hero> _heroes = new List<Hero>();

        public void EnterGame(Hero newHero)
        {
            if (newHero == null)
                return;

            lock (_lock)
            {
                _heroes.Add(newHero);
                newHero.Room = this;
            }
        }

        public void LeaveGame(int playerId) 
        {
        }
    }
}
