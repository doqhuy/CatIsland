using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamateEXPCalculator : MonoBehaviour
{
	public Button ReturnToCurrentMap;

	public TMP_Text EXPText;
	public Slider EXPBar; 

	//public Slider HPBar;
	//public Slider MPBar;

    public int TeamateIndex;
	SaveSystem SaveSystem = SaveSystem.Instance;
    GeneralInformation GeneralInformation = GeneralInformation.Instance;


    public Image Avatar;
	public TMP_Text Name;
	//public TMP_Text HP;
	//public TMP_Text MP;
	public TMP_Text Lv;

	public Character thisCharacter;
	private void Start()
	{

		for(int i=0; i<GeneralInformation.EnemyInBattle.Count; i++)
		{
			for(int j=0; j<SaveSystem.saveLoad.questClaim.quests.Count; j++)
			{
				Quest thisq = SaveSystem.saveLoad.questClaim.quests[j];
				thisq.CheckKill(GeneralInformation.EnemyInBattle[i]);
            }	
		}	
		
		thisCharacter = SaveSystem.saveLoad.team.Teamate[TeamateIndex];

		if (thisCharacter!=null)
		{
			thisCharacter.InitialStat();
		}
		else
		{
			this.gameObject.SetActive(false);
		}
		EXPCalculator();
	}

	private void Update()
	{
		if(thisCharacter!=null)
		{
			GetCharacterInformation();
		}
		if(thisCharacter.HP <= 0)
		{
			this.gameObject.SetActive(false);
		}	
	}

	void GetCharacterInformation()
	{
		EXPText.text = thisCharacter.EXP + "/" + thisCharacter.UpEXP;
		EXPBar.value = (float)thisCharacter.EXP / (float)thisCharacter.UpEXP;


		Avatar.sprite = thisCharacter.Avatar;
		Name.text = thisCharacter.Name;
		//HP.text = "HP: " + thisCharacter.HP + "/" + thisCharacter.MaxHP;
		//MP.text = "MP: " + thisCharacter.MP + "/" + thisCharacter.MaxMP;
		Lv.text = "" + thisCharacter.Level;
		//HPBar.value = thisCharacter.HP / thisCharacter.MaxHP;
		//MPBar.value = thisCharacter.MP / thisCharacter.MaxMP;
	}

	public void EXPCalculator()
	{
		int alive = 0;
		for (int i = 0; i < 4; i++)
		{
			int index = i;
			Character thisChar = SaveSystem.saveLoad.team.Teamate[index];
			if (thisChar != null && thisChar.HP > 0)
			{
				alive++;
			}
		}
		int TotalDropEXP = 0;
		for (int i = 0; i < GeneralInformation.EnemyInBattle.Count; i++)
		{
			int index = i;
			Enemy thisEnemy = GeneralInformation.EnemyInBattle[index];
			TotalDropEXP = TotalDropEXP + thisEnemy.DropEXP * thisEnemy.Level / 3;
		}

		int eachCharEXP = TotalDropEXP / alive;
		if(this.gameObject.activeSelf)
		{
			StartCoroutine(IncreaseExpOverTime(eachCharEXP));
		}

	}

	IEnumerator IncreaseExpOverTime(int targetEXP)
	{
		while(targetEXP > 0)
		{
			targetEXP--;
			thisCharacter.EXP++;
			if(thisCharacter.EXP >= thisCharacter.UpEXP) 
			{
				thisCharacter.EXP = 0;
				thisCharacter.Level++;
				thisCharacter.InitialStat();
			}
			yield return new WaitForSeconds(0.05f);
		}
		if(ReturnToCurrentMap != null && targetEXP==0)
		{
			ReturnToCurrentMap.gameObject.SetActive(true);
		}

	}






}
