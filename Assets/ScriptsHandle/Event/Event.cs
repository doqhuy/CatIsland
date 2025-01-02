using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[System.Serializable]
public class Event
{
    //Move, Disappear, Talk, Appear, Battle
    public string EventType = "Disappear";

    public List<EventMove> Moves;
    public List<string> Talks;

    public Quest ConditionalQuest;
    public Quest SetQuestDone;

    public List<Item> RequiredItems;

    //Talk, Near, InArea, Auto
    public string ActiveMethod = "Talk";
    public float RadiusActive;

    public Vector3 SpawnPosition;
}



[System.Serializable]
public class EventMove
{
    //Left, Right, Up, Down
    public string direction = "Up";
    public float distance = 50f;
    public float time = 1.5f;
}




