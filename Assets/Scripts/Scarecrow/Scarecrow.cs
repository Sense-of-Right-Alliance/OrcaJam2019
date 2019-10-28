using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scarecrow : MonoBehaviour
{
    [SerializeField] private ScarecrowState state = ScarecrowState.Alive;
    [SerializeField] private GameObject fireObject;

    [SerializeField] private ScarecrowPart head;
    [SerializeField] private ScarecrowPart leftArm;
    [SerializeField] private ScarecrowPart rightArm;
    [SerializeField] private ScarecrowPart peg;

    public Player Player { get; set; }

    private readonly Dictionary<ScarecrowPartType, ScarecrowPart> _parts = new Dictionary<ScarecrowPartType, ScarecrowPart>();

    public bool IsIntact => state != ScarecrowState.Dead;
    public bool IsAflame => state == ScarecrowState.Aflame;

    public ScarecrowPart[] Parts => new[] { peg, leftArm, rightArm, head };

    private void Start()
    {
        _parts[ScarecrowPartType.Head] = head;
        _parts[ScarecrowPartType.LeftArm] = leftArm;
        _parts[ScarecrowPartType.RightArm] = rightArm;
        _parts[ScarecrowPartType.Peg] = peg;

        fireObject.SetActive(false);
    }

    private void Update()
    {

    }

    public ScarecrowPart GetRandomPart()
    {
        var intactParts = _parts.Where(p => p.Value.State == ScarecrowPartState.Intact);
        if (intactParts.Any())
        {
            return intactParts.ElementAt(Random.Range(0, intactParts.Count())).Value;
        }
        else
        {
            return peg;
        }
    }

    public ScarecrowPart DamageRandomPart(int amount)
    {
        var intactParts = _parts.Where(p => p.Value.State == ScarecrowPartState.Intact);
        if (intactParts.Any())
        {
            var randomPart = intactParts.ElementAt(Random.Range(0, intactParts.Count()));
            DamagePart(randomPart.Key, amount);
            return randomPart.Value;
        }
        else
        {
            return peg;
        }
    }

    public List<ScarecrowPart> DamageAllParts(int amount)
    {
        List<ScarecrowPart> hitParts = new List<ScarecrowPart>();

        var intactParts = _parts.Where(p => p.Value.State == ScarecrowPartState.Intact);
        foreach (var part in intactParts)
        {
            DamagePart(part.Key, amount);
            hitParts.Add(part.Value);
        }

        return hitParts;
    }

    public void DamagePart(ScarecrowPartType partType, int amount)
    {
        if (_parts.ContainsKey(partType))
        {
            _parts[partType].Damage(amount);
        }

        if (partType == ScarecrowPartType.Peg && _parts[partType].State == ScarecrowPartState.Ruined)
        {
            transform.Find("Sprites").Find("Dead").GetComponent<SpriteRenderer>().enabled = true;
        }

        if (_parts.Values.All(p => p.State == ScarecrowPartState.Ruined))
        {
            state = ScarecrowState.Dead;
            fireObject.SetActive(false);
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
                fireObject.SetActive(false);
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
                fireObject.SetActive(true);
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
            fireObject.SetActive(false);
        }
    }
}
