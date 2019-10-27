using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utility
{
    public static T GetManager<T>() => GameObject.Find("/GameManager").GetComponent<T>();

    public static GameManager GameManager => GetManager<GameManager>();
    public static UiManager UiManager => GetManager<UiManager>();
    public static SeasonManager SeasonManager => GetManager<SeasonManager>();
    public static SeasonEventManager SeasonEventManager => GetManager<SeasonEventManager>();
    public static ScarecrowManager ScarecrowManager => GetManager<ScarecrowManager>();
    public static TimePassageCinematicManager TimePassageCinematicManager => GetManager<TimePassageCinematicManager>();
}
