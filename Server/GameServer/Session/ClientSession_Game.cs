using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Protocol;
using ServerCore;

namespace GameServer
{
    public partial class ClientSession : PacketSession
    {
        public void HandleEnterGame(C_EnterGame enterGamePacket)
        {
            Console.WriteLine("HandleEnterGame");

            MyHero = ObjectManager.Instance.Spawn<Hero>(1);
            {
                MyHero.ObjectInfo.PosInfo.State = EObjectState.Idle;
                MyHero.ObjectInfo.PosInfo.MoveDir = EMoveDir.Down;
                MyHero.ObjectInfo.PosInfo.PosX = 0;
                MyHero.ObjectInfo.PosInfo.PosY = 0;
                MyHero.Session = this;
            }

            GameLogic.Instance.Push(() =>
            {
                GameRoom room = GameLogic.Instance.Find(1);

                room?.Push(() =>
                {
                    Hero hero = MyHero;
                    room.EnterGame(hero, respawn: false, pos: null);
                });
            });
        }
    }
}
