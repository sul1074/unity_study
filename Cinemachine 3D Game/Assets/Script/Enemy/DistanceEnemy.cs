using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceEnemy : MonoBehaviour
{
    private float hp = 22f;
    private float damage = 10f;
    public GameObject player;
    public GameObject weapon;

    EnemyBulletGenerator enemyBulletGenerator;
    BoxCollider boxCollider;
    Slider hpSlider;
    Material mat;
    AudioSource audio;
    Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        enemyBulletGenerator = GetComponentInChildren<EnemyBulletGenerator>();
        boxCollider = GetComponent<BoxCollider>();
        hpSlider = GetComponentInChildren<Slider>();
        StartCoroutine("FireBullet");
        mat = GetComponent<MeshRenderer>().material;
        originalColor = mat.color;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);

        if (hpSlider.value <= 0)
        {
            player.GetComponent<PlayerController>().setMoney(75);
            Destroy(gameObject);
        }
    }

    IEnumerator FireBullet()
    {
        while(true)
        {
            enemyBulletGenerator.fire();

            yield return new WaitForSeconds(Random.Range(4.0f, 7.0f));
        }
    }

    public IEnumerator beAttacked()
    {
        mat.color = Color.red;
        audio.Play();
        yield return new WaitForSeconds(0.1f);
        boxCollider.enabled = false;

        yield return new WaitForSeconds(0.2f);
        boxCollider.enabled = true;
        mat.color = originalColor;

        hpSlider.value -= weapon.GetComponent<Weapon>().getDamage() / hp;
    }

    public float getDamage()
    {
        return damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            StartCoroutine("beAttacked");
        }

        else if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().isHit(damage);
        }
    }
}
