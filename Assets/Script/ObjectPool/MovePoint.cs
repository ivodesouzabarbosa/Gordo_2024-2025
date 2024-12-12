using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : ObjectPool
{
    public static MovePoint _movePoint;

    public override void Awake()
    {
        base.Awake();
        _movePoint = this;

    }

    
}
