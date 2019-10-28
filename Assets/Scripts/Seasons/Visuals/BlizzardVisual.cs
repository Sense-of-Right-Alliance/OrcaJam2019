using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlizzardVisual : HazySeasonVisualEffect
{
    [SerializeField] private GameObject blizzardParticleEffectPrefab;
    [SerializeField] private GameObject explosionPrefab;

    GameObject particleEffect;

    List<ScarecrowPart> _parts;

    public void Init(float duration, bool leftToRight, List<ScarecrowPart> parts)
    {
        base.Init(duration);

        _parts = parts;

        StartCoroutine(CreateExplosions());

        particleEffect = Instantiate(blizzardParticleEffectPrefab);

        ParticleSystem p = particleEffect.GetComponent<ParticleSystem>();
        Vector3 r = p.shape.rotation;
        
        if (leftToRight)
        {
            particleEffect.transform.position = new Vector3(-20, 6, -10);
            r.y = -50f;
        }
        else
        {
            particleEffect.transform.position = new Vector3(20, 6, -10);
            r.y = 50f;
        }

        ParticleSystem.ShapeModule shape = p.shape;
        shape.rotation = r;
    }

    protected override void CleanUp()
    {
        if (particleEffect) Destroy(particleEffect);
    }

    private IEnumerator CreateExplosions()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < _parts.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);
            Vector3 pos = new Vector3(_parts[i].transform.position.x, _parts[i].transform.position.y, -5);
            Instantiate(explosionPrefab, pos, Quaternion.identity);
        }
    }
}

