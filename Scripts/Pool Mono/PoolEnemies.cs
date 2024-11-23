using System;
using Characters.Example;
using UnityEngine;

public class PoolEnemies : MonoBehaviour
{
    [SerializeField] private Enemy prefab;
    [Space]
    [SerializeField] private int count;
    [SerializeField] private bool isExpand;
    
    private PoolMono<Enemy> _pool;

    private void Start()
    {
        _pool = new PoolMono<Enemy>(prefab, count, isExpand, transform);
    }

    public Enemy CreateEnemy(int networkId, int internalId)
    {
        var poolObject = _pool.GetFreeElement();
            poolObject.ConnectionId = networkId;
            poolObject.internalId = internalId;

        return poolObject;
    }
}