//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;
//using System.IO;

//public class Select : MonoBehaviour
//{
//    public GameObject creat;	// �÷��̾� �г��� �Է�UI
//    public Text[] slotText;		// ���Թ�ư �Ʒ��� �����ϴ� Text��
//    public Text newPlayerName;	// ���� �Էµ� �÷��̾��� �г���

//    bool[] savefile = new bool[3];	// ���̺����� �������� ����

//    void Start()
//    {
//        ���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�.
//        for (int i = 0; i < 3; i++)
//        {
//            if (File.Exists(DataManager.Instance.path + $"{i}"))	// �����Ͱ� �ִ� ���
//            {
//                savefile[i] = true;			// �ش� ���� ��ȣ�� bool�迭 true�� ��ȯ
//                DataManager..nowSlot = i;	// ������ ���� ��ȣ ����
//                DataManager.Instance.LoadData();	// �ش� ���� ������ �ҷ���
//                slotText[i].text = DataManager.Instance.nowPlayer.name;	// ��ư�� �г��� ǥ��
//            }
//            else	// �����Ͱ� ���� ���
//            {
//                slotText[i].text = "�������";
//            }
//        }
//        �ҷ��� �����͸� �ʱ�ȭ��Ŵ.(��ư�� �г����� ǥ���ϱ������̾��� ����)
//        DataManager.Instance.DataClear();
//    }

//    public void Slot(int number)	// ������ ��� ����
//    {
//        DataManager.Instance.nowSlot = number;	// ������ ��ȣ�� ���Թ�ȣ�� �Է���.

//        if (savefile[number])	// bool �迭���� ���� ���Թ�ȣ�� true��� = ������ �����Ѵٴ� ��
//        {
//            DataManager.Instance.LoadData();	// �����͸� �ε��ϰ�
//            GoGame();	// ���Ӿ����� �̵�
//        }
//        else	// bool �迭���� ���� ���Թ�ȣ�� false��� �����Ͱ� ���ٴ� ��
//        {
//            Creat();	// �÷��̾� �г��� �Է� UI Ȱ��ȭ
//        }
//    }

//    public void Creat()	// �÷��̾� �г��� �Է� UI�� Ȱ��ȭ�ϴ� �޼ҵ�
//    {
//        creat.gameObject.SetActive(true);
//    }

//    public void GoGame()	// ���Ӿ����� �̵�
//    {
//        if (!savefile[DataManager.Instance.nowSlot])	// ���� ���Թ�ȣ�� �����Ͱ� ���ٸ�
//        {
//            DataManager.Instance.nowPlayer.name = newPlayerName.text; // �Է��� �̸��� �����ؿ�
//            DataManager.Instance.SaveData(); // ���� ������ ������.
//        }
//        SceneManager.LoadScene(1); // ���Ӿ����� �̵�
//    }
//}