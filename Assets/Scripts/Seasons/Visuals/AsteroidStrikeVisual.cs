using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidStrikeVisual : HazySeasonVisualEffect
{
    [SerializeField] private GameObject asteroidPrefab;

    public override void Init(float duration)
    {
        base.Init(duration);

        GameObject asteroid = Instantiate(asteroidPrefab);
        asteroid.transform.position = new Vector3(0, 8, 10);
        asteroid.GetComponent<Meteorite>().SetTarget(new Vector3(0, 0, 10));
    }
}
