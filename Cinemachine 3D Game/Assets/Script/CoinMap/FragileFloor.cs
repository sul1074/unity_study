using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileFloor : MonoBehaviour
{
    public GameObject checkPhysics;

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
        if(other.gameObject.tag == "Player" && gameObject.tag == "Fragile")
        {
            Destroy(checkPhysics.gameObject);
        }
    }
}
