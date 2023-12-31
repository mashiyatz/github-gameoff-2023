using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulObject : MonoBehaviour
{
    public Image linkedUIObject;
    public NPCQuest linkedQuest;
    // public Sprite soulObjectSprite;
    public bool isAcquired = false;

    // private InteractionDialogue interaction;
    // private SoulObject soulObject;

    private void Start()
    {
        // interaction = GetComponent<InteractionDialogue>();
    }

    public void AcquireSoulObject()
    {
        isAcquired = true;
        linkedUIObject.color = Color.white;
        linkedQuest.QuestLogEntry.GetComponent<TextMeshProUGUI>().text = linkedQuest.questLogHint[1];
        linkedQuest.gameObject.GetComponent<InteractionDialogue>().UpdateDialogue();
        AkSoundEngine.PostEvent("Play_PickUpItem", gameObject);
        // gameObject.SetActive(false);
        enabled = false;
    }
}
