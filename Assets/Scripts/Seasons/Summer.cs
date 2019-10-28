﻿using System;
using System.Collections.Generic;
using System.Linq;

public class Summer : Season
{
    public Summer() : base(SeasonType.Summer) { }

    public override IEnumerable<SeasonEvent> PossibleEvents { get; } = new List<SeasonEvent>
    {
        new SeasonEvent(SeasonEventType.MeteoriteStorm, 3f, 5f),
        new SeasonEvent(SeasonEventType.AsteroidStrike, 3f, 5f),
        new SeasonEvent(SeasonEventType.Tornado, 3f, 15f),
        new SeasonEvent(SeasonEventType.Thunderstorm, 3f, 10f),
    };
}
