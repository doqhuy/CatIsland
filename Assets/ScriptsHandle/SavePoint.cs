using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    private SaveSystem SaveSystem = SaveSystem.Instance;
    private GeneralInformation GeneralInformation = GeneralInformation.Instance;

    // Update is called once per frame
    void Update()
    {
        GameObject PlayerObj = GameObject.Find("Player");
        if (Vector2.Distance(transform.position, PlayerObj.transform.position) < 1.4f)
        {
            SaveSystem.saveLoad.SavePointMap = transform.parent.gameObject.name;
            SaveSystem.saveLoad.SavePointScene = SceneManager.GetActiveScene().name;
            for (int i = 0; i < 4; i++)
            {
                int index = i;
                Character thisChar = SaveSystem.saveLoad.team.Teamate[index];
                if (thisChar != null)
                {
                    thisChar.HP = thisChar.MaxHP;
                    thisChar.MP = thisChar.MaxMP;
                }
            }
            GameObject SPA = transform.GetChild(0).gameObject;
            SPA.SetActive(true);
        }
        else
        {
            GameObject SPA = transform.GetChild(0).gameObject;
            SPA.SetActive(false);
        }    
    }
}
