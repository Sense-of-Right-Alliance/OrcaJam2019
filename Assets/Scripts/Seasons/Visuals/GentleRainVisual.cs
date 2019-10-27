using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GentleRainVisual : MonoBehaviour
{
    bool initialized = false;
    float timer = -1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(float duration)
    {
        timer = duration;
        initialized = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (initialized)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
