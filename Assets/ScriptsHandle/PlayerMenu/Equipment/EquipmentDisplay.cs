using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentDisplay : MonoBehaviour
{
	private SaveSystem SaveSystem = SaveSystem.Instance;
    private GeneralInformation GeneralInformation = GeneralInformation.Instance;

    public Image Helmet;
	public Image Armor;
	public Image Weapon;
	public Image Accessory;

    public GameObject EquipmentInventory;
    public GameObject EquipmentInformation;

    public Image SelectedCharacter;

	private Character thisCharacter;
	private List<Equipment> equipments;

	void Start()
    {
		equipments = SaveSystem.saveLoad.equipmentInventory.Helmets;
    }

	private void OnEnable()
	{
		thisCharacter = SaveSystem.saveLoad.team.Teamate[GeneralInformation.charSelectionNumber];
		EquipmentInformation.SetActive(false);
	}

	// Update is called once per frame
	void Update()
    {
        SelectedCharacter.sprite = thisCharacter.Avatar;
        ShowEquipmentInventory();
        ShowCharacterEquip();
    }



    public void ShowCharacterEquip()
    {
        if (thisCharacter.Helmet != null)
        {
            Helmet.sprite = thisCharacter.Helmet.Image;
            Color cl = Helmet.color;
            cl.a = 1f;
            Helmet.color = cl;
        }
        else
        {
            Color cl = Helmet.color;
            cl.a = 1f;
            Helmet.color = cl;
        }


        if (thisCharacter.Armor != null)
        {
            Armor.sprite = thisCharacter.Armor.Image;
            Color cl = Armor.color;
            cl.a = 1f;
            Armor.color = cl;
        }
        else
        {
            Color cl = Armor.color;
            cl.a = 1f;
            Armor.color = cl;
        }

        if (thisCharacter.Weapon != null)
        {
            Weapon.sprite = thisCharacter.Weapon.Image;
            Color cl = Weapon.color;
            cl.a = 1f;
            Weapon.color = cl;
        }
        else
        {
            Color cl = Weapon.color;
            cl.a = 1f;
            Weapon.color = cl;
        }

        if (thisCharacter.Accessory != null)
        {
            Accessory.sprite = thisCharacter.Accessory.Image;
            Color cl = Accessory.color;
            cl.a = 1f;
            Accessory.color = cl;
        }
        else
        {
            Color cl = Accessory.color;
            cl.a = 1f;
            Accessory.color = cl;
        }

        Button ShowHelmet = Helmet.GetComponent<Button>();
        Button ShowArmor = Armor.GetComponent<Button>();
        Button ShowWeapon = Weapon.GetComponent<Button>();
        Button ShowAccessory = Accessory.GetComponent<Button>();

        ShowHelmet.onClick.RemoveAllListeners();
        ShowHelmet.onClick.AddListener(() =>
        {
            equipments = SaveSystem.saveLoad.equipmentInventory.Helmets;
            ShowEquipmentEquiped(thisCharacter.Helmet);
        });

        ShowArmor.onClick.RemoveAllListeners();
        ShowArmor.onClick.AddListener(() =>
        {
            equipments = SaveSystem.saveLoad.equipmentInventory.Armors;
            ShowEquipmentEquiped(thisCharacter.Armor);
        });

        ShowWeapon.onClick.RemoveAllListeners();
        ShowWeapon.onClick.AddListener(() =>
        {
            equipments = SaveSystem.saveLoad.equipmentInventory.Weapons;
            ShowEquipmentEquiped(thisCharacter.Weapon);
        });

        ShowAccessory.onClick.RemoveAllListeners();
        ShowAccessory.onClick.AddListener(() =>
        {
            equipments = SaveSystem.saveLoad.equipmentInventory.Accessories;
            ShowEquipmentEquiped(thisCharacter.Accessory);
        });
    }

    void ShowEquipmentEquiped(Equipment equipment)
    {

        GameObject ImageObj = EquipmentInformation.transform.Find("Image").gameObject;
        GameObject NameObj = EquipmentInformation.transform.Find("Name").gameObject;
        GameObject StatObj = EquipmentInformation.transform.Find("Stat").gameObject;
        GameObject DescriptionObj = EquipmentInformation.transform.Find("Description").gameObject;
        GameObject EquipObj = EquipmentInformation.transform.Find("Equip").gameObject;
        GameObject RecycleObj = EquipmentInformation.transform.Find("Recycle").gameObject;
        GameObject UnequipObj = EquipmentInformation.transform.Find("Unequip").gameObject;

        Image Image = ImageObj.GetComponent<Image>();
        TMP_Text Name = NameObj.GetComponent<TMP_Text>();
        TMP_Text Stat = StatObj.GetComponent<TMP_Text>();
        TMP_Text Description = DescriptionObj.GetComponent<TMP_Text>();
        Button Equip = EquipObj.GetComponent<Button>();
        Button Recycle = RecycleObj.GetComponent<Button>();
        Button Unequip = UnequipObj.GetComponent<Button>();

        if (equipment != null)
        {
            EquipmentInformation.SetActive(true);
            Image.sprite = equipment.Image;
            Name.text = equipment.Name;
            Stat.text = equipment.Stat;
            Description.text = equipment.Description;


            Unequip.onClick.RemoveAllListeners();
            Unequip.onClick.AddListener(() =>
            {
                switch (equipment.Type)
                {
                    case "Helmet": SaveSystem.saveLoad.equipmentInventory.AddEquipment(thisCharacter.Unequip("Helmet")); break;
                    case "Armor": SaveSystem.saveLoad.equipmentInventory.AddEquipment(thisCharacter.Unequip("Armor")); break;
                    case "Weapon": SaveSystem.saveLoad.equipmentInventory.AddEquipment(thisCharacter.Unequip("Weapon")); break;
                    case "Accessory": SaveSystem.saveLoad.equipmentInventory.AddEquipment(thisCharacter.Unequip("Accessory")); break;
                }
                EquipmentInformation.SetActive(false);

            });
        }
        else
        {
            EquipmentInformation.SetActive(false);
        }



        EquipObj.SetActive(false);
        RecycleObj.SetActive(false);
        UnequipObj.SetActive(true);
    }

    void ShowEquipmentInfo(int index)
    {
        EquipmentInformation.SetActive(true);

        Equipment equipment = equipments[index];

        GameObject ImageObj = EquipmentInformation.transform.Find("Image").gameObject;
        GameObject NameObj = EquipmentInformation.transform.Find("Name").gameObject;
        GameObject StatObj = EquipmentInformation.transform.Find("Stat").gameObject;
        GameObject DescriptionObj = EquipmentInformation.transform.Find("Description").gameObject;
        GameObject EquipObj = EquipmentInformation.transform.Find("Equip").gameObject;
        GameObject RecycleObj = EquipmentInformation.transform.Find("Recycle").gameObject;
        GameObject UnequipObj = EquipmentInformation.transform.Find("Unequip").gameObject;

        Image Image = ImageObj.GetComponent<Image>();
        TMP_Text Name = NameObj.GetComponent<TMP_Text>();
        TMP_Text Stat = StatObj.GetComponent<TMP_Text>();
        TMP_Text Description = DescriptionObj.GetComponent<TMP_Text>();
        Button Equip = EquipObj.GetComponent<Button>();
        Button Recycle = RecycleObj.GetComponent<Button>();
        Button Unequip = UnequipObj.GetComponent<Button>();

        Image.sprite = equipment.Image;
        Name.text = equipment.Name;
        Stat.text = equipment.Stat;
        Description.text = equipment.Description;
        Equip.onClick.RemoveAllListeners();
        Equip.onClick.AddListener(() =>
        {
            SaveSystem.saveLoad.equipmentInventory.AddEquipment(thisCharacter.Equip(equipment));
            SaveSystem.saveLoad.equipmentInventory.DropEquipment(index, equipment.Type);
            ShowEquipmentEquiped(equipment);
        });

        Recycle.onClick.RemoveAllListeners();
        Recycle.onClick.AddListener(() =>
        {
            SaveSystem.saveLoad.inventory.Coin += equipment.Price / 3;
            SaveSystem.saveLoad.equipmentInventory.DropEquipment(index, equipment.Type);
            EquipmentInformation.SetActive(false);
        });



        EquipObj.SetActive(true);
        RecycleObj.SetActive(true);
        UnequipObj.SetActive(false);
    }

    public void ShowEquipmentInventory()
    {
        GameObject Viewport = EquipmentInventory.transform.Find("Viewport").gameObject;
        GameObject Content = Viewport.transform.Find("Content").gameObject;
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            int index = i;
            GameObject thisEquipObj = Content.transform.GetChild(index).gameObject;
            Button ShowInfo = thisEquipObj.GetComponent<Button>();
            ShowInfo.onClick.RemoveAllListeners();

			GameObject EquipmentImage = thisEquipObj.transform.Find("Image").gameObject;
            Image thisEquipmentImage = EquipmentImage.GetComponent<Image>();
            if (index < equipments.Count)
            {
                thisEquipObj.SetActive(true);
                thisEquipmentImage.sprite = equipments[index].Image;
                ShowInfo.onClick.AddListener(() =>
                {
                    ShowEquipmentInfo(index);
                });
            }
            else
            {
                thisEquipObj.SetActive(false);
            }
        }
    }

}

