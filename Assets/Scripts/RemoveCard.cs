using UnityEngine;
using System.Collections.Generic;

public class RemoveCard : MonoBehaviour
{
    private GameManager gameManager;
    private RemoveCard removeCardButton;

    public List<GameObject> cardList = new List<GameObject>();
    private GameObject cardsParent;

    private Multiplier multiplier;
    private TimeBar timeBar;

    internal bool canSkip;
    internal bool skip;

    internal bool isClicked = true;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        cardsParent = GameObject.Find("Cards");
        multiplier = FindObjectOfType<Multiplier>();
        timeBar = FindObjectOfType<TimeBar>();
        
        if (cardsParent == null)
            Debug.LogError("\"Cards\" not found\n");

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
            skip = true;

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

    private void ResetSkip()
    {
        canSkip = false;
        skip = false;
    }
}