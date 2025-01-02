using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class TeamDisplay : MonoBehaviour
{
    public GameObject CharacterClaimedList;
    public GameObject CharacterInfor;
    public GameObject InTeamList;

    private SaveSystem SaveSystem = SaveSystem.Instance;
    private GeneralInformation GeneralInformation = GeneralInformation.Instance;


    private void Start()
	{
    }

    private void OnEnable()
    {
        CharacterInfor.SetActive(false);
    }

    void ShowCharacterInfor(Character character)
	{
		CharacterInfor.SetActive(true);

		GameObject avatar = CharacterInfor.transform.Find("Avatar").gameObject;
        Image avatarImage = avatar.GetComponent<Image>();
        avatarImage.sprite = character.Avatar;

        GameObject level = CharacterInfor.transform.Find("Lv").gameObject;
        TMP_Text levelText = level.GetComponent<TMP_Text>();
        levelText.text = character.Level.ToString();

        GameObject name = CharacterInfor.transform.Find("Name").gameObject;
        TMP_Text nameText = name.GetComponent<TMP_Text>();
        nameText.text = character.Name;

        GameObject job = CharacterInfor.transform.Find("Job").gameObject;
        TMP_Text jobText = job.GetComponent<TMP_Text>();
		if(character.Job!=null)
		{
            jobText.text = character.Job.Name;
        }
		else
		{
            jobText.text = "None Job";
        }

        GameObject description = CharacterInfor.transform.Find("Description").gameObject;
        TMP_Text descriptionText = description.GetComponent<TMP_Text>();
        descriptionText.text = character.Description;

		GameObject Slot1 = CharacterInfor.transform.Find("Slot1").gameObject;
		Button Slot1Button = Slot1.GetComponent<Button>();
		Slot1Button.onClick.RemoveAllListeners();
		Slot1Button.onClick.AddListener(() =>
		{
			SaveSystem.saveLoad.team.AddToTeam(0, character);
			ShowCharacterInfor(character);
		}
		);

		GameObject Slot2 = CharacterInfor.transform.Find("Slot2").gameObject;
		Button Slot2Button = Slot2.GetComponent<Button>();
		Slot2Button.onClick.AddListener(() =>
		{
			SaveSystem.saveLoad.team.AddToTeam(1, character);
			ShowCharacterInfor(character);


		}
		);

		GameObject Slot3 = CharacterInfor.transform.Find("Slot3").gameObject;
		Button Slot3Button = Slot3.GetComponent<Button>();
		Slot3Button.onClick.AddListener(() =>
		{
			SaveSystem.saveLoad.team.AddToTeam(2, character);
			ShowCharacterInfor(character);


		}
		);

		GameObject Slot4 = CharacterInfor.transform.Find("Slot4").gameObject;
		Button Slot4Button = Slot4.GetComponent<Button>();
		Slot4Button.onClick.AddListener(() =>
		{
			SaveSystem.saveLoad.team.AddToTeam(3, character);
			ShowCharacterInfor(character);

		}
		);

		if (SaveSystem.saveLoad.team.CheckInTeam(character))
		{
			Slot1.SetActive(false);
			Slot2.SetActive(false);
			Slot3.SetActive(false);
			Slot4.SetActive(false);
		}
        else
        {
            Slot1.SetActive(true);
			Slot2.SetActive(true);
			Slot3.SetActive(true);
			Slot4.SetActive(true);
        }



    }
	void ShowCharacterInTeam()
	{
		GameObject Teamate1 = InTeamList.transform.Find("Slot1").gameObject;
		Image Teamate1Image = Teamate1.GetComponent<Image>();
		Teamate1Image.sprite = SaveSystem.saveLoad.team.Teamate[0].Avatar;

		GameObject Teamate2 = InTeamList.transform.Find("Slot2").gameObject;
		Image Teamate2Image = Teamate2.GetComponent<Image>();
		if (SaveSystem.saveLoad.team.Teamate[1] != null)
		{
			Teamate2.SetActive(true);
			Teamate2Image.sprite = SaveSystem.saveLoad.team.Teamate[1].Avatar;
		}
		else
		{
            Teamate2.SetActive(false);
        }
        GameObject remove2ButtonObj = Teamate2.transform.Find("Remove").gameObject;
		Button remove2Button = remove2ButtonObj.GetComponent<Button>();
		remove2Button.onClick.RemoveAllListeners();
		remove2Button.onClick.AddListener(() =>
		{
			SaveSystem.saveLoad.team.RemoveFromTeam(1);
		}
		);


		GameObject Teamate3 = InTeamList.transform.Find("Slot3").gameObject;
		Image Teamate3Image = Teamate3.GetComponent<Image>();
		if(SaveSystem.saveLoad.team.Teamate[2]!=null)
		{
			Teamate3.SetActive(true);
			Teamate3Image.sprite = SaveSystem.saveLoad.team.Teamate[2].Avatar;
		}
		else
		{
            Teamate3.SetActive(false);
        }
        GameObject remove3ButtonObj = Teamate3.transform.Find("Remove").gameObject;
		Button remove3Button = remove3ButtonObj.GetComponent<Button>();
		remove3Button.onClick.RemoveAllListeners();
		remove3Button.onClick.AddListener(() =>
		{
			SaveSystem.saveLoad.team.RemoveFromTeam(2);
		}
		);


		GameObject Teamate4 = InTeamList.transform.Find("Slot4").gameObject;
		Image Teamate4Image = Teamate4.GetComponent<Image>();
		if (SaveSystem.saveLoad.team.Teamate[3] != null)
		{
            Teamate4.SetActive(true);
            Teamate4Image.sprite = SaveSystem.saveLoad.team.Teamate[3].Avatar;
		}
		else
		{
            Teamate4.SetActive(false);
        }

        GameObject remove4ButtonObj = Teamate4.transform.Find("Remove").gameObject;
		Button remove4Button = remove4ButtonObj.GetComponent<Button>();
		remove4Button.onClick.RemoveAllListeners();
		remove4Button.onClick.AddListener(() =>
		{
			SaveSystem.saveLoad.team.RemoveFromTeam(3);
		}
		);


	}

	void ShowListCharacterClaimed()
    {
		GameObject Viewport = CharacterClaimedList.transform.Find("Viewport").gameObject;
		GameObject content = Viewport.transform.Find("Content").gameObject;
		for(int i=0; i< content.transform.childCount; i++)
		{
			GameObject nextObj = content.transform.GetChild(i).gameObject;
			if(i< SaveSystem.saveLoad.team.ClaimedCharacter.Count)
			{
                nextObj.SetActive(true);
                GameObject Avatar = nextObj.transform.Find("Avatar").gameObject;
                Image AvatarImage = Avatar.GetComponent<Image>();
                AvatarImage.sprite = SaveSystem.saveLoad.team.ClaimedCharacter[i].Avatar;

                GameObject Name = nextObj.transform.Find("Name").gameObject;
                TMP_Text NameText = Name.GetComponent<TMP_Text>();
                NameText.text = SaveSystem.saveLoad.team.ClaimedCharacter[i].Name;

                GameObject Lv = nextObj.transform.Find("Lv").gameObject;
                TMP_Text LvText = Lv.GetComponent<TMP_Text>();
                LvText.text = SaveSystem.saveLoad.team.ClaimedCharacter[i].Level.ToString();

                Character inforCharacter = SaveSystem.saveLoad.team.ClaimedCharacter[i];
                Button inforButton = nextObj.GetComponent<Button>();
                inforButton.onClick.RemoveAllListeners();
                inforButton.onClick.AddListener(() => ShowCharacterInfor(inforCharacter));
            }
			else
			{
				nextObj.SetActive(false);
			}
		}
		
	}    
	// Update is called once per frame
	void Update()
    {
		ShowCharacterInTeam();
		ShowListCharacterClaimed();
	}
}
