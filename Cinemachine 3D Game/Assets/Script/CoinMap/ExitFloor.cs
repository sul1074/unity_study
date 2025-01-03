using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitFloor : MonoBehaviour
{
    public GameObject entry;
    Vector3 exitPos;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        exitPos = entry.transform.position;
        exitPos.y += 1.0f;

        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().teleportPlayer(exitPos);
        }
    }
}
