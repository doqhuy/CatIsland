using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class SkillDisplay : MonoBehaviour
{
	private SaveSystem SaveSystem = SaveSystem.Instance;
	private GeneralInformation GeneralInformation = GeneralInformation.Instance;

	public GameObject SkillsList;
	public GameObject SkillInfor;
	public GameObject CharacterSkill;

    private void Start()
	{
        ShowListSkills();
    }

    private void Update()
	{
		ShowCharacterInfo();
	}

    private void OnEnable()
    {
        SkillInfor.SetActive(false);
    }

	public void ShowCharacterInfo()
	{
        Character thisCharacter = SaveSystem.saveLoad.team.Teamate[GeneralInformation.charSelectionNumber];

        GameObject Avatar = CharacterSkill.transform.Find("Avatar").gameObject;
        GameObject Name = CharacterSkill.transform.Find("Name").gameObject;
		GameObject SkillCount = CharacterSkill.transform.Find("SkillCount").gameObject;


        Image AvatarImage = Avatar.GetComponent<Image>();
        TMP_Text NameText = Name.GetComponent<TMP_Text>();
        TMP_Text SkillCountText = SkillCount.GetComponent<TMP_Text>();


        AvatarImage.sprite = thisCharacter.Avatar;
        NameText.text = thisCharacter.Name;
		SkillCountText.text = "Skill Learned: " + thisCharacter.learnedSkills.Count.ToString();
    }	


	public void ShowSkillInfo(Skill skill, int LvReq)
	{
		SkillInfor.SetActive(true);
		Character thisCharacter = SaveSystem.saveLoad.team.Teamate[GeneralInformation.charSelectionNumber];


		GameObject Image = SkillInfor.transform.Find("Image").gameObject;
		GameObject Name = SkillInfor.transform.Find("Name").gameObject;
		GameObject LvReqObj = SkillInfor.transform.Find("LvReq").gameObject;
		GameObject ManaCost = SkillInfor.transform.Find("ManaCost").gameObject;
		GameObject Type = SkillInfor.transform.Find("Type").gameObject;
		GameObject Effect = SkillInfor.transform.Find("Effect").gameObject;
		GameObject Description = SkillInfor.transform.Find("Description").gameObject;
        GameObject Learned = SkillInfor.transform.Find("Learned").gameObject;

        GameObject Learn = SkillInfor.transform.Find("Learn").gameObject;

		Image ImageImage = Image.GetComponent<Image>();
		TMP_Text NameText = Name.GetComponent<TMP_Text>();
		TMP_Text LvReqText = LvReqObj.GetComponent<TMP_Text>();
		TMP_Text ManaCostText = ManaCost.GetComponent<TMP_Text>();
		TMP_Text TypeText = Type.GetComponent<TMP_Text>();
		TMP_Text EffectText = Effect.GetComponent<TMP_Text>();
		TMP_Text DescriptionText = Description.GetComponent<TMP_Text>();
        TMP_Text LearnedText = Learned.GetComponent<TMP_Text>();

        Button LearnButton = Learn.GetComponent<Button>();

        

        ImageImage.sprite = skill.Image;
		NameText.text = skill.Name;
		LvReqText.text = LvReq.ToString();
		ManaCostText.text = skill.MPCost.ToString();
		//TypeText.text = skill.Type;
		EffectText.text = skill.Effect;
		DescriptionText.text = skill.Description;

        bool isSkillLearned = thisCharacter.learnedSkills.Contains(skill);
        LearnButton.interactable = true;
        if (isSkillLearned  ||  LvReq > thisCharacter.Level)
		{
            LearnButton.interactable = false;
        }


        LearnButton.onClick.RemoveAllListeners();

        LearnButton.onClick.AddListener(() =>
        {			
            LearnButton.interactable = false;
            thisCharacter.learnedSkills.Add(skill);
        });
    }
	public void ShowListSkills()
    {
		Character thisCharacter = SaveSystem.saveLoad.team.Teamate[GeneralInformation.charSelectionNumber];

		GameObject Viewport = SkillsList.transform.Find("Viewport").gameObject;
		GameObject Content = Viewport.transform.Find("Content").gameObject;

		for (int i = 0; i < Content.transform.childCount; i++)
		{
			GameObject SkillObj = Content.transform.GetChild(i).gameObject;
			GameObject Image = SkillObj.transform.Find("Image").gameObject;
			GameObject LvReq = SkillObj.transform.Find("LvReq").gameObject;
			GameObject Name = SkillObj.transform.Find("Name").gameObject;

			Image ImageImage = Image.GetComponent<Image>();
			TMP_Text LvReqText = LvReq.GetComponent<TMP_Text>();
			TMP_Text NameText = Name.GetComponent<TMP_Text>();

			Button MapInfor = SkillObj.GetComponent<Button>();
			MapInfor.onClick.RemoveAllListeners();

			if (i < thisCharacter.ListSkill.Count)
			{
				SkillObj.SetActive(true);
				Skill thisSkill = thisCharacter.ListSkill[i].Skill;
				int lvrq = thisCharacter.ListSkill[i].LvRequired;
				ImageImage.sprite = thisSkill.Image;
				NameText.text = thisSkill.Name;

				LvReqText.text = "LvReq:" + lvrq.ToString();
				MapInfor.onClick.AddListener(() =>
				{
					ShowSkillInfo(thisSkill, lvrq);
				});

				if(lvrq > thisCharacter.Level)
				{
					MapInfor.interactable = false;
				}
			}
			else
			{
				SkillObj.SetActive(false);
			}

		}

	}
}
