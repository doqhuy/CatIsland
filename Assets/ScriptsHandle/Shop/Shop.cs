using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class Shop : MonoBehaviour
{
    public List<Item> items;
    public GameObject ShopItems;
    public GameObject ItemInfo;
    public TMP_Text Coin;
    public TMP_Text Notification;

    private SaveSystem SaveSystem = SaveSystem.Instance;
	private GeneralInformation GeneralInformation = GeneralInformation.Instance;
    void Start()
    {
  
    }

   

    private void OnDisable()
    {
        GeneralInformation.Actioning = "Playing";
    }


    // Update is called once per frame
    void Update()
    {
        Coin.text = SaveSystem.saveLoad.inventory.Coin.ToString();
        ShowShopItems();
	}

	private void OnEnable()
	{
		ItemInfo.SetActive(false);
        GeneralInformation.Actioning = "Shoping";
	}

    void ShowInfoItem(Item item)
    {
        ItemInfo.SetActive(true);
        GameObject ImageObj = ItemInfo.transform.Find("Image").gameObject;
		GameObject NameObj = ItemInfo.transform.Find("Name").gameObject;
		GameObject DescriptionObj = ItemInfo.transform.Find("Description").gameObject;
		GameObject EffectObj = ItemInfo.transform.Find("Effect").gameObject;
		GameObject PriceObj = ItemInfo.transform.Find("Price").gameObject;
        GameObject BuyObj = ItemInfo.transform.Find("Buy").gameObject;

        Image Image = ImageObj.GetComponent<Image>();
        TMP_Text Name = NameObj.GetComponent<TMP_Text>();
		TMP_Text Description = DescriptionObj.GetComponent<TMP_Text>();
		TMP_Text Effect = EffectObj.GetComponent<TMP_Text>();
		TMP_Text Price = PriceObj.GetComponent<TMP_Text>();
        Button Buy = BuyObj.GetComponent<Button>();

        Image.sprite = item.Image;
        Name.text = item.Name;
        Description.text = item.Description;
        Effect.text = item.EffectDescription;
        Price.text = item.Price.ToString();
        Buy.onClick.RemoveAllListeners();
        Buy.onClick.AddListener(() =>
        {
            if(SaveSystem.saveLoad.inventory.Coin >= item.Price)
            {
				SaveSystem.saveLoad.inventory.AddItem(item);
				SaveSystem.saveLoad.inventory.Coin -= item.Price;
				StartCoroutine(ShowNotification("Bạn đã mua " + item.Name + " thành công!"));
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

	void ShowShopItems()
    {
        GameObject Viewport = ShopItems.transform.Find("Viewport").gameObject;
        GameObject Content = Viewport.transform.Find("Content").gameObject;
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            int index = i;
            GameObject thisItemObj = Content.transform.GetChild(i).gameObject;
			Button ShowItem = thisItemObj.GetComponent<Button>();
			ShowItem.onClick.RemoveAllListeners();

            Image ItemImage = thisItemObj.GetComponent<Image>();
            if(index < items.Count)
            {
                thisItemObj.SetActive(true);
                if (items[index] != null) 
                {
					ItemImage.sprite = items[index].Image;
					ShowItem.onClick.AddListener(() => ShowInfoItem(items[index]));
				}
			}    
            else
            {
                thisItemObj.SetActive(false);
            }
        }
    }    
}
