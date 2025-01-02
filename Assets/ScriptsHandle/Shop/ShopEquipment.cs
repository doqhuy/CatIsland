using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ShopEquipment : MonoBehaviour
{
	public List<Equipment> equipments;
	public GameObject ShopEquipments;
	public GameObject EquipmentInfo;
	public TMP_Text Coin;

	public TMP_Text Notification;

	private SaveSystem SaveSystem = SaveSystem.Instance;
	private GeneralInformation GeneralInformation = GeneralInformation.Instance;
	// Start is called before the first frame update
	void Update()
    {
		Coin.text = SaveSystem.saveLoad.inventory.Coin.ToString();
		ShowShopEquipment();
    }

	private void OnEnable()
	{
		EquipmentInfo.SetActive(false);
		GeneralInformation.Actioning = "Shoping";
	}

    private void OnDisable()
    {
        GeneralInformation.Actioning = "Playing";
    }

    void ShowInfoItem(Equipment equipment)
	{
		EquipmentInfo.SetActive(true);
		GameObject ImageObj = EquipmentInfo.transform.Find("Image").gameObject;
		GameObject NameObj = EquipmentInfo.transform.Find("Name").gameObject;
		GameObject DescriptionObj = EquipmentInfo.transform.Find("Description").gameObject;
		GameObject StatObj = EquipmentInfo.transform.Find("Stat").gameObject;
		GameObject PriceObj = EquipmentInfo.transform.Find("Price").gameObject;
		GameObject BuyObj = EquipmentInfo.transform.Find("Buy").gameObject;

		Image Image = ImageObj.GetComponent<Image>();
		TMP_Text Name = NameObj.GetComponent<TMP_Text>();
		TMP_Text Description = DescriptionObj.GetComponent<TMP_Text>();
		TMP_Text Stat = StatObj.GetComponent<TMP_Text>();
		TMP_Text Price = PriceObj.GetComponent<TMP_Text>();
		Button Buy = BuyObj.GetComponent<Button>();

		Image.sprite = equipment.Image;
		Name.text = equipment.Name;
		Description.text = equipment.Description;
		Stat.text = equipment.Stat;
		Price.text = equipment.Price.ToString() + "coin";
		Buy.onClick.RemoveAllListeners();
		Buy.onClick.AddListener(() =>
		{
			if (SaveSystem.saveLoad.inventory.Coin >= equipment.Price)
			{
				SaveSystem.saveLoad.equipmentInventory.AddEquipment(equipment);
				SaveSystem.saveLoad.inventory.Coin -= equipment.Price;
				StartCoroutine(ShowNotification("Bạn đã mua " + equipment.Name + " thành công!"));
			}
			else
			{
				StartCoroutine(ShowNotification("Bạn không có đủ tiền để mua món này!"));
			}

		});
	}

	IEnumerator ShowNotification(string noti)
	{
		Notification.gameObject.SetActive(true);
		Notification.text = noti;
		yield return new WaitForSeconds(5f);
		Notification.gameObject.SetActive(false);
	}


	void ShowShopEquipment()
	{
		GameObject Viewport = ShopEquipments.transform.Find("Viewport").gameObject;
		GameObject Content = Viewport.transform.Find("Content").gameObject;
		for (int i = 0; i < Content.transform.childCount; i++)
		{
			int index = i;
			GameObject thisEquipmentObj = Content.transform.GetChild(i).gameObject;
			Button ShowEquipment = thisEquipmentObj.GetComponent<Button>();
			ShowEquipment.onClick.RemoveAllListeners();

			Image ItemImage = thisEquipmentObj.GetComponent<Image>();
			if (index < equipments.Count)
			{
				thisEquipmentObj.SetActive(true );
				if (equipments[index] != null)
				{
					ItemImage.sprite = equipments[index].Image;
					ShowEquipment.onClick.AddListener(() => ShowInfoItem(equipments[index]));
				}
			}
			else
			{
                thisEquipmentObj.SetActive(false);
            }
		}
	}
}
