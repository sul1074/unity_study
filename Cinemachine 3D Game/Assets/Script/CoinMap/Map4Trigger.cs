using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map4Trigger : MonoBehaviour
{
    public GameObject teleportSpot;
    Vector3 teleportPos;

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
        if (other.gameObject.tag == "Player")
        {
            teleportPos = teleportSpot.transform.position;
            teleportPos.y += 1.0f;
            other.GetComponent<PlayerController>().teleportPlayer(teleportPos);
        }
    }
}
