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
    public static void S_ChatHandler(PacketSession session, IMessage message)
    {
        S_Chat chatPacket = message as S_Chat;
        ServerSession serverSession = session as ServerSession;

        Debug.Log(chatPacket.Context);
    }

    public static void S_EnterGameHandler(PacketSession session, IMessage message)
    {
        S_EnterGame enterGamePacket = message as S_EnterGame;
        ServerSession serverSession = session as ServerSession;
    }
}
