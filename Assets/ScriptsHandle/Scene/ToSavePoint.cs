using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToSavePoint : MonoBehaviour
{
    public GeneralInformation GeneralInformation;
    public SaveSystem SaveSystem;

    public Button ToSavePointButton;
    void Start()
    {
        GeneralInformation = GeneralInformation.Instance;
        SaveSystem = SaveSystem.Instance;

        ToSavePointButton.onClick.RemoveAllListeners();
        ToSavePointButton.onClick.AddListener(() =>
        {
            GeneralInformation.ToSavePointNow = true;
            SceneManager.LoadScene(SaveSystem.saveLoad.SavePointScene);
        });


	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
