using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Text finalScoreText;

    public AudioClip clipVictory;
    public AudioClip rightAnswer;
    public AudioClip wrongAnswer;

    private AudioSource sourceFSX;

    private ButtonAnswer[] buttonAnswers = new ButtonAnswer[6];
    private CanvasGroup canvasGroup;
    private Image image;

    private RemoveCard removeCardButton;

    internal byte index = 0;
    internal bool feedbackIsActive = false;
    internal bool isLastImage;

    public NewPhase[] gamePhase;
    
    [System.Serializable]
    public struct NewPhase
    {
        public Sprite image;
        
        public Button button1;
        public Button button2;
        public Button button3;
        public Button button4;
        public Button button5;
        public Button button6;
    }

    [System.Serializable]
    public struct Button
    {
        public int score;
        public string text;
    }

    private void Awake()
    {
        image = GameObject.FindGameObjectWithTag("Image").GetComponent<Image>();

        if (image == null)
            Debug.LogError("Image not found");

        Transform answers = GameObject.Find("Answers").transform;

        for (int i = 0; i < answers.childCount; i++)
            buttonAnswers[i] = answers.GetChild(i).GetComponent<ButtonAnswer>();

        canvasGroup = GameObject.FindGameObjectWithTag("Fade").GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            Debug.Log("CanvasGroup not found");

        removeCardButton = FindObjectOfType<RemoveCard>();

        sourceFSX = GameObject.FindGameObjectWithTag("FSX").GetComponent<AudioSource>();

        IncreasePhase();
    }

    public void IncreasePhase()
    {
        if (index == gamePhase.Length)
            FinalGame();
        else
        {
            image.sprite = gamePhase[index].image;

            buttonAnswers[0].score = gamePhase[index].button1.score;
            buttonAnswers[1].score = gamePhase[index].button2.score;
            buttonAnswers[2].score = gamePhase[index].button3.score; 
            buttonAnswers[3].score = gamePhase[index].button4.score;
            buttonAnswers[4].score = gamePhase[index].button5.score; 
            buttonAnswers[5].score = gamePhase[index].button6.score; 

            buttonAnswers[0].transform.GetComponentInChildren<Text>().text = gamePhase[index].button1.text;
            buttonAnswers[1].transform.GetComponentInChildren<Text>().text = gamePhase[index].button2.text;
            buttonAnswers[2].transform.GetComponentInChildren<Text>().text = gamePhase[index].button3.text;
            buttonAnswers[3].transform.GetComponentInChildren<Text>().text = gamePhase[index].button4.text;
            buttonAnswers[4].transform.GetComponentInChildren<Text>().text = gamePhase[index].button5.text;
            buttonAnswers[5].transform.GetComponentInChildren<Text>().text = gamePhase[index].button6.text;

            index++;
        }
    }

    private void FinalGame()
    {
        StartCoroutine(FinalGameCO());

        isLastImage = true;
        Debug.Log("Finito");

        finalScoreText.text = FindObjectOfType<TotalScore>().totalScore.ToString();
        GameObject.Find("Btn_BackGame").SetActive(false);
    }

    private IEnumerator FinalGameCO()
    {
        canvasGroup.blocksRaycasts = true;

        while (canvasGroup.alpha < 0.9f)
        {
            canvasGroup.alpha += Time.deltaTime / 1.5f;
            yield return null;
        }

        sourceFSX.PlayOneShot(clipVictory);
        canvasGroup.alpha = 0.9f;
    }

    internal void ColorAllButtons()
    {
        for (int i = 0; i < buttonAnswers.Length; i++)
            StartCoroutine(ColorButtonCO(buttonAnswers[i]));
    }

    internal void PlayAnswerSound(ButtonAnswer _buttonAnswer)
    {
        if (_buttonAnswer.score.Equals(0))
            sourceFSX.PlayOneShot(wrongAnswer);

        else if (_buttonAnswer.score.Equals(5))
            sourceFSX.PlayOneShot(rightAnswer);

        else if (_buttonAnswer.score.Equals(10))
            sourceFSX.PlayOneShot(rightAnswer);
    }
    
    private IEnumerator ColorButtonCO(ButtonAnswer _buttonAnswer)
    {
        yield return new WaitForSecondsRealtime(0.8f);

        Color newColor = Color.white;

        if (_buttonAnswer.score.Equals(0))
            newColor = Color.red;

        else if (_buttonAnswer.score.Equals(5))
            newColor = Color.yellow;

        else if (_buttonAnswer.score.Equals(10))
            newColor = Color.green;
            
        Image buttonImage = _buttonAnswer.GetComponent<Image>();
        Color startColor = buttonImage.color;

        buttonImage.color = newColor;

        yield return new WaitForSecondsRealtime(3f);

        float step = 0;

        while (step < 1)
        {
            step += Time.deltaTime / 2;
            buttonImage.color = Color.Lerp(newColor, startColor, step);
            yield return null;
        }
    }
}