using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMScript : MonoBehaviour
{
    public enum STATE { MOVE, PAUSE, INTERACT }
    private STATE currentState = STATE.MOVE;
    private STATE lastState;

    public GameObject pauseScreen;
    public GameObject openMenuButton;
    public GameObject closeMenuButton;

    void Start()
    {
        currentState = STATE.MOVE;
    }

    public STATE GetCurrentState()
    {
        return currentState;
    }

    public void SetCurrentState(STATE state)
    {
        currentState = state;
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


    void Update()
    {


    }
}
