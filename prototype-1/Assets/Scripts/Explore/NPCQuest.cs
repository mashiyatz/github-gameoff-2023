using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCQuest : MonoBehaviour
{
    // maybe turn into array of strings for multiple quest steps? e.g., 1) go to object, 2) return to quest
    public string[] questLogHint;
    public bool isQuestCompleted = false;
    public GameObject QuestLogEntry { get; set; }
    public SoulObject soulObject;

    void Start()
    {
        
    }

    public void SetSoulObjectActive()
    {
        soulObject.gameObject.SetActive(true);
    }

/*    public void SetQuestLogEntry(GameObject questLogEntryObject)
    {
        questLogEntry = questLogEntryObject;
    }*/

    public void SetQuestComplete()
    {
        isQuestCompleted = true;
        Destroy(QuestLogEntry);
    }
}
