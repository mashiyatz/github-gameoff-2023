using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulObject : MonoBehaviour
{
    public Image linkedUIObject;
    public NPCQuest linkedQuest;
    public Sprite soulObjectSprite;
    public bool isAcquired = false;
    
    public void AcquireSoulObject()
    {
        isAcquired = true;
        linkedUIObject.sprite = soulObjectSprite;
        linkedQuest.QuestLogEntry.GetComponent<TextMeshProUGUI>().text = linkedQuest.questLogHint[1];
        linkedQuest.gameObject.GetComponent<InteractionDialogue>().UpdateDialogue();
        gameObject.SetActive(false);
    }
}
