using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Recipe", menuName = "ScriptableObjects/Recipe")]

public class Recipe : ScriptableObject
{
    public Item Item;
    public Equipment Equipment;
    public bool IsEquipment = false;
    public List<ItemWithQuantity> Materials;

    bool CheckMaterials(Inventory inventory)
    {
        foreach (ItemWithQuantity item in Materials)
        {
            if (!inventory.CheckItemHas(item.Item, item.Quantity)) return false;
        }
        return true;
    }    
    public bool CraftItem(Inventory inventory)
    {
        if (!CheckMaterials(inventory)) return false;
        else
        {
            foreach (ItemWithQuantity item in Materials)
            {
                int itemindex = inventory.GetItemIndex(item.Item);
                inventory.Items[itemindex].Quantity -= item.Quantity;
                if(inventory.Items[itemindex].Quantity <= 0) inventory.RemoveItem(itemindex);
            }
            inventory.AddItem(Item);
            return true;
        }
    }    

    public Equipment CraftEquipment()
    {
        return Equipment;
    }    
}




