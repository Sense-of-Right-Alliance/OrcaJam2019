using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scarecrow : MonoBehaviour
{
    [SerializeField] private ScarecrowState state = ScarecrowState.Alive;

    [SerializeField] private ScarecrowPart head;
    [SerializeField] private ScarecrowPart leftArm;
    [SerializeField] private ScarecrowPart rightArm;
    [SerializeField] private ScarecrowPart peg;

    public Player Player { get; set; }

    private readonly Dictionary<ScarecrowPartType, ScarecrowPart> _parts = new Dictionary<ScarecrowPartType, ScarecrowPart>();

    public bool IsIntact => state != ScarecrowState.Dead;

    public ScarecrowPart[] Parts => new[] { peg, leftArm, rightArm, head };

    private void Start()
    {
        _parts[ScarecrowPartType.Head] = head;
        _parts[ScarecrowPartType.LeftArm] = leftArm;
        _parts[ScarecrowPartType.RightArm] = rightArm;
        _parts[ScarecrowPartType.Peg] = peg;
    }

    private void Update()
    {

    }

    public ScarecrowPart DamageRandomPart(int amount)
    {
        var intactParts = _parts.Where(p => p.Value.State == ScarecrowPartState.Intact);
        var randomPart = _parts.ElementAt(Random.Range(0, intactParts.Count()));
        DamagePart(randomPart.Key, amount);
        return randomPart.Value;
    }

    public void DamageAllParts(int amount)
    {
        var intactParts = _parts.Where(p => p.Value.State == ScarecrowPartState.Intact);
        foreach (var part in intactParts)
        {
            DamagePart(part.Key, amount);
        }
    }

    public void DamagePart(ScarecrowPartType partType, int amount)
    {
        if (_parts.ContainsKey(partType))
        {
            _parts[partType].Damage(amount);
        }

        if (_parts.Values.All(p => p.State == ScarecrowPartState.Ruined))
        {
            state = ScarecrowState.Dead;
        }
    }

    public void RepairPart(ScarecrowPartType partType, int amount)
    {
        if (_parts.ContainsKey(partType))
        {
            _parts[partType].Repair(amount);
        }
    }

    public void SetWet()
    {
        switch (state)
        {
            case ScarecrowState.Alive:
                state = ScarecrowState.Wet;
                break;
            case ScarecrowState.Aflame:
                state = ScarecrowState.Alive;
                break;
        }
    }

    public void RemoveWet()
    {
        if (state == ScarecrowState.Wet)
        {
            state = ScarecrowState.Alive;
        }
    }

    public void SetAflame()
    {
        switch (state)
        {
            case ScarecrowState.Alive:
                state = ScarecrowState.Aflame;
                break;
            case ScarecrowState.Wet:
                state = ScarecrowState.Alive;
                break;
        }
    }

    public void RemoveAflame()
    {
        if (state == ScarecrowState.Aflame)
        {
            state = ScarecrowState.Alive;
        }
    }
}
