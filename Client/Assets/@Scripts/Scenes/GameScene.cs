using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Awake()
    {
        base.Awake();

        SceneType = Define.EScene.GameScene;

        Managers.Map.LoadMap("Map_001");

        C_EnterGame enterGame = new C_EnterGame();
        Managers.Network.Send(enterGame);

        
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Clear()
    {

    }

    void OnApplicationQuit()
    {
        Managers.Network.GameServer.Disconnect();
    }
}
