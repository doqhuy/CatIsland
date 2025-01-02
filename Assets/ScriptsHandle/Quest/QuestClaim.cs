using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[System.Serializable]
public class QuestClaim
{
    public List<Quest> quests;
    public QuestDefaultData QuestDefaultData;
    bool CheckQuestExist(Quest quest)
    {
        for(int i = 0; i < quests.Count; i++)
        {
            if (quests[i] == quest) return true;
        }   
        return false;
    }

    public void CheckNextQuest()
    {
        for(int i=0; i < quests.Count; ++i)
        {
            if (quests[i].Status == "Done")
            {
                foreach (Quest q in quests[i].NextQuests)
                {
                    AddQuest(q);
                }
            }
        }
    }

    public bool CheckTalkRequiredAllQuestReturn(string s)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].Status == "Questing")
            {
                if (quests[i].CheckTalkRequiredReturn(s)) return true;
            }
        }
        return false;
    }

    public void CheckTalkRequiredAllQuest(string s)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].Status == "Questing")
            {
                quests[i].CheckTalkRequired(s);
            }
        }
    }    

    public void CheckKillAllQuest(Enemy enemy)
    {
        for(int i =0;  i < quests.Count; i++)
        {
            if (quests[i].Status == "Questing")
            {
                quests[i].CheckKill(enemy);
            }    
        }    
    }    


    public void AddQuest(Quest quest)
    {
        QuestDefaultData.SetDefaultData(quest);
        
        if (!CheckQuestExist(quest))
        {
            quests.Add(quest);
            quest.Status = "Questing";
        } 
    }    
}
