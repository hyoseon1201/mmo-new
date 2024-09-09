using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.Linq;
using static Define;

class PacketHandler
{
    ///////////////////////////////////// GameServer - Client /////////////////////////////////////

    public static void S_EnterGameHandler(PacketSession session, IMessage packet)
    {
        S_EnterGame enterGamePacket = packet as S_EnterGame;
        ServerSession serverSession = session as ServerSession;
    }

    internal static void S_DespawnHandler(PacketSession session, IMessage message)
    {
        throw new NotImplementedException();
    }

    internal static void S_LeaveGameHandler(PacketSession session, IMessage message)
    {
        throw new NotImplementedException();
    }

    internal static void S_MoveHandler(PacketSession session, IMessage message)
    {
        throw new NotImplementedException();
    }

    internal static void S_SpawnHandler(PacketSession session, IMessage message)
    {
        throw new NotImplementedException();
    }
}
