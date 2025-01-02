using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Enemy")]
[System.Serializable]
public class Enemy : ScriptableObject
{
    public Sprite imageEnemy;
    public Sprite imageEmenyInBattle;

    public int Id;
    public string Name;
    public string Description = "An Enemy";

    public int Level = 1;
	public int DropEXP = 5;
	//Recipe: DropEXPReal = DropEXP*(Level/3)

    public float BaseHP = 100;
    public float HP = 100;
    public float MaxHP = 100;

    public float BaseDEF = 50;
    public float DEF = 50;

    public float BaseMagicRES = 50;
    public float MagicRES = 50;

    public float BaseATK = 50;
    public float ATK = 50;

    public float BaseMagic = 50;
    public float Magic = 50;

    public float BaseSpeed = 10;
    public float Speed = 10;

	public List<Skill> skill;

	//Hit, Slash, Shoot, QuickShoot
	public string AttackAnimationType = "Hit";

	//If enemy cast protect
	private bool IsProtect = false;

	public int BuffATKCount = 0;
	public int BuffATK = 0;

	public int BuffDEFCount = 0;
	public int BuffDEF = 0;

	public int BuffSpeedCount = 0;
	public int BuffSpeed = 0;
	public void InitialStat()
    {
        MaxHP = BaseHP + BaseHP * (Level - 1) / 10;
        HP = MaxHP;

        DEF = BaseDEF + BaseDEF * (Level-1) / 10;
        DEF = BaseATK + BaseATK * (Level - 1) / 10;
        Magic = BaseMagic + BaseMagic * (Level - 1) / 10;
        MagicRES = BaseMagicRES + BaseMagicRES * (Level - 1) / 10;
        Speed = BaseSpeed + BaseSpeed * (Level - 1) / 10;
		IsProtect = false;
		
	}

	public void ReStat()
	{
		if (HP > MaxHP)
		{
			HP = MaxHP;
		}
		DEF = BaseDEF + BaseDEF * (Level - 1) / 10;
		ATK = BaseATK + BaseATK * (Level - 1) / 10;
		Magic = BaseMagic + BaseMagic * (Level - 1) / 10;
		MagicRES = BaseMagicRES + BaseMagicRES * (Level - 1) / 10;
		Speed = BaseSpeed + BaseSpeed * (Level - 1) / 10;

		ATK = ATK + ATK * BuffATK/100;
		Magic = Magic + Magic * BuffATK/100;

		DEF = DEF + DEF * BuffDEF/100;
		MagicRES = MagicRES + MagicRES * BuffDEF / 100;

		Speed = Speed + Speed * BuffSpeed /100;
	}

	public void CastSkill(Enemy enemy, Character character, Skill skill)
	{
		if (skill.IsAttack)
		{
			character.ReceiveDamage(skill.DealDamage(this), skill.DamageType);
		}
		if (skill.IsBuffATK)
		{
			if (skill.Target == "Self") skill.DealBuffATK(this);
			else if (skill.Target == "Team") skill.DealBuffATK(character);
			else if (skill.Target == "Enemy") skill.DealBuffATK(enemy);
		}
		if (skill.IsBuffDEF)
		{
			if (skill.Target == "Self") skill.DealBuffDEF(this);
			else if (skill.Target == "Team") skill.DealBuffDEF(character);
			else if (skill.Target == "Enemy") skill.DealBuffDEF(enemy);
		}
		if (skill.IsBuffSpeed)
		{
			if (skill.Target == "Self") skill.DealBuffSpeed(this);
			else if (skill.Target == "Team") skill.DealBuffSpeed(character);
			else if (skill.Target == "Enemy") skill.DealBuffSpeed(enemy);
		}
		if (skill.IsRestore)
		{
			if (skill.Target == "Self") skill.Restore(this);
			else if (skill.Target == "Team") skill.Restore(character);
			else if (skill.Target == "Enemy") skill.Restore(enemy);
		}
		TurnCount();
	}

	public void ReceiveDamage(float damage, string type)
    {
        if (IsProtect)
		{
			damage = damage * 60 / 100;
		}
			
        if (type == "Magic")
		{
			float HPLost = damage * (1 - (this.MagicRES / (this.MagicRES + 33.5f)));
			this.HP = this.HP - HPLost;
		}
		else //Physic
		{
			float HPLost = damage * (1 - (this.DEF / (this.DEF + 33.5f)));
			this.HP = this.HP - HPLost;
		}
	}

	public void BuffCount()
	{
		BuffATKCount--;
		if (BuffATKCount <= 0) BuffATK = 0;

		BuffDEFCount--;
		if (BuffDEFCount <= 0) BuffDEF = 0;

		BuffSpeedCount--;
		if (BuffSpeedCount <= 0) BuffSpeed = 0;
	}

	public void ResetAllBuffCount()
	{
		BuffATKCount = 0;
		BuffDEFCount = 0;
		BuffSpeedCount = 0;
	}

	public void NormalAttack(Character character)
	{
		float PhysicDamage = this.ATK / 3 * 2;
		character.ReceiveDamage(PhysicDamage, "Physic");
		TurnCount();
	}

	public void SetProtect()
	{
		TurnCount();
		IsProtect = true;
	}
	public void UnProtect()
	{
		IsProtect = false;
	}

	public bool CheckProtect()
	{
		if (IsProtect)
		{
			return true;
		}
		else { return false; }
	}

	public void TurnCount()
	{
		UnProtect();
		BuffCount();
		ReStat();
	}

	public Enemy Clone()
	{
		Enemy clone = ScriptableObject.CreateInstance<Enemy>();

		// Copy properties
		clone.imageEnemy = this.imageEnemy;
		clone.imageEmenyInBattle = this.imageEmenyInBattle;
		clone.Id = this.Id;
		clone.Name = this.Name;
		clone.Description = this.Description;
		clone.Level = this.Level;
		clone.BaseHP = this.BaseHP;
		clone.HP = this.HP;
		clone.MaxHP = this.MaxHP;
		clone.BaseDEF = this.BaseDEF;
		clone.DEF = this.DEF;
		clone.BaseMagicRES = this.BaseMagicRES;
		clone.MagicRES = this.MagicRES;
		clone.BaseATK = this.BaseATK;
		clone.ATK = this.ATK;
		clone.BaseMagic = this.BaseMagic;
		clone.Magic = this.Magic;
		clone.BaseSpeed = this.BaseSpeed;
		clone.Speed = this.Speed;
		clone.AttackAnimationType = this.AttackAnimationType;
		clone.DropEXP = this.DropEXP;
		clone.skill = this.skill;

		return clone;

	}
}
