using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePassageCinematicManager : MonoBehaviour
{
    [SerializeField] GameObject DayNightBackground;
    [SerializeField] float RotationsPerSecond = 1.0f;

    bool timePassing = false;
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !timePassing)
        {
            PassTime();
        }

        if (timePassing)
        {
            DayNightBackground.transform.Rotate(Vector3.forward * RotationsPerSecond * 360f * Time.deltaTime);

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timePassing = false;
                DayNightBackground.transform.rotation = Quaternion.identity;
            }
        }
    }

    void PassTime(float duration=5f)
    {
        timePassing = true;
        timer = duration;
    }
}
