using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public Item Item;
    public Equipment Equipment;
    public Recipe Recipe;
    public int Coin = 0;
    SaveSystem SaveSystem = SaveSystem.Instance;

    // Update is called once per frame
    private void Update()
    {
        
        Transform player = GameObject.Find("Player").transform;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= 1.4f && (Input.GetKeyDown(KeyCode.J) || ActionButton.Instance.IsPress))
        {
            string itemname = "";
            ActionButton.Instance.ResetIsPress();
            if (Item != null)
            {
                SaveSystem.saveLoad.inventory.AddItem(Item);
                itemname = Item.Name;
            }
            if(Equipment != null)
            {
                SaveSystem.saveLoad.equipmentInventory.AddEquipment(Equipment);
                itemname = Equipment.Name;
            }
            if (Recipe != null)
            {
                SaveSystem.saveLoad.inventory.AddRecipe(Recipe);
                itemname = Recipe.name;
            }
            SaveSystem.saveLoad.inventory.Coin += Coin;
            GameObject DropItemSaveObj = transform.parent.gameObject;
            this.gameObject.SetActive(false);
            GeneralInformation.Instance.CallPlayerNoti("Bạn đã nhặt được " + itemname + "!");
            DropItemSave DropItemSave = DropItemSaveObj.GetComponent<DropItemSave>();
            DropItemSave.SaveDropItem();

        }
    }
}
