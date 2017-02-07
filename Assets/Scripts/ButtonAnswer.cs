using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonAnswer : MonoBehaviour
{
    public int score;
    
    private Text buttonScoreText;
    private Vector3 startPositionOfButtonText;

    private RemoveCard sbangButton;

    private GameManager gameManager;

    private CurrentScore currentScoreText;
    private Multiplier multiplierText;
    private TimeBar timeBarText;

    private TotalScore totalScoreText;

    private void Awake()
    {
        buttonScoreText = GameObject.Find("ButtonScore").GetComponent<Text>();
        startPositionOfButtonText = buttonScoreText.transform.position;

        gameManager = FindObjectOfType<GameManager>();

        sbangButton = FindObjectOfType<RemoveCard>();

        currentScoreText = FindObjectOfType<CurrentScore>();
        multiplierText = FindObjectOfType<Multiplier>();
        timeBarText = FindObjectOfType<TimeBar>();

        totalScoreText = FindObjectOfType<TotalScore>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            ScorePhase();
    }

    public void ScorePhase()
    {
        if (gameManager.feedbackIsActive || sbangButton.isFirstCard)
            return;

        gameManager.feedbackIsActive = true;

        buttonScoreText.text = score.ToString();
        buttonScoreText.color = new Color(0, 1, 0, 0);
        buttonScoreText.transform.position = this.transform.position + new Vector3(0, 21f, 0);

        StartCoroutine(ScorePhaseCO());
    }

    private IEnumerator ScorePhaseCO()
    {
        Color newColor = buttonScoreText.color;

        while (newColor.a < 1)
        {
            newColor.a += Time.deltaTime;
            buttonScoreText.color = newColor;
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1);

        Vector3 currentPositionOfText = buttonScoreText.transform.position;
        float step = 0;

        while ((currentScoreText.transform.position - buttonScoreText.transform.position).sqrMagnitude > 0.1f)
        {
            step += Time.deltaTime / 2;
            buttonScoreText.transform.position = Vector2.Lerp(currentPositionOfText, currentScoreText.transform.position, step);
            yield return null;
        }

        currentScoreText.currentScore = score;
        currentScoreText.GetComponent<Text>().text = currentScoreText.currentScore.ToString();
        buttonScoreText.text = "";

        yield return new WaitForSecondsRealtime(1);

        buttonScoreText.text = multiplierText.GetComponent<Text>().text;
        buttonScoreText.transform.position = multiplierText.transform.position;

        step = 0;

        while ((currentScoreText.transform.position - buttonScoreText.transform.position).sqrMagnitude > 0.1f)
        {
            step += Time.deltaTime;
            buttonScoreText.transform.position = Vector2.Lerp(multiplierText.transform.position, currentScoreText.transform.position, step);
            yield return null;
        }

        currentScoreText.currentScore *= multiplierText.GetComponent<Multiplier>().currentMultiplier;
        currentScoreText.GetComponent<Text>().text = currentScoreText.currentScore.ToString();
        buttonScoreText.text = "";

        yield return new WaitForSecondsRealtime(1);

        buttonScoreText.text = timeBarText.currentSeconds.ToString();
        buttonScoreText.transform.position = timeBarText.transform.position;

        step = 0;

        while (step < 1)
        {
            step += Time.deltaTime / 2;
            buttonScoreText.transform.position = Vector2.Lerp(timeBarText.transform.position, currentScoreText.transform.position, step);
            yield return null;
        }

        currentScoreText.currentScore += timeBarText.currentSeconds;
        currentScoreText.GetComponent<Text>().text = currentScoreText.currentScore.ToString();
        buttonScoreText.text = "";

        yield return new WaitForSecondsRealtime(1);

        buttonScoreText.text = currentScoreText.currentScore.ToString();
        buttonScoreText.transform.position = currentScoreText.transform.position;

        step = 0;

        while (step < 1)
        {
            step += Time.deltaTime * 2;
            buttonScoreText.transform.position = Vector2.Lerp(currentScoreText.transform.position, totalScoreText.transform.position, step);
            yield return null;
        }

        totalScoreText.totalScore += currentScoreText.currentScore;
        totalScoreText.GetComponent<Text>().text = totalScoreText.totalScore.ToString();
        buttonScoreText.text = "";

        gameManager.feedbackIsActive = false;

        ResetCurrentVariables();
    }

    private void ResetCurrentVariables()
    {
        currentScoreText.currentScore = 0;
        currentScoreText.GetComponent<Text>().text = currentScoreText.currentScore.ToString();
        
        multiplierText.currentMultiplier = multiplierText.MAX_MULTIPLIER;
        multiplierText.GetComponent<Text>().text = "x " + multiplierText.MAX_MULTIPLIER.ToString();

        timeBarText.currentSeconds = timeBarText.maxSeconds;
        timeBarText.GetComponent<Text>().text = "0." + timeBarText.maxSeconds.ToString() + "s";
        timeBarText.StopTimer();
    }
}