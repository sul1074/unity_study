using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingTarget : MonoBehaviour
{
    private int speed = 15;
    public GameObject shooter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "SafetyWall")
        {
            speed = -speed;
        }
    }

    public void setN()
    {
        shooter.GetComponent<Shooter>().setRemainTarget();
    }
}
