using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utility
{
    public static T GetManager<T>() => GameObject.Find("/GameManager").GetComponent<T>();

    public static GameManager GetGameManager() => GetManager<GameManager>();
}
