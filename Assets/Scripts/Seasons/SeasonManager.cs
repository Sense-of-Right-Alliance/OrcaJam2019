using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SeasonManager : MonoBehaviour
{
    [SerializeField] private SeasonType currentSeasonType = SeasonType.Fall;
    [SerializeField] private int eventsPerSeason = 3;
    [SerializeField] private float timePassingDuration = 5f;

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

    public SeasonEvent CurrentSeasonEvent => _currentSeasonEventIndex < _currentSeasonEvents.Count() ? _currentSeasonEvents.ElementAt(_currentSeasonEventIndex) : null;

    public class SeasonChangedEvent : UnityEvent<SeasonType> { }
    public class SeasonEventChangedEvent : UnityEvent<SeasonEventType> { }

    public SeasonChangedEvent OnSeasonChanged { get; } = new SeasonChangedEvent();
    public SeasonEventChangedEvent OnSeasonEventChanged { get; } = new SeasonEventChangedEvent();
    public UnityEvent OnSeasonEnded { get; } = new UnityEvent();
    public UnityEvent OnYearEnded { get; } = new UnityEvent();

    private SeasonEventManager _seasonEventManager;
    private TimePassageCinematicManager _timePassageCinematicManager;

    private void Start()
    {
        _seasonEventManager = Utility.SeasonEventManager;
        _timePassageCinematicManager = Utility.TimePassageCinematicManager;
    }

    private void Update()
    {

    }

    public void BeginYear()
    {
        GetEventsForSeason();

        StartCoroutine(ProcessSeasonEvents());
    }

    public void BeginNextSeason()
    {
        GetEventsForSeason();

        StartCoroutine(ProcessSeasonEvents());
    }

    IEnumerator ProcessSeasonEvents()
    {
        OnSeasonEventChanged.Invoke(CurrentSeasonEvent.Type);

        foreach (var seasonEvent in _currentSeasonEvents)
        {
            _seasonEventManager.ProcessSeasonEvent(seasonEvent);
            yield return new WaitForSeconds(seasonEvent.Duration);
            FinishSeasonEvent();
        }
    }

    private void ChangeSeason()
    {
        //_timePassageCinematicManager.PassTime(timePassingDuration);

        currentSeasonType = currentSeasonType.NextSeasonType();
        OnSeasonChanged.Invoke(currentSeasonType);
    }

    private void FinishSeasonEvent()
    {
        _currentSeasonEventIndex += 1;
        if (_currentSeasonEventIndex < _currentSeasonEvents.Count())
        {
            OnSeasonEventChanged.Invoke(CurrentSeasonEvent.Type);
        }
        else
        {
            ClearSeasonEvents();

            OnSeasonEventChanged.Invoke(SeasonEventType.None);

            if (CurrentSeason.Type == SeasonType.Summer)
            {
                OnYearEnded.Invoke();
            }
            else
            {
                OnSeasonEnded.Invoke();
            }

            ChangeSeason();
        }
    }

    private void ClearSeasonEvents()
    {
        _currentSeasonEvents = new List<SeasonEvent>();
        _currentSeasonEventIndex = 0;
    }

    private void GetEventsForSeason()
    {
        _currentSeasonEvents = CurrentSeason.SelectRandomizedEvents(eventsPerSeason);
        _currentSeasonEventIndex = 0;
    }
}
