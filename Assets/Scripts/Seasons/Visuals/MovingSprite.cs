using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSprite : MonoBehaviour
{
    [SerializeField] public float speed = 1f;
    [SerializeField] GameObject explosionPrefab;

    protected bool _initialized;

    protected Vector2 _target;
    protected Vector2 _start;

    protected float _progress;
    protected float _rate = 1f;

    protected float _rotateDir = 1f;

    protected float _delay = 0f;

    public int easing = 0;

    public virtual void SetTarget(Vector2 target, float delay = 0f)
    {
        _target = target;
        _start = transform.position;

        _delay = delay;

        _progress = 0;

        _rotateDir = Random.value > 0.5f ? 1f : -1f;

        float distance = Mathf.Abs((target - _start).magnitude);

        _rate = speed / distance;

        _initialized = true;
    }

    private void Start()
    {

    }

    private void Update()
    {
        UpdateMovement();
    }

    protected virtual void UpdateMovement()
    {
        if (_delay > 0)
        {
            _delay -= Time.deltaTime;
        }
        else if (_initialized)
        {
            //transform.Rotate(Vector3.forward * 1000f * rotateDir * Time.deltaTime);

            var t = _progress;
            if (easing > 0) t = Easing.Cubic.InOut(_progress);

            Vector3 newPos = Vector2.Lerp(_start, _target, t);
            _progress += _rate * Time.deltaTime;

           

            newPos.z = -5;

            transform.position = newPos;
            if (_progress >= 1)
            {
                if (explosionPrefab != null) Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}

