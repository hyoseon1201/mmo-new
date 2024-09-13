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

        S_EnterGame enterGamePacket = packet as S_EnterGame;
        MyHero myHero = Managers.Object.Spawn(enterGamePacket.MyHeroInfo);
        //myHero.SetInfo(1); // TEMP
        myHero.State = EObjectState.Idle;
    }

    public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
    {
        S_LeaveGame leaveGamePacket = packet as S_LeaveGame;

        Managers.Object.Despawn(Managers.Object.MyHero.ObjectId);
    }

    public static void S_SpawnHandler(PacketSession session, IMessage packet)
    {
        S_Spawn spawnPacket = packet as S_Spawn;
        foreach (HeroInfo hero in spawnPacket.Heroes)
        {
            Managers.Object.Spawn(hero);
        }
    }

    public static void S_DespawnHandler(PacketSession session, IMessage packet)
    {
        S_Despawn despawnPacket = packet as S_Despawn;
        foreach (int id in despawnPacket.ObjectIds)
        {
            Managers.Object.Despawn(id);
        }
    }

    public static void S_MoveHandler(PacketSession session, IMessage packet)
    {
        S_Move movePacket = packet as S_Move;
        
        GameObject go = Managers.Object.FindById(movePacket.ObjectId);

        if (go == null) return;

        Creature cc = go.GetComponent<Creature>();

        if (cc == null) return;
        cc.PosInfo = movePacket.PosInfo;
    }
}


