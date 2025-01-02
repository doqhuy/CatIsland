using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeadUpHandle : MonoBehaviour
{
    public GameObject GamePad;
    public GameObject OpenMenu;

    private void Start()
    {
        Button openmenubutton = OpenMenu.GetComponent<Button>();
        openmenubutton.onClick.RemoveAllListeners();
        openmenubutton.onClick.AddListener(
            () =>
            {
                GameObject[] inactiveObjects = Resources.FindObjectsOfTypeAll<GameObject>();
                foreach (GameObject obj in inactiveObjects)
                {
                    if (obj.name == "MenuPlayer")
                    {
                        obj.SetActive(true);
                    }
                }
            });
    }
    private void Update()
    {
        if (GeneralInformation.Instance.Actioning != "Playing")
        {
            if(GamePad != null)
            {
                GamePad.SetActive(false);
            }
            OpenMenu.SetActive(false);
        }    
        else
        {
            if (GamePad != null)
            {
                GamePad.SetActive(true);
            }
            OpenMenu.SetActive(true);
        }    
    }
}
