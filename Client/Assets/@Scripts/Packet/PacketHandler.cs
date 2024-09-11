using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PacketHandler
{
    public static void S_ConnectedHandler(PacketSession session, IMessage packet)
    {
        Debug.Log("S_Connected");
    }

    public static void S_EnterGameHandler(PacketSession session, IMessage packet)
    {
        Debug.Log("S_EnterGameHandler");

        S_EnterGame enterGamePacket = (S_EnterGame)packet;
        MyHero myHero = Managers.Object.Spawn(enterGamePacket.MyHeroInfo);
        myHero.SetInfo(1); // TEMP
        myHero.ObjectState = EObjectState.Idle;
    }

    public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
    {

    }

    public static void S_SpawnHandler(PacketSession session, IMessage packet)
    {

    }

    public static void S_DespawnHandler(PacketSession session, IMessage packet)
    {

    }

    public static void S_MoveHandler(PacketSession session, IMessage packet)
    {

    }
}


