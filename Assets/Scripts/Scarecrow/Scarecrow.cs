using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scarecrow : MonoBehaviour
{
    public ScarecrowState state = ScarecrowState.Alive;

    public ScarecrowPart head;
    public ScarecrowPart leftArm;
    public ScarecrowPart rightArm;
    public ScarecrowPart peg;

    private readonly Dictionary<ScarecrowPartType, ScarecrowPart> _parts = new Dictionary<ScarecrowPartType, ScarecrowPart>();

    private void Start()
    {
        _parts[ScarecrowPartType.Head] = head;
        _parts[ScarecrowPartType.LeftArm] = leftArm;
        _parts[ScarecrowPartType.RightArm] = rightArm;
        _parts[ScarecrowPartType.Peg] = peg;
    }

    public ScarecrowPart[] GetScarecrowPartTransforms()
    {
        return new ScarecrowPart[] { peg, leftArm, rightArm, head };//Transform[] { peg.transform, leftArm.transform, rightArm.transform, head.transform};
    }

    private void Update()
    {

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
}
