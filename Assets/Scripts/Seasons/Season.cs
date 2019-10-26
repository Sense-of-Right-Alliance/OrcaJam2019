using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class Season
{
    public SeasonType Type { get; set; }

    public abstract IEnumerable<SeasonEvent> PossibleEvents { get; }

    public Season(SeasonType type)
    {
        Type = type;
    }

    public IEnumerable<SeasonEvent> SelectRandomizedEvents(int numberOfEvents)
    {
        for (int i = 0; i < numberOfEvents; i++)
        {
            yield return SelectRandomEvent();
        }
    }

    private SeasonEvent SelectRandomEvent()
    {
        float currentWeight = 0f;
        float totalWeights = PossibleEvents.Sum(e => e.WeightedProbability);
        float randomSelection = UnityEngine.Random.Range(0f, totalWeights);

        for (int i = 0; i < PossibleEvents.Count(); i++)
        {
            var seasonEvent = PossibleEvents.ElementAt(i);

            currentWeight += seasonEvent.WeightedProbability;
            if (randomSelection <= currentWeight)
            {
                return seasonEvent;
            }
        }

        return null;
    }
}
