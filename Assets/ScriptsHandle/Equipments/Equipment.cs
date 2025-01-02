using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Equipment", menuName = "ScriptableObjects/Equipment")]

public class Equipment : ScriptableObject
{
    public Sprite Image;

    public int Id;
    public string Name;
    public string Stat;
    public string Description;


    //Helmet, Armor, Weapon, Accessory
    public string Type;
    //Slash, Shot, Cast, QuickShot, Hit
    public string AttackType;

    public Job Job;

    public int HP = 0;
    public int MP = 0;

    public int ATK = 0;
    public int Magic = 0;

    public int DEF = 0;
    public int MagicRES = 0;

    public int Speed = 0;
    public int Luck = 0;

    public int Price = 10;
}
