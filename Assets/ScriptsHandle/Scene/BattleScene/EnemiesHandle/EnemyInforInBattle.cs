using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInforInBattle : MonoBehaviour
{
    private GameObject EiB;
    public int EnemyNumber;

	public Image EAvatar;
	public TMP_Text Name;
	public TMP_Text HP;

	private void Start()
	{
		EiB = GameObject.Find("EnemiesInBattle");
	}
	// Update is called once per frame
	void Update()
    {
			EnemiesInBattle enemiesInBattle = EiB.GetComponent<EnemiesInBattle>();
			if(enemiesInBattle.Enemy[EnemyNumber] != null)
			{

			}
			

    }
}
