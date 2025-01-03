using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float damage = 0f; // ExplosiveBottle�� ���� ������ ������.
    Animator animator;
    public float Damage 
    {
        get { return damage; }
        set { damage = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("ExplosionDone"))
        {
            Destroy(gameObject);
        }
    }

    // Animation�� Event�� ���� ����
    public void setAnimExplosionDone()
    {
        animator.SetBool("ExplosionDone", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().BeAttacked(damage);
        }
    }
}
