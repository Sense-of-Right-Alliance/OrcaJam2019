using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazySeasonVisualEffect : SeasonVisualEffect
{
    float hazeAlpha = 0f;
    SpriteRenderer spriteRenderer;
    float i = 0f;

    int state = -1;

    float maxAlpha = 0.3f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color c = spriteRenderer.color;
        c.a = 0;
        spriteRenderer.color = c;
    }

    public override void Init(float duration)
    {
        base.Init(duration);

        state++;
    }

    protected override void UpdateVisuals()
    {
        Color c = spriteRenderer.color;

        i += Time.deltaTime;
        float percent = i / (_duration / 2);
        float t = Easing.Quintic.InOut(percent);

        if (state == 0)
        {
            hazeAlpha = Mathf.Lerp(0f, maxAlpha, t);
            if (percent > 1f)
            {
                state++;
                i = 0;
            }
        }
        else if (state == 1)
        {
            hazeAlpha = Mathf.Lerp(maxAlpha, 0f, t);
            if (percent > 1f)
            {
                state++;
            }
        }

        //Debug.Log("percent " + percent + " t = " + t + " ; alpha = " + hazeAlpha);

        c.a = hazeAlpha;

        spriteRenderer.color = c;
    }
}
