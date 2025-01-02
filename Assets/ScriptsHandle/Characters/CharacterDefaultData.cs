using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterDefaultData", menuName = "ScriptableObjects/CharacterDefaultData")]
public class CharacterDefaultData : ScriptableObject
{
    public List<Character> Characters;

    bool CheckCharacterExist(Character character)
    {
        if(Characters.Contains(character)) return true;
        else
        return false;
    }    

    public void SaveThisData()
    {
        string saveFolderPath = "";

        #if UNITY_ANDROID
        string rootPath = Application.persistentDataPath;
        saveFolderPath = Path.Combine(rootPath, "Save0");
        #else
        saveFolderPath = "Assets/Resources/Save0";
        #endif

        string SaveDefaultCharacter = JsonUtility.ToJson(this);
        File.WriteAllText(Path.Combine(saveFolderPath, "CharacterDefaultData.json"), SaveDefaultCharacter);
    }

    public void SetDefaultData(Character character)
    {

        string saveFolderPath = "";
#if UNITY_ANDROID
        string rootPath = Application.persistentDataPath;
        saveFolderPath = Path.Combine(rootPath, "Save0" );
#else
        saveFolderPath = "Assets/Resources/Save0";
#endif
        if (!CheckCharacterExist(character))
        {

            Characters.Add(character);
            string saveCharacter = JsonUtility.ToJson(character);
            File.WriteAllText(Path.Combine(saveFolderPath, "CharacterDefaultData/" + character.Name + ".json"), saveCharacter);
        }
        SaveThisData();
    }
    public void GetDefaultData()
    {
        string saveFolderPath = "";
        string rootPath = Application.persistentDataPath;
#if UNITY_ANDROID

        saveFolderPath = Path.Combine(rootPath, "Save0");
#else
        saveFolderPath = "Assets/Resources/Save0";
#endif

        //for (int i = 0; i < Characters.Count; i++)
        //{
        //    string saveQuest = File.ReadAllText(Path.Combine(saveFolderPath, "CharacterDefaultData/" + Characters[i].Name + ".json"));
        //    JsonUtility.FromJsonOverwrite(saveQuest, Characters[i]);
        //}
        Dictionary<string, string> characterFileMap = new Dictionary<string, string>();
        for (int i = 0; i < Characters.Count; i++)
        {
            string defaultFileName = Characters[i].Name + ".json";
            string filePath = Path.Combine(saveFolderPath, "CharacterDefaultData/" + defaultFileName);

            // Tìm file với hậu tố nếu file mặc định không tồn tại
            int counter = 1;
            while (!File.Exists(filePath) && counter < 100) // Giới hạn vòng lặp
            {
                defaultFileName = $"{Characters[i].Name}_{counter}.json";
                filePath = Path.Combine(saveFolderPath, "CharacterDefaultData/" + defaultFileName);
                counter++;
            }

            if (File.Exists(filePath))
            {
                characterFileMap[Characters[i].Name] = defaultFileName;
            }
            else
            {
                Debug.LogWarning($"Không tìm thấy file phù hợp cho nhân vật {Characters[i].Name}");
            }
        }

        // Sử dụng ánh xạ này để đọc dữ liệu
        foreach (var character in Characters)
        {
            if (characterFileMap.TryGetValue(character.Name, out string fileName))
            {
                string filePath = Path.Combine(saveFolderPath, "CharacterDefaultData/" + fileName);
                string saveQuest = File.ReadAllText(filePath);
                JsonUtility.FromJsonOverwrite(saveQuest, character);
            }
        }
    }


}
