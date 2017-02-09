using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeBar : MonoBehaviour
{
    private ButtonAnswer buttonAnswer;
    public AudioClip clipTime;
    private AudioSource sourceFSX;

    public int maxSeconds = 30;
    internal int currentSeconds;

    private IEnumerator coroutine;
    private Text secondsText;

    private void Awake()
    {
        buttonAnswer = FindObjectOfType<ButtonAnswer>();
        secondsText = this.GetComponent<Text>();
        currentSeconds = maxSeconds;

        sourceFSX = GameObject.FindGameObjectWithTag("FSX").GetComponent<AudioSource>();
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
            secondsText.text = currentSeconds + "s";

            if (currentSeconds == 2)
                sourceFSX.PlayOneShot(clipTime);

            yield return new WaitForSecondsRealtime(1);
        }

        buttonAnswer.score = 0;
        buttonAnswer.TimeFinished();
    }
}