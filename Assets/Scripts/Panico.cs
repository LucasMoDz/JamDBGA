using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Panico : MonoBehaviour
{
    public GameObject panelMenù;
    public GameObject panelRanks;
    public GameObject panelCredits;

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Ranks()
    {
        panelMenù.SetActive(false);
        panelRanks.SetActive(true);
        panelCredits.SetActive(false);
    }

    public void Credits()
    {
        panelMenù.SetActive(false);
        panelRanks.SetActive(false);
        panelCredits.SetActive(true);
    }

    public void Menù()
    {
        panelMenù.SetActive(true);
        panelRanks.SetActive(false);
        panelCredits.SetActive(false);
    }
}
