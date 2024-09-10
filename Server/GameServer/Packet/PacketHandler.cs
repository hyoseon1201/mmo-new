using GameServer;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PacketHandler
{
    public static void C_MoveHandler(PacketSession session, IMessage packet)
    {
        
    }

    internal static void C_EnterGameHandler(PacketSession session, IMessage message)
    {
        throw new NotImplementedException();
    }
}

