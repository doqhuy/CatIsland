using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
	public int Id;
	public Sprite Image;
	public bool IsStack = false;
	public bool IsKeyItem = false;
	public bool Usable = true;
	public bool Removable = true;

    public string Name;
    public string EffectDescription;
    public string Description;

	//Type Heal or Buff
	public bool IsHeal = false;
	public bool IsBuff = false;
	public bool IsMultiUse = false;

	//Revive
	public string SpecialEffect;
	public string MaterialTag;

	public int HPRestore = 0;
	public int MPRestore = 0;

	public int BuffCount = 0;

	public int ATKBuff = 0;
	public int DEFBuff = 0;
	public int SpeedBuff = 0;
	public int LuckBuff = 0;

	public int Price = 100;
	public bool Restore(Character character)
	{
		if(character.HP <= 0)
		{
			return false;
		}	
		else
		{
			character.HP = character.HP + HPRestore;
			character.MP = character.MP + MPRestore;
			character.InitialStat();
			return true;
		}	
	}	
	 
	public bool Buff(Character character)
	{
		if (character.HP <= 0)
		{
			return false;
		}
		else
		{
			character.BuffATKCount = BuffCount;
			character.BuffDEFCount = BuffCount;
			character.BuffSpeedCount = BuffCount;

			character.BuffATK = ATKBuff;
			character.BuffDEF = DEFBuff;
			character.BuffSpeed = SpeedBuff;
			return true;
		}	
		
	}

	public bool Revive(Character character)
	{
		if (character.HP > 0) return false;
		else character.HP += 1;
		return true;
	}	
}
