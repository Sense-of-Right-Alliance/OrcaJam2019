using System;
using System.Collections.Generic;
using System.Linq;

public class Winter : Season
{
    public Winter() : base(SeasonType.Winter) { }

    public override IEnumerable<SeasonEvent> PossibleEvents { get; } = new List<SeasonEvent>
    {
        new SeasonEvent(SeasonEventType.MeteoriteStorm, 3f, 10f),
        new SeasonEvent(SeasonEventType.AsteroidStrike, 3f, 5f),
        new SeasonEvent(SeasonEventType.Blizzard, 3f, 20f),
        new SeasonEvent(SeasonEventType.Thunderstorm, 3f, 10f),
    };
}
