using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject weapon;
    public GameObject shopEntryCanvas;
    public GameObject shoppingListCanvas;
    public GameObject player;
    public GameObject systemCanvas;

    public GameObject shopEntryButton;
    public GameObject enhanceAttackButton;
    public GameObject healHpButton;
    public GameObject skillButton;
    public GameObject exitButton;

    public Slider hpSlider;

    PlayerController playerInfo;

    private int enhanceAttackCost = 200;
    private int healHpCost = 150;
    private int skillCost = 500;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enterShop()
    {
        shoppingListCanvas.SetActive(true);
        shopEntryCanvas.SetActive(false);
    }

    public void enhanceAttack()
    {
        if (playerInfo.getMoney() >= enhanceAttackCost)
        {
            weapon.GetComponent<Weapon>().enhanceDamage();
            playerInfo.setMoney(-enhanceAttackCost);
            buySound();
        }

        else
        {
            indicateNoMoney();
            noMoneySound();
        }
    }
    public void healHP()
    {
        if (playerInfo.getMoney() >= healHpCost && hpSlider.value != 1)
        {
            hpSlider.value += 0.3f;
            playerInfo.setMoney(-healHpCost);
            buySound();
        }

        else
        {
            indicateNoMoney();
            noMoneySound();
        }
    }
    public void purchaseSkill()
    {

    }

    public void exitShop()
    {
        shoppingListCanvas.SetActive(false);
    }

    void indicateNoMoney()
    {
        systemCanvas.GetComponent<IndicateInfo>().indicateNoMoney();
    }

    void buySound()
    {
        player.GetComponent<PlayerSound>().buySound();
    }

    void noMoneySound()
    {
        player.GetComponent<PlayerSound>().noMoneySound();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            shopEntryCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            shopEntryCanvas.SetActive(false);
            shoppingListCanvas.SetActive(false);
        }
    }

}
