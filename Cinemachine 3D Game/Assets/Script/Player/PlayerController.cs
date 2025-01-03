using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f; // 이동 속도 지정
    public float rotationSpeed = 360f; // 회전 속도 지정
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
            // 좌우 방향키와 상하 방향키를 눌렀는지 판별
            direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            direction = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up) * direction;
            direction = direction.normalized;

            if (direction.sqrMagnitude > 0.01f)
            {
                Vector3 forward = Vector3.Slerp( // 메소드를 조합해 플레이어의 방향 변환
                transform.forward,
                direction,
                rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction)
                );
                transform.LookAt(transform.position + forward);

                animator.SetBool("isWalking", true);
                playerSound.moveSound();

                // 질주
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

            // 걷기 해제
            else
            {
                animator.SetBool("isWalking", false);
            }

            // 점프
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

        // Move()를 이용해 이동, 충돌 처리, 속도 값 얻기 가능
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
