using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.EScene.GameScene;

        Managers.Map.LoadMap("asdf");

        Screen.SetResolution(640, 480, false);

        //GameObject hero = Managers.Resource.Instantiate("Creature/Hero");
        //hero.name = "Hero";
        //Managers.Object.Add(hero);

        //for (int i = 0; i < 5; i++)
        //{
        //    GameObject monster = Managers.Resource.Instantiate("Creature/Monster");
        //    monster.name = $"Monster_{i + 1}";

        //    Vector3Int pos = new Vector3Int()
        //    {
        //        x = Random.Range(-10, 10),
        //        y = Random.Range(-10, 10)
        //    };

        //    MonsterController mc = monster.GetComponent<MonsterController>();
        //    mc.CellPos = pos;

        //    Managers.Object.Add(monster);
        //}
    }

    public override void Clear()
    {

    }
}
