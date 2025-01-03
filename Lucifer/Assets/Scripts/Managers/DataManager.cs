using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    private string name;
    private int level;
    private int coin;
    private int item;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    public int Coin
    {
        get { return coin; }
        set { coin = value; }
    }
    public int Item
    {
        get { return item; }
        set { item = value; }
    }
}

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<DataManager>();
            DontDestroyOnLoad(_instance.gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        path = Application.persistentDataPath + "/save";
    }

    private PlayerData nowPlayer = new PlayerData();
    private string path;
    private string filename = "save";

    // Start is called before the first frame update
    void Start()
    { 

    }
    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + filename, data);
    }
    public void LoadData()
    {
        string data = File.ReadAllText(path + filename);
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }
}
