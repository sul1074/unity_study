using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f; // �̵� �ӵ� ����
    public float rotationSpeed = 360f; // ȸ�� �ӵ� ����
    public float jumpPower = 4.5f;
    private bool isJumping = false;
    private int money = 0;
    bool isSprinting = false;
    float noSprintingTime;
    public GameObject gameManager;
    public GameObject cam;
    public IndicateInfo info;

    float fireDelay = 0;
    bool isFireReady = true;

    Vector3 direction;
    CharacterController characterController;
    Animator animator;
    Weapon weapon;
    GameManager gm;
    PlayerSound playerSound;

    void Start()
    {
        noSprintingTime = 0;

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();
        gm = gameManager.GetComponent<GameManager>();
        playerSound = GetComponent<PlayerSound>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // �¿� ����Ű�� ���� ����Ű�� �������� �Ǻ�
            direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            direction = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up) * direction;
            direction = direction.normalized;

            if (direction.sqrMagnitude > 0.01f)
            {
                Vector3 forward = Vector3.Slerp( // �޼ҵ带 ������ �÷��̾��� ���� ��ȯ
                transform.forward,
                direction,
                rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction)
                );
                transform.LookAt(transform.position + forward);

                animator.SetBool("isWalking", true);
                playerSound.moveSound();

                // ����
                if (Input.GetKey(KeyCode.LeftShift) && gm.getStaminaValue() > 0)
                {
                    moveSpeed = 9.0f;
                    animator.SetBool("isSprinting", true);
                    isSprinting = true;
                    gm.consumeStamina();
                    noSprintingTime = 0;
                }

                else
                {
                    moveSpeed = 5.0f;
                    animator.SetBool("isSprinting", false);
                    isSprinting = false;
                }
            }

            // �ȱ� ����
            else
            {
                animator.SetBool("isWalking", false);
            }

            // ����
            if (Input.GetButton("Jump") && isJumping == false)
            {
                direction.y = jumpPower;
                isJumping = true;
                animator.SetBool("isJumping", true);
                playerSound.jumpSound();
            }

            else animator.SetBool("isJumping", false);
        }

        else
        {
            isJumping = false;
        }

        Attack();

        direction.y -= 5.0f * Time.deltaTime;

        if (isSprinting == false) noSprintingTime += Time.deltaTime;

        if (noSprintingTime > 1.5f)
        {
            gm.healStamina();
        }

        // Move()�� �̿��� �̵�, �浹 ó��, �ӵ� �� ��� ����
        characterController.Move(direction * moveSpeed * Time.deltaTime);
    }

    void Attack()
    {
        fireDelay += Time.deltaTime;
        isFireReady = weapon.getRate() < fireDelay;

        if (Input.GetButton("Fire1") && isFireReady)
        {
            weapon.attack();
            animator.SetTrigger("doAttack");
            playerSound.attackSound();
            fireDelay = 0;
        }
    }

    public void teleportPlayer(Vector3 pos)
    {
        Vector3 teleportPos = pos;
        teleportPos.y += 1.0f;

        characterController.enabled = false;
        transform.position = pos;
        playerSound.teleportSound();
        characterController.enabled = true;
    }

    public void setMoney(int money)
    {
        this.money += money;
    }

    public int getMoney()
    {
        return money;
    }

    public void isHit(float enemyDamage)
    {
        gm.setHpValue(enemyDamage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            gm.collectCoin();
            Destroy(other.gameObject);
            money += 200;
            info.indicateCoin();
        }

        else if (other.gameObject.name == "StartTrigger")
        {
            other.gameObject.GetComponent<StartTrigger>().startGame();
        }

        else if(other.gameObject.name == "Map1StartTrigger")
        {
            other.gameObject.GetComponent<CoinMap1Trigger>().startGame();
        }
    }
}
