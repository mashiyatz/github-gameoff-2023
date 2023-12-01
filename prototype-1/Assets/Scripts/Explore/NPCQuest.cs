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
    public DayMonitor dayMonitor;

    private InteractionDialogue npcDialogue;

    void Start()
    {
        npcDialogue = GetComponent<InteractionDialogue>();
    }

    public void SetSoulObjectActive()
    {
        soulObjectPlace.GetComponent<SoulObject>().enabled = true;
        soulObject = soulObjectPlace.GetComponent<SoulObject>();
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
        }
        else
        {
            npcDialogue.dialogueList.First().RemoveAt(1);
        }

        isQuestCompleted = true;
        npcDialogue.allowMultipleInteractions = true;
        dayMonitor.ShiftTime();
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
