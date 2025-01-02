using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    private SaveSystem SaveSystem = SaveSystem.Instance;
	private GeneralInformation GeneralInformation = GeneralInformation.Instance;

	public TMP_Text Notification;

    public GameObject InventoryItems;
    public GameObject ItemInfo;
	public GameObject SelectedCharacter;
	public TMP_Text Coin;

    public Button NormalItems;
	public Button KeyItems;

	private List<ItemWithQuantity> items;
	private List<Character> teamates;
	private Character thisChar;
    void Start()
    {
		thisChar = SaveSystem.saveLoad.team.Teamate[GeneralInformation.charSelectionNumber];
		teamates = SaveSystem.saveLoad.team.Teamate;
		items = SaveSystem.saveLoad.inventory.Items;
        ShowListItem();
    }

	private void OnEnable()
	{
		ItemInfo.SetActive(false);
		thisChar = SaveSystem.saveLoad.team.Teamate[GeneralInformation.charSelectionNumber];
        
    }

	// Update is called once per frame
	void Update()
    {
        ShowListItem();
		ShowInforSelectedCharacter();
        NormalItems.onClick.RemoveAllListeners();
        KeyItems.onClick.RemoveAllListeners();
        NormalItems.onClick.AddListener(() => { items = SaveSystem.saveLoad.inventory.Items; });
        KeyItems.onClick.AddListener(() => { items = SaveSystem.saveLoad.inventory.KeyItems; });
    }

	IEnumerator ShowNotification(string alert)
	{
		Notification.gameObject.SetActive(true);
		Notification.text = alert;
		yield return new WaitForSeconds(5f);
		Notification.gameObject.SetActive(false);
	}
	void ShowInforSelectedCharacter()
	{
		GameObject AvatarObj = SelectedCharacter.transform.Find("Avatar").gameObject;
		GameObject HPBarObj = SelectedCharacter.transform.Find("HPBar").gameObject;
		GameObject MPBarObj = SelectedCharacter.transform.Find("MPBar").gameObject;

		Image Avatar = AvatarObj.GetComponent<Image>();
		Slider HPBar = HPBarObj.GetComponent<Slider>();
		Slider MPBar = MPBarObj.GetComponent<Slider>();

		Avatar.sprite = thisChar.Avatar;
		HPBar.value = (float) thisChar.HP / (float)thisChar.MaxHP;
		MPBar.value = (float) thisChar.MP / (float)thisChar.MaxMP;
	}

	void ShowInfoItem(int index)
    {
		//Lấy tham chiếu
        GameObject Image = ItemInfo.transform.Find("Image").gameObject;
		GameObject Name = ItemInfo.transform.Find("Name").gameObject;
		GameObject Description = ItemInfo.transform.Find("Description").gameObject;
		GameObject Effect = ItemInfo.transform.Find("Effect").gameObject;
		GameObject Quantity = ItemInfo.transform.Find("Quantity").gameObject;
		GameObject Use = ItemInfo.transform.Find("Use").gameObject;
		GameObject Recycle = ItemInfo.transform.Find("Recycle").gameObject;

		Image ImageImage = Image.GetComponent<Image>();
        TMP_Text NameText = Name.GetComponent<TMP_Text>();
		TMP_Text DescriptionText = Description.GetComponent<TMP_Text>();
		TMP_Text EffectText = Effect.GetComponent<TMP_Text>();
		TMP_Text QuantityText = Quantity.GetComponent<TMP_Text>();

		Button UseButton = Use.GetComponent<Button>();
		Button RecycleButton = Recycle.GetComponent<Button>();

		ItemInfo.SetActive(true);

		Item item = items[index].Item;

		//Xử lý tham chiếu
		ImageImage.sprite = item.Image;
		NameText.text = item.Name;
		DescriptionText.text = item.Description;
		EffectText.text = item.EffectDescription;
		QuantityText.text = "Available: " + items[index].Quantity.ToString();

		UseButton.onClick.RemoveAllListeners();
		UseButton.onClick.AddListener(()=>
		{
			StartCoroutine( ShowNotification(SaveSystem.saveLoad.inventory.UseItem(thisChar, teamates, index)));
			ItemInfo.SetActive(false );
            ShowListItem();
        });

		RecycleButton.onClick.RemoveAllListeners();
		RecycleButton.onClick.AddListener(()=>
		{
			SaveSystem.saveLoad.inventory.RecycleItem(index);
            ItemInfo.SetActive(false);
            ShowListItem();
        }
		);

		if(item.Usable == false)
		{
			Use.SetActive(false);
		}	
		else
		{
			Use.SetActive(true);
		}

		if(item.Removable == false)
		{
			Recycle.SetActive(false);
		}	
		else
		{
			Recycle.SetActive(true);
		}



	}

    void ShowListItem()
    {
        Coin.text = SaveSystem.saveLoad.inventory.Coin.ToString();
        GameObject Viewport = InventoryItems.transform.Find("Viewport").gameObject;
        GameObject content = Viewport.transform.Find("Content").gameObject;

        for (int i = 0; i < content.transform.childCount; i++)
        {
            int index = i;

            GameObject thisItem = content.transform.GetChild(index).gameObject;
            Image thisItemImage = thisItem.GetComponent<Image>();

            GameObject quantityItem = thisItem.transform.Find("Quantity").gameObject;
            TMP_Text quantityItemText = quantityItem.GetComponent<TMP_Text>();

            Button showButton = thisItem.GetComponent<Button>();

            if (index < items.Count)
            {
                thisItem.SetActive(true);

                thisItemImage.sprite = items[index].Item.Image;
                quantityItemText.text = items[index].Quantity.ToString();

                showButton.onClick.RemoveAllListeners();
                showButton.onClick.AddListener(() => ShowInfoItem(index));
            }
            else
            {
                thisItem.SetActive(false);
            }
        }
    }
}
