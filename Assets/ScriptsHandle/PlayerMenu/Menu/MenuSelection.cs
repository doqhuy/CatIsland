using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ToMenuSelection
{
	public GameObject featurePanel;
	public Button featureButton;

}
public class MenuSelection : MonoBehaviour
{
	public GameObject[] toggles;
	public ToMenuSelection[] selections;
	public GameObject menuToHide;


	private void Update()
	{
		GameObject infor = GameObject.Find("GeneralInformation");
		GeneralInformation generalInformation = infor.GetComponent<GeneralInformation>();
		for (int i = 0; i < toggles.Length; i++)
		{
			//Hiển thị các thông tin nhân vật
			GameObject Avatar = toggles[i].transform.Find("Avatar").gameObject;
			Image AvatarImage = Avatar.GetComponent<Image>();
			GameObject Name = toggles[i].transform.Find("Name").gameObject;
			TMP_Text NameText = Name.GetComponent<TMP_Text>();
			GameObject Lv = toggles[i].transform.Find("Lv").gameObject;
			TMP_Text LvText = Lv.GetComponent<TMP_Text>();
			GameObject Job = toggles[i].transform.Find("Job").gameObject;
			TMP_Text JobText = Job.GetComponent<TMP_Text>();
			GameObject HP = toggles[i].transform.Find("HP").gameObject;
			TMP_Text HPText = HP.GetComponent<TMP_Text>();
			GameObject MP = toggles[i].transform.Find("MP").gameObject;
			TMP_Text MPText = MP.GetComponent<TMP_Text>();

			GameObject SaveS = GameObject.Find("SaveSystem");
			SaveSystem save = SaveS.GetComponent<SaveSystem>();
			if(save.saveLoad.team.Teamate[i] != null)
			{
				Character thisChar = save.saveLoad.team.Teamate[i];
				thisChar.InitialStat();
				toggles[i].SetActive(true);
				AvatarImage.sprite = thisChar.Avatar;
				NameText.text = thisChar.Name;
				LvText.text = thisChar.Level.ToString();
				if(thisChar.Job != null)
				{
                    JobText.text = thisChar.Job.Name;
                }
				else
				{
                    JobText.text = "None Job";
                }
                HPText.text = thisChar.HP.ToString() + "/" + thisChar.MaxHP;
				MPText.text = thisChar.MP.ToString() + "/" + thisChar.MaxMP;
			}
			else
			{
				toggles[i].SetActive(false);
			}	





			//Đặt các toggle để chuyển character mapping hiện tại
			int x = i;
			Toggle toggle = toggles[i].GetComponent<Toggle>();
			toggle.onValueChanged.RemoveAllListeners();
			toggle.onValueChanged.AddListener((isOn) =>
			{
				if (isOn)
				{
					generalInformation.charSelectionNumber = x;
				}
			}
			);

			//Tự động lấy toggle hiện tại
			if (i == generalInformation.charSelectionNumber)
			{
				toggle.isOn = true;
				if(save.saveLoad.team.Teamate[i] == null)
				{
					generalInformation.charSelectionNumber = 0;
				}	
			}

		}
		for(int i = 0; i < selections.Length; i++)
		{
			GameObject activeObj = selections[i].featurePanel.gameObject;
			Button button = selections[i].featureButton;
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(()=>
			{
				menuToHide.SetActive(false);
				activeObj.SetActive(true);
			});

		}
	}
}
