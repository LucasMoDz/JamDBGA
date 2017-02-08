using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PingPongScore : MonoBehaviour
{
    private TotalScore refScore;

    private float myAlpha = 1;
    private Text myText;
    private const float SECONDS = 1.5f;

    private void Awake()
    {
        myText = this.GetComponent<Text>();
        refScore = FindObjectOfType<TotalScore>();
    }

    internal void SetFinalScore()
    {
        myText.text = refScore.totalScore.ToString();
    }

    private IEnumerator Start()
    {
        float step = 0;
        
        Vector3 currentScale = Vector3.one;
        Vector3 targetScale = new Vector3(1.3f, 1.3f, 1.3f);

        while (true)
        {
            while (step < 1)
            {
                step += Time.deltaTime / SECONDS;
                myText.transform.localScale = Vector3.Lerp(currentScale, targetScale, step);
                yield return null;
            }

            while (step > 0)
            {
                step -= Time.deltaTime / SECONDS;
                myText.transform.localScale = Vector3.Lerp(currentScale, targetScale, step);
                yield return null;
            }

            yield return null;
        }
    }
}