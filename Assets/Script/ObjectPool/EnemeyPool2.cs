using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyPool2 : ObjectPool
{
    public static EnemeyPool2 _enemeyPool2;

    public override void Awake()
    {
        base.Awake();
        _enemeyPool2 = this;

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
            EnemyBaseControl._listEnemy2.Add(pooledObjects[i]);
        }
    }
}
