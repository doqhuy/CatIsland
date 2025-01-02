using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DropItemSave : MonoBehaviour
{
    public string DropSaveName = "DropItem_[index]";
    public ListDropItem ListDropItem;
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

        if (!File.Exists(Path.Combine(save0Path, "DropItem/" + DropSaveName + ".json")))
        {
            ListDropItem newLDI = new ListDropItem();
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject thisItem = transform.GetChild(i).gameObject;
                if (!thisItem.activeSelf)
                {
                    newLDI.IsNotPicked.Add(false);
                }
                else
                {
                    newLDI.IsNotPicked.Add(true);
                }
            }
            string saveRecord = JsonUtility.ToJson(newLDI);
            File.WriteAllText(Path.Combine(save0Path, "DropItem/" + DropSaveName + ".json"), saveRecord);
        }
        
        LoadDropItem();
    }

    private void OnEnable()
    {
        LoadDropItem();
    }

    private void Update()
    {
        
    }

    public void SaveDropItem()
    {
        rootPath = Application.persistentDataPath;
#if UNITY_ANDROID
        defaultsavePath = Path.Combine(rootPath, "Save");
        save0Path = Path.Combine(rootPath, "Save0");
#else
        defaultsavePath = "Assets/Resources/Save";
        save0Path = "Assets/Resources/Save0";
#endif
        ListDropItem newLDI = new ListDropItem();
        for(int i = 0; i<transform.childCount; i++)
        {
            GameObject thisItem = transform.GetChild(i).gameObject;
            if(!thisItem.activeSelf)
            {
                newLDI.IsNotPicked.Add(false);
            }    
            else
            {
                newLDI.IsNotPicked.Add(true);
            }
        }
        string saveRecord = JsonUtility.ToJson(newLDI);
        File.WriteAllText(Path.Combine(defaultsavePath, "DropItem/" + DropSaveName + ".json"), saveRecord);
    }

    void LoadDropItem()
    {
        rootPath = Application.persistentDataPath;
#if UNITY_ANDROID
        defaultsavePath = Path.Combine(rootPath, "Save");
        save0Path = Path.Combine(rootPath, "Save0");
#else
        defaultsavePath = "Assets/Resources/Save";
        save0Path = "Assets/Resources/Save0";
#endif
        if (File.Exists(Path.Combine(defaultsavePath, "DropItem/" + DropSaveName + ".json")))
        {
            string saveRecord = File.ReadAllText(Path.Combine(defaultsavePath, "DropItem/" + DropSaveName + ".json"));
            ListDropItem = JsonUtility.FromJson<ListDropItem>(saveRecord);
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject thisItem = transform.GetChild(i).gameObject;
                thisItem.SetActive(ListDropItem.IsNotPicked[i]);
            }
        }    
    }
}
[System.Serializable]
public class ListDropItem
{
    public List<bool> IsNotPicked = new List<bool>();
}

