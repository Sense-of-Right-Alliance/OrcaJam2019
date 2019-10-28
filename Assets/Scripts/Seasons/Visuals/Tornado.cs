using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MovingSprite
{
    int state = -1;

    public override void SetTarget(Vector2 target, float delay=0f)
    {
        base.SetTarget(target, delay);
        /*
        Vector2 dir = _target - _start;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 270;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector3 scale = transform.localScale;
        scale.x = (dir.x < 0) ? -1 : 1;

        transform.localScale = scale;
        */

        state++;

        Vector2 dir = _target - _start;
        
        Vector3 scale = transform.localScale;
        scale.x = (dir.x > 0) ? -1 : 1;
        transform.localScale = scale;

        /*
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (transform.localScale.x < 0) angle -= 180;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        */
    }

    protected override void UpdateMovement()
    {
        if (_delay > 0)
        {
            _delay -= Time.deltaTime;
        }
        else if (_initialized)
        {
            if (_target != null)
            {
                if (state == 1)
                {
                    _progress += Time.deltaTime * _rate;
                }
                else
                {
                    var t = _progress;
                    //if (easing > 0) t = Easing.Cubic.In(_progress);

                    Vector3 newPos = Vector2.Lerp(_start, _target, t);
                    _progress += _rate * Time.deltaTime;

                    newPos.z = -5;

                    transform.position = newPos;
                }

                if (_progress >= 1)
                {
                    if (state == 0)
                    {
                        state++;
                        _progress = 0;
                    }
                    else if (state == 1)
                    {
                        SetTarget(_start);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }

            }
        }
    }
}
