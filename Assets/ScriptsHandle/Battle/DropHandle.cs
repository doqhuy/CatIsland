using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropHandle : MonoBehaviour
{
	public GameObject EquipmentDropObj;
	public GameObject ItemDropObj;

	private GeneralInformation GeneralInformation = GeneralInformation.Instance; 
    private SaveSystem SaveSystem = SaveSystem.Instance; 

    // Start is called before the first frame update
    void Start()
    {
        ItemDrop();
		EquipmentDrop();
		GeneralInformation.WinLose = true;
    }



    public void EquipmentDrop()
    {
	
		for (int i = 0; i < EquipmentDropObj.transform.childCount; i++)
		{
			int index = i;
			GameObject thisEquipObj = EquipmentDropObj.transform.GetChild(i).gameObject;
			Image thisEquipImage = thisEquipObj.GetComponent<Image>();
			if (index < GeneralInformation.EquipmentDrop.Count)
			{
				Equipment thisEquip = GeneralInformation.EquipmentDrop[index];
				thisEquipImage.sprite = thisEquip.Image;
				SaveSystem.saveLoad.equipmentInventory.AddEquipment(thisEquip);
			}
			else
			{
				thisEquipObj.SetActive(false);
			}
		}
	}    

	public void ItemDrop()
	{
		for (int i = 0; i < ItemDropObj.transform.childCount; i++)
		{
			int index = i;
			GameObject thisItemObj = ItemDropObj.transform.GetChild(i).gameObject;
			Image thisItemImage = thisItemObj.GetComponent<Image>();
			if (index < GeneralInformation.ItemDrop.Count)
			{
				Item thisItem = GeneralInformation.ItemDrop[index];
				thisItemImage.sprite = thisItem.Image;
				SaveSystem.saveLoad.inventory.AddItem(thisItem);
			}
			else
			{
				thisItemObj.SetActive(false);
			}
		}
	}	
}
