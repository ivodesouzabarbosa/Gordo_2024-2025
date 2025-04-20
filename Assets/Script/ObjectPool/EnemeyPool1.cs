using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyPool1 : ObjectPool
{
    public static EnemeyPool1 _enemeyPool1;

    public override void Awake()
    {

        base.Awake();
        _enemeyPool1 = this;

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
            EnemyBaseControl._listEnemy1.Add(pooledObjects[i]);
        }
    }
}
