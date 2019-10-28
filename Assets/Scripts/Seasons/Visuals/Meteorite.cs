using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MovingSprite
{
    public override void SetTarget(Vector2 target, float delay = 0f)
    {
        base.SetTarget(target, delay);

        Vector2 dir = _target - _start;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 270;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

