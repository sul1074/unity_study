using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetBullet : MonoBehaviour
{
    Rigidbody rigid;
    Vector3 vecDirection;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3.0f);
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.AddForce(vecDirection * 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().beAttacked();
            Destroy(gameObject);
        }

        else if(collision.gameObject.tag == "DistanceEnemy")
        {
            collision.gameObject.GetComponent<DistanceEnemy>().beAttacked();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyController>().beAttacked();
            Destroy(gameObject);
        }

        else if (other.gameObject.tag == "DistanceEnemy")
        {
            other.gameObject.GetComponent<DistanceEnemy>().beAttacked();
            Destroy(gameObject);
        }
    }

    public void setVecDirection(Vector3 vec)
    {
        vecDirection = vec - vecDirection;
        vecDirection = vecDirection.normalized;
    }
}
