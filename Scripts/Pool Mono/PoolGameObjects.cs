using System;
using UnityEngine;

public class PoolGameObjects : MonoBehaviour
{
    [SerializeField] private PoolObject prefab;
    [Space]
    [SerializeField] private int count;
    [SerializeField] private bool isExpand;
    
    private PoolMono<PoolObject> _pool;

    private void Start()
    {
        _pool = new PoolMono<PoolObject>(prefab, count, transform);
    }

    public void CreateObject()
    {
        _pool.GetFreeElement();
    }
}