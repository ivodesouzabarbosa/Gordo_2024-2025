using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyPool3 : ObjectPool
{
    public static EnemeyPool3 _enemeyPool3;

    public override void Awake()
    {
        base.Awake();
        _enemeyPool3 = this;

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
            EnemyBaseControl._listEnemy3.Add(pooledObjects[i]);
        }
    }
}
