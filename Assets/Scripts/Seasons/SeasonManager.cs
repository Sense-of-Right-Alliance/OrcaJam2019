using System;
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

    private IList<SeasonEvent> _currentSeasonEvents = new List<SeasonEvent>();

    public bool IsInSeason { get { return _currentSeasonEvents.Count > 0; } }

    public class SeasonChangedEvent : UnityEvent<SeasonType> { }
    public class SeasonEventChangedEvent : UnityEvent<SeasonEventType> { }

    public SeasonChangedEvent OnSeasonChanged { get; } = new SeasonChangedEvent();
    public SeasonEventChangedEvent OnSeasonEventChanged { get; } = new SeasonEventChangedEvent();
    public UnityEvent OnSeasonEnded { get; } = new UnityEvent();
    public UnityEvent OnYearEnded { get; } = new UnityEvent();

    private ScarecrowManager _scarecrowManager;
    private SeasonEventManager _seasonEventManager;
    private TimePassageCinematicManager _timePassageCinematicManager;
    private UiManager _uiManager;

    private void Start()
    {
        _scarecrowManager = Utility.ScarecrowManager;
        _seasonEventManager = Utility.SeasonEventManager;
        _timePassageCinematicManager = Utility.TimePassageCinematicManager;
        _uiManager = Utility.UiManager;
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

    private IEnumerator ProcessSeasonEvents()
    {
        Debug.Log("Your forecast for " + CurrentSeason.Type + ": " + string.Join(", ", _currentSeasonEvents.Select(e => e.Type)));

        if (CurrentSeason.Type == SeasonType.Summer)
        {
            foreach (var scarecrow in _scarecrowManager.ScarecrowsLeftToRight)
            {
                scarecrow.RemoveWet();
            }
        }

        float duration = 0f;
        for (int i = 0; i < _currentSeasonEvents.Count; i++)
        {
            var seasonEvent = _currentSeasonEvents.ElementAt(i);
            duration += seasonEvent.Duration;
        }

        _timePassageCinematicManager.PassTime(duration);

        _uiManager.PlayNextSeasonOnBar((int)CurrentSeason.Type, duration);

        for (int i = 0; i < _currentSeasonEvents.Count; i++)
        {
            var seasonEvent = _currentSeasonEvents.ElementAt(i);
            OnSeasonEventChanged.Invoke(seasonEvent.Type);
            _seasonEventManager.ProcessSeasonEvent(seasonEvent);
            yield return new WaitForSeconds(seasonEvent.Duration);
        }

        ClearSeasonEvents();

        OnSeasonEventChanged.Invoke(SeasonEventType.None);

        _scarecrowManager.AssignResourcesToAllPlayers(GetResourcesForSeason());

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

    private int GetResourcesForSeason()
    {
        switch (CurrentSeason.Type)
        {
            case SeasonType.Fall:
                return 15;
            case SeasonType.Winter:
                return 5;
            case SeasonType.Spring:
                return 15;
            case SeasonType.Summer:
                return 10;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ChangeSeason()
    {
        currentSeasonType = currentSeasonType.NextSeasonType();
        OnSeasonChanged.Invoke(currentSeasonType);
    }

    private void ClearSeasonEvents()
    {
        _currentSeasonEvents = new List<SeasonEvent>();
    }

    private void GetEventsForSeason()
    {
        _currentSeasonEvents = CurrentSeason.SelectRandomizedEvents(eventsPerSeason).ToList();
    }
}
