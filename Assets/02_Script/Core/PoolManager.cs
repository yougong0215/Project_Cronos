using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    public static PoolManager Instance;

    private GameObject _Item;

    private Dictionary<string, Pool<PoolAble>> _pools = new Dictionary<string, Pool<PoolAble>>();

    private Transform _trmParent;

    public PoolManager(Transform trmParent)
    {
        _trmParent = trmParent;
    }
    public void CreatePool(PoolAble prefab, int cnt = 5)
    {
        Pool<PoolAble> pool = new Pool<PoolAble>(prefab, _trmParent, cnt);
        _pools.Add(prefab.gameObject.name, pool);
    }

    public PoolAble Pop(string prefabName)
    {
        if (_pools.ContainsKey(prefabName) == false)
        {
            Debug.LogError("ÇÁ¸®Æé¾øµ¥");
            return null;
        }


        PoolAble item = _pools[prefabName].Pop();
        item.Reset();
        return item;
    }


    public void Push(PoolAble obj)
    {
        _pools[obj.name].Push(obj);
    }
}
