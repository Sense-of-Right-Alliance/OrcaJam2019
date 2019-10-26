using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScarecrowPart : MonoBehaviour
{
    public ScarecrowPartType type;

    public int durability = 100;

    public ScarecrowPartState State => durability > 0 ? ScarecrowPartState.Intact : ScarecrowPartState.Ruined;

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    public void Damage(int amount)
    {
        durability = Math.Min(0, durability - amount);
    }

    public void Repair(int amount)
    {
        if (State == ScarecrowPartState.Intact)
        {
            durability = Math.Max(100, durability + amount);
        }
    }
}
