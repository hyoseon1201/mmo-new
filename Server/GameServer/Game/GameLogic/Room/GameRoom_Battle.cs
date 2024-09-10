using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Protocol;

namespace GameServer
{
    public partial class GameRoom : JobSerializer
    {
        public void HandleMove(Hero hero, C_Move movePacket)
        {
            if (hero == null)
                return;
            if (hero.State == EObjectState.Dead)
                return;

            PositionInfo movePosInfo = movePacket.PosInfo;
            ObjectInfo info = hero.ObjectInfo;

            // TODO : 거리 검증 등

            if (Map.CanGo(hero, new Vector2Int(movePosInfo.PosX, movePosInfo.PosY)) == false)
                return;

            info.PosInfo.State = movePosInfo.State;
            info.PosInfo.MoveDir = movePosInfo.MoveDir;
            Map.ApplyMove(hero, new Vector2Int(movePosInfo.PosX, movePosInfo.PosY));

            hero.BroadcastMove();
        }
    }
}
