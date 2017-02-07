using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private ButtonAnswer[] buttonAnswers = new ButtonAnswer[6];

    private Image image;

    private byte index = 0;
    internal bool feedbackIsActive = false;

    public NewPhase[] gamePhase;
    
    [System.Serializable]
    public struct NewPhase
    {
        public Image image;

        public int scoreA;
        public int scoreB;
        public int scoreC;
        public int scoreD;
        public int scoreE;
        public int scoreF;
    }

    private void Awake()
    {
        image = GameObject.Find("Image").GetComponent<Image>();

        if (image == null)
            Debug.LogError("Image not found");

        Transform answers = GameObject.Find("Answers").transform;

        for (int i = 0; i < answers.childCount; i++)
            buttonAnswers[i] = answers.GetChild(i).GetComponent<ButtonAnswer>();
    }

    public void IncreasePhase()
    {
        image.sprite = gamePhase[index].image.sprite;

        buttonAnswers[0].score = gamePhase[index].scoreA;
        buttonAnswers[1].score = gamePhase[index].scoreB;
        buttonAnswers[2].score = gamePhase[index].scoreC;
        buttonAnswers[3].score = gamePhase[index].scoreD;
        buttonAnswers[4].score = gamePhase[index].scoreE;
        buttonAnswers[5].score = gamePhase[index].scoreF;

        index++;
    }
}