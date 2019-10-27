using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScarecrowPart : MonoBehaviour
{
    [SerializeField] int MaxDurability = 100;
    [SerializeField] int durability = 0;

    [SerializeField] Sprite[] PartGraphics;
    [SerializeField] SpriteRenderer spriteRenderer;

    public ScarecrowPartType type;

    public ScarecrowPartState State => durability > 0 ? ScarecrowPartState.Intact : ScarecrowPartState.Ruined;

    private void Awake()
    {
        //if (spriteRenderer == null) spriteRenderer = transform.parent.parent.GetComponent<SpriteRenderer>();

        durability = MaxDurability;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        /* Debug
        if (Input.GetKeyDown(KeyCode.D))
        {
            Damage(10);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Repair(10);
        }
        */
    }

    public void Damage(int amount)
    {
        durability = Math.Max(0, durability - amount);
        UpdateSprite();
    }

    public void Repair(int amount)
    {
        if (State == ScarecrowPartState.Intact)
        {
            durability = Math.Min(MaxDurability, durability + amount);
            UpdateSprite();
        }
    }

    private void UpdateSprite()
    {
        if (durability <= 0)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            spriteRenderer.enabled = true;
            int index = Mathf.FloorToInt(((float)durability / (float)MaxDurability) * PartGraphics.Length);
            spriteRenderer.sprite = PartGraphics[index];
        }
    }
}
