using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EventHandle : MonoBehaviour
{
    public string EventName = "EventHandle_1";
    [SerializeField]
    public EventList EventList;
    private GameObject Player;

    private Coroutine Eventing;
    private bool IsEventing = false;

    GeneralInformation GeneralInformation = GeneralInformation.Instance;
    SaveSystem SaveSystem = SaveSystem.Instance;

    private void Start()
    {
        string saveFolderPath = "";
        string save0Path = "";

#if UNITY_ANDROID
        string rootPath = Application.persistentDataPath;
        saveFolderPath = Path.Combine(rootPath, "Save");
        save0Path = Path.Combine(rootPath, "Save0");
#else
        saveFolderPath = "Assets/Resources/Save";
        save0Path = "Assets/Resources/Save0";

#endif
        Player = GameObject.Find("Player").gameObject;
        if(File.Exists(Path.Combine(saveFolderPath, "Event/" + EventName + ".json")))
        {
            string ThisEH = File.ReadAllText(Path.Combine( saveFolderPath,"Event/" + EventName + ".json"));
            if (!string.IsNullOrEmpty(ThisEH))
            {
                EventList NewEventList = JsonUtility.FromJson<EventList>(ThisEH);
                for(int i = 0; i< EventList.EventObj.Count; i++)
                {
                    EventList.EventObj[i].Copy(NewEventList.EventObj[i]);
                }    
            }
        }
        else
        {
            string saveRecord = JsonUtility.ToJson(EventList);
            File.WriteAllText(Path.Combine(save0Path, "Event/" + EventName + ".json"), saveRecord);
        }
        

        if (EventList.EventObj.Count > 0)
        {
            for (int i = 0; i < EventList.EventObj.Count; i++)
            {
                if (EventList.EventObj[i].IsDone == true)
                {
                    if(EventList.EventObj[i].Event.EventType == "Move")
                    {
                        EventList.EventObj[i].Target.transform.position = EventList.EventObj[i].Position;
                    }    
                }
            }
        }
    }

    private void Update()
    {
        if(IsEventing == false && GeneralInformation.Actioning == "Playing")
        {
            if (EventList.EventObj.Count > 0)
            {
                for (int i = 0; i < EventList.EventObj.Count; i++)
                {
                    if (EventList.EventObj[i].IsDone != true)
                    {
                        if(CheckEventActive(EventList.EventObj[i]))
                        PlayEvent(EventList.EventObj[i]);
                        break;
                    }
                }
            }
        }    
    }
    void ForAppear(GameObject go)
    {
        go.SetActive(true);
    }

    void ForDisappear(GameObject go)
    {
        go.SetActive(false);
    }    

    void ForBattle()
    {

    }
    void ForTalk(EventObject eo)
    {
        TalkingAndQuest taq = eo.Target.GetComponent<TalkingAndQuest>();
        if(taq.QuestList.Count > 0)
        {
            if (eo.IsGiveQuest == true && taq.QuestList[0].quest.IsEventClaimed == true)
            {
                taq.EventQuestGive = taq.QuestList[0].quest;
            }
            else
            {
                taq.EventQuestGive = null;
            }    
        }
        taq.IsEventTalk = true;
        taq.TalkText = eo.Event.Talks;
        taq.ShowTalk();
    }

    IEnumerator MoveAction(GameObject Go, Vector3 TargetPosition, float TimeGo)
    {
        Vector3 startPosition = Go.transform.position;
        float distanceToMove = Vector3.Distance(startPosition, TargetPosition);
        float speed = distanceToMove / TimeGo;
        while (Vector3.Distance(Go.transform.position, startPosition) < distanceToMove)
        {
            Go.transform.position += (TargetPosition - startPosition).normalized * speed * Time.deltaTime;
            yield return null;
        }
        Go.transform.position = TargetPosition; 
    }
    
    IEnumerator ForMove(GameObject Go, Event Ev, EventObject Eo)
    {
        IsEventing = true;
        GeneralInformation.Actioning = "Eventing";
        for (int i = 0; i < Ev.Moves.Count; i++)
        {
            Vector3 GoPosition = Go.transform.position;
            switch (Ev.Moves[i].direction)
            {
                case "Up":
                    GoPosition += Vector3.up * Ev.Moves[i].distance;
                    Eo.Position = GoPosition;
                    yield return StartCoroutine(MoveAction(Go, GoPosition, Ev.Moves[i].time));
                    break;
                case "Down":
                    GoPosition += Vector3.down * Ev.Moves[i].distance;
                    Eo.Position = GoPosition;
                    yield return StartCoroutine(MoveAction(Go, GoPosition, Ev.Moves[i].time));
                    break;
                case "Left":
                    GoPosition += Vector3.left * Ev.Moves[i].distance;
                    Eo.Position = GoPosition;
                    yield return StartCoroutine(MoveAction(Go, GoPosition, Ev.Moves[i].time));
                    break;
                case "Right":
                    GoPosition += Vector3.right * Ev.Moves[i].distance;
                    Eo.Position = GoPosition;
                    yield return StartCoroutine(MoveAction(Go, GoPosition, Ev.Moves[i].time));
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(0.35f);
        }
        IsEventing = false;
        GeneralInformation.Actioning = "Playing";
        yield return null;
    }

    public void PlayEvent(EventObject EO)
    {
        if (EO.Event.SetQuestDone!= null)
        {
            DoneQuest(EO.Event.SetQuestDone);
        }
        if (EO.Event.EventType == "Move")
        {
            StartCoroutine(ForMove(EO.Target, EO.Event, EO));
        }
        if (EO.Event.EventType == "Disappear")
        {
            EO.IsOnMap = false;
            ForDisappear(EO.Target);
        }
        if (EO.Event.EventType == "Appear")
        {
            EO.IsOnMap = true;
            ForAppear(EO.Target);
        }
        if (EO.Event.EventType == "Talk")
        {
            ForTalk(EO);
        }
        EO.IsDone = true;


        // Chuyển đổi mảng dữ liệu thành chuỗi JSON
        string saveFolderPath = "";
#if UNITY_ANDROID
        string rootPath = Application.persistentDataPath;
        saveFolderPath = Path.Combine(rootPath, "Save");
#else
        if (string.IsNullOrEmpty(saveFolderPath))
        {
            saveFolderPath = "Assets/Resources/Save";
        }
#endif
        string saveRecord = JsonUtility.ToJson(EventList);
        File.WriteAllText(Path.Combine(saveFolderPath,"Event/" + EventName + ".json"), saveRecord);

    }


    bool CheckEventActive(EventObject EO)
    {
        if (EO.Event.ConditionalQuest!=null)
        {
            if (EO.Event.ConditionalQuest.Status != "Done") return false;
        }
        if (EO.Event.RequiredItems.Count > 0)
        {
            foreach(Item item in EO.Event.RequiredItems)
            {
                if(!SaveSystem.saveLoad.inventory.CheckItemHas(item, 1) && !SaveSystem.saveLoad.inventory.CheckKeyItemHas(item, 1))
                {
                    return false;
                }    
            }    
        }    
        if (EO.Event.ActiveMethod == "InArea")
        {
            //float distancetoplayer = Vector2.Distance(Player.transform.position, EO.AreaActive.transform.position);
            //if (distancetoplayer <= EO.Event.RadiusActive)
            //{
            //    return true;
            //}
            SpriteRenderer playerSpriteRenderer = Player.GetComponent<SpriteRenderer>();
            Bounds playerBounds = playerSpriteRenderer.bounds;
            SpriteRenderer objectSpriteRenderer = EO.AreaActive.GetComponent<SpriteRenderer>();
            Bounds objectBounds = objectSpriteRenderer.bounds;
            Vector3 playerMin = playerBounds.min;
            Vector3 playerMax = playerBounds.max;
            Vector3 objectMin = objectBounds.min;
            Vector3 objectMax = objectBounds.max;

            if (playerMax.x > objectMin.x && playerMin.x < objectMax.x &&
                playerMax.y > objectMin.y && playerMin.y < objectMax.y)
            {
                return true;
            }

            //if (objectBounds.Intersects(playerBounds))
            //{
            //    return true;
            //}
        }    
        else if (EO.Event.ActiveMethod == "Near")
        {
            float distancetoplayer = Vector2.Distance(Player.transform.position, EO.Target.transform.position);
            if(distancetoplayer <= EO.Event.RadiusActive)
            {
                return true;
            }
        }
        else if (EO.Event.ActiveMethod == "Auto")
        {
            return true;
        }    

        return false;
    }    

    void GiveQuest(Quest quest, QuestClaim claim)
    {
        claim.AddQuest(quest);
    }    

    void DoneQuest(Quest quest)
    {
        quest.Status = "Done";
    }    
}
[System.Serializable]

public class EventList
{
    public List<EventObject> EventObj;
}

[System.Serializable]
public class EventObject
{
    public GameObject AreaActive;
    public GameObject Target;
    public Event Event;
    public bool IsOnMap = true;
    public Vector3 Position;
    public bool IsDone = false;
    public bool IsGiveQuest = false;

    public void Copy(EventObject eo)
    {
        Event = eo.Event;
        Position = eo.Position;
        IsDone = eo.IsDone;
        IsOnMap = eo.IsOnMap;
    }
}
