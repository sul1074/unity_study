using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyBullet : MonoBehaviour
{
    private float shotSpeed = 3f;
    private Vector3 direction;
    private float damage = 2f;

    private void Start()
    {
        Destroy(this.gameObject, 3f);

        // ó���� �÷��̾� �������� �ʱ�ȭ
        UpdateDirection();
        // ���� ������Ʈ �ڷ�ƾ ����
        StartCoroutine(UpdateDirectionCoroutine());
    }

    private void Update()
    {
        // ���⿡ ���� �̵�
        transform.position += direction * shotSpeed * Time.deltaTime;
    }

    // �÷��̾� ��ġ�� ������� ���� ������Ʈ
    private void UpdateDirection()
    {
        Vector3 playerPosition = GameManager.Instance.GetPlayer().transform.position;
        Vector3 newDirection = (playerPosition - transform.position).normalized;

        // ��Ȯ���� ���߱� ���� �ణ�� ���������� �߰�
        Vector3 offset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
        newDirection += offset;
        newDirection.Normalize();

        // ���� ����� ���ο� ������ ���ݾ� ��� �ε巯�� ȸ�� ȿ�� �߰�
        direction = Vector3.Lerp(direction, newDirection, 0.1f);
    }

    // ���� �ð����� ������ ������Ʈ�ϴ� �ڷ�ƾ
    private IEnumerator UpdateDirectionCoroutine()
    {
        while (true)
        {
            UpdateDirection();
            yield return new WaitForSeconds(0.2f);  // 0.2�� �������� ������Ʈ
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GetPlayer().GetComponent<Player>().BeAttacked(damage);
            if (this.gameObject != null) Destroy(this.gameObject);
        }
    }
}
