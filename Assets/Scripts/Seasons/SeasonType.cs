using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum SeasonType
{
    Fall,
    Winter,
    Spring,
    Summer,
}

public static class SeasonTypeExtensions
{
    public static SeasonType NextSeasonType(this SeasonType currentSeasonType)
    {
        switch (currentSeasonType)
        {
            case SeasonType.Fall:
                return SeasonType.Winter;
            case SeasonType.Winter:
                return SeasonType.Spring;
            case SeasonType.Spring:
                return SeasonType.Summer;
            case SeasonType.Summer:
                return SeasonType.Fall;
            default:
                throw new ArgumentOutOfRangeException(nameof(currentSeasonType), currentSeasonType, null);
        }
    }
}
