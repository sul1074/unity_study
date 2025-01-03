using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public float attackDelay = 0;
    public GameObject bulletPrefab;
    public GameObject bg;
    public Vector3 vecEnemy;
    public Transform player;
    Vector3 originalPos;
    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        attackDelay += Time.deltaTime;
        Debug.Log(attackDelay);
    }

    public void fireToEnemy(Transform enemyTransform)
    {
        Vector3 vecFire = vecEnemy;
        transform.LookAt(enemyTransform);

        GameObject instantiatedObject = Instantiate(bulletPrefab, bg.transform.position, bg.transform.rotation);
        instantiatedObject.GetComponent<PetBullet>().setVecDirection(vecFire);
        attackDelay = 0;
    }
}
