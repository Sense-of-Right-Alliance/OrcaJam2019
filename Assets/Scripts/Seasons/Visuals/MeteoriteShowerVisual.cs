using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteShowerVisual : SeasonVisualEffect
{
    [SerializeField] GameObject MeteoritePrefab;

    GameObject[] meteorites;

    public int numMeteorites = 1;
    public ScarecrowPart[] hitParts;

    public override void Init(float duration)
    {
        base.Init(duration);

        meteorites = new GameObject[numMeteorites];

        for (int i = 0; i < numMeteorites; i++)
        {
            meteorites[i] = Instantiate(MeteoritePrefab);
            meteorites[i].transform.position = new Vector3(Random.Range(-5,5),6,10);
            //Vector3 target = new Vector3(0, 0, meteorites[i].transform.position.z);
            meteorites[i].GetComponent<Meteorite>().SetTarget(hitParts[i].transform.position, 0.1f * i);
        }
    }
}
