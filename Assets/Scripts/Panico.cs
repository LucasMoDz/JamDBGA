using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Panico : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }
}
