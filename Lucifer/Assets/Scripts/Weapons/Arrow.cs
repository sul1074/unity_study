using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{
    // Start is called before the first frame update
    private GameObject enemy;
    private Vector3 moveDirection;

    private float fireSpeed = 5.0f;
    
    public override void Upgrade()
    {
        Damage = Damage + 2;
        WeaponInfo = "���� �����Ͽ� " + Damage.ToString() + "��ŭ�� �������� ��";
    }

    void Start()
    {
        Damage = 5f;
        WeaponName = "����ȭ��";
        WeaponInfo = "���� �����Ͽ� " + Damage.ToString() + "��ŭ�� �������� ��";
        enemy = GameObject.FindWithTag("Enemy");
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        ChaseEnemy();
    }
    void ChaseEnemy()
    {
        // ������ �߰��ϴ� ���� ������, ���ο� ���� ã��
        if (enemy == null) {
            enemy = GameObject.FindWithTag("Enemy");
        }

        if (enemy != null)
        {
            moveDirection = (enemy.transform.position - transform.position).normalized;
            transform.position += moveDirection * fireSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().BeAttacked(Damage);
            Destroy(this.gameObject);
        }
    }
}
