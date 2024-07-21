using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private List<GameObject> _inactiveObjects = new List<GameObject>();
    private GameObject _prefab;

    public Pool(GameObject prefab)
    {
        _prefab = prefab;
    }

    public GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        GameObject obj;
        if(_inactiveObjects.Count == 0)
        {
            obj = PoolManager.Instantiate(_prefab, position, rotation);
            obj.name = _prefab.name;
            obj.transform.SetParent(PoolManager.Instance.transform);
        }
        else
        {
            obj = _inactiveObjects[_inactiveObjects.Count - 1];
            _inactiveObjects.RemoveAt(_inactiveObjects.Count - 1);
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        return obj;
    }

    public void Despawn(GameObject obj)
    {
        obj.SetActive(false);
        _inactiveObjects.Add(obj);
    }
}
