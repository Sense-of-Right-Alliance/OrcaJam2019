using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStormVisual : HazySeasonVisualEffect
{
    [SerializeField] private GameObject lightninPrefab;

    public void Init(float duration, int count, ScarecrowPart[] targets)
    {
        base.Init(duration);

        var meteorites = new GameObject[count];
        float delay = duration / (1 + count);
        
        for (int i = 0; i < count; i++)
        {
            meteorites[i] = Instantiate(lightninPrefab);
            meteorites[i].transform.position = new Vector3(targets[i].transform.position.x, 6, 5);
            //Vector3 target = new Vector3(0, 0, meteorites[i].transform.position.z);
            meteorites[i].GetComponent<LightningBolt>().SetTarget(targets[i].transform.position, delay * i);
        }
    }
}
