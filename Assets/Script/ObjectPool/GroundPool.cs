using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPool : ObjectPool
{
    public static GroundPool _groundPool;

  

    public override void Awake()
    {
        base.Awake();
        _groundPool = this;

    }

    public override void Start()
    {
        base.Start();
        SetList(_gameControl._groundControl);
    }

    void SetList(GroundControl _groundControl)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            _groundControl._groundList.Add(pooledObjects[i]);
        }
    }
}
