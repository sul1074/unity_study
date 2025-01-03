using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    private int remainBullet = 7;
    private int remainTargets = 4;

    public GameObject map3Trigger;
    public GameObject bulletPrefab;
    public GameObject bulletGenerator;
    public GameManager gameManager;
    public GameObject[] flyingTargets;

    BoxCollider boxCollider;
    Transform generatorTransform;
    Text remainBulletText;

    // Start is called before the first frame update
    void Start()
    {
        generatorTransform = bulletGenerator.transform;
        remainBulletText = GetComponentInChildren<Text>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(remainTargets == 0)
        {
            map3Trigger.GetComponent<Map3Trigger>().setWin();
        }

       
    }

    public IEnumerator isHit()
    {
        boxCollider.enabled = false;

        yield return new WaitForSeconds(0.1f);
        boxCollider.enabled = true;
    }

   public void Fire()
   {
        if (remainBullet == 0)
        {
            checkEnd();
        }

        else
        {
            Instantiate(bulletPrefab, generatorTransform.position, generatorTransform.rotation);
            remainBullet--;
        }

        remainBulletText.text = remainBullet.ToString();
    }

    void checkEnd()
    {
        if (remainTargets != 0)
        {
            for (int i = 0; i < 4; i++)
            {
                flyingTargets[i].SetActive(true);
            }
            initRemainBullet();
            remainTargets = 4;
            gameManager.GetComponent<GameManager>().setHpValue(15);
        }
    }

    public void initRemainBullet()
    {
        remainBullet = 7;
    }


    public void setRemainTarget()
    {
        remainTargets--;
    }

}
