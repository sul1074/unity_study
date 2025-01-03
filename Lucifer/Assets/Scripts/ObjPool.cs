using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    const int SPAWN_NUM = 100;
    private static ObjPool _instance;
    public static ObjPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ObjPool>();
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
        }
        else if (_instance != this)
        {
            Destroy(gameObject);  // 중복된 인스턴스는 삭제
        }
    }

    [SerializeField] private GameObject poolObj;
    public Queue<GameObject> poolObjectQueue = new Queue<GameObject>();

    private void InitSpawn()
    {
        for (int i = 0; i < SPAWN_NUM; i++)
        {
            GameObject chasingEnemy = Instantiate(GameManager.Instance.GetChasingEnemyPrefab(), this.transform.position, Quaternion.identity);
            chasingEnemy.transform.SetParent(this.transform, true);
            chasingEnemy.SetActive(false);
            poolObjectQueue.Enqueue(chasingEnemy);

            GameObject rangeEnemy = Instantiate(GameManager.Instance.GetRangeEnemyPrefab(), this.transform.position, Quaternion.identity);
            rangeEnemy.transform.SetParent(this.transform, true);
            rangeEnemy.SetActive(false);
            poolObjectQueue.Enqueue(rangeEnemy);     
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(poolObjectQueue.Count);
    }
    public GameObject GetObject()
    {
        GameObject returnObj = poolObjectQueue.Dequeue();
        returnObj.SetActive(true);
        return returnObj;
    }

    public void ReturnObject(GameObject obj)
    {
        poolObjectQueue.Enqueue(obj);
        obj.SetActive(false);
    }

    public void InitObjPool()
    {
        foreach(GameObject obj in poolObjectQueue)
        {
            obj.SetActive(false);
        }
    }
}
