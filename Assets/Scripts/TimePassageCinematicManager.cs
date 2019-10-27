using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePassageCinematicManager : MonoBehaviour
{
    [SerializeField] GameObject DayNightBackground;
    [SerializeField] SpriteRenderer HillsDay;
    [SerializeField] SpriteRenderer HillsNight;
    [SerializeField] GameObject Sun;
    [SerializeField] GameObject Moon;

    [SerializeField] float celestialRadius = 3f;
    [SerializeField] float celestialOffset = -1.5f;

    [SerializeField] float RotationsPerSecond = 1.0f;

    bool timePassing = false;
    float timer = 0f;

    float degrees = 1f;

    Vector3 sunStart;
    Vector3 moonStart;

    // Start is called before the first frame update
    void Start()
    {
        sunStart = Sun.transform.position;
        moonStart = Moon.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && !timePassing)
        {
            PassTime();
        }
        

        if (timePassing)
        {
            DayNightBackground.transform.Rotate(Vector3.forward * RotationsPerSecond * 360f * Time.deltaTime);

            // fade hills, night/day
            degrees = (degrees + (RotationsPerSecond * 360f * Time.deltaTime)) % 360f;
            float alpha = degrees / 360.0f;
            UpdateHillAlpha(alpha);
            // rotate celestial bodies
            Vector3 point = new Vector3(0, celestialOffset, 0);
            Vector3 axis = new Vector3(0, 0, 1);
            Sun.transform.RotateAround(point, axis, RotationsPerSecond * 360f * Time.deltaTime);
            Moon.transform.RotateAround(point, axis, RotationsPerSecond * 360f * Time.deltaTime);

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timePassing = false;
                DayNightBackground.transform.rotation = Quaternion.identity;
                UpdateHillAlpha(1f);
                Sun.transform.position = sunStart;
                Moon.transform.position = moonStart;
                Sun.transform.rotation = Quaternion.identity;
                Moon.transform.rotation = Quaternion.identity;
            }
        }
    }

    public void PassTime(float duration=5f)
    {
        timePassing = true;
        timer = duration;
    }

    private void UpdateHillAlpha(float alpha)
    {
        Color c = HillsDay.color;
        c.a = alpha;
        HillsDay.color = c;

        c = HillsNight.color;
        c.a = 1f - alpha;
        HillsNight.color = c;
    }
}
