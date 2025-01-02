using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSelection : MonoBehaviour
{
    public string SceneToChange = "FirstScene";
    private SaveSystem SaveSystem = SaveSystem.Instance;
    public LoadingScreen loadingScreen; // Tham chiếu tới LoadingScreen

    private void Start()
    {
        ShowAllRecord();
    }
    private void ShowAllRecord()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int index = i + 1;
            GameObject record = transform.GetChild(i).gameObject;

            //Lấy các button thêm xóa
            GameObject saveObj = record.transform.Find("Save").gameObject;
            Button saveButton = saveObj.GetComponent<Button>();
            saveButton.onClick.RemoveAllListeners();
            saveButton.onClick.AddListener(() =>
            {
                SaveSystem.SaveGame(index);
                ShowAllRecord();
            }
            );

            GameObject delObj = record.transform.Find("Delete").gameObject;
            Button delButton = delObj.GetComponent<Button>();
            delButton.onClick.RemoveAllListeners();
            delButton.onClick.AddListener(() =>
            {
                SaveSystem.DeleteRecord(index);
                ShowAllRecord();
            }
            );

            GameObject staObj = record.transform.Find("Start").gameObject;
            Button staButton = staObj.GetComponent<Button>();
            staButton.onClick.RemoveAllListeners();
            staButton.onClick.AddListener(() =>
            {
                SaveSystem.LoadRecord(index);
                //SceneManager.LoadScene(SaveSystem.saveLoad.currentScene);
                loadingScreen.LoadSceneWithLoadingScreen(SaveSystem.saveLoad.currentScene); // Sử dụng màn hình loading
            }
            );

            if (SaveSystem.Instance.CheckSave(index))
            {
                Save saveLoad = ScriptableObject.CreateInstance<Save>();
                saveLoad = LoadRecord(index, saveLoad);

                //Tham chiếu đến tên bản đồ
                GameObject Map = record.transform.Find("Map").gameObject;
                TMP_Text MapName = Map.GetComponent<TMP_Text>();
                MapName.text = saveLoad.currentMapName;


                //Tham chiếu đến hình các Teamate
                GameObject Teamate1 = record.transform.Find("Teamate1").gameObject;
                Image imageTeamate1 = Teamate1.GetComponent<Image>();
                if (saveLoad.team.Teamate[0] != null)
                {
                    Teamate1.SetActive(true);
                    imageTeamate1.sprite = saveLoad.team.Teamate[0].Avatar;
                }
                else
                {
                    Teamate1.SetActive(false);
                }


                GameObject Teamate2 = record.transform.Find("Teamate2").gameObject;
                Image imageTeamate2 = Teamate2.GetComponent<Image>();
                if (saveLoad.team.Teamate[1] != null)
                {
                    Teamate2.SetActive(true);
                    imageTeamate2.sprite = saveLoad.team.Teamate[1].Avatar;
                }
                else
                {
                    Teamate2.SetActive(false);
                }


                GameObject Teamate3 = record.transform.Find("Teamate3").gameObject;
                Image imageTeamate3 = Teamate3.GetComponent<Image>();
                if (saveLoad.team.Teamate[2] != null)
                {
                    Teamate3.SetActive(true);
                    imageTeamate3.sprite = saveLoad.team.Teamate[2].Avatar;
                }
                else
                {
                    Teamate3.SetActive(false);
                }

                GameObject Teamate4 = record.transform.Find("Teamate4").gameObject;
                Image imageTeamate4 = Teamate4.GetComponent<Image>();
                if (saveLoad.team.Teamate[3] != null)
                {
                    Teamate3.SetActive(true);
                    imageTeamate4.sprite = saveLoad.team.Teamate[3].Avatar;
                }
                else
                {
                    Teamate3.SetActive(false);
                }

                Map.SetActive(true);
                Teamate1.SetActive(true);
                Teamate2.SetActive(true);
                Teamate3.SetActive(true);
                Teamate4.SetActive(true);

                delObj.SetActive(true);
                staObj.SetActive(true);

            }

            else
            {
                GameObject Map = record.transform.Find("Map").gameObject;
                GameObject Teamate1 = record.transform.Find("Teamate1").gameObject;
                GameObject Teamate2 = record.transform.Find("Teamate2").gameObject;
                GameObject Teamate3 = record.transform.Find("Teamate3").gameObject;
                GameObject Teamate4 = record.transform.Find("Teamate4").gameObject;

                Map.SetActive(false);
                Teamate1.SetActive(false);
                Teamate2.SetActive(false);
                Teamate3.SetActive(false);
                Teamate4.SetActive(false);

                delObj.SetActive(false);
                staObj.SetActive(false);
            }
        }
    }

    public Save LoadRecord(int index, Save saveLoad)
    {
        string rootpath = Application.persistentDataPath;
        string savePath = "";
#if UNITY_ANDROID
		savePath = Path.Combine(rootpath, "Save" + index);
#else
        savePath = "Assets/Resources/Save" + index;
#endif
        if (Directory.Exists(savePath))
        {
            string saveRecord = File.ReadAllText(Path.Combine(savePath, "saveRecord.json"));
            {
                JsonUtility.FromJsonOverwrite(saveRecord, saveLoad);
            }
        }
        return saveLoad;
    }

    public void StartNewGame()
    {
        SaveSystem.Instance.LoadRecord(0);
        //SceneManager.LoadScene(SceneToChange);
        loadingScreen.LoadSceneWithLoadingScreen(SceneToChange); // Sử dụng màn hình loading
    }
}
