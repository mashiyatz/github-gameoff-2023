using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public bool isPlayerInView;
    public string[] dialogArray;
    public DialogueBox dialogueBox;
    public GMScript gameManager;

    public bool isBattleTrigger;

    private void Start()
    {
        isPlayerInView = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInView = true;
            print("NPC False");
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
            dialogueBox.PlayDialog(dialogArray, transform);
            //stops the weird looping issue
            gameManager.SetCurrentState(GMScript.STATE.INTERACT);
        }
    }
}
