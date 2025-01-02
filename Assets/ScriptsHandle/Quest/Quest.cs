using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Quest", menuName = "ScriptableObjects/Quest")]

public class Quest : ScriptableObject
{
    public Sprite Image;

    public int Id;
    public string Name;
    public string Description;

    //Main, Side, Extra
    public string Type;
    //Claimed, UnClaim, Questing, Done, Canceled, Reach
    public string Status = "UnClaimed";
    public bool IsReturn = true;
    public bool IsEventClaimed = false;

    public List<ItemWithQuantity> ItemsRequired;
    public List<QuestKill> KillList;
    public List<string> QuestRequired;


    //QuestAward
    public int CoinReward;
    public List<ItemRewards> ItemReward;
    public List<EquipmentRewards> EquipmentReward;
    public List<Character> CharacterReward;

    //Quest auto claim when this quest complete
    public List<Quest> NextQuests;
    //This quest can be claim if those quest complete
    public List<Quest> PreviousQuests;
    //If all mini quest clear and this quest reach the required, this can be done
    public List<Quest> MiniQuests;

    public bool CheckQuestAvailable()
    {
        for (int i = 0; i < PreviousQuests.Count; i++)
        {
            if (PreviousQuests[i].Status != "Done")
                return false;
        }
        return true;
    }    

    public bool CheckTalkRequiredReturn(string s)
    {
        foreach (string item in QuestRequired)
        {
            if (s == item) return true;
        }
        return false;
    }

    public void CheckTalkRequired(string s)
    {
        List<string> itemsToRemove = new List<string>();

        foreach (string item in QuestRequired)
        {
            if (s == item)
            {
                itemsToRemove.Add(item);
            }
        }

        // Remove the items outside the loop
        foreach (string item in itemsToRemove)
        {
            QuestRequired.Remove(item);
        }

        CheckQuest();
    }


    public void CheckKill(Enemy enemy)
    {
        for(int i = 0; i < KillList.Count;i++)
        {
            if (enemy.Name == KillList[i].enemy.Name)
            {
                KillList[i].amount--;
                if (KillList[i].amount <= 0)
                {
                    KillList.RemoveAt(i);
                    CheckQuest();
                }    
            }    
        }
    }

    bool CheckItemRequired()
    {
        for (int i = 0; i < ItemsRequired.Count; i++)
        {
            if (!SaveSystem.Instance.saveLoad.inventory.CheckItemHas(ItemsRequired[i].Item, ItemsRequired[i].Quantity))
            {
                return false;
            }    
        }    
        return true;
    }    

 
    public bool CheckQuest()
    {
        if (MiniQuests != null)
        {
            foreach (Quest quest in MiniQuests)
            {
                if (!quest.CheckQuest()) { return false; }
            }
        }
        if (KillList.Count == 0 && QuestRequired.Count == 0 && CheckItemRequired())
        {
            if(IsReturn == true) 
            {
                Status = "Reach";
            }
            else
            {
                Status = "Done";
            }    
            return true;
        } 
            
        return false;
    }    
}

[System.Serializable]
public class ItemRewards
{
    public Item item;
    public int amount = 1;
}

[System.Serializable]
public class EquipmentRewards
{
    public Equipment equipment;
    public int amount = 1;
}

[System.Serializable]
public class QuestKill
{
    public Enemy enemy;
    public int amount = 1;
}
