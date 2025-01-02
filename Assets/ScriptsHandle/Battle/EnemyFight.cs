using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFight : MonoBehaviour
{
	public int EnemyIndex;
	public Enemy thisEnemy;

	public Slider HPBar;
	public Image ImageEnemy;
	public TMP_Text HP;
    public TMP_Text Name;
	public GameObject Protect;
	public GameObject Buff;


	public GeneralInformation GeneralInformation;
	public SaveSystem SaveSystem;

	private void Start()
	{
		GeneralInformation = GeneralInformation.Instance;
		SaveSystem = SaveSystem.Instance;
		if (EnemyIndex < GeneralInformation.EnemyInBattle.Count)
		{
			thisEnemy = GeneralInformation.EnemyInBattle[EnemyIndex];
			thisEnemy.InitialStat();
		}
		else
		{
			this.gameObject.SetActive(false);
		}		
	}

	private void Update()
	{
		if(thisEnemy != null)
		{
			GetInforEnemy();
		}
        thisEnemy.ReStat();
        if (thisEnemy.HP <= 0)
		{
			thisEnemy.HP = 0;
            StartCoroutine(DisableGameObjectAfterDelay(1f));
        }	
		
	}

	

	void GetInforEnemy()
	{
		ImageEnemy.sprite = thisEnemy.imageEmenyInBattle;
		Name.text = thisEnemy.Name;
		HP.text = Math.Ceiling(thisEnemy.HP).ToString() + "/" + thisEnemy.MaxHP.ToString();
		float HPBarValue = (float)thisEnemy.HP / (float)thisEnemy.MaxHP;
		HPBar.value = HPBarValue;
		if(thisEnemy.CheckProtect())
		{
			Protect.SetActive(true);
		}
		else { Protect.SetActive(false); }

		GameObject ATKBuffObj = Buff.transform.Find("ATKBuff").gameObject;
		GameObject ATKDebuffObj = Buff.transform.Find("ATKDebuff").gameObject;
		GameObject DEFBuffObj = Buff.transform.Find("DEFBuff").gameObject;
		GameObject DEFDebuffObj = Buff.transform.Find("DEFDebuff").gameObject;
		GameObject SpeedBuffObj = Buff.transform.Find("SpeedBuff").gameObject;
		GameObject SpeedDebuffObj = Buff.transform.Find("SpeedDebuff").gameObject;

		if (thisEnemy.BuffATK == 0) { ATKBuffObj.SetActive(false); ATKDebuffObj.SetActive(false); }
		else if (thisEnemy.BuffATK > 0) { ATKBuffObj.SetActive(true); ATKDebuffObj.SetActive(false); }
		else if (thisEnemy.BuffATK < 0) { ATKBuffObj.SetActive(false); ATKDebuffObj.SetActive(true); }


		if (thisEnemy.BuffDEF == 0) { DEFBuffObj.SetActive(false); DEFDebuffObj.SetActive(false); }
		else if (thisEnemy.BuffDEF > 0) { DEFBuffObj.SetActive(true); DEFDebuffObj.SetActive(false); }
		else if (thisEnemy.BuffDEF < 0) { DEFBuffObj.SetActive(false); DEFDebuffObj.SetActive(true); }

		if (thisEnemy.BuffSpeed == 0) { SpeedBuffObj.SetActive(false); SpeedDebuffObj.SetActive(false); }
		else if (thisEnemy.BuffSpeed > 0) { SpeedBuffObj.SetActive(true); SpeedDebuffObj.SetActive(false); }
		else if (thisEnemy.BuffSpeed < 0) { SpeedBuffObj.SetActive(false); SpeedDebuffObj.SetActive(true); }

	}

    IEnumerator DisableGameObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

}
