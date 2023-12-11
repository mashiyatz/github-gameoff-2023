using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//credit: adapted this walking tutorial for point-and-click, https://github.com/PawelDrozdowski/Point-and-Click-Game/tree/main

public class GMScript : MonoBehaviour
{
    public enum STATE { MOVE, PAUSE, INTERACT }
    private STATE currentState = STATE.MOVE;
    private STATE lastState;
    
    static float moveSpeed = 4.5f, moveAccuracy = 0.15f;

    public GameObject pauseScreen;
    public GameObject openMenuButton;
    public GameObject closeMenuButton;
    
    public GameObject player;

    public RectTransform questLog;
    public GameObject questPrefab;

    [SerializeField]
    private AK.Wwise.Event ambience;

    void Start()
    {
        currentState = STATE.MOVE;
        ambience.Post(gameObject);
    }

    public GameObject CreateQuestLogEntry(string questHint)
    {
        var go = Instantiate(questPrefab, questLog);
        go.GetComponent<TextMeshProUGUI>().text = $"- {questHint}";
        return go;
    }

    public STATE GetCurrentState()
    {
        return currentState;
    }

    public void SetCurrentState(STATE newState)
    {
        currentState = newState; 
    }

    public void TogglePauseMenu()
    {
        if (currentState != STATE.PAUSE)
        {
            lastState = currentState;
            pauseScreen.SetActive(true);
            openMenuButton.SetActive(false);
            closeMenuButton.SetActive(true);
            currentState = STATE.PAUSE;
        }
        else if (currentState == STATE.PAUSE)
        {
            pauseScreen.SetActive(false);
            openMenuButton.SetActive(true);
            closeMenuButton.SetActive(false);
            currentState = lastState;
        }
    }
    
    public IEnumerator MoveToPoint(Transform myObject, Vector2 point)
    {
        // calculate position difference only for the x-axis
        float xPositionDifference = point.x - myObject.position.x;

        // flip object
        if (myObject.GetComponentInChildren<SpriteRenderer>() && xPositionDifference != 0)
            myObject.GetComponentInChildren<SpriteRenderer>().flipX = xPositionDifference > 0;

        // stop when we are near the point
        while (Mathf.Abs(xPositionDifference) > moveAccuracy)
        {
            // move only along the x-axis
            myObject.Translate(new Vector3(moveSpeed * Mathf.Sign(xPositionDifference) * Time.deltaTime, 0, 0));

            // recalculate position difference for the x-axis
            xPositionDifference = point.x - myObject.position.x;
            yield return null;
        }

        // snap to point
        myObject.position = new Vector3(point.x, myObject.position.y, myObject.position.z);

        // tell ClickManager that the player has arrived
        if (myObject == FindObjectOfType<PlayerClickAndMove>().player)
        {
            FindObjectOfType<PlayerClickAndMove>().playerWalking = false;
            //FindObjectOfType<PlayerClickAndMove>().canInteract = true;
        }

        yield return null;
    }


    void Update()
    {
/*        //affects NPC interaction/movement
        if (GetCurrentState() == STATE.INTERACT)
        {
            player.GetComponent<PlayerClickAndMove>().canInteract = false;
        }
        else
        {
            player.GetComponent<PlayerClickAndMove>().canInteract = true;
        }*/

    }
}
