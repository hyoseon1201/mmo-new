using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    //public MyHeroController MyHero { get; set; }
    //Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();

    //public void Add(HeroInfo info, bool myHero = false)
    //{
    //    if (myHero)
    //    {
    //        GameObject go = Managers.Resource.Instantiate("Creature/MyHero");
    //        go.name = info.Name;
    //        _objects.Add(info.HeroId, go);

    //        MyHero = go.GetComponent<MyHeroController>();
    //        MyHero.Id = info.HeroId;
    //        MyHero.PosInfo = info.PosInfo;
    //    }
    //    else
    //    {
    //        GameObject go = Managers.Resource.Instantiate("Creature/Hero");
    //        go.name = info.Name;
    //        _objects.Add(info.HeroId, go);

    //        HeroController hc = go.GetComponent<HeroController>();
    //        hc.Id = info.HeroId;
    //        hc.PosInfo = info.PosInfo;
    //    }
    //}

    //public void Add(int id, GameObject go)
    //{
    //    _objects.Add(id, go);
    //}

    //public void Remove(int id)
    //{
    //    _objects.Remove(id);
    //}

    //public void RemoveMyHero()
    //{
    //    if (MyHero == null)
    //        return;

    //    Remove(MyHero.Id);
    //    MyHero = null;
    //}

    //public GameObject FindById(int id)
    //{
    //    GameObject go = null;
    //    _objects.TryGetValue(id, out go);
    //    return go;
    //}

    //public GameObject Find(Vector3Int cellPos)
    //{
    //    foreach (GameObject obj in _objects.Values)
    //    {
    //        CreatureController cc = obj.GetComponent<CreatureController>();
    //        if (cc == null)
    //            continue;

    //        if (cc.CellPos == cellPos)
    //            return obj;
    //    }

    //    return null;
    //}

    //public GameObject Find(Func<GameObject, bool> condition)
    //{
    //    foreach (GameObject obj in _objects.Values)
    //    {
    //        if (condition.Invoke(obj))
    //            return obj;
    //    }

    //    return null;
    //}

    //public void Clear()
    //{
    //    _objects.Clear();
    //}
}
