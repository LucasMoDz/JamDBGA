using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RemoveCard : MonoBehaviour
{
    private Text myText;

    private GameManager gameManager;
    private RemoveCard removeCardButton;

    public List<GameObject> cardList = new List<GameObject>();
    private GameObject cardsParent;

    private Multiplier multiplier;
    private TimeBar timeBar;

    internal bool canSkip;
    internal bool skip;

    internal bool isClicked = true;

    private bool nextButtonIsClicked;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        cardsParent = GameObject.Find("Cards");
        multiplier = FindObjectOfType<Multiplier>();
        timeBar = FindObjectOfType<TimeBar>();
        
        if (cardsParent == null)
            Debug.LogError("\"Cards\" not found\n");

        myText = this.GetComponentInChildren<Text>();

        InitialiseList();
    }

    private void InitialiseList()
    {
        for (int i = 0; i < cardsParent.transform.childCount; i++)
            cardList.Add(cardsParent.transform.GetChild(i).gameObject);
    }

    public void ResetList()
    {
        cardList.Clear();
        InitialiseList();

        multiplier.ResetCurrentMultiplier();

        if (!gameManager.isLastImage)
        {
            foreach (var card in cardList)
                card.SetActive(true);
        }
        
        isClicked = true;
    }
    
    public void RemovingCard()
    {
        if (gameManager.feedbackIsActive && canSkip)
        {
            skip = true;
            nextButtonIsClicked = true;
        }
           
        else if (cardList.Count > 1 && !gameManager.feedbackIsActive)
        {
            if (isClicked)
            {
                isClicked = false;
                ResetSkip();
                timeBar.StartTimeBar();
            }
            else
                multiplier.DecreaseMultiplier();

            int randomIndex = Random.Range(0, cardList.Count);
            cardList[randomIndex].gameObject.SetActive(false);
            cardList.RemoveAt(randomIndex);
        }
    }

    public void SetText()
    {
        nextButtonIsClicked = false;
        myText.text = "Next";
        StartCoroutine(SetTextCO(myText));
    }

    private IEnumerator SetTextCO(Text _text)
    {
        float step = 0;
        
        Quaternion currentRotation = _text.transform.rotation;
        
        Vector3 currentScale = Vector3.one;
        Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f);
        Quaternion targetRotation;

        while (!NextButtonIsClicked())
        {
            int randomValue = Random.Range(1, 101);
            targetRotation = currentRotation * new Quaternion(0, 0, randomValue > 50 ? 0.2f : -0.2f, 1);

            while (step < 1)
            {
                step += Time.deltaTime / 1.5f;
                _text.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, step);
                _text.transform.localScale = Vector3.Lerp(currentScale, targetScale, step);
                yield return null;
            }

            while (step > 0)
            {
                step -= Time.deltaTime / 0.5f;
                _text.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, step);
                _text.transform.localScale = Vector3.Lerp(currentScale, targetScale, step);
                yield return null;
            }

            yield return null;
        }

        gameManager.feedbackIsActive = false;
        myText.text = "Break";
    }

    private bool NextButtonIsClicked()
    {
        if (nextButtonIsClicked)
            return true;

        return false;
    }

    private void ResetSkip()
    {
        canSkip = false;
        skip = false;
    }
}