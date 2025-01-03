using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMap1Trigger : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject[] startWall;
    public GameObject coin;
    BoxCollider boxCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        endGame();
    }

    public void startGame()
    {
        for (int i = 0; i < 4; i++)
        {
            enemy[i].SetActive(true);
        }

        for (int i = 0; i < 4; i++)
        {
            startWall[i].SetActive(true);
        }

        boxCollider.enabled = false;
    }

    void endGame()
    {
        for (int i = 0; i < 4; i++)
        {
            if (enemy[i] != null) return;
        }

        for (int j = 0; j < 4; j++)
        {
            startWall[j].SetActive(false);
        }

        coin.SetActive(true);
        gameObject.SetActive(false);
    }
}
