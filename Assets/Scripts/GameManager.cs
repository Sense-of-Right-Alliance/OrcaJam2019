using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Scarecrow[] scarecrows = new Scarecrow[4];

    public SeasonType currentSeasonType = SeasonType.Fall;

    public GameState GameState { get; set; } = GameState.GameStarting;


    private void Start()
    {

    }

    private void Update()
    {

    }
}
