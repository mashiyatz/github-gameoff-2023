using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public bool isPlayerInView;
    public string[] dialogArray;
    public DialogueBox dialogueBox;
    public GMScript gameManager;

    private void Start()
    {
        isPlayerInView = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isPlayerInView = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isPlayerInView = false;
    }

    private void OnMouseDown()
    {
        if (isPlayerInView)
        {
            dialogueBox.PlayDialog(dialogArray);
            gameManager.SetCurrentState(GMScript.STATE.INTERACT);
        }
    }
}
