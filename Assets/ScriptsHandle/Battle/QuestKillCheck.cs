using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestKillCheck : MonoBehaviour
{
    SaveSystem SaveSystem = SaveSystem.Instance;
    GeneralInformation GeneralInformation = GeneralInformation.Instance;
    void Start()
    {
        GeneralInformation.WinLose = true;
        for (int i = 0; i < GeneralInformation.EnemyInBattle.Count; i++)
        {
            SaveSystem.saveLoad.questClaim.CheckKillAllQuest(GeneralInformation.EnemyInBattle[i]);  
        }    
    } 

}
