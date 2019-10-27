using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonVisualEffect : MonoBehaviour
{
    private bool _initialized;
    private float _timer = -1f;

    protected float _duration = 1f;

    private void Start()
    {
        
    }

    public virtual void Init(float duration)
    {
        _duration = duration;
        _timer = duration;
        _initialized = true;
    }

    private void Update()
    {
        if (_initialized)
        {
            UpdateVisuals();

            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual void UpdateVisuals()
    {

    }
}
