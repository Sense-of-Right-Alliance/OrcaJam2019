using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonTrackerBar : MonoBehaviour
{
    [SerializeField] GameObject Pointer;
    [SerializeField] Transform FallPoint;
    [SerializeField] Transform WinterPoint;
    [SerializeField] Transform SpringPoint;
    [SerializeField] Transform SummerPoint;
    [SerializeField] Transform EndPoint;

    Vector3 pointerStart;
    Vector3 pointerEnd;

    float _duration = 0f;
    float t = 0f;

    bool playing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlaySeason(int s, float duration)
    {
        playing = true;
        _duration = duration;
        t = 0;

        if (s == 0)
        {
            pointerStart = FallPoint.position;
            pointerEnd = WinterPoint.position;
        }
        else if (s == 1)
        {
            pointerStart = WinterPoint.position;
            pointerEnd = SpringPoint.position;
        }
        else if (s == 2)
        {
            pointerStart = SpringPoint.position;
            pointerEnd = SummerPoint.position;
        }
        else if (s == 3)
        {
            pointerStart = SummerPoint.position;
            pointerEnd = EndPoint.position;
        }

        UpdatePointer();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            t += Time.deltaTime;
            UpdatePointer();

            if (t >= _duration)
            {
                playing = false;
            }
        }
        
    }

    void UpdatePointer()
    {
        float progress = t / _duration;
        Vector3 pos = Vector3.Lerp(pointerStart, pointerEnd, progress);
        pos.z = -7;
        Pointer.transform.position = pos;
    }
}
