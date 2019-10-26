using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SeasonEvent
{
    public SeasonEventType Type { get; }

    public float Duration { get; }

    public float WeightedProbability { get; }

    public SeasonEvent(SeasonEventType type, float duration, float weightedProbability)
    {
        Type = type;
        Duration = duration;
        WeightedProbability = weightedProbability;
    }
}
