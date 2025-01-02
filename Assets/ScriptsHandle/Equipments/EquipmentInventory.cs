using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentInventory
{
	public int EquipmentInventorySize = 20;
    public List<Equipment> Helmets;
	public List<Equipment> Armors;
	public List<Equipment> Weapons;
	public List<Equipment> Accessories;

	public void DropEquipment(int index, string type)
	{

		switch (type)
		{
			case "Helmet": Helmets.RemoveAt(index); break;
			case "Armor": Armors.RemoveAt(index); break;
			case "Weapon": Weapons.RemoveAt(index); break;
			case "Accessory": Accessories.RemoveAt(index); break;
		}
	}

	public void AddEquipment(Equipment eq)
	{
		if(eq != null)
		{
			string type = eq.Type;
			switch (type)
			{
				case "Helmet": Helmets.Add(eq); break;
				case "Armor": Armors.Add(eq); break;
				case "Weapon": Weapons.Add(eq); break;
				case "Accessory": Accessories.Add(eq); break;
			}
		}	
	}	
}
