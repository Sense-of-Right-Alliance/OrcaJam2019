using System;
using System.Collections.Generic;
using System.Linq;

public class Fall : Season
{
    public Fall() : base(SeasonType.Fall) { }

    public override IEnumerable<SeasonEvent> PossibleEvents { get; } = new List<SeasonEvent>
    {
        new SeasonEvent(SeasonEventType.GentleRain, 3f, 10f),
        new SeasonEvent(SeasonEventType.MeteoriteStorm, 3f, 10f),
        new SeasonEvent(SeasonEventType.AsteroidStrike, 3f, 5f),
        new SeasonEvent(SeasonEventType.Tornado, 3f, 10f),
        new SeasonEvent(SeasonEventType.Thunderstorm, 3f, 10f),
    };
}
