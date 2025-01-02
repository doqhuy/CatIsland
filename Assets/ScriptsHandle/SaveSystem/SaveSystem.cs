using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SaveSystem : MonoBehaviour
{
	private static SaveSystem _instance;
	public static SaveSystem Instance => _instance;

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
            LoadRecord(0);
            Destroy(gameObject);
		}
	}

	public Save saveLoad;
	public int EventsHandleNumber = 100;
	public int DropItemSave = 100;
    public int EnemySave = 100;

	private void Start()
	{

		if(!CheckSave(0))
		{
            saveLoad.inventory.Items.Clear();
            saveLoad.inventory.KeyItems.Clear();
            saveLoad.inventory.Recipes.Clear();
            saveLoad.equipmentInventory.Helmets.Clear();
            saveLoad.equipmentInventory.Accessories.Clear();
            saveLoad.equipmentInventory.Armors.Clear();
            saveLoad.equipmentInventory.Weapons.Clear();
            saveLoad.team.ClaimedCharacter.Clear();
            saveLoad.questClaim.quests.Clear();
            saveLoad.currentPosition.x = 100;
            saveLoad.currentPosition.y = 42;
            saveLoad.currentPosition.z = 0;
            if(saveLoad.team.Teamate[0] == null)
            {
                saveLoad.team.Teamate[0] = Resources.Load<Character>("AllCharacters/NormalCat/NormalCat");
            }
            saveLoad.team.CharacterDefaultData = Resources.Load<CharacterDefaultData>("Save/CharacterDefaultData");
            saveLoad.team.CharacterDefaultData.Characters.Clear();
            saveLoad.questClaim.QuestDefaultData = Resources.Load<QuestDefaultData>("Save/QuestDefaultData");
            saveLoad.questClaim.QuestDefaultData.Quests.Clear();

            if(Directory.Exists("Assets/Resources/Save/DropItem"))
            {
                Directory.Delete("Assets/Resources/Save/DropItem", true);
                Directory.CreateDirectory("Assets/Resources/Save/DropItem");
            }
            if (Directory.Exists("Assets/Resources/Save/EnemySave"))
            {
                Directory.Delete("Assets/Resources/Save/EnemySave", true);
                Directory.CreateDirectory("Assets/Resources/Save/EnemySave");

            }
            if (Directory.Exists("Assets/Resources/Save/Event"))
            {
                Directory.Delete("Assets/Resources/Save/Event", true);
                Directory.CreateDirectory("Assets/Resources/Save/Event");

            }
            SaveGame(0);
		}	
		StartNewRecord();
	}

	public void StartNewRecord()
	{
        LoadRecord(0);
	}


	public bool CheckSave(int index)
	{
        string savePath = "";
        string rootPath = Application.persistentDataPath;
#if UNITY_ANDROID
        savePath = Path.Combine(rootPath, "Save"+index);
#else
        savePath = "Assets/Resources/Save" + index;
#endif
        if (Directory.Exists(savePath))
		{
			return true;
		}	
		else
		{
			return false;
		}
	}	



	public void LoadRecord(int index)
	{

        string saveFolderPath = "";
        string defaultsave = "";
        string rootPath = Application.persistentDataPath;
        string save0path = "";


        #if UNITY_ANDROID
        saveFolderPath = Path.Combine(rootPath, "Save" + index);
        defaultsave = Path.Combine(rootPath, "Save");
        save0path = Path.Combine(rootPath, "Save0");
        #else
        saveFolderPath = "Assets/Resources/Save" + index;
        defaultsave = "Assets/Resources/Save";
        save0path = "Assets/Resources/Save0";
        #endif

        if (Directory.Exists(saveFolderPath))
        {
            string saveRecordPath = Path.Combine(saveFolderPath, "saveRecord.json");
            string saveRecord_questClaimPath = Path.Combine(saveFolderPath, "saveRecord_questClaim.json");

            if (File.Exists(saveRecordPath))
            {
                string saveRecord = File.ReadAllText(saveRecordPath);
                JsonUtility.FromJsonOverwrite(saveRecord, saveLoad);
            }

            if (File.Exists(saveRecord_questClaimPath))
            {
                string saveRecord_questClaim = File.ReadAllText(saveRecord_questClaimPath);
                JsonUtility.FromJsonOverwrite(saveRecord_questClaim, saveLoad.questClaim);
            }



            foreach (Character character in saveLoad.team.ClaimedCharacter)
            {
                string characterSaveRecordPath = Path.Combine(saveFolderPath, "CharacterData", character.name + ".json");
                if (File.Exists(characterSaveRecordPath))
                {
                    string characterSaveRecord = File.ReadAllText(characterSaveRecordPath);
                    JsonUtility.FromJsonOverwrite(characterSaveRecord, character);
                }
            }

            string questDefaultSavePath = Path.Combine(save0path, "QuestDefaultData.json");
            if (File.Exists(questDefaultSavePath))
            {
                string questDefaultSave = File.ReadAllText(questDefaultSavePath);
                JsonUtility.FromJsonOverwrite(questDefaultSave, saveLoad.questClaim.QuestDefaultData);
                saveLoad.questClaim.QuestDefaultData.GetDefaultData();
            }

            string characterDefaultSavePath = Path.Combine(save0path, "CharacterDefaultData.json");
            if(File.Exists(characterDefaultSavePath))
            {
                string characterDefaultSave = File.ReadAllText(Path.Combine(save0path, "CharacterDefaultData.json"));
                JsonUtility.FromJsonOverwrite(characterDefaultSave, saveLoad.team.CharacterDefaultData);
                saveLoad.team.CharacterDefaultData.GetDefaultData();
            }

            //if (File.Exists(Path.Combine(save0path, "CharacterDefaultData.json")))
            //{
            //    string characterDefaultSave = File.ReadAllText(Path.Combine(save0path, "CharacterDefaultData.json"));
            //    JsonUtility.FromJsonOverwrite(characterDefaultSave, saveLoad.team.CharacterDefaultData);
            //    saveLoad.team.CharacterDefaultData.GetDefaultData();
            //}

            for (int i = 0; i < saveLoad.questClaim.quests.Count; i++)
            {
                string questSaveRecordPath = Path.Combine(saveFolderPath, "QuestData", "Quest" + i + ".json");
                if (File.Exists(questSaveRecordPath))
                {
                    string questSaveRecord = File.ReadAllText(questSaveRecordPath);
                    JsonUtility.FromJsonOverwrite(questSaveRecord, saveLoad.questClaim.quests[i]);
                }
            }



            for (int i = 0; i < EventsHandleNumber; i++)
            {
                string eventSavePath = Path.Combine(saveFolderPath, "Event", "EventHandle_" + i + ".json");
                if (File.Exists(eventSavePath))
                {
                    string thisEvent = File.ReadAllText(eventSavePath);
                    File.WriteAllText(Path.Combine(defaultsave, "Event", "EventHandle_" + i + ".json"), thisEvent);
                }
            }

            for (int i = 0; i < DropItemSave; i++)
            {
                string dropItemSavePath = Path.Combine(saveFolderPath, "DropItem", "DropItem_" + i + ".json");
                if (File.Exists(dropItemSavePath))
                {
                    string thisDropItem = File.ReadAllText(dropItemSavePath);
                    File.WriteAllText(Path.Combine(defaultsave, "DropItem", "DropItem_" + i + ".json"), thisDropItem);
                }
            }

            for (int i = 0; i < EnemySave; i++)
            {
                string enemySavePath = Path.Combine(saveFolderPath, "EnemySave", "EnemySave_" + i + ".json");
                if (File.Exists(enemySavePath))
                {
                    string thisEnemy = File.ReadAllText(enemySavePath);
                    File.WriteAllText(Path.Combine(defaultsave,"EnemySave/EnemySave_" + i + ".json"), thisEnemy);
                }
            }
        }

        
    
  //      if (Directory.Exists("Assets/Resources/Save" + index))
  //{
  //	string saveRecord = File.ReadAllText("Assets/Resources/Save" + index + "/saveRecord.json");
  //          string saveRecord_questClaim = File.ReadAllText("Assets/Resources/Save" + index + "/saveRecord_questClaim.json");


                    //          JsonUtility.FromJsonOverwrite(saveRecord, saveLoad);
                    //          JsonUtility.FromJsonOverwrite(saveRecord_questClaim, saveLoad.questClaim);


                    //          for (int i = 0; i < saveLoad.team.ClaimedCharacter.Count; i++)
                    //          {
                    //              if(File.Exists("Assets/Resources/Save" + index + "/CharacterData/" + saveLoad.team.ClaimedCharacter[i].name + ".json"))
                    //              {
                    //                  string CharacterSaveRecord = File.ReadAllText("Assets/Resources/Save" + index + "/CharacterData/" + saveLoad.team.ClaimedCharacter[i].name + ".json");
                    //                  JsonUtility.FromJsonOverwrite(CharacterSaveRecord, saveLoad.team.ClaimedCharacter[i]);
                    //              }    

                    //          }

                    //	if(File.Exists("Assets/Resources/Save0/QuestDefaultData.json"))
                    //	{
                    //              string QuestDefaultSave = File.ReadAllText("Assets/Resources/Save0/QuestDefaultData.json");
                    //              JsonUtility.FromJsonOverwrite(QuestDefaultSave, saveLoad.questClaim.QuestDefaultData);
                    //              saveLoad.questClaim.QuestDefaultData.GetDefaultData();
                    //          }

                    //          if (File.Exists("Assets/Resources/Save0/CharacterDefaultData.json"))
                    //          {
                    //              string CharacterDefaultSave = File.ReadAllText("Assets/Resources/Save0/CharacterDefaultData.json");
                    //              JsonUtility.FromJsonOverwrite(CharacterDefaultSave, saveLoad.team.CharacterDefaultData);
                    //              saveLoad.team.CharacterDefaultData.GetDefaultData();
                    //          }

                    //          for (int i = 0; i < saveLoad.questClaim.quests.Count; i++)
                    //          {
                    //              string QuestSaveRecord = File.ReadAllText("Assets/Resources/Save" + index + "/QuestData/Quest" + i + ".json");
                    //              JsonUtility.FromJsonOverwrite(QuestSaveRecord, saveLoad.questClaim.quests[i]);
                    //          }

                    //          for (int i = 0; i < saveLoad.team.ClaimedCharacter.Count; i++)
                    //	{
                    //		string CharacterSaveRecord = File.ReadAllText("Assets/Resources/Save" + index + "/CharacterData/" + saveLoad.team.ClaimedCharacter[i].name + ".json");
                    //              JsonUtility.FromJsonOverwrite(CharacterSaveRecord, saveLoad.team.ClaimedCharacter[i]);
                    //          }

                    //          for (int i = 0; i < EventsHandleNumber; i++)
                    //          {
                    //		if(File.Exists("Assets/Resources/Save" + index + "/Event/EventHandle_" + i + ".json"))
                    //		{
                    //                  string ThisEvent = File.ReadAllText("Assets/Resources/Save" + index + "/Event/EventHandle_" + i + ".json");
                    //                  File.WriteAllText("Assets/Resources/Save/Event/EventHandle_" + i + ".json", ThisEvent);
                    //              }
                    //          }

                    //          for (int i = 0; i < DropItemSave; i++)
                    //          {
                    //              if (File.Exists("Assets/Resources/Save" + index + "/DropItem/DropItem_" + i + ".json"))
                    //              {
                    //                  string ThisDropItem = File.ReadAllText("Assets/Resources/Save" + index + "/DropItem/DropItem_" + i + ".json");
                    //                  File.WriteAllText("Assets/Resources/Save/DropItem/DropItem_" + i + ".json", ThisDropItem);
                    //              }
                    //          }

                    //          for (int i = 0; i < EnemySave; i++)
                    //          {
                    //              if (File.Exists("Assets/Resources/Save" + index + "/EnemySave/EnemySave_" + i + ".json"))
                    //              {
                    //                  string ThisEnemy = File.ReadAllText("Assets/Resources/Save" + index + "/EnemySave/EnemySave_" + i + ".json");
                    //                  File.WriteAllText("Assets/Resources/Save/EnemySave/EnemySave_" + i + ".json", ThisEnemy);
                    //              }
                    //          }
                    //      }

        if (index == 0)
        {
            saveLoad.team.CharacterDefaultData.Characters.Clear();
            saveLoad.questClaim.QuestDefaultData.Quests.Clear();
            foreach (Character character in saveLoad.team.Teamate)
            {
                if (character != null)
                    saveLoad.team.AddCharacter(character);
            }
            saveLoad.Time = 0f;
        }
    }





	public void DeleteRecord(int index)
	{
        string rootPath = Application.persistentDataPath;
        string recordPath = "";
        #if UNITY_ANDROID
        recordPath = Path.Combine(rootPath, "Save" + index);
#else
        recordPath = "Assets/Resources/Save" + index;
#endif
        if (Directory.Exists(recordPath))
		{
			File.Delete(recordPath + ".meta");
			Directory.Delete(recordPath, true);
		}
	}

	public void SaveGame(int index)
	{
        string saveFolderPath = "";
        string defaultSavePath = "";
        string rootPath = Application.persistentDataPath;

#if UNITY_ANDROID
        saveFolderPath = Path.Combine(rootPath, "Save" + index);
        defaultSavePath = Path.Combine(rootPath, "Save");
#else
        saveFolderPath = "Assets/Resources/Save" + index;
        defaultSavePath = "Assets/Resources/Save";
#endif


        //      if (!Directory.Exists("Assets/Resources/Save" + index))
        //{
        //	Directory.CreateDirectory("Assets/Resources/Save" + index);
        //	Directory.CreateDirectory("Assets/Resources/Save" + index + "/CharacterData");
        //          Directory.CreateDirectory("Assets/Resources/Save" + index + "/CharacterDefaultData");
        //	Directory.CreateDirectory("Assets/Resources/Save" + index + "/Event");
        //          Directory.CreateDirectory("Assets/Resources/Save" + index + "/DropItem");
        //          Directory.CreateDirectory("Assets/Resources/Save" + index + "/QuestData");
        //          Directory.CreateDirectory("Assets/Resources/Save" + index + "/QuestDefaultData");
        //          Directory.CreateDirectory("Assets/Resources/Save" + index + "/EnemySave");
        //      }

        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
            Directory.CreateDirectory(Path.Combine(saveFolderPath, "CharacterData"));
            Directory.CreateDirectory(Path.Combine(saveFolderPath, "CharacterDefaultData"));
            Directory.CreateDirectory(Path.Combine(saveFolderPath, "Event"));
            Directory.CreateDirectory(Path.Combine(saveFolderPath, "DropItem"));
            Directory.CreateDirectory(Path.Combine(saveFolderPath, "QuestData"));
            Directory.CreateDirectory(Path.Combine(saveFolderPath, "QuestDefaultData"));
            Directory.CreateDirectory(Path.Combine(saveFolderPath, "EnemySave"));
        }

        if (!Directory.Exists(defaultSavePath))
        {
            Directory.CreateDirectory(defaultSavePath);
            Directory.CreateDirectory(Path.Combine(defaultSavePath, "CharacterData"));
            Directory.CreateDirectory(Path.Combine(defaultSavePath, "DropItem"));
            Directory.CreateDirectory(Path.Combine(defaultSavePath, "EnemySave"));
            Directory.CreateDirectory(Path.Combine(defaultSavePath, "Event"));
            Directory.CreateDirectory(Path.Combine(defaultSavePath, "QuestData"));
        }    

            string saveRecord = JsonUtility.ToJson(saveLoad);
        string saveRecord_questClaim = JsonUtility.ToJson(saveLoad.questClaim);

        File.WriteAllText(Path.Combine(saveFolderPath, "saveRecord.json"), saveRecord);
        File.WriteAllText(Path.Combine(saveFolderPath, "saveRecord_questClaim.json"), saveRecord_questClaim);

        for (int i = 0; i < saveLoad.team.ClaimedCharacter.Count; i++)
        {
            string CharacterSaveRecord = JsonUtility.ToJson(saveLoad.team.ClaimedCharacter[i]);
            File.WriteAllText(Path.Combine(saveFolderPath, "CharacterData", saveLoad.team.ClaimedCharacter[i].name + ".json"), CharacterSaveRecord);
        }

        for (int i = 0; i < saveLoad.questClaim.quests.Count; i++)
        {
            string QuestSaveRecord = JsonUtility.ToJson(saveLoad.questClaim.quests[i]);
            File.WriteAllText(Path.Combine(saveFolderPath, "QuestData", "Quest" + i + ".json"), QuestSaveRecord);
        }

        for (int i = 0; i < EventsHandleNumber; i++)
        {
            if (File.Exists(Path.Combine(defaultSavePath,"Event", "EventHandle_" + i + ".json")))
            {
                string thisEvent = File.ReadAllText(Path.Combine(defaultSavePath, "Event", "EventHandle_" + i + ".json"));
                File.WriteAllText(Path.Combine(saveFolderPath, "Event", "EventHandle_" + i + ".json"), thisEvent);
            }
        }

        for (int i = 0; i < DropItemSave; i++)
        {
            if (File.Exists(Path.Combine(defaultSavePath, "DropItem", "DropItem_" + i + ".json")))
            {
                string thisDropItem = File.ReadAllText(Path.Combine(defaultSavePath, "DropItem", "DropItem_" + i + ".json"));
                File.WriteAllText(Path.Combine(saveFolderPath, "DropItem", "DropItem_" + i + ".json"), thisDropItem);
            }
        }

        for (int i = 0; i < EnemySave; i++)
        {
            if (File.Exists(Path.Combine(defaultSavePath, "EnemySave", "EnemySave_" + i + ".json")))
            {
                string thisEnemy = File.ReadAllText(Path.Combine(defaultSavePath, "EnemySave", "EnemySave_" + i + ".json"));
                File.WriteAllText(Path.Combine(saveFolderPath, "EnemySave", "EnemySave_" + i + ".json"), thisEnemy);
            }
        }


        //string saveRecord = JsonUtility.ToJson(saveLoad);
        //string saveRecord_questClaim = JsonUtility.ToJson(saveLoad.questClaim);

        //File.WriteAllText("Assets/Resources/Save" + index + "/saveRecord.json", saveRecord);
        //File.WriteAllText("Assets/Resources/Save" + index + "/saveRecord_questClaim.json", saveRecord_questClaim);

        //      for (int i = 0; i < saveLoad.team.ClaimedCharacter.Count; i++)
        //{
        //	string CharacterSaveRecord = JsonUtility.ToJson(saveLoad.team.ClaimedCharacter[i]);
        //	File.WriteAllText("Assets/Resources/Save" + index + "/CharacterData/"+ saveLoad.team.ClaimedCharacter[i].name+ ".json", CharacterSaveRecord);
        //}

        //      for (int i = 0; i < saveLoad.questClaim.quests.Count; i++)
        //      {
        //	string QuestSaveRecord = JsonUtility.ToJson(saveLoad.questClaim.quests[i]);
        //          File.WriteAllText("Assets/Resources/Save" + index + "/QuestData/Quest" + i + ".json", QuestSaveRecord);
        //      }

  //      for (int i = 0; i < EventsHandleNumber; i++)
		//{
		//	if(File.Exists("Assets/Resources/Save/Event/EventHandle_" + i + ".json"))
		//	{
  //              string ThisEvent = File.ReadAllText("Assets/Resources/Save/Event/EventHandle_" + i + ".json");
  //              File.WriteAllText("Assets/Resources/Save" + index + "/Event/EventHandle_" + i + ".json", ThisEvent);
  //          }
  //      }

  //      for (int i = 0; i < DropItemSave; i++)
  //      {
  //          if (File.Exists("Assets/Resources/Save/DropItem/DropItem_" + i + ".json"))
  //          {
  //              string ThisDropItem = File.ReadAllText("Assets/Resources/Save/DropItem/DropItem_" + i + ".json");
  //              File.WriteAllText("Assets/Resources/Save" + index + "/DropItem/DropItem_" + i + ".json", ThisDropItem);
  //          }
  //      }

  //      for (int i = 0; i < EnemySave; i++)
  //      {
  //          if (File.Exists("Assets/Resources/Save/EnemySave/EnemySave_" + i + ".json"))
  //          {
  //              string ThisEnemy = File.ReadAllText("Assets/Resources/Save/EnemySave/EnemySave_" + i + ".json");
  //              File.WriteAllText("Assets/Resources/Save" + index + "/EnemySave/EnemySave_" + i + ".json", ThisEnemy);
  //          }
  //      }

        

    }

}
