﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeBar : MonoBehaviour
{
    private ButtonAnswer buttonAnswer;

    public int maxSeconds = 30;
    internal int currentSeconds;

    private IEnumerator coroutine;
    private Text secondsText;

    private void Awake()
    {
        buttonAnswer = FindObjectOfType<ButtonAnswer>();
        secondsText = this.GetComponent<Text>();
        currentSeconds = maxSeconds;
    }

    public void StartTimeBar()
    {
        coroutine = TimeBarCO();
        StartCoroutine(coroutine);
    }

    public void StopTimer()
    {
        StopCoroutine(coroutine);
    }

    private IEnumerator TimeBarCO()
    {
        while (currentSeconds > 0)
        {
            currentSeconds--;
            secondsText.text = "0." + currentSeconds + "s";
            yield return new WaitForSecondsRealtime(1);
        }

        buttonAnswer.score = 0;
        buttonAnswer.TimeFinished();
    }
}