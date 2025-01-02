using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

[System.Serializable]
[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObjects/Character")]
public class Character : ScriptableObject
{
    public int Id;
    public Sprite Avatar;
    public Sprite AvatarInBattle;
    public RuntimeAnimatorController AnimatorController;

    public string Name = "New Hero";
    public Job Job;
    public string Description = "";

    public int Level = 1;
    public int EXP = 0;
    public int UpEXP = 0;

    public int BaseHP = 150;
    public int HP = 150;
    public int MaxHP = 150;

    public int BaseMP = 100;
    public int MP = 100;
    public int MaxMP = 100;

    public float BaseATK = 50;
    public float ATK = 50;

    public float BaseMagic = 50;
    public float Magic = 50;

    public float BaseDEF = 50;
    public float DEF = 50;

    public float BaseMagicRES = 50;
    public float MagicRES = 50;

    public float BaseLuck = 2;
    public float Luck = 2;

    public float BaseSpeed = 20;
    public float Speed = 20;

    public Equipment Helmet;
    public Equipment Armor;
    public Equipment Weapon;
    public Equipment Accessory;

    public List<Skill> learnedSkills = new List<Skill>();

    [SerializeField]
    public List<CharacterListSkill> ListSkill;

    //Hit, Slash, Shoot, QuickShoot
    public string AttackAnimationType = "Hit";

    public bool IsPlaying = false;
    public bool IsInTeam = false;
    public bool IsClaimed = false;

    private bool IsProtect = false;

    public int BuffATKCount = 0;
    public int BuffATK = 0;

    public int BuffDEFCount = 0;
    public int BuffDEF = 0;

    public int BuffSpeedCount = 0;
    public int BuffSpeed = 0;

    public void InitialStat()
    {
        UpEXP = Level * 10 + Level ^ 2;

        MaxHP = BaseHP + BaseHP * (Level - 1) / 10;
        MaxMP = BaseMP + BaseMP * (Level - 1) / 10;


        ATK = BaseATK + BaseATK * (Level - 1) / 10;
        Magic = BaseMagic + BaseMagic * (Level - 1) / 10;

        DEF = BaseDEF + BaseDEF * (Level - 1) / 10;
        MagicRES = BaseMagicRES + BaseMagicRES * (Level - 1) / 10;

        Speed = BaseSpeed + BaseSpeed * (Level - 1) / 10;
        Luck = BaseLuck;

        if (Job != null)
        {
            if (Job.IsHPBonus) MaxHP += MaxHP * Job.HPBonusScale / 100;
            if (Job.IsATKBonus) ATK += ATK * Job.ATKBonusScale / 100;
            if (Job.IsMagicBonus) Magic += Magic * Job.MagicBonusScale / 100;
            if (Job.IsDEFBonus) DEF += DEF * Job.DEFBonusScale / 100;
            if (Job.IsMagicRESBonus) MagicRES += MagicRES * Job.MagicRESBonusScale / 100;
            if (Job.IsSpeedBonus) Speed += Speed * Job.SpeedBonusScale / 100;
            if (Job.IsLuckBonus) Luck += Luck * Job.LuckBonusScale / 100;
        }

        if (Helmet != null)
        {
            if (Helmet.Job.Name == Job.Name)
            {
                MaxHP += Helmet.HP;
                MaxMP += Helmet.MP;
                ATK += Helmet.ATK;
                Magic += Helmet.Magic;
                DEF += Helmet.MagicRES;
                MagicRES += Helmet.MagicRES;
                Speed += Helmet.Speed;
                Luck += Helmet.Luck;
            }
        }
        if (Armor != null)
        {
            if (Armor.Job.Name == Job.Name)
            {
                MaxHP += Armor.HP;
                MaxMP += Armor.MP;
                ATK += Armor.ATK;
                Magic += Armor.Magic;
                DEF += Armor.DEF;
                MagicRES += Armor.MagicRES;
                Speed += Armor.Speed;
                Luck += Armor.Luck;
            }
        }

        if (Weapon != null)
        {
            if (Weapon.Job.Name == Job.Name)
            {
                MaxHP += Weapon.HP;
                MaxMP += Weapon.MP;
                ATK += Weapon.ATK;
                Magic += Weapon.Magic;
                DEF += Weapon.DEF;
                MagicRES += Weapon.MagicRES;
                Speed += Weapon.Speed;
                Luck += Weapon.Luck;
            }
        }

        if (Accessory != null)
        {
            if (Accessory.Job.Name == Job.Name)
            {
                MaxHP += Accessory.HP;
                MaxMP += Accessory.MP;
                ATK += Accessory.ATK;
                Magic += Accessory.Magic;
                DEF += Accessory.DEF;
                MagicRES += Accessory.MagicRES;
                Speed += Accessory.Speed;
                Luck += Accessory.Luck;
            }
        }

        if (HP > MaxHP) HP = MaxHP;
        if (MP > MaxMP) MP = MaxMP;
        if (HP < 0) HP = 0;
        if (MP < 0) MP = 0;
    }

    public void ReStat()
    {
        MaxHP = BaseHP + BaseHP * (Level - 1) / 10;
        MaxMP = BaseMP + BaseMP * (Level - 1) / 10;

        ATK = BaseATK + BaseATK * (Level - 1) / 10;
        Magic = BaseMagic + BaseMagic * (Level - 1) / 10;

        DEF = BaseDEF + BaseDEF * (Level - 1) / 10;
        MagicRES = BaseMagicRES + BaseMagicRES * (Level - 1) / 10;

        Speed = BaseSpeed + BaseSpeed * (Level - 1) / 10;
        Luck = BaseLuck;

        if (Job != null)
        {
            if (Job.IsHPBonus) MaxHP += MaxHP * Job.HPBonusScale / 100;
            if (Job.IsATKBonus) ATK += ATK * Job.ATKBonusScale / 100;
            if (Job.IsMagicBonus) Magic += Magic * Job.MagicBonusScale / 100;
            if (Job.IsDEFBonus) DEF += DEF * Job.DEFBonusScale / 100;
            if (Job.IsMagicRESBonus) MagicRES += MagicRES * Job.MagicRESBonusScale / 100;
            if (Job.IsSpeedBonus) Speed += Speed * Job.SpeedBonusScale / 100;
            if (Job.IsLuckBonus) Luck += Luck * Job.LuckBonusScale / 100;
        }

        if (Helmet != null)
        {
            if (Helmet.Job.Name == Job.Name)
            {
                MaxHP += Helmet.HP;
                MaxMP += Helmet.MP;
                ATK += Helmet.ATK;
                Magic += Helmet.Magic;
                DEF += Helmet.MagicRES;
                MagicRES += Helmet.MagicRES;
                Speed += Helmet.Speed;
                Luck += Helmet.Luck;
            }
        }
        if (Armor != null)
        {
            if (Armor.Job.Name == Job.Name)
            {
                MaxHP += Armor.HP;
                MaxMP += Armor.MP;
                ATK += Armor.ATK;
                Magic += Armor.Magic;
                DEF += Armor.DEF;
                MagicRES += Armor.MagicRES;
                Speed += Armor.Speed;
                Luck += Armor.Luck;
            }
        }

        if (Weapon != null)
        {
            if (Weapon.Job.Name == Job.Name)
            {
                MaxHP += Weapon.HP;
                MaxMP += Weapon.MP;
                ATK += Weapon.ATK;
                Magic += Weapon.Magic;
                DEF += Weapon.DEF;
                MagicRES += Weapon.MagicRES;
                Speed += Weapon.Speed;
                Luck += Weapon.Luck;
            }
        }

        if (Accessory != null)
        {
            if (Accessory.Job.Name == Job.Name)
            {
                MaxHP += Accessory.HP;
                MaxMP += Accessory.MP;
                ATK += Accessory.ATK;
                Magic += Accessory.Magic;
                DEF += Accessory.DEF;
                MagicRES += Accessory.MagicRES;
                Speed += Accessory.Speed;
                Luck += Accessory.Luck;
            }
        }

        if (HP > MaxHP) HP = MaxHP;
        if (MP > MaxMP) MP = MaxMP;
        if (HP < 0) HP = 0;

        ATK = ATK + ATK * BuffATK / 100;
        Magic = Magic + Magic * BuffATK / 100;

        DEF = DEF + DEF * BuffDEF / 100;
        MagicRES = MagicRES + MagicRES * BuffDEF / 100;

        Speed = Speed + Speed * BuffSpeed / 100;
    }

    public void CastSkill(Enemy enemy, Character character, Skill skill)
    {
        if (skill.IsAttack)
        {
            enemy.ReceiveDamage(skill.DealDamage(this), skill.DamageType);
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

    public Equipment Equip(Equipment equipment)
    {
        Equipment returnEquipment = null;
        switch (equipment.Type)
        {
            case "Helmet": returnEquipment = this.Helmet; this.Helmet = equipment; break;
            case "Armor": returnEquipment = this.Armor; this.Armor = equipment; break;
            case "Weapon": returnEquipment = this.Weapon; this.Weapon = equipment; break;
            case "Accessory": returnEquipment = this.Accessory; this.Accessory = equipment; break;
        }
        return returnEquipment;
    }

    public Equipment Unequip(string EquipType)
    {
        Equipment returnEquipment = null;
        switch (EquipType)
        {
            case "Helmet": returnEquipment = this.Helmet; this.Helmet = null; break;
            case "Armor": returnEquipment = this.Armor; this.Armor = null; break;
            case "Weapon": returnEquipment = this.Weapon; this.Weapon = null; break;
            case "Accessory": returnEquipment = this.Accessory; this.Accessory = null; break;
        }
        return returnEquipment;
    }

    public void ReceiveDamage(float damage, string type)
    {
        if (IsProtect)
        {
            damage = damage * 60 / 100;
        }

        if (type == "Magic")
        {
            int HPLost = (int)(damage * (1 - (this.MagicRES / (this.MagicRES + 33.5f))));
            this.HP = this.HP - HPLost;
        }
        else //Physic
        {
            int HPLost = (int)(damage * (1 - (this.DEF / (this.DEF + 33.5f))));
            this.HP = this.HP - HPLost;
        }

    }

    public void TurnCount()
    {
        UnProtect();
        BuffCount();
        ReStat();
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

    public void NormalAttack(Enemy enemy)
    {
        float PhysicDamage = this.ATK / 2;
        enemy.ReceiveDamage(PhysicDamage, "Physic");
        this.MP += this.MaxMP * 10 / 100;
        TurnCount();
    }

    public bool CheckProtect()
    {
        if (IsProtect == true)
        {
            return true;
        }
        return false;
    }

    public void SetProtect()
    {
        this.MP += this.MaxMP * 5 / 100;
        TurnCount();
        IsProtect = true;
    }
    public void UnProtect()
    {
        IsProtect = false;
    }
}

[System.Serializable]
public class CharacterListSkill
{
    public Skill Skill;
    public int LvRequired = 0;
}


