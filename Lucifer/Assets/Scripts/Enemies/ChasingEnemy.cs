using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : Enemy
{
    private Vector3 playerPosition;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        Hp = 10f;
        MoveSpeed = 1f;
        Damage = 2f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        ChasePlayer();
    }
    protected void ChasePlayer()
    {
        // Player ��ġ ��������
        playerPosition = GameManager.Instance.GetPlayer().transform.position;
        // Player ������ �ٰ����� ���� ���⺤�� ���ϱ�
        direction = (playerPosition - transform.position).normalized;
        // ���� ���⺤�͸� ���� �÷��̾� �߰��ϱ�
        transform.position += direction * MoveSpeed * Time.deltaTime;
    }
}
