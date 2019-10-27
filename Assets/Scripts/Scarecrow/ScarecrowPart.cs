using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScarecrowPart : MonoBehaviour
{
    private const int MaxDurability = 100;

    [SerializeField] private ScarecrowPartType type;

    [SerializeField] int durability = MaxDurability;

    [SerializeField] private Sprite[] partGraphics;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public ScarecrowPartType Type => type;

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
            int index = Mathf.FloorToInt(((float)durability / (float)MaxDurability) * partGraphics.Length);
            //Debug.Log("Scarecrow graphic index = " + index);
            spriteRenderer.sprite = partGraphics[index];
        }
    }
}
