﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScarecrowManager : MonoBehaviour
{
    [SerializeField]
    private Scarecrow outerLeftScarecrow;
    [SerializeField]
    private Scarecrow centreLeftScarecrow;
    [SerializeField]
    private Scarecrow centreRightScarecrow;
    [SerializeField]
    private Scarecrow outerRightScarecrow;

    private List<Scarecrow> _scarecrows;

    public Scarecrow OuterLeftScarecrow => outerLeftScarecrow;
    public Scarecrow CentreLeftScarecrow => centreLeftScarecrow;
    public Scarecrow CentreRightScarecrow => centreRightScarecrow;
    public Scarecrow OuterRightScarecrow => outerRightScarecrow;

    public IEnumerable<Scarecrow> OuterScarecrows => new List<Scarecrow> { outerLeftScarecrow, outerRightScarecrow };

    public IEnumerable<Scarecrow> CentreScarecrows => new List<Scarecrow> { centreLeftScarecrow, centreRightScarecrow };

    public IEnumerable<Scarecrow> ScarecrowsLeftToRight => _scarecrows;

    public IEnumerable<Scarecrow> ScarecrowsRightToLeft => ScarecrowsLeftToRight.Reverse();

    private void Awake()
    {
        _scarecrows = new List<Scarecrow>
        {
            outerLeftScarecrow,
            centreLeftScarecrow,
            centreRightScarecrow,
            outerRightScarecrow,
        };
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}