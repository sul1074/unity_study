using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private float damage = 10f;
    private int speed = 20;

    GameObject player;
    Rigidbody rigid;

    Vector3 vecTarget;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        vecTarget = player.transform.position - this.transform.position;
        vecTarget = vecTarget.normalized;
        rigid = GetComponent<Rigidbody>();
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        rigid.AddForce(vecTarget * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().isHit(damage);
            Destroy(this.gameObject);
        }
    }
}
