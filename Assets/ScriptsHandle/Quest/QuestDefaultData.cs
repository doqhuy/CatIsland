using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[CreateAssetMenu(fileName = "New QuestDefaultData", menuName = "ScriptableObjects/QuestDefaultData")]
public class QuestDefaultData : ScriptableObject
{
    public List<Quest> Quests;
    bool CheckQuestExist(Quest quest)
    {
        for (int i = 0; i < Quests.Count; i++)
        {
            if (Quests[i] == quest) return true;
        }
        return false;
    }

    public void SaveThisData()
    {
        string saveDefaultQuest = JsonUtility.ToJson(this);
        File.WriteAllText("Assets/Resources/Save0/QuestDefaultData.json", saveDefaultQuest);
    }    
    public void SetDefaultData(Quest quest)
    {
        if(!CheckQuestExist(quest))
        {
            Quests.Add(quest);
                string saveQuest = JsonUtility.ToJson(quest);
                File.WriteAllText("Assets/Resources/Save0/QuestDefaultData/" + quest.name + ".json", saveQuest);
        }
        SaveThisData();
    }    
    public void GetDefaultData()
    {
        for (int i = 0; i < Quests.Count; i++)
        {
            string saveQuest = File.ReadAllText("Assets/Resources/Save0/QuestDefaultData/" + Quests[i].name + ".json");
            JsonUtility.FromJsonOverwrite(saveQuest, Quests[i]);
        }
    }    
}
