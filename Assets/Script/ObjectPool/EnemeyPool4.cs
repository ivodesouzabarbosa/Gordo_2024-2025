using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyPool4 : ObjectPool
{
    public static EnemeyPool4 _enemeyPool4;

    public override void Awake()
    {
        base.Awake();
        _enemeyPool4 = this;

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
            EnemyBaseControl._listEnemy4.Add(pooledObjects[i]);
        }
    }
}
