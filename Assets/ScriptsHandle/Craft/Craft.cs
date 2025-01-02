using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Craft : MonoBehaviour
{
    public GameObject RecipeList;
    public GameObject MaterialsList;
    public GameObject RecipeInfo;
    public TMP_Text Coin;
    public GameObject Notification;
    public bool IsEquipment = false;

    private SaveSystem SaveSystem = SaveSystem.Instance;
    private GeneralInformation GeneralInformation = GeneralInformation.Instance;

    List<Recipe> Recipes;

    private void Start()
    {
        Recipes = SaveSystem.saveLoad.inventory.Recipes;
        if(IsEquipment)
        {
            Recipes = Recipes.Where(c => c.IsEquipment == true).ToList();
        }
        else
        {
            Recipes = Recipes.Where(c => c.IsEquipment == false).ToList();
        }
        ShowRecipeList();
    }

    private void OnEnable()
    {
        MaterialsList.SetActive(false);
        RecipeInfo.SetActive(false);
        GeneralInformation.Actioning = "Shoping";
    }

    private void OnDisable()
    {
        GeneralInformation.Actioning = "Playing";
    }
    public void ShowRecipeList()
    {
        GameObject Viewport = RecipeList.transform.Find("Viewport").gameObject;
        GameObject Content = Viewport.transform.Find("Content").gameObject;
        for(int  i = 0; i<Content.transform.childCount; i++)
        {
            int index = i;
            GameObject RecipeObj = Content.transform.GetChild(index).gameObject;
            if (index < Recipes.Count)
            {
                GameObject ImageObj = RecipeObj.transform.Find("Image").gameObject;
                GameObject NameObj = RecipeObj.transform.Find("Name").gameObject;

                Image Image = ImageObj.GetComponent<Image>();
                TMP_Text Name = NameObj.GetComponent<TMP_Text>();
                if (!Recipes[index].IsEquipment)
                {
                    Image.sprite = Recipes[index].Item.Image;
                    Name.text = Recipes[index].Item.Name;
                }
                else
                {
                    Image.sprite = Recipes[index].Equipment.Image;
                    Name.text = Recipes[index].Equipment.Name;
                }
                
                Button thisRecipe = RecipeObj.GetComponent<Button>();
                thisRecipe.onClick.RemoveAllListeners();
                thisRecipe.onClick.AddListener(() => ShowRecipeInfo(Recipes[index]));

                RecipeObj.SetActive(true);
            }
            else
            {
                RecipeObj.SetActive(false);
            }    
        }
    }    

    public void ShowRecipeInfo(Recipe recipe)
    {
        RecipeInfo.SetActive(true);
        ShowMaterialsList(recipe);
        GameObject ImageObj = RecipeInfo.transform.Find("Image").gameObject;
        GameObject NameObj = RecipeInfo.transform.Find("Name").gameObject;
        GameObject DescriptionObj = RecipeInfo.transform.Find("Description").gameObject;
        GameObject EffectObj = RecipeInfo.transform.Find("Effect").gameObject;
        GameObject CraftObj = RecipeInfo.transform.Find("Craft").gameObject;

        Image Image = ImageObj.GetComponent<Image>();
        TMP_Text Name = NameObj.GetComponent <TMP_Text>();
        TMP_Text Description = DescriptionObj.GetComponent<TMP_Text>();
        TMP_Text Effect = EffectObj.GetComponent<TMP_Text>();
        Button Craft = CraftObj.GetComponent<Button>();


        if (!recipe.IsEquipment)
        {
            Image.sprite = recipe.Item.Image;
            Name.text = recipe.Item.Name;
            Description.text = recipe.Item.Description;
            Effect.text = recipe.Item.EffectDescription;
        }
        else
        {
            Image.sprite = recipe.Equipment.Image;
            Name.text = recipe.Equipment.Name;
            Description.text = recipe.Equipment.Description;
            Effect.text = "";
        }

        Craft.onClick.RemoveAllListeners();
        Craft.onClick.AddListener(() => LetCraft(recipe));

    }

    public void ShowMaterialsList(Recipe recipe)
    {
        MaterialsList.SetActive(true);
        GameObject Viewport = MaterialsList.transform.Find("Viewport").gameObject;
        GameObject Content = Viewport.transform.Find("Content").gameObject;
        for (int i = 0; i< Content.transform.childCount; i++)
        {
            int index = i;
            GameObject MaterialObj = Content.transform.GetChild(index).gameObject;
            if(index < recipe.Materials.Count)
            {
                GameObject ImageObj = MaterialObj.transform.Find("Image").gameObject;
                GameObject NameObj = MaterialObj.transform.Find("Name").gameObject;

                Image Image = ImageObj.GetComponent<Image>();
                TMP_Text Name = NameObj.GetComponent<TMP_Text>();

                Name.text = recipe.Materials[index].Quantity + " x " + recipe.Materials[index].Item.Name;
                Image.sprite = recipe.Materials[index].Item.Image;
                MaterialObj.SetActive(true);

            }    
            else
                MaterialObj.SetActive(false);
        }
    }

  
    IEnumerator ShowNotification(string result)
    {
        GameObject ProgressBar = Notification.transform.Find("Progress").gameObject;
        GameObject Noti = Notification.transform.Find("Noti").gameObject;
        Slider Progress = ProgressBar.GetComponent<Slider>();
        TMP_Text NotiText = Noti.GetComponent<TMP_Text>();
        NotiText.text = "Crafting";
        while (Progress.value < 1f)
        {
            Progress.value = Progress.value + 0.025f;
            yield return new WaitForSeconds(0.05f);
        }
        NotiText.text = result;
        Progress.value = 0f;
    }

    public void LetCraft(Recipe recipe)
    {
        if(recipe.CraftItem(SaveSystem.saveLoad.inventory))
        {
            StartCoroutine(ShowNotification("Chế tạo thành công!"));
        }    
        else
        {
            StartCoroutine(ShowNotification("Chế tạo thất bại!"));
        }
    }    

}
