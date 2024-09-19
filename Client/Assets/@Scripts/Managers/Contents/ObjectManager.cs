using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    public MyHero MyHero { get; set; }
    Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();

    #region Roots

    public Transform GetRootTransform(string name)
    {
        GameObject root = GameObject.Find(name);
        if (root == null)
            root = new GameObject { name = name };
        return root.transform;
    }

    public Transform HeroRoot { get { return GetRootTransform("@Heroes"); } }
    public Transform MonsterRoot { get { return GetRootTransform("@Monsters"); } }

    #endregion


    public ObjectManager()
    {
    }

    public MyHero Spawn(MyHeroInfo myHeroInfo)
    {
        HeroInfo info = myHeroInfo.HeroInfo;
        if (info == null || info.CreatureInfo == null || info.CreatureInfo.ObjectInfo == null)
            return null;
        ObjectInfo objectInfo = info.CreatureInfo.ObjectInfo;
        if (MyHero != null && MyHero.ObjectId == objectInfo.ObjectId)
            return null;
        if (_objects.ContainsKey(objectInfo.ObjectId))
            return null;
        EGameObjectType objectType = Utils.GetObjectTypeFromId(objectInfo.ObjectId);
        if (objectType != EGameObjectType.Hero)
            return null;

        GameObject go = Managers.Resource.Instantiate("Hero"); // TEMP		
        go.name = info.Name;
        go.transform.parent = HeroRoot;
        _objects.Add(objectInfo.ObjectId, go);

        MyHero = Utils.GetOrAddComponent<MyHero>(go);
        MyHero.ObjectId = objectInfo.ObjectId;
        MyHero.PosInfo = objectInfo.PosInfo;

        MyHero.SyncWorldPosWithCellPos();

        return MyHero;
    }

    public Hero Spawn(HeroInfo info)
    {
        if (info == null || info.CreatureInfo == null || info.CreatureInfo.ObjectInfo == null)
            return null;
        ObjectInfo objectInfo = info.CreatureInfo.ObjectInfo;
        if (MyHero.ObjectId == objectInfo.ObjectId)
            return null;
        if (_objects.ContainsKey(objectInfo.ObjectId))
            return null;
        EGameObjectType objectType = Utils.GetObjectTypeFromId(objectInfo.ObjectId);
        if (objectType != EGameObjectType.Hero)
            return null;

        GameObject go = Managers.Resource.Instantiate("Hero"); // TEMP
        go.name = info.Name;
        go.transform.parent = HeroRoot;
        _objects.Add(objectInfo.ObjectId, go);

        Hero hero = Utils.GetOrAddComponent<Hero>(go);
        hero.ObjectId = objectInfo.ObjectId;
        Debug.Log("������Ŷ pos : " + objectInfo.PosInfo);
        hero.PosInfo = objectInfo.PosInfo;
        hero.SetInfo(1);

        hero.SyncWorldPosWithCellPos();

        return hero;
    }

    public void Despawn(int objectId)
    {
        if (MyHero != null && MyHero.ObjectId == objectId)
            return;
        if (_objects.ContainsKey(objectId) == false)
            return;

        GameObject go = FindById(objectId);
        if (go == null)
            return;

        BaseObject bo = go.GetComponent<BaseObject>();
        if (bo != null)
        {

        }

        _objects.Remove(objectId);
        Managers.Resource.Destroy(go);
    }

    public GameObject FindById(int id)
    {
        GameObject go = null;
        _objects.TryGetValue(id, out go);
        return go;
    }

    public GameObject FindCreature(Vector3Int cellPos)
    {
        foreach (GameObject obj in _objects.Values)
        {
            Creature creature = obj.GetComponent<Creature>();
            if (creature == null)
                continue;

            //if (creature.CellPos == cellPos)
            //	return obj;
        }

        return null;
    }

    public GameObject Find(Vector3Int cellPos)
    {
        foreach (GameObject obj in _objects.Values)
        {
            BaseObject bo = obj.GetComponent<BaseObject>();
            if (bo == null)
                continue;

            if (bo.CellPos == cellPos)
                return obj;
        }

        return null;
    }

    public GameObject Find(Func<GameObject, bool> condition)
    {
        foreach (GameObject obj in _objects.Values)
        {
            if (condition.Invoke(obj))
                return obj;
        }

        return null;
    }

    public List<T> FindAllComponents<T>(Func<T, bool> condition) where T : UnityEngine.Component
    {
        List<T> ret = new List<T>();

        foreach (GameObject obj in _objects.Values)
        {
            T t = Utils.FindChild<T>(obj, recursive: true);
            if (t != null && condition.Invoke(t))
                ret.Add(t);
        }

        return ret;
    }

    public void Clear()
    {
        foreach (GameObject obj in _objects.Values)
            Managers.Resource.Destroy(obj);

        _objects.Clear();
        MyHero = null;
    }
}