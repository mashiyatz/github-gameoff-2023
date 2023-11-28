using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDialogue : MonoBehaviour
{
    public bool isPlayerInView;
    public string[] dialogArray;
    public string[] secondInteractionDialogue;
    public DialogueManager dialogueBox;
    public GMScript gameManager;

    public bool hasAlreadyInteracted;
    

    private void Start()
    {
        isPlayerInView = false;
        hasAlreadyInteracted = false;
    }

    public void UpdateDialogue()
    {
        dialogArray = new string[secondInteractionDialogue.Length];
        dialogArray = secondInteractionDialogue;
        hasAlreadyInteracted = false;
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
            if (!hasAlreadyInteracted)
            {
                dialogueBox.PlayDialog(dialogArray, transform);
                //stops the weird looping issue
                gameManager.SetCurrentState(GMScript.STATE.INTERACT);
                hasAlreadyInteracted = true;
            }
        }
    }
}
