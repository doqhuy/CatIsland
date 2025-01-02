using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Job", menuName = "ScriptableObjects/Job")]
[System.Serializable]
public class Job : ScriptableObject
{
    public string Name = "Fighter";

    public bool IsHPBonus = false;
    public int HPBonusScale = 5;

    public bool IsATKBonus = false;
    public int ATKBonusScale = 5;

    public bool IsMagicBonus = false;
    public int MagicBonusScale = 5;

    public bool IsDEFBonus = false;
    public int DEFBonusScale = 5;

    public bool IsMagicRESBonus = false;
    public int MagicRESBonusScale = 5;

    public bool IsSpeedBonus = false;
    public int SpeedBonusScale = 5;

    public bool IsLuckBonus = false;
    public int LuckBonusScale = 5;

}
