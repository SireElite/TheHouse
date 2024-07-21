using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    private Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception($"More than one {name} Instance");
        }
        #endregion 
    }

    private void Init(GameObject prefab)
    {
        if(prefab != null && pools.ContainsKey(prefab.name) == false)
        {
            pools[prefab.name] = new Pool(prefab);
        }
    }

    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        Init(prefab);
        return pools[prefab.name].Spawn(position, rotation);
    }

    public void Despawn(GameObject obj)
    {
        if(pools.ContainsKey(obj.name))
        {
            pools[obj.name].Despawn(obj);   
        }
        else
        {
            Destroy(obj);
        }
    }
}
