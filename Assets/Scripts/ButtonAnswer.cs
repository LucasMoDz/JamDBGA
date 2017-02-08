using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonAnswer : MonoBehaviour
{
    public int score;
    
    private Text buttonScoreText;
    private Vector3 startPositionOfButtonText;
    
    private GameManager gameManager;

    private RemoveCard sbangButton;

    private CurrentScore currentScoreText;
    private Multiplier multiplierText;
    private TimeBar timeBarText;

    private TotalScore totalScoreText;
    
    private void Awake()
    {
        #region References

        buttonScoreText = GameObject.Find("ButtonScore").GetComponent<Text>();
        startPositionOfButtonText = buttonScoreText.transform.position;

        gameManager = FindObjectOfType<GameManager>();

        sbangButton = FindObjectOfType<RemoveCard>();

        currentScoreText = FindObjectOfType<CurrentScore>();
        multiplierText = FindObjectOfType<Multiplier>();
        timeBarText = FindObjectOfType<TimeBar>();

        totalScoreText = FindObjectOfType<TotalScore>();

        #endregion
    }

    public void AcceptAnswer()
    {
        if (gameManager.feedbackIsActive || sbangButton.isClicked)
            return;

        gameManager.feedbackIsActive = true;
        timeBarText.StopTimer();

        StartCoroutine(FirstFadeCO(0.8f));
    }

    public void TimeFinished()
    {
        if (gameManager.feedbackIsActive || sbangButton.isClicked)
            return;

        gameManager.feedbackIsActive = true;
        timeBarText.StopTimer();

        StartCoroutine(TimeFinishedCO(0.8f));
    }

    private IEnumerator TimeFinishedCO(float _seconds)
    {
        #region Fading score

        buttonScoreText.text = score.ToString();
        buttonScoreText.color = new Color(0, 1, 0, 0);
        buttonScoreText.transform.position = sbangButton.transform.position + new Vector3(0, 70f, 0); ;

        Color newColor = buttonScoreText.color;

        while (newColor.a < 1)
        {
            newColor.a += Time.deltaTime;
            buttonScoreText.color = newColor;
            yield return null;
        }

        #endregion  

        yield return new WaitForSecondsRealtime(_seconds);

        #region Score button moves to current score

        Vector3 currentPositionOfText = buttonScoreText.transform.position;
        float step = 0;

        while (step < 1)
        {
            step += Time.deltaTime / 1.5f;
            buttonScoreText.transform.position = Vector2.Lerp(currentPositionOfText, totalScoreText.transform.position, step);
            yield return null;
        }

        buttonScoreText.text = "";

        #endregion

        StartCoroutine(FinalPhaseCO());
    }

    private IEnumerator FirstFadeCO(float _seconds)
    {
        #region Fading score

        buttonScoreText.text = score.ToString();
        buttonScoreText.color = new Color(0, 1, 0, 0);
        buttonScoreText.transform.position = this.transform.position + new Vector3(0, 70f, 0);

        Color newColor = buttonScoreText.color;

        while (newColor.a < 1)
        {
            newColor.a += Time.deltaTime;
            buttonScoreText.color = newColor;
            yield return null;
        }

        #endregion  

        yield return new WaitForSecondsRealtime(_seconds);

        #region Choose coroutine

        if (score.Equals(0))
        {
            StartCoroutine(ScoreAnswerNullCO(0.5f));
            multiplierText.GetComponent<Text>().text = "x 0";
        }
        else
            StartCoroutine(AcceptAnswerCO(0.5f));

        #endregion
    }

    private IEnumerator ScoreAnswerNullCO(float _seconds)
    {
        #region Score button moves to current score

        Vector3 currentPositionOfText = buttonScoreText.transform.position;
        float step = 0;

        while (step < 1)
        {
            step += Time.deltaTime / 1.5f;
            buttonScoreText.transform.position = Vector2.Lerp(currentPositionOfText, totalScoreText.transform.position, step);
            yield return null;
        }
        
        buttonScoreText.text = "";
        
        #endregion

        StartCoroutine(FinalPhaseCO());
    }

    private IEnumerator AcceptAnswerCO(float _seconds)
    {
        #region Score button moves to current score

        Vector3 currentPositionOfText = buttonScoreText.transform.position;
        float step = 0;

        while (step < 1)
        {
            step += Time.deltaTime / 1.5f;
            buttonScoreText.transform.position = Vector2.Lerp(currentPositionOfText, currentScoreText.transform.position, step);
            yield return null;
        }

        currentScoreText.currentScore = score;
        currentScoreText.GetComponent<Text>().text = currentScoreText.currentScore.ToString();
        buttonScoreText.text = "";

        #endregion

        yield return new WaitForSecondsRealtime(_seconds);

        #region Multiplier moves to current score

        buttonScoreText.text = multiplierText.GetComponent<Text>().text;
        buttonScoreText.transform.position = multiplierText.transform.position;

        step = 0;

        while (step < 1)
        {
            step += Time.deltaTime;
            buttonScoreText.transform.position = Vector2.Lerp(multiplierText.transform.position, currentScoreText.transform.position, step);
            yield return null;
        }

        currentScoreText.currentScore *= multiplierText.GetComponent<Multiplier>().currentMultiplier;
        currentScoreText.GetComponent<Text>().text = currentScoreText.currentScore.ToString();
        buttonScoreText.text = "";

        #endregion

        yield return new WaitForSecondsRealtime(_seconds);

        #region Time score moves to current score

        buttonScoreText.text = "+ " + timeBarText.currentSeconds.ToString();
        buttonScoreText.transform.position = timeBarText.transform.position;

        step = 0;

        while (step < 1)
        {
            step += Time.deltaTime / 1.5f;
            buttonScoreText.transform.position = Vector2.Lerp(timeBarText.transform.position, currentScoreText.transform.position, step);
            yield return null;
        }

        currentScoreText.currentScore += timeBarText.currentSeconds;
        currentScoreText.GetComponent<Text>().text = currentScoreText.currentScore.ToString();
        buttonScoreText.text = "";

        #endregion

        yield return new WaitForSecondsRealtime(_seconds);

        #region Current score moves to total score

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

        #endregion

        StartCoroutine(FinalPhaseCO());
    }

    private IEnumerator FinalPhaseCO()
    {
        #region Fading covered images

        for (int i = 0; i < sbangButton.cardList.Count; i++)
            StartCoroutine(FadingCoveredImages(sbangButton.cardList[i].GetComponent<Image>()));

        #endregion

        sbangButton.SetText();

        yield return new WaitUntil(Skip);

        // Reset current score, multipier and seconds
        ResetCurrentVariables();
        
        // Change image and buttons scores
        gameManager.IncreasePhase();

        // Cover all cards 
        sbangButton.ResetList();
    }

    internal bool Skip()
    {
        if (sbangButton.skip)
            return true;

        return false;
    }

    private IEnumerator FadingCoveredImages(Image _card)
    {
        Color newColor = _card.color;

        while (newColor.a > 0)
        {
            newColor.a -= Time.deltaTime;
            _card.color = newColor;
            yield return null;
        }

        newColor.a = 1;
        _card.color = newColor;
        _card.gameObject.SetActive(false);

        sbangButton.canSkip = true;
    }

    private void ResetCurrentVariables()
    {
        gameManager.feedbackIsActive = false;

        currentScoreText.currentScore = 0;
        currentScoreText.GetComponent<Text>().text = currentScoreText.currentScore.ToString();
        
        multiplierText.currentMultiplier = multiplierText.MAX_MULTIPLIER;
        multiplierText.GetComponent<Text>().text = "x " + multiplierText.MAX_MULTIPLIER.ToString();

        timeBarText.currentSeconds = timeBarText.maxSeconds;
        timeBarText.GetComponent<Text>().text = "0." + timeBarText.maxSeconds.ToString() + "s";
        
        timeBarText.currentSeconds = 30;
        timeBarText.StopTimer();
    }
}