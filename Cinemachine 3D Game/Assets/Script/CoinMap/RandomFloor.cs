using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFloor : MonoBehaviour
{
    public GameObject[] randomFloors;

    // Start is called before the first frame update
    void Start()
    {
        setRandomFloor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setRandomFloor()
    {
        int randomInt = Random.Range(1, 10);

        if (randomInt % 2 == 0)
        {
            randomFloors[0].tag = "Fragile";
        }

        else randomFloors[1].tag = "Fragile";
    }
}
