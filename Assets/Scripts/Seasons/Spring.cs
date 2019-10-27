using System;
using System.Collections.Generic;
using System.Linq;

public class Spring : Season
{
    public Spring() : base(SeasonType.Spring) { }

    public override IEnumerable<SeasonEvent> PossibleEvents { get; } = new List<SeasonEvent>
    {
        new SeasonEvent(SeasonEventType.None, 3f, 20f),
        new SeasonEvent(SeasonEventType.GentleRain, 3f, 50f),
        new SeasonEvent(SeasonEventType.MeteoriteStorm, 3f, 5f),
        new SeasonEvent(SeasonEventType.AsteroidStrike, 3f, 5f),
        new SeasonEvent(SeasonEventType.Tornado, 3f, 20f),
    };
}
