using UnityEngine;
using System.Collections;

public class FadeInCanvasGroup : MonoBehaviour
{
    private CanvasGroup myCanvasGroup;

    private void Awake()
    {
        myCanvasGroup = this.GetComponent<CanvasGroup>();
    }

    private IEnumerator Start()
    {
        while (myCanvasGroup.alpha > 0)
        {
            myCanvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }

        myCanvasGroup.alpha = 0;
    }
}