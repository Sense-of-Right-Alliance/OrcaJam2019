using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public Scarecrow[] scarecrows = new Scarecrow[4];

    public SeasonType currentSeasonType = SeasonType.Fall;

    public GameState GameState { get; set; } = GameState.GameStarting;

    public class SeasonChangedEvent : UnityEvent<SeasonType> { };
    public static SeasonChangedEvent OnSeasonChanged;

    public class YearChangedEvent : UnityEvent<int> { };
    public static YearChangedEvent OnYearChanged;

    public int currentYear = 1842;

    [SerializeField] float TimePassingDuration = 5f;

    TimePassageCinematicManager timePassageCinematicManager;

    private void Awake()
    {
        if (OnSeasonChanged == null) OnSeasonChanged = new SeasonChangedEvent();
        if (OnYearChanged == null) OnYearChanged = new YearChangedEvent();

        timePassageCinematicManager = GameObject.FindObjectOfType<TimePassageCinematicManager>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) NextSeason();
        if (Input.GetKeyDown(KeyCode.Y)) NextYear();
    }

    private void NextSeason()
    {
        timePassageCinematicManager.PassTime(TimePassingDuration);
        currentSeasonType = currentSeasonType == SeasonType.Summer ? SeasonType.Fall : (currentSeasonType + 1);
        OnSeasonChanged.Invoke(currentSeasonType);
    }

    private void NextYear()
    {
        currentYear++;
        OnYearChanged.Invoke(currentYear);
    }
}
