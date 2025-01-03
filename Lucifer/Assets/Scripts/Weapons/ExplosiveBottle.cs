using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBottle : Weapon
{
    private GameObject explosionPrefab;

    private bool explosionTrigger = false;

    private float timeToExplode = 2f;
    private float currTime = 0f;

    public override void Upgrade() 
    { 
        Damage = Damage + 5f;
        WeaponInfo = "��� �� �����ϸ� " + Damage.ToString() + "��ŭ�� �������� ��";
    }

    // Start is called before the first frame update
    void Start()
    {
        Damage = 10f;
        WeaponName = "�����ϴ� ��";
        WeaponInfo = "��� �� �����ϸ� " + Damage.ToString() + "��ŭ�� �������� ��";
        explosionPrefab = GameManager.Instance.GetExplosionPrefab();
    }

    void Explode()
    {
        // �� ���� �����ϵ���
        if (explosionTrigger) return;

        // ���� �ִϸ��̼� ����
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosion.GetComponent<Explosion>().Damage = Damage;

        Destroy(this.gameObject, 0.1f);
        explosionTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currTime >= timeToExplode)
        {
            Explode();
        } 
        currTime += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            Explode(); 
        }
    }
}
