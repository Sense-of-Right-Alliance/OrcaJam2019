﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI yearText;
    public TextMeshProUGUI seasonText;
    public TextMeshProUGUI seasonEventText;

    public TextMeshProUGUI resourceText1;
    public TextMeshProUGUI resourceText2;
    public TextMeshProUGUI resourceText3;
    public TextMeshProUGUI resourceText4;

    private TextMeshProUGUI[] _resourceTexts;

    public Image resourceImage1;
    public Image resourceImage2;
    public Image resourceImage3;
    public Image resourceImage4;
    private Image[] _resourceImages;

    private void Awake()
    {
        if (countdownText == null) countdownText = GameObject.Find("Countdown Text").GetComponent<TextMeshProUGUI>();
        if (yearText == null) yearText = GameObject.Find("Year Text").GetComponent<TextMeshProUGUI>();
        if (seasonText == null) seasonText = GameObject.Find("Season Text").GetComponent<TextMeshProUGUI>();
        if (seasonEventText == null) seasonEventText = GameObject.Find("Season Event Text").GetComponent<TextMeshProUGUI>();

        if (resourceText1 == null) resourceText1 = GameObject.Find("Resource1").GetComponentInChildren<TextMeshProUGUI>();
        if (resourceText2 == null) resourceText2 = GameObject.Find("Resource2").GetComponentInChildren<TextMeshProUGUI>();
        if (resourceText3 == null) resourceText3 = GameObject.Find("Resource3").GetComponentInChildren<TextMeshProUGUI>();
        if (resourceText4 == null) resourceText4 = GameObject.Find("Resource4").GetComponentInChildren<TextMeshProUGUI>();
        _resourceTexts = new[] { resourceText1, resourceText2, resourceText3, resourceText4 };

        if (resourceImage1 == null) resourceImage1 = GameObject.Find("Resource1").GetComponentInChildren<Image>();
        if (resourceImage2 == null) resourceImage2 = GameObject.Find("Resource2").GetComponentInChildren<Image>();
        if (resourceImage3 == null) resourceImage3 = GameObject.Find("Resource3").GetComponentInChildren<Image>();
        if (resourceImage4 == null) resourceImage4 = GameObject.Find("Resource4").GetComponentInChildren<Image>();
        _resourceImages = new Image[] { resourceImage1, resourceImage2, resourceImage3, resourceImage4 };

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
        seasonEventText.text = null;
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
}
