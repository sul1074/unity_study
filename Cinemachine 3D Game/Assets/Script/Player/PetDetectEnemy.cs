using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDetectEnemy : MonoBehaviour
{
    public GameObject pet;

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
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "DistanceEnemy")
        {
            if (pet.GetComponent<PetController>().attackDelay > 1.5f)
            {
                pet.GetComponent<PetController>().vecEnemy = other.gameObject.transform.position;
                pet.GetComponent<PetController>().fireToEnemy(other.gameObject.transform);
            }
        }
    }
}
