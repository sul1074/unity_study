using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicateInfo : MonoBehaviour
{
    float remainTime;   
    private bool noMoney = false;
    public GameObject money;
    public GameObject remainCoin;
    public GameObject timer;
    public GameObject player;
    public GameManager gm;

    Text moneyText;
    Text remainCoinText;
    Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        moneyText = money.GetComponent<Text>();
        remainCoinText = remainCoin.GetComponent<Text>();
        timerText = timer.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        remainTime = gm.getRemainTime();
        int min = (int)remainTime / 60;
        int sec = (int)remainTime % 60;
        timerText.text = min.ToString() + " : " + sec.ToString();

        if(noMoney == false)
            moneyText.text = "Money: " + player.GetComponent<PlayerController>().getMoney().ToString();
        else
        {
            StartCoroutine("blinking");
            noMoney = false;
        }
    }

    public void indicateNoMoney()
    {
        noMoney = true;
    }

    public void indicateCoin()
    {
        remainCoinText.text = "Coin " + gm.getCoinNum().ToString() + " / 4";
    }

    IEnumerator blinking()
    {
        moneyText.color = Color.red;

        yield return new WaitForSeconds(0.1f);
        moneyText.color = Color.green;

        yield return new WaitForSeconds(0.2f);
        moneyText.color = Color.red;

        yield return new WaitForSeconds(0.3f);
        moneyText.color = Color.green;
    }
}
