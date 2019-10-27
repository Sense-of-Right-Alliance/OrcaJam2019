using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteShowerVisual : HazySeasonVisualEffect
{
    [SerializeField] private GameObject meteoritePrefab;

    public void Init(float duration, int meteoriteCount, ScarecrowPart[] targets)
    {
        base.Init(duration);

        var meteorites = new GameObject[meteoriteCount];
        float delay = duration / (1 + meteoriteCount);

        float xSpawnOffset = Random.Range(-5, 5);
        for (int i = 0; i < meteoriteCount; i++)
        {
            meteorites[i] = Instantiate(meteoritePrefab);
            meteorites[i].transform.position = new Vector3(xSpawnOffset + Random.Range(-1,1),6,10);
            //Vector3 target = new Vector3(0, 0, meteorites[i].transform.position.z);
            meteorites[i].GetComponent<Meteorite>().SetTarget(targets[i].transform.position, delay * i);
        }
    }
}
