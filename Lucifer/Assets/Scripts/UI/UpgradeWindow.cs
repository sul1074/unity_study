using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWindow : MonoBehaviour
{
    private GameObject[] upgradeSlots;  // 3���� ���׷��̵� ���� UI ��� (��: ��ư ��)
    private Weapon[] selectedWeapons;

    void Start()
    {
        List<GameObject> childObjectsList = new List<GameObject>();

        foreach (Transform child in transform)
        {
            childObjectsList.Add(child.gameObject);
        }

        upgradeSlots = childObjectsList.ToArray();
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    // ���׷��̵� â�� ���� �ɼ��� ǥ��
    public void ShowUpgradeOptions(Weapon[] weapons)
    {
        selectedWeapons = weapons;
        for (int i = 0; i < upgradeSlots.Length; i++)
        {
            if (i < selectedWeapons.Length)
            {
                upgradeSlots[i].SetActive(true);

                // ���Կ� ���� �̸��� ���� ǥ��
                Text upgradeInfo = upgradeSlots[i].GetComponentInChildren<Text>();
                upgradeInfo.text = selectedWeapons[i].WeaponName + "\n\n" + selectedWeapons[i].WeaponInfo;

                // �� ���Կ� Ŭ�� �̺�Ʈ �߰�
                int index = i;  // Ŭ���� ���� ����
                Button button = upgradeSlots[i].GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.RemoveAllListeners();  // ���� ������ ����
                    button.onClick.AddListener(() => OnUpgradeSelected(index));  // ���׷��̵� ���� ó��
                }
            }
            // ���Ⱑ �� ���� ��� �� ���Կ� ���� ó��
            else
            {
                upgradeSlots[i].SetActive(false);
            }
        }
    }

    private void OnUpgradeSelected(int index)
    {
        // ���õ� ���� ���׷��̵�
        GameManager.Instance.UpgradeWeapon(selectedWeapons[index]);
        gameObject.SetActive(false);
    }
}

