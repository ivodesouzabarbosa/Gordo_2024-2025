using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyPool5 : ObjectPool
{
    public static EnemeyPool5 _enemeyPool5;

    public override void Awake()
    {
        base.Awake();
        _enemeyPool5 = this;

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
            EnemyBaseControl._listEnemy5.Add(pooledObjects[i]);
        }
    }
}
