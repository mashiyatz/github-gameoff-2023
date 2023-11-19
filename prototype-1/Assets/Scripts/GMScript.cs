using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMScript : MonoBehaviour
{
    public enum STATE { PLAY, PAUSE }
    private STATE currentState = STATE.PLAY;

    public GameObject pauseScreen;

    void Start()
    {
        
    }

    public STATE GetCurrentState()
    {
        return currentState;
    }


    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (currentState == STATE.PLAY)
            {
                pauseScreen.SetActive(true);
                currentState = STATE.PAUSE;
            } else if (currentState == STATE.PAUSE)
            {
                pauseScreen.SetActive(false);
                currentState = STATE.PLAY;
            }
            
        }
    }
}
