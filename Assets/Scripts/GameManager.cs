using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameState GameState { get; set; } = GameState.GameStarting;

    public Scarecrow[] scarecrows = new Scarecrow[4];

    public class YearChangedEvent : UnityEvent<int> { }
    public YearChangedEvent OnYearChanged { get; } = new YearChangedEvent();

    public class CountdownChangedEvent : UnityEvent<int?> { }
    public CountdownChangedEvent OnCountdownChanged { get; } = new CountdownChangedEvent();

    public int currentYear = 0;

    public int firstYearPlanningTime = 3;
    public int seasonEndPlanningTime = 5;
    public int yearEndPlanningTime = 7;

    private SeasonManager _seasonManager;

    private void Awake()
    {
        _seasonManager = GetComponent<SeasonManager>();

        _seasonManager.OnSeasonEnded.AddListener(SeasonEndedHandler);
        _seasonManager.OnYearEnded.AddListener(YearEndedHandler);
    }

    private void Start()
    {
        OnYearChanged.Invoke(currentYear);

        StartCoroutine(CountdownToFirstYear());
    }

    private void Update()
    {

    }

    private void SeasonEndedHandler()
    {
        StartCoroutine(CountdownToNextSeason());
    }

    private void YearEndedHandler()
    {
        currentYear++;
        OnYearChanged.Invoke(currentYear);

        StartCoroutine(CountdownToNextYear());
    }

    private IEnumerator CountdownToFirstYear()
    {
        yield return Countdown(firstYearPlanningTime);

        GameState = GameState.SeasonElapsing;
        _seasonManager.BeginYear();
    }

    private IEnumerator CountdownToNextSeason()
    {
        GameState = GameState.SeasonElapsed;

        yield return Countdown(seasonEndPlanningTime);

        // TODO: spiderfriends should repair parts

        GameState = GameState.SeasonElapsing;
        _seasonManager.BeginNextSeason();
    }

    private IEnumerator CountdownToNextYear()
    {
        GameState = GameState.YearElapsed;

        yield return Countdown(yearEndPlanningTime);

        // TODO: spiderfriends should repair parts

        GameState = GameState.SeasonElapsing;
        _seasonManager.BeginYear();
    }

    private IEnumerator Countdown(int seconds)
    {
        for (int countdownTime = seconds; countdownTime > 0; countdownTime--)
        {
            OnCountdownChanged.Invoke(countdownTime);
            yield return new WaitForSeconds(1);
        }
        OnCountdownChanged.Invoke(null);
    }
}
