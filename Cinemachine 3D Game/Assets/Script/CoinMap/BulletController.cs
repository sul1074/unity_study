using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody rigid;
    Vector3 bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        bulletSpeed = new Vector3(33, 0, 0);
        Destroy(gameObject, 7.0f);
    }

    // Update is called once per frame
    void Update()
    {
        rigid.AddForce(bulletSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "FlyingTarget")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<FlyingTarget>().setN();
            collision.gameObject.SetActive(false);
        }
    }
}
