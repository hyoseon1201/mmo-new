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
                // TODO: PosX랑 Y값 랜덤이 아니라 db저장된 값으로 불러와야함
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
                    Vector2Int heroPos = new Vector2Int(hero.PosInfo.PosX, hero.PosInfo.PosY);
                    room.EnterGame(hero, respawn: false, pos: heroPos);
                });
            });
        }
    }
}
