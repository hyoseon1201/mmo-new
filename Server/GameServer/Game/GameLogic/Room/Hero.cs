using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class Hero
    {
        public GameRoom Room { get; internal set; }
        public ClientSession Session { get; set; }
    }
}
