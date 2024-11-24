using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyPool6 : ObjectPool
{
    public static EnemeyPool6 _enemeyPool6;

    public override void Awake()
    {
        base.Awake();
        _enemeyPool6 = this;

    }

    public override void Start()
    {
        base.Start();
        SetList(_gameControl._enemyBaseControl);
    }

    void SetList(EnemyBaseControl EnemyBaseControl)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            EnemyBaseControl._listEnemy6.Add(pooledObjects[i]);
        }
    }
}
