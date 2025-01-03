using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const int PHASE_CONDITION_NUM = 10;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);  // �ߺ��� �ν��Ͻ��� ����
        }
    }

    private void Update()
    {
        
    }

    // varient related-game system
    private bool bossPhase = false;
    private int numOfKilledEnemies = 0;

    // Prefab Manager. Prefab Manager�� ���� �� ��� �����鿡 ������ �� �����Ƿ�, ���ȼ��� ��ȭ�ϱ� ���� GameManager�� ���ؼ��� ������ �� �ְ� ��.
    // ���� ���� get�� �� ���� �����ν�, �����鿡 �����Ϸ��� ���� �Ŵ��������� �Լ��� ���� �����ؾ� ��.
    private PrefabManager prefabManager;

    // game system related
    private int money = 0;
    private GameObject player;
    private WeaponSpawner weaponSpawner;
    private EnemySpawner enemySpawner;

    // getters related-game progress
    public int NumOfKilledEnemies
    {
        get { return numOfKilledEnemies; }
        set { numOfKilledEnemies = value; }
    }

    public int Money
    {
        get { return money; }
        set 
        {   money = value;
            UIManager.Instance.UpdateMoneyText(money);
        }
    }

    // getters related-player
    public GameObject GetPlayer() { return player; }
    public WeaponSpawner GetWeaponSpawener() { return weaponSpawner; }

    // getters related-prefabs
    // related-game system
    public GameObject GetExpPrefab() { return prefabManager.GetExpPrefab(); }

    // related-weapons
    public GameObject GetArrowPreafb() { return prefabManager.GetArrowPreafb(); }
    public GameObject GetMagneticFieldPrefab() { return prefabManager.GetMagneticFieldPrefab(); }
    public GameObject GetExplosiveBottlePrefab() { return prefabManager.GetExplosiveBottlePrefab(); }
    public GameObject GetExplosionPrefab() { return prefabManager.GetExplosionPrefab(); }

    // related-enemies
    public GameObject GetChasingEnemyPrefab() { return prefabManager.GetChasingEnemyPrefab(); }
    public GameObject GetRangeEnemyPrefab() { return prefabManager.GetRangeEnemyPrefab(); }
    public GameObject GetRangeEnemyBulletPrefab() { return prefabManager.GetRangeEnemyBulletPrefab(); }
    public GameObject GetSlimeBossPrefab() { return prefabManager.GetSlimeBossPrefab(); }

    // functions related-game system
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
            InitMainScene();
        }
        else if (scene.name == "Title")
        {
            InitTitleScene();
        }
    }

    private void InitMainScene()
    {
        NumOfKilledEnemies = 0;
        ObjPool.Instance.InitObjPool();

        prefabManager = transform.Find("PrefabManager").GetComponent<PrefabManager>();
        if (prefabManager == null) { Debug.LogError("Can't find prefabManager"); }

        player = GameObject.FindWithTag("Player");
        if (player == null) { Debug.LogError("Can't find player"); }

        weaponSpawner = GameObject.Find("WeaponSpawner").GetComponent<WeaponSpawner>();
        if (weaponSpawner == null) { Debug.LogError("Can't find weaponSpawner"); }

        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        if (enemySpawner == null) { Debug.LogError("Can't find enemySpawner"); }
    }

    private void InitTitleScene()
    {
        ObjPool.Instance.InitObjPool();
    }


    // functions related-game progress
    private void CheckPhase()
    {
        if (NumOfKilledEnemies == PHASE_CONDITION_NUM && !bossPhase)
        {
            bossPhase = true;
            enemySpawner.SpawnSlimeBoss();
        }
    }

    public void GamePause()
    {
        Time.timeScale = 0;
    }

    public void GameResume()
    {
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        UIManager.Instance.OpenGameOverPanel();
        GamePause();
    }

    public void RestartGame()
    {
        GameResume();
        SceneManager.LoadScene("Main");
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
    public void LoadShopScene()
    {
        SceneManager.LoadScene("Shop");
    }

    public void EnemyKilled()
    {
        NumOfKilledEnemies++;
        CheckPhase();
    }

    public void OnBossDefeated() 
    {
        bossPhase = false;      
    }

    // functions related-ui
    public void OpenUpgradeWindow()
    {
        UIManager.Instance.OpenUpgradeUI(GetWeaponSpawener().GetRandomWeapons(3));
    }

    public void UpgradeWeapon(Weapon weapon)
    {
        weapon.Upgrade(); // ���õ� ���� ���׷��̵� ó�� ����
        Debug.Log($"{weapon.WeaponName}��(��) ���׷��̵�Ǿ����ϴ�.");
        UIManager.Instance.HideUpgradeUI();
    }
}

