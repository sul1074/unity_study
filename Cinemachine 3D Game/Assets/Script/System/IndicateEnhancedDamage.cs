using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicateEnhancedDamage : MonoBehaviour
{
    Text damageText;
    public GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        damageText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        damageText.text = "���ݷ� ��ȭ(" + weapon.GetComponent<Weapon>().getDamage().ToString() + "->"+ (weapon.GetComponent<Weapon>().getDamage() + 2).ToString() + ")\n(200��)";
    }
}
