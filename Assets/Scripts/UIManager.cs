using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI YearText;
    [SerializeField] TextMeshProUGUI SeasonText;
    [SerializeField] TextMeshProUGUI WeatherText;

    // Start is called before the first frame update
    void Start()
    {
        if (YearText == null) YearText = GameObject.Find("Year Text").GetComponent<TextMeshProUGUI>();
        if (SeasonText == null) SeasonText = GameObject.Find("Season Text").GetComponent<TextMeshProUGUI>();
        if (SeasonText == null) WeatherText = GameObject.Find("Weather Text").GetComponent<TextMeshProUGUI>();

        Utility.SeasonManager.OnSeasonChanged.AddListener(UpdateSeason);
        Utility.GameManager.OnYearChanged.AddListener(UpdateYear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateSeason(SeasonType season)
    {
        SeasonText.text = season.ToString();
    }

    private void UpdateYear(int year)
    {
        YearText.text = year.ToString();
    }

    private void UpdateWeather(SeasonEventType weather)
    {
        WeatherText.text = weather.ToString();
    }
}
