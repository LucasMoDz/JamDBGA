using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//TODO: ARRAY DI STRUCT CON IMMAGINE E VALORI BOTTONI

public class TimeBar : MonoBehaviour
{
    private const int maxSeconds = 30;
    private int currentSeconds;

    private IEnumerator coroutine;
    private Text secondsText;

    private void Awake()
    {
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
        currentSeconds = 30;
    }

    private IEnumerator TimeBarCO()
    {
        while (currentSeconds > 0)
        {
            currentSeconds--;
            secondsText.text = "0." + currentSeconds + "s";
            yield return new WaitForSecondsRealtime(1);
        }
    }
}