using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonVisualEffect : MonoBehaviour
{
    private bool _initialized;
    private float _timer = -1f;

    private void Start()
    {
        
    }

    public virtual void Init(float duration)
    {
        _timer = duration;
        _initialized = true;
    }

    private void Update()
    {
        if (_initialized)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
