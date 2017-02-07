using UnityEngine;
using System.Collections.Generic;

public class RemoveCard : MonoBehaviour
{
    public List<GameObject> cardList = new List<GameObject>();
    private GameObject cardsParent;

    private Multiplier multiplier;
    private TimeBar timeBar;

    private bool isFirstCard = true;

    private void Awake()
    {
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

        foreach (var card in cardList)
            card.SetActive(true);

        isFirstCard = true;
    }

    // TO TEST
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ResetList();
    }

    public void RemovingCard()
    {
        if (cardList.Count == 0)
            return;

        if (isFirstCard)
        {
            isFirstCard = false;
            timeBar.StartTimeBar();
        }
        else
            multiplier.DecreaseMultiplier();

        int randomIndex = Random.Range(0, cardList.Count);
        cardList[randomIndex].gameObject.SetActive(false);
        cardList.RemoveAt(randomIndex);
    }
}