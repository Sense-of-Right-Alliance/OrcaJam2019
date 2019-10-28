using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI yearText;
    [SerializeField] private TextMeshProUGUI seasonText;
    [SerializeField] private TextMeshProUGUI seasonEventText;

    [SerializeField] private GameObject resource1;
    [SerializeField] private GameObject resource2;
    [SerializeField] private GameObject resource3;
    [SerializeField] private GameObject resource4;

    [SerializeField] SeasonTrackerBar seasonBar;

    private List<GameObject> _resources;
    private List<TextMeshProUGUI> _resourceTexts;
    private List<Image> _resourceImages;

    private void Awake()
    {
        if (countdownText == null) countdownText = GameObject.Find("Countdown Text").GetComponent<TextMeshProUGUI>();
        if (yearText == null) yearText = GameObject.Find("Year Text").GetComponent<TextMeshProUGUI>();
        if (seasonText == null) seasonText = GameObject.Find("Season Text").GetComponent<TextMeshProUGUI>();
        if (seasonEventText == null) seasonEventText = GameObject.Find("Season Event Text").GetComponent<TextMeshProUGUI>();

        if (resource1 == null) resource1 = GameObject.Find("Resource1");
        if (resource2 == null) resource2 = GameObject.Find("Resource2");
        if (resource3 == null) resource3 = GameObject.Find("Resource3");
        if (resource4 == null) resource4 = GameObject.Find("Resource4");

        if (seasonBar == null) seasonBar = GameObject.Find("SeasonTrackerBar").GetComponent<SeasonTrackerBar>();

        _resources = new List<GameObject> { resource1, resource2, resource3, resource4};
        _resourceTexts = _resources.Select(r => r.GetComponentInChildren<TextMeshProUGUI>()).ToList();
        _resourceImages = _resources.Select(r => r.GetComponentInChildren<Image>()).ToList();

        HideResources();

        Utility.GameManager.OnCountdownChanged.AddListener(CountdownChangedHandler);
        Utility.GameManager.OnYearChanged.AddListener(YearChangedHandler);
        Utility.SeasonManager.OnSeasonChanged.AddListener(SeasonChangedHandler);
        Utility.SeasonManager.OnSeasonEventChanged.AddListener(SeasonEventChangedHandler);
        Player.OnResourceChanged.AddListener(ResourceChangedHandler);
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void HideResources()
    {
        foreach (var resource in _resources)
        {
            resource.SetActive(false);
        }
    }

    public void ShowResource(int playerId)
    {
        if (playerId < 0 || playerId >= _resources.Count)
        {
            return;
        }

        _resources.ElementAt(playerId).SetActive(true);
    }

    private void CountdownChangedHandler(int? countdownValue)
    {
        countdownText.text = countdownValue.ToString();
    }

    private void YearChangedHandler(int year)
    {
        yearText.text = year.ToString();
    }

    private void SeasonChangedHandler(SeasonType season)
    {
        seasonText.text = season.ToString();
    }

    private void SeasonEventChangedHandler(SeasonEventType seasonEvent)
    {
        seasonEventText.text = seasonEvent == SeasonEventType.None ? null : seasonEvent.ToString();
    }

    private void ResourceChangedHandler(int playerId, int amount)
    {
        _resourceTexts[playerId].text = amount.ToString();
    }

    public Vector2 GetPlayerResourceBoxPosition(int playerId)
    {
        return Camera.main.ScreenToWorldPoint(_resourceImages[playerId].rectTransform.transform.position);
    }

    public void PlayNextSeasonOnBar(int seasonId, float duration)
    {
        seasonBar.PlaySeason(seasonId, duration);
    }
}
