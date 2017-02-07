using UnityEngine;
using System.Collections;

public class GridGenerator : MonoBehaviour
{
    public GameObject sprite;
    private int[,] grid = new int[3, 3];
    
    private void Start()
    {
        GameObject parent = new GameObject("Sprite");
        parent.transform.position = Vector3.zero;

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                GameObject newcell = Instantiate(sprite);
                newcell.transform.position = new Vector3(j * 3f, -i *3);
                newcell.name = "Cella " + i + " " + j;
                newcell.transform.SetParent(parent.transform);
            }
        }
    }
}