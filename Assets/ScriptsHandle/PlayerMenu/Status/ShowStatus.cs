using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowStatus : MonoBehaviour
{
    public Image avatarCharacter;
    public TMP_Text nameCharacter;
    public TMP_Text descriptionCharacter;
    public TMP_Text lvCharacter;
    public TMP_Text jobCharacter;
    public TMP_Text expCharacter;
    public TMP_Text hpCharacter;
    public TMP_Text mpCharacter;
    public TMP_Text atkCharacter;
	public TMP_Text defCharacter;
	public TMP_Text speedCharacter;
	public TMP_Text magicCharacter;
	public TMP_Text m_resCharacter;
	public TMP_Text luckCharacter;

    public Image helmetImage;
    public TMP_Text helmetText;
    public Image armorImage;
    public TMP_Text armorText;
    public Image weaponImage;
    public TMP_Text weaponText;
    public Image accessoryImage;
    public TMP_Text accessoryText;

    public SaveSystem save;
    public GeneralInformation infor;

	public void Start()
	{
		GameObject saveObject = GameObject.Find("SaveSystem");
		save = saveObject.GetComponent<SaveSystem>();
		GameObject inforObject = GameObject.Find("GeneralInformation");
		infor = inforObject.GetComponent<GeneralInformation>();
	}
	public void Update()
	{
        Character character = save.saveLoad.team.Teamate[infor.charSelectionNumber];
		character.InitialStat();
        avatarCharacter.sprite = character.Avatar;
        nameCharacter.text = character.Name;
        descriptionCharacter.text = character.Description;
		lvCharacter.text = "Level: " + character.Level.ToString();
		jobCharacter.text = "Job: " + character.Job;
		expCharacter.text = "EXP: " + character.EXP.ToString() + "/" + character.UpEXP.ToString();
		hpCharacter.text = "HP: " + character.HP.ToString() + "/" + character.MaxHP.ToString();
		mpCharacter.text = "MP: " + character.MP.ToString() + "/" + character.MaxMP.ToString();
		atkCharacter.text = "ATK: " + character.ATK.ToString();
		defCharacter.text = "DEF: " + character.DEF.ToString();
		speedCharacter.text = "Speed: " + character.Speed.ToString();
		magicCharacter.text = "Magic: " + character.Magic.ToString();
		m_resCharacter.text = "Magic Res: " + character.MagicRES.ToString();
		luckCharacter.text = "Luck: " + character.Luck.ToString();

		if (character.Helmet!=null)
		{
			helmetImage.sprite = character.Helmet.Image;
			helmetText.text = character.Helmet.name;
		}
		else
		{
			helmetImage.sprite = null;
			helmetText.text = "empty";
		}

		if (character.Armor != null)
		{
			armorImage.sprite = character.Armor.Image;
			armorText.text = character.Armor.name;
		}
		else
		{
			armorImage.sprite = null;
			armorText.text = "empty";
		}

		// Weapon
		if (character.Weapon != null)
		{
			weaponImage.sprite = character.Weapon.Image;
			weaponText.text = character.Weapon.name;
		}
		else
		{
			weaponImage.sprite = null;
			weaponText.text = "empty";
		}

		// Accessory
		if (character.Accessory != null)
		{
			accessoryImage.sprite = character.Accessory.Image;
			accessoryText.text = character.Accessory.name;
		}
		else
		{
			accessoryImage.sprite = null;
			accessoryText.text = "empty";
		}
	}

}
