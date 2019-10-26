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

    public int currentYear = 0;

    private void Start()
    {
        GameState = GameState.SeasonElapsing;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) NextYear();
    }

    private void NextYear()
    {
        currentYear++;
        OnYearChanged.Invoke(currentYear);
    }
}
