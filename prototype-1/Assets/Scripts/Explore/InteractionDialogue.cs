using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Dialogue
{
    public List<string> stringList;
}

[System.Serializable]
public class DialogueList
{
    public List<Dialogue> dialogueList;

    public List<Dialogue> Pop() {
        try { 
            dialogueList.RemoveAt(0);
            return dialogueList;
        }
        catch { return dialogueList; }
    }

    public List<string> First()
    {
        return dialogueList[0].stringList;
    } 
}

public class InteractionDialogue : MonoBehaviour
{
    public bool isPlayerInView;
    public DialogueList dialogueList;

    public DialogueManager dialogueBox;
    public GMScript gameManager;

    public bool hasAlreadyInteracted;
    public bool allowMultipleInteractions;

    public GameObject chatIndicator;

    private void Start()
    {
        isPlayerInView = false;
        hasAlreadyInteracted = false;
        chatIndicator.SetActive(true);
    }

    public void UpdateDialogue()
    {
        dialogueList.Pop();
        hasAlreadyInteracted = false; 
        chatIndicator.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInView = true;
            // print("NPC False");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isPlayerInView = false;
    }

    private void OnMouseDown()
    {
        if (isPlayerInView && gameManager.GetCurrentState() != GMScript.STATE.INTERACT)
        {
            if (!hasAlreadyInteracted || allowMultipleInteractions)
            {
                dialogueBox.PlayDialog(dialogueList.First(), transform);                
                //stops the weird looping issue
                gameManager.SetCurrentState(GMScript.STATE.INTERACT);
                hasAlreadyInteracted = true;
                chatIndicator.SetActive(false);
            }
        }
    }
}
