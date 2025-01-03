using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private int damage = 10;
    private float rate = 0.7f;
    public BoxCollider meleeArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void attack()
    {
        StopCoroutine("Swing");
        StartCoroutine("Swing");
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.4f);
        meleeArea.enabled = false;     
    }

    public float getRate()
    {
        return rate;
    }

    public int getDamage()
    {
        return damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Shooter")
        {
            other.GetComponent<Shooter>().Fire();
            other.GetComponent<Shooter>().isHit();
        }
    }

    public void enhanceDamage()
    {
        damage += 2;
    }
}
