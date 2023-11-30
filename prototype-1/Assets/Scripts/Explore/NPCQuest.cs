using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCQuest : MonoBehaviour
{
    // maybe turn into array of strings for multiple quest steps? e.g., 1) go to object, 2) return to quest
    public string[] questLogHint;
    public bool isQuestCompleted = false;
    public bool isSoulConsumed = false;
    public bool isDecisionMade = false;
    public GameObject QuestLogEntry { get; set; }

    public SoulObject soulObject;
    public GameObject soulObjectPlace;

    public GameObject decisionPanel;
    public HeartMonitor heart;

    private InteractionDialogue npcDialogue;

    void Start()
    {
        npcDialogue = GetComponent<InteractionDialogue>();
    }

    public void SetSoulObjectActive()
    {
        soulObject.gameObject.SetActive(true);

        soulObjectPlace.GetComponent<SoulObject>().enabled = true;
        soulObjectPlace.GetComponent<InteractionDialogue>().enabled = true;
    }

/*    public void SetQuestLogEntry(GameObject questLogEntryObject)
    {
        questLogEntry = questLogEntryObject;
    }*/

    public void SetQuestComplete()
    {
        npcDialogue.UpdateDialogue();
        heart.AddFill(isSoulConsumed);

        if (isSoulConsumed)
        {
            npcDialogue.dialogueList.First().RemoveAt(0);
            print(npcDialogue.dialogueList.First()[0]);
        }
        else
        {
            npcDialogue.dialogueList.First().RemoveAt(1);
            print(npcDialogue.dialogueList.First()[0]);
        }

        isQuestCompleted = true;
        npcDialogue.allowMultipleInteractions = true;
        DayMonitor.ShiftTime();
        Destroy(QuestLogEntry);
    }

    public IEnumerator DecideOnSoulCapture()
    {
        decisionPanel.SetActive(true);
        decisionPanel.GetComponent<DecisionBox>().currentQuest = this;
        while (!isDecisionMade)
        {
            yield return null;
        }
        SetQuestComplete();
    }
}
