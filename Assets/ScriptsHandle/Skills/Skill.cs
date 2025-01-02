using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[System.Serializable]
[CreateAssetMenu(fileName = "New Skill", menuName = "ScriptableObjects/Skill")]
public class Skill : ScriptableObject
{
    public Sprite Image;
	
    public string Name;
    public string Description;

    //Magic, Physic
    public string DamageType = "Physic";

    //Single, Multiple
    public bool IsMultiple = false;
	//Enemy, Self, Team
	public string Target = "Enemy";

    public bool IsAttack = false;
	public bool IsBuffATK = false;
	public bool IsBuffDEF = false;
	public bool IsBuffSpeed = false;
	
	public bool IsRestore = false;
    public int BuffCount = 0;

    //Magic_Element, 
    public string AnimationType = "Hit";

    //Restore, Buff, Debuff...
    public string Effect = "Restore 10 HP";

    public int MPCost = 0;

    public int ATKScale = 0;
    public int MagicScale = 0;

    public int RestoreHP = 0;
    public int RestoreMP = 0;
 
    public int BuffATK = 0;
    public int BuffDEF = 0;
    public int BuffMagic = 0;
    public int BuffMagicRES = 0;
    public int BuffSpeed = 0;
    public int BuffLuck = 0;

	
	public void Restore(Character character)
	{
		if(character.HP > 0)
		character.HP += RestoreHP;
		character.MP += RestoreMP;
		
	}

	public void Restore(Enemy enemy)
	{
		if(enemy.HP > 0)
		enemy.HP += RestoreHP;
		if (enemy.HP > enemy.MaxHP)
		{
			enemy.HP = enemy.MaxHP;
		}
	}

	public float DealDamage(Character character)
    {
        float damage = character.ATK * (ATKScale / 100) + character.Magic * (MagicScale / 100);
        character.MP = character.MP - MPCost;
        return damage;
    }    

    public float DealDamage(Enemy enemy)
    {
		float damage = enemy.ATK * (ATKScale / 100) + enemy.Magic * (MagicScale / 100);
		return damage;
	}

    public void DealBuffATK(Character character)
    {
		character.BuffATK = BuffATK;
        character.BuffATKCount = BuffCount;
		character.ReStat();
    }

	public void DealBuffATK(Enemy enemy)
	{
		enemy.BuffATK = BuffATK;
		enemy.BuffATKCount = BuffCount;
		enemy.ReStat();
	}

	public void DealBuffDEF(Character character)
	{
		character.BuffDEF = BuffDEF;
		character.BuffDEFCount = BuffCount;
		character.ReStat();
	}

	public void DealBuffDEF(Enemy enemy)
	{
		enemy.BuffDEF = BuffDEF;
		enemy.BuffDEFCount = BuffCount;
		enemy.ReStat();
	}

	public void DealBuffSpeed(Character character)
	{
		character.BuffSpeed = BuffSpeed;
		character.BuffSpeedCount = BuffCount;
		character.ReStat();
	}

	public void DealBuffSpeed(Enemy enemy)
	{
		enemy.BuffSpeed = BuffSpeed;
		enemy.BuffSpeedCount = BuffCount;
		enemy.ReStat();
	}


}
