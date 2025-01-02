using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TalkingAndQuest : MonoBehaviour
{
    public List<string> TalkText;
    public GameObject TalkingUI;
    public GameObject QuestUI_Done;
    public GameObject QuestUI_Claim;
    public GameObject QuestUI_Questing;
    public GameObject QuestUI_Required;

    private int TextIndex = 0;
    public float RadiusActive = 1.2f;

    public GameObject Shop;
    public List<QuestWithTalk> QuestList;
    public List<QuestRequiredWithTalk> QuestRequired;

    private SaveSystem SaveSystem = SaveSystem.Instance;
    private GeneralInformation GeneralInformation = GeneralInformation.Instance;

    public Quest EventQuestGive;
    public bool IsEventTalk = false;

    private bool IsTexting = false;
    private Coroutine Texting;

    private void Update()
    {
        // Kiểm tra nếu người chơi nhấn J hoặc chuột trái khi đang hiển thị giao diện TalkingUI.
        if (TalkingUI.activeSelf && (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0)))
        {
            if (IsTexting)
            {
                // Hoàn tất ngay lập tức câu chữ hiện tại.
                IsTexting = false;
                StopCoroutine(Texting);
                GameObject TextObj = TalkingUI.transform.Find("Text").gameObject;
                TMP_Text text = TextObj.GetComponent<TMP_Text>();
                text.text = TalkText[TextIndex];
                TextIndex++; // Tiếp tục đến đoạn hội thoại tiếp theo.
            }
            else
            {
                // Tiếp tục hiển thị đoạn hội thoại tiếp theo.
                ShowTalk();
            }
        }

        Transform player = GameObject.Find("Player").transform;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < RadiusActive && GeneralInformation.Actioning == "Playing")
        {
            if (Input.GetKeyDown(KeyCode.J) || TalkingUI.activeSelf && Input.GetMouseButtonDown(0))
            {
                ShowTalk();
            }
        }
    }

    public void ShowTalk()
    {
        if (!TalkingUI.activeSelf)
        {
            PrepareTalkText();
            TalkingUI.SetActive(true);
            GeneralInformation.Actioning = "Talking";
        }

        if (TextIndex >= TalkText.Count)
        {
            EndTalk();
            return;
        }

        GameObject TextObj = TalkingUI.transform.Find("Text").gameObject;
        TMP_Text text = TextObj.GetComponent<TMP_Text>();
        text.text = "";

        Texting = StartCoroutine(ShowTalkDelay(text));
    }

    private void PrepareTalkText()
    {
        if (IsEventTalk)
        {
            if (EventQuestGive != null)
                SaveSystem.saveLoad.questClaim.AddQuest(EventQuestGive);
            IsEventTalk = false;
        }

        if (QuestList.Count > 0)
        {
            while (QuestList[0].quest.Status == "Done" || QuestList[0].quest.Status == "Canceled")
            {
                QuestList.RemoveAt(0);
                if (QuestList.Count == 0) break;
            }
        }

        if (CheckRequired() != null)
        {
            TalkText = CheckRequired();
            RemoveRequired();
        }
        else if (QuestList.Count > 0)
        {
            var quest = QuestList[0].quest;
            if (quest.Status == "UnClaimed" && quest.CheckQuestAvailable() && !quest.IsEventClaimed)
            {
                TalkText = QuestList[0].ClaimQuestTexts;
                SaveSystem.saveLoad.questClaim.AddQuest(quest);
            }
            else if (quest.Status == "Questing")
            {
                TalkText = QuestList[0].QuestingTexts;
            }
            else if (quest.Status == "Reach")
            {
                TalkText = QuestList[0].DoneQuestTexts;
                SaveSystem.saveLoad.ClaimQuestReward(quest);
            }
        }
    }

    private void EndTalk()
    {
        TalkingUI.SetActive(false);
        GeneralInformation.Actioning = "Playing";
        TextIndex = 0;

        if (Shop != null)
        {
            Shop.SetActive(true);
            GeneralInformation.Actioning = "Shoping";
        }
    }

    IEnumerator ShowTalkDelay(TMP_Text textshow)
    {
        // Kiểm tra TalkText và TextIndex trước khi sử dụng
        if (TalkText == null || TalkText.Count == 0)
        {
            Debug.LogError("TalkText is null or empty.");
            yield break;
        }

        if (TextIndex < 0 || TextIndex >= TalkText.Count)
        {
            Debug.LogError($"TextIndex is out of range. TextIndex: {TextIndex}, TalkText.Count: {TalkText.Count}");
            yield break;
        }

        IsTexting = true;
        int i = 0;

        // Đảm bảo không xảy ra lỗi khi truy cập TalkText[TextIndex]
        string currentText = TalkText[TextIndex];
        while (i < currentText.Length)
        {
            if (!IsTexting) break;
            textshow.text += currentText[i];
            i++;
            yield return new WaitForSeconds(0.05f);
        }

        IsTexting = false;

        if (i == currentText.Length)
        {
            TextIndex++;
        }
    }




    private void RemoveRequired()
    {
        foreach (var item in QuestRequired)
        {
            SaveSystem.saveLoad.questClaim.CheckTalkRequiredAllQuest(item.QuestRequired);
        }
    }

    private List<string> CheckRequired()
    {
        foreach (var quest in SaveSystem.saveLoad.questClaim.quests)
        {
            if (quest.Status == "Questing")
            {
                foreach (var q in QuestRequired)
                {
                    if (quest.QuestRequired.Contains(q.QuestRequired))
                    {
                        return q.TalkForQuest;
                    }
                }
            }
        }
        return null;
    }
}

[System.Serializable]
public class QuestWithTalk
{
    public Quest quest;
    public List<string> ClaimQuestTexts;
    public List<string> QuestingTexts;
    public List<string> DoneQuestTexts;
}

[System.Serializable]
public class QuestRequiredWithTalk
{
    public string QuestRequired;
    public List<string> TalkForQuest;
}