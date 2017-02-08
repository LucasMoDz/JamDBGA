using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public int score = 0;
    private int savedScore;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerPrefs.SetInt("Score", score);
            Debug.Log(score);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            savedScore = PlayerPrefs.GetInt("Score");
            Debug.Log(savedScore);
        }
    }
}