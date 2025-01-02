using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyOnMapSave : MonoBehaviour
{
    public string EnemySaveName = "EnemySave_1";
    private ListEnemyOnMapSave ListEnemyOnMapSave;
    private string rootPath = "";
    private string defaultsavePath = "";
    private string save0Path = "";
    private void Start()
    {
        rootPath = Application.persistentDataPath;
#if UNITY_ANDROID
        defaultsavePath = Path.Combine(rootPath, "Save");
        save0Path = Path.Combine(rootPath, "Save0");
#else
        defaultsavePath = "Assets/Resources/Save";
        save0Path = "Assets/Resources/Save0";
#endif
    }

    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {

                GameObject E = transform.GetChild(i).gameObject;
                ToBattle toBattle = E.GetComponent<ToBattle>();
                float restime = toBattle.SpawnTime;
                if(ListEnemyOnMapSave.EnemyStatus[i].IsNotDead == false)
                {
                    if(ListEnemyOnMapSave.EnemyStatus[i].DeadTime + restime < SaveSystem.Instance.saveLoad.Time)
                    {
                        ListEnemyOnMapSave.EnemyStatus[i].IsNotDead = true;
                        E.SetActive(true);
                        SaveEnemy();
                    }
                }
        }
    }

    void CheckDead()
    {
        if(ListEnemyOnMapSave.EnemyStatus[ListEnemyOnMapSave.BattleIndex].IsBattle)
        {
            if(GeneralInformation.Instance.WinLose == true)
            {
                ListEnemyOnMapSave.EnemyStatus[ListEnemyOnMapSave.BattleIndex].IsNotDead = false;
                ListEnemyOnMapSave.EnemyStatus[ListEnemyOnMapSave.BattleIndex].DeadTime = SaveSystem.Instance.saveLoad.Time;
                GeneralInformation.Instance.WinLose = false;
                ListEnemyOnMapSave.EnemyStatus[ListEnemyOnMapSave.BattleIndex].IsBattle = false;
            }    
        }    
    }    
    void SaveEnemy()
    {
        rootPath = Application.persistentDataPath;
#if UNITY_ANDROID
        defaultsavePath = Path.Combine(rootPath, "Save");
        save0Path = Path.Combine(rootPath, "Save0");
#endif

#if UNITY_EDITOR_WIN
        defaultsavePath = "Assets/Resources/Save";
        save0Path = "Assets/Resources/Save0";
#endif
        string saveRecord = JsonUtility.ToJson(ListEnemyOnMapSave);
        File.WriteAllText(Path.Combine(defaultsavePath, "EnemySave/" + EnemySaveName + ".json"), saveRecord);
    }

    private void OnDisable()
    {
        SaveEnemy();
    }

    private void OnEnable()
    {
        LoadEnemy();
    }

    void LoadEnemy()
    {
        rootPath = Application.persistentDataPath;
#if UNITY_ANDROID
        defaultsavePath = Path.Combine(rootPath, "Save");
        save0Path = Path.Combine(rootPath, "Save0");
#else
        defaultsavePath = "Assets/Resources/Save";
        save0Path = "Assets/Resources/Save0";
#endif
        if (File.Exists(Path.Combine(defaultsavePath, "EnemySave/" + EnemySaveName + ".json")))
        {
            string saveRecord = File.ReadAllText(Path.Combine(defaultsavePath, "EnemySave/" + EnemySaveName + ".json"));
            ListEnemyOnMapSave = JsonUtility.FromJson<ListEnemyOnMapSave>(saveRecord);
            CheckDead();
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject thisEnemy = transform.GetChild(i).gameObject;
                thisEnemy.SetActive(ListEnemyOnMapSave.EnemyStatus[i].IsNotDead);
            }
            SaveEnemy();
        }
        else
        {
            ListEnemyOnMapSave = new ListEnemyOnMapSave();
            for (int i = 0; i < transform.childCount; i++)
            {
                EnemyStatus enemy = new EnemyStatus();
                enemy.DeadTime = 0f;
                enemy.IsNotDead = true;
                enemy.IsBattle = false;
                ListEnemyOnMapSave.EnemyStatus.Add(enemy);
            }
            if (!File.Exists(Path.Combine( save0Path, "EnemySave/" + EnemySaveName + ".json")))
            {
                string saveRecord = JsonUtility.ToJson(ListEnemyOnMapSave);
                File.WriteAllText(Path.Combine(save0Path, "EnemySave/" + EnemySaveName + ".json"), saveRecord);
            }
        }    
    }    

    public void GetChildBattle(GameObject gameObject)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(gameObject ==  transform.GetChild(i).gameObject)
            {
                ListEnemyOnMapSave.EnemyStatus[i].IsBattle = true;
                ListEnemyOnMapSave.BattleIndex = i;
            }    
        }
    }
}

[System.Serializable]
public class ListEnemyOnMapSave
{
    public List<EnemyStatus> EnemyStatus = new List<EnemyStatus>();
    public int BattleIndex = 0;
}
[System.Serializable]
public class EnemyStatus
{
    public float DeadTime = 0f;
    public bool IsNotDead = true;
    public bool IsBattle = false;
}

