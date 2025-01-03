using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : Weapon
{
    private CircleCollider2D circleCollider2D;
    private Dictionary<GameObject, Coroutine> enemyDict = new Dictionary<GameObject, Coroutine>();

    private float shockPeriod = 0.75f;
    private float originalRadius;
    public override void Upgrade()
    { 
        Damage = Damage + 1f;
        transform.localScale = new Vector3(transform.localScale.x * 1.2f, transform.localScale.y * 1.2f, 1); // Ʈ������ ũ�� �ø��� �ݶ��̴��� �ڵ����� Ŀ������
        WeaponInfo = "�������� ������ �ֱ������� " + Damage.ToString() + "��ŭ�� �������� ��";
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Damage = 1f;
        WeaponName = "������";
        WeaponInfo = "�������� ������ �ֱ������� " + Damage.ToString() + "��ŭ�� �������� ��";
        circleCollider2D = GetComponent<CircleCollider2D>();
        originalRadius = circleCollider2D.radius;
    }

    IEnumerator Shock(GameObject enemy)
    {
        while (enemy != null)
        {
            enemy.GetComponent<Enemy>().BeAttacked(Damage);
            yield return new WaitForSeconds(shockPeriod);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !enemyDict.ContainsKey(collision.gameObject))
        {
            // ������ ���� ���� ���� ���鿡 ���� �ǰ� ó���� ����, �� ���� ���� ���� �ڷ�ƾ ����
            Coroutine shockCoroutine = StartCoroutine(Shock(collision.gameObject));
            enemyDict.Add(collision.gameObject, shockCoroutine);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && enemyDict.ContainsKey(collision.gameObject))
        {
            // ���� ������ ����� �ش� �ڷ�ƾ ����
            StopCoroutine(enemyDict[collision.gameObject]);
            enemyDict.Remove(collision.gameObject);
        }
    }
}
