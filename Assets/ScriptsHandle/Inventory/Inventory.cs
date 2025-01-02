using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class ItemWithQuantity
{
    public Item Item;
    public int Quantity = 1;
}
[System.Serializable]
public class Inventory
{
    public int Coin = 500;
    public int InventorySize = 40;
    public List<ItemWithQuantity> Items;
    public List<ItemWithQuantity> KeyItems;
    public List<Recipe> Recipes;


    public void AddRecipe(Recipe recipe)
    {
        if(!Recipes.Contains(recipe)) Recipes.Add(recipe);
    }


    public int GetItemIndex(Item item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (item.Name == Items[i].Item.Name)
            {
                return i;
            }
        }
        return -1;
    }    

    public bool CheckItemHas(Item item, int amount)
    {
		for (int i = 0; i < Items.Count; i++)
		{
			if (item.Name == Items[i].Item.Name)
			{
                if(Items[i].Quantity >= amount) return true;
			}
		}
		return false;
    }

    public bool CheckKeyItemHas(Item item, int amount)
    {
        for (int i = 0; i < KeyItems.Count; i++)
        {
            if (item.Name == KeyItems[i].Item.Name)
            {
                if (KeyItems[i].Quantity >= amount) return true;
            }
        }
        return false;
    }    

    public void AddItem(Item item)
    {
        if(!CheckItemHas(item,1))
        {
            ItemWithQuantity newItem = new ItemWithQuantity();
            newItem.Item = item;
            newItem.Quantity = 1;
            if(item.IsKeyItem)
            {
                KeyItems.Add(newItem);
            }
            else
            {
                Items.Add(newItem);
            }
        }    
        else
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (item.Name == Items[i].Item.Name)
                {
                    Items[i].Quantity++;
                }
            }
        }
    }    
    public void RemoveItem(int index)
    {
        Items.RemoveAt(index);
    }    

    public void RecycleItem(int index)
    {
        Coin += Items[index].Quantity * Items[index].Item.Price / 3;
		RemoveItem(index);

	}


	public string UseItem(Character character, List<Character> characters, int index)
    {
        Item item = Items[index].Item;
        int CheckUse = 0;
        if(!item.IsMultiUse)
        {
            if (item.SpecialEffect == "Revive") 
            {
				if (item.Revive(character))
                {
                    CheckUse++;
                    item.Restore(character);
                    item.Buff(character);
				} 
			}
            else
			{
				if (item.IsHeal) if (item.Restore(character)) CheckUse++;
				if (item.IsBuff) if (item.Buff(character)) CheckUse++;
			}
		}    
        else
        {
            foreach (Character c in characters)
            {
                if(c!= null)
                {
					if (item.SpecialEffect == "Revive")
					{
						if (item.Revive(c))
						{
							CheckUse++;
							item.Restore(c);
							item.Buff(c);
						}
					}
					else
					{
						if (item.IsHeal) if (item.Restore(c)) CheckUse++;
						if (item.IsBuff) if (item.Buff(c)) CheckUse++;
					}
				}
			}
        }


        if (CheckUse > 0)
        {
            Items[index].Quantity--;
            if (Items[index].Quantity == 0)
            {
                Items.RemoveAt(index);
            }
            return "Use Item Successfully";
        }
        else return "Use Item Failed";

    }    
}
