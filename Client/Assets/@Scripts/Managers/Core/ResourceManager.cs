using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public class ResourceManager
{
    // ���� �ε��� ���ҽ�.
    private Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, UnityEngine.Object>();

    #region ���ҽ� �ε�
    public bool CheckResource<T>(string key) where T : Object
    {
        if (_resources.TryGetValue(key, out Object resource))
        {
            return true;
        }

        if (typeof(T) == typeof(Sprite))
        {
            key = key + ".sprite";
            if (_resources.TryGetValue(key, out Object temp))
            {
                return true;
            }
        }

        return false;
    }

    public T Load<T>(string key) where T : Object
    {
        if (_resources.TryGetValue(key, out Object resource))
        {
            return resource as T;
        }
        return null;
    }

    public GameObject Instantiate(string key, Transform parent = null, bool pooling = false)
    {
        GameObject prefab = Load<GameObject>($"{key}");
        if (prefab == null)
        {
            Debug.LogError($"Failed to load prefab : {key}");
            return null;
        }

        if (pooling)
            return Managers.Pool.Pop(prefab);

        GameObject go = Object.Instantiate(prefab, parent);

        go.name = prefab.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        if (Managers.Pool.Push(go))
            return;

        Object.Destroy(go);
    }

    #endregion

    #region ��巹����

    public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object
    {
        //��������Ʈ�� ��� ������ü�� ���̸����� �ε��ϸ� ��������Ʈ�� �ε��� ��
        string loadKey = key;
        if (typeof(T) == typeof(Sprite))
        {
            loadKey = $"{key}[{key}]";
        }

        var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
        asyncOperation.Completed += (op) =>
        {
            // ĳ�� Ȯ��.
            if (_resources.TryGetValue(key, out Object resource))
            {
                callback?.Invoke(op.Result);
                return;
            }

            _resources.Add(key, op.Result);
            callback?.Invoke(op.Result);
        };
    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object
    {
        var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        opHandle.Completed += (op) =>
        {
            int loadCount = 0;

            int totalCount = op.Result.Count;

            foreach (var result in op.Result)
            {
                if (result.ResourceType == typeof(Texture2D) || result.ResourceType == typeof(Sprite))
                {
                    LoadAsync<Sprite>(result.PrimaryKey, (obj) =>
                    {
                        loadCount++;
                        callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                    });
                }
                else
                {
                    LoadAsync<T>(result.PrimaryKey, (obj) =>
                    {
                        loadCount++;
                        callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                    });
                }
            }
        };
    }

    #endregion
}