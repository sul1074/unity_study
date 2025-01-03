using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map3Trigger : MonoBehaviour
{
    public GameObject teleportSpot;
    public GameObject coin;
    private bool win = false;
    Vector3 teleportPos;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(win)
        {
            coin.SetActive(true);
            win = false;
            transform.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            teleportPos = teleportSpot.transform.position;
            teleportPos.y += 1.0f;
            other.GetComponent<PlayerController>().teleportPlayer(teleportPos);
        }
    }

    public void setWin()
    {
        win = true;
    }
}
