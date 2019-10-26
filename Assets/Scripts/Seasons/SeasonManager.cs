using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SeasonManager : MonoBehaviour
{
    public SeasonType currentSeasonType = SeasonType.Fall;

    private readonly Dictionary<SeasonType, Season> _seasons = new Dictionary<SeasonType, Season>
    {
        { SeasonType.Fall, new Fall() },
        { SeasonType.Winter, new Winter() },
        { SeasonType.Spring, new Spring() },
        { SeasonType.Summer, new Summer() },
    };

    public Season CurrentSeason => _seasons[currentSeasonType];

    private IEnumerable<SeasonEvent> _currentSeasonEvents = new List<SeasonEvent>();

    private int _currentSeasonEventIndex = 0;

    public SeasonEvent CurrentSeasonEvent => _currentSeasonEvents.Count() > _currentSeasonEventIndex ? _currentSeasonEvents.ElementAt(_currentSeasonEventIndex) : null;

    public int eventsPerSeason = 3;

    public class SeasonChangedEvent : UnityEvent<SeasonType> { }
    public SeasonChangedEvent OnSeasonChanged { get; } = new SeasonChangedEvent();

    public float timePassingDuration = 5f;

    private TimePassageCinematicManager _timePassageCinematicManager;

    private void Start()
    {
        _timePassageCinematicManager = Utility.TimePassageCinematicManager;
    }

    private void Update()
    {

    }

    private void NextSeason()
    {
        _timePassageCinematicManager.PassTime(timePassingDuration);

        currentSeasonType = currentSeasonType.NextSeasonType();
        OnSeasonChanged.Invoke(currentSeasonType);

        _currentSeasonEvents = CurrentSeason.SelectRandomizedEvents(eventsPerSeason);
        _currentSeasonEventIndex = 0;
    }

    private void NextSeasonEvent()
    {
        _currentSeasonEventIndex += 1;
        if (_currentSeasonEventIndex >= _currentSeasonEvents.Count())
        {
            ClearSeasonEvents();
        }
    }

    private void ClearSeasonEvents()
    {
        _currentSeasonEvents = new List<SeasonEvent>();
        _currentSeasonEventIndex = 0;
    }
}
