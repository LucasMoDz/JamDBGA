using UnityEngine;
using UnityEngine.UI;

public class Multiplier : MonoBehaviour
{
    internal byte MAX_MULTIPLIER = 4;
    internal byte currentMultiplier;

    private Text multiplierText;

    private void Awake()
    {
        multiplierText = this.GetComponent<Text>();
        ResetCurrentMultiplier();
    }

    internal void ResetCurrentMultiplier()
    {
        currentMultiplier = MAX_MULTIPLIER;
        multiplierText.text = "x " + MAX_MULTIPLIER;
    }

    internal void DecreaseMultiplier()
    {
        if (currentMultiplier <= 1)
            return;

        currentMultiplier--;
        multiplierText.text = "x " + currentMultiplier.ToString();
    }
}