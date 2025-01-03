using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int coinNum = 4;
    private float remainTime = 4f * 60f;

    public Slider hpSlider;
    public Slider staminaSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        remainTime -= Time.deltaTime;

        if (coinNum == 0)
        {
            SceneManager.LoadScene("GameClear");
        }

        if(remainTime <= 0 || hpSlider.value <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void collectCoin() { coinNum--; }
    public void setHpValue(float damage) { hpSlider.value -= (damage * 0.01f); }
    public int getCoinNum()  { return coinNum;  }
    public float getRemainTime() { return remainTime; }
    public float getStaminaValue() { return staminaSlider.value; }
    public void consumeStamina()
    {
        staminaSlider.value -= 0.15f * Time.deltaTime;
    }

    public void healStamina()
    {
        if (staminaSlider.value < 1) staminaSlider.value += 0.2f * Time.deltaTime;
    }
}
