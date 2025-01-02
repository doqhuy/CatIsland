using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnMap : MonoBehaviour
{
    GeneralInformation GeneralInformation = GeneralInformation.Instance;
    SaveSystem SaveSystem = SaveSystem.Instance;

    public GameObject Enemies;

    private void Start()
    {
        for (int i = 0; i<Enemies.transform.childCount; i++)
        {
            GameObject thisEObj = Enemies.transform.GetChild(i).gameObject;
            ToBattle EtoBattle = thisEObj.GetComponent<ToBattle>();
            if(EtoBattle.CheckDead == true)
            {
                EtoBattle.CheckDead = false;
                if (GeneralInformation.WinLose == true)
                {
                    GeneralInformation.WinLose = false;
                    thisEObj.SetActive(false);
                }
            }

            if(!thisEObj.activeSelf)
            {
               float TimeNow = Time.realtimeSinceStartup;
            }    
        }    
    }
}


