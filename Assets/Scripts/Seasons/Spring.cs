using System;
using System.Collections.Generic;
using System.Linq;

public class Spring : Season
{
    public Spring() : base(SeasonType.Spring) { }

    public override IEnumerable<SeasonEvent> PossibleEvents { get; } = new List<SeasonEvent>
    {
        new SeasonEvent(SeasonEventType.GentleBreeze, 3f, 1f),
        new SeasonEvent(SeasonEventType.LightShower, 3f, 1f),
        new SeasonEvent(SeasonEventType.StrongWinds, 3f, 0.5f),
        new SeasonEvent(SeasonEventType.TorrentialDownpour, 3f, 0.25f),
    };
}
