using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDisplay : MonoBehaviour
{
    public GameObject QuestInfo;
    public GameObject QuestList;
    public Button MainQuestList;
    public Button ExtraQuestList;
    public Button QuestDoneList;

    public List<Quest> quests;
    private SaveSystem SaveSystem = SaveSystem.Instance;
    private GeneralInformation GeneralInformation = GeneralInformation.Instance;

    private void OnEnable()
    {
        QuestInfo.SetActive(false);
    }
    private void Start()
    {
        ChooseQuestType("Main");
        ShowListQuest();
        MainQuestList.onClick.RemoveAllListeners();
        ExtraQuestList.onClick.RemoveAllListeners();
        QuestDoneList.onClick.RemoveAllListeners();

        MainQuestList.onClick.AddListener(() => ChooseQuestType("Main"));
        ExtraQuestList.onClick.AddListener(() => ChooseQuestType("Extra"));
        QuestDoneList.onClick.AddListener(() =>ChooseQuestType("Done"));
    }

    public void ChooseQuestType(string s)
    {
        quests = SaveSystem.saveLoad.questClaim.quests;
        if (s=="Done")
        {
            quests = quests.Where(c => c.Status == s).ToList();
        }    
        else
        {
            quests = quests.Where(c => c.Type == s && c.Status != "Done").ToList();
        }
        ShowListQuest();

    }

    public void ShowListQuest()
    {
        GameObject Viewport = QuestList.transform.Find("Viewport").gameObject;
        GameObject Content = Viewport.transform.Find("Content").gameObject;
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            int index = i;
            GameObject thisQuest = Content.transform.GetChild(index).gameObject;
            GameObject NameObj = thisQuest.transform.Find("Name").gameObject;
            GameObject StatusObj = thisQuest.transform.Find("Status").gameObject;

            TMP_Text NameText = NameObj.GetComponent<TMP_Text>();
            TMP_Text StatusText = StatusObj.GetComponent<TMP_Text>();

            if (index < quests.Count)
            {
                thisQuest.SetActive(true);
                NameText.text = quests[index].Name;
                StatusText.text = quests[index].Status;
                Button button = thisQuest.GetComponent<Button>();
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => ShowQuestInfo(quests[index]));
            }
            else
            {
                thisQuest.SetActive(false);
            }
        }
    }

    public void ShowQuestInfo(Quest quest)
    {
        QuestInfo.SetActive(true);
        GameObject NameObj = QuestInfo.transform.Find("Name").gameObject;
        GameObject DescriptionObj = QuestInfo.transform.Find("Description").gameObject;
        GameObject StatusObj = QuestInfo.transform.Find("Status").gameObject;
        GameObject RewardsObj = QuestInfo.transform.Find("Rewards").gameObject;

        TMP_Text Name = NameObj.GetComponent<TMP_Text>();
        TMP_Text Description = DescriptionObj.GetComponent<TMP_Text>();
        TMP_Text Status = StatusObj.GetComponent<TMP_Text>();
        TMP_Text Rewards = RewardsObj.GetComponent<TMP_Text>();

        Name.text = quest.Name;
        Status.text = "Status: " + quest.Status;
        Description.text = quest.Description;
        string rewardsofquest = "";
        for(int i = 0; i < quest.ItemReward.Count; i++)
        {
            rewardsofquest = rewardsofquest + quest.ItemReward[i].amount + " " + quest.ItemReward[i].item.Name + ",";
        }
        rewardsofquest += "\n";
        for (int i = 0; i < quest.EquipmentReward.Count; i++)
        {
            rewardsofquest = rewardsofquest + quest.EquipmentReward[i].amount + " " + quest.EquipmentReward[i].equipment.Name + ",";
        }
        rewardsofquest += "\n";
        for (int i = 0; i < quest.CharacterReward.Count; i++)
        {
            rewardsofquest = rewardsofquest + quest.CharacterReward[i].Name;
        }
        rewardsofquest += "\n";
        rewardsofquest += " + " + quest.CoinReward + " Coin";
        Rewards.text = rewardsofquest;

    }    
}
