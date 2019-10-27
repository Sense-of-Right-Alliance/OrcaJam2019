using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    private bool _initialized;

    private Vector2 _target;
    private Vector2 _start;

    private float _progress;
    private float _rate = 1f;

    private float _rotateDir = 1f;

    private float _delay = 0f;

    public void SetTarget(Vector2 target, float delay = 0f)
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
        if (_delay > 0)
        {
            _delay -= Time.deltaTime;
        }
        else if (_initialized)
        {
            //transform.Rotate(Vector3.forward * 1000f * rotateDir * Time.deltaTime);

            Vector3 newPos = Vector2.Lerp(_start, _target, _progress);
            _progress += _rate * Time.deltaTime;

            newPos.z = -5;

            transform.position = newPos;
            if (_progress >= 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
