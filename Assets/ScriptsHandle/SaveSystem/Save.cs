using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Save", menuName = "ScriptableObjects/Save")]
public class Save : ScriptableObject
{
	public string currentScene = "FirstScene";
    public string currentMapName = "Island";
    public Vector3 currentPosition = Vector3.zero;

    public string SavePointScene = "FirstScene";
    public string SavePointMap = "Island";

    public float Time = 0f;

    public Team team;
    public Inventory inventory;
    public EquipmentInventory equipmentInventory;
    public QuestClaim questClaim;

    public void ClaimQuestReward(Quest quest)
    {
        foreach (ItemRewards item in quest.ItemReward)
        {
            for(int i = 0; i<item.amount;i++)
            {
                inventory.AddItem(item.item);
            }
        }
        foreach (EquipmentRewards equipment in quest.EquipmentReward)
        {
            for (int i = 0; i < equipment.amount; i++)
            {
                equipmentInventory.AddEquipment(equipment.equipment);
            }
        }
        foreach (Character character in quest.CharacterReward)
        {
            team.AddCharacter(character);
        }    
        inventory.Coin += quest.CoinReward;
        quest.Status = "Done";
        for(int i=0; i<quest.NextQuests.Count;i++)
        {
            questClaim.AddQuest(quest.NextQuests[i]);
        }
    }

    public Save Copy()
    {
        Save newSave = ScriptableObject.CreateInstance<Save>();

        // Sao chép các trường dữ liệu
        newSave.currentScene = this.currentScene;
        newSave.currentMapName = this.currentMapName;
        newSave.currentPosition = this.currentPosition;
        newSave.SavePointScene = this.SavePointScene;

        // Sao chép các tham chiếu đến các đối tượng khác
        newSave.team = this.team;
        newSave.inventory = this.inventory;
        newSave.equipmentInventory = this.equipmentInventory;

        // Sao chép danh sách các nhân vật

        return newSave;
    }
}
