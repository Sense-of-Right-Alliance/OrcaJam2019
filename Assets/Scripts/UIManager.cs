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

    [SerializeField] TextMeshProUGUI ResourceText1;
    [SerializeField] TextMeshProUGUI ResourceText2;
    [SerializeField] TextMeshProUGUI ResourceText3;
    [SerializeField] TextMeshProUGUI ResourceText4;

    TextMeshProUGUI[] ResourceTexts;

    // Start is called before the first frame update
    void Start()
    {
        if (YearText == null) YearText = GameObject.Find("Year Text").GetComponent<TextMeshProUGUI>();
        if (SeasonText == null) SeasonText = GameObject.Find("Season Text").GetComponent<TextMeshProUGUI>();
        if (WeatherText == null) WeatherText = GameObject.Find("Weather Text").GetComponent<TextMeshProUGUI>();

        if (ResourceText1 == null) ResourceText1 = GameObject.Find("Resource1").GetComponentInChildren<TextMeshProUGUI>();
        if (ResourceText2 == null) ResourceText2 = GameObject.Find("Resource2").GetComponentInChildren<TextMeshProUGUI>();
        if (ResourceText3 == null) ResourceText3 = GameObject.Find("Resource3").GetComponentInChildren<TextMeshProUGUI>();
        if (ResourceText4 == null) ResourceText4 = GameObject.Find("Resource4").GetComponentInChildren<TextMeshProUGUI>();
        ResourceTexts = new TextMeshProUGUI[] { ResourceText1, ResourceText2, ResourceText3, ResourceText4 };

        Utility.SeasonManager.OnSeasonChanged.AddListener(UpdateSeason);
        Utility.GameManager.OnYearChanged.AddListener(UpdateYear);
        Player.OnResourceChanged.AddListener(UpdateResource);
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

    private void UpdateResource(int playerId, int amount)
    {
        ResourceTexts[playerId].text = amount.ToString();
    }
}
