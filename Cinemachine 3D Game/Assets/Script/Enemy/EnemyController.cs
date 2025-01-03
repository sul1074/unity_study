using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    float hp = 13.0f;
    private float damage = 10.0f;
    bool chaseTrigger = false;
    public GameObject weapon;
    public GameObject target;

    BoxCollider boxCollider;
    NavMeshAgent agent;
    Slider hpSlider;
    Rigidbody rigid;
    Material mat;
    AudioSource audio;
    
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        hpSlider = GetComponentInChildren<Slider>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponent<MeshRenderer>().material;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chaseTrigger)
        {
            startChase();
        }
        if (hpSlider.value <= 0)
        {
            target.GetComponent<PlayerController>().setMoney(50);
            Destroy(gameObject);
        }
    }

   public IEnumerator beAttacked()
   {
        Vector3 direction = new Vector3(weapon.transform.position.x, 0, weapon.transform.position.z);
        direction += Vector3.up;

        rigid.AddForce(direction.normalized * 7.5f, ForceMode.Impulse);
        mat.color = Color.red;
        audio.Play();

        yield return new WaitForSeconds(0.1f);
        boxCollider.enabled = false;

        yield return new WaitForSeconds(0.2f);
        boxCollider.enabled = true;
        mat.color = Color.green;

        hpSlider.value -= weapon.GetComponent<Weapon>().getDamage() / hp;
    }

    void startChase()
    {
        agent.destination = target.transform.position;
        boxCollider.enabled = true;
    }

    public void setChaseTrigger()
    {
        chaseTrigger = true;
    }

    public float getDamage()
    {
        return damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sword")
        {
            StartCoroutine("beAttacked");
        }

        else if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().isHit(damage);
        }
    }
}
