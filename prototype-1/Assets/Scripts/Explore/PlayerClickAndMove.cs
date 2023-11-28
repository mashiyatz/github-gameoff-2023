using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickAndMove : MonoBehaviour
{
    public Transform player;
    [SerializeField]
    public bool playerWalking = false;
    //checks if player is walking
    public bool canInteract = true;
    //checks is player is interacting with UI elements, i.e. NPC
    private GMScript gameManager;
    public Camera myCamera;
    public Coroutine GoToClick;
    
    private float mousePressStartTime = 0f;
    private float longPressThreshold = 0.2f;
    // private bool isMousePressed = false;


    void Start()
    {
       // player = this.GetComponent<Transform>().transform;
        gameManager = GameObject.Find("GameManager").GetComponent<GMScript>();
        //myCamera = Camera.main; // Assign the main camera to myCamera
    }

    void Update()
    {
        if (gameManager.GetCurrentState() != GMScript.STATE.MOVE) return;

        if (Input.GetMouseButton(0) && canInteract)
        {
            if (Input.mousePosition.y > 500) return;

            print("pressed");
            // if (GoToClick != null && canInteract && playerWalking)
            //     StopCoroutine(GoToClick); // Stop the coroutine if it's already running
            // GoToClick = StartCoroutine(GoToClickCoroutine(Input.mousePosition));
            // isMousePressed = true;
            mousePressStartTime = Time.time;
        }
        
        if (Input.GetMouseButtonUp(0) && canInteract)
        {
            if (Input.mousePosition.y > 500) return;

            // isMousePressed = false;
            float pressDuration = Time.time - mousePressStartTime;

            if (pressDuration < longPressThreshold)
            {
                if (GoToClick != null && playerWalking)
                    StopCoroutine(GoToClick); // Stop the coroutine if it's already running

                else if (canInteract && !playerWalking)
                {
                    GoToClick = StartCoroutine(GoToClickCoroutine(Input.mousePosition));
                }
            }
            // else, it was a long press and we don't trigger the coroutine
        }
    }

    public IEnumerator GoToClickCoroutine(Vector2 mousePos)
    {
        print("coroutine");
        // wait to make room for GoToItem() checks
        yield return new WaitForSeconds(0.05f);

        Vector2 targetPos = myCamera.ScreenToWorldPoint(mousePos);
        print("targetPos: " + targetPos);

        // start walking
        playerWalking = true;
        yield return StartCoroutine(gameManager.MoveToPoint(player, targetPos));
        
        // play animation
        // GetComponent<SpriteAnimator>().PlayAnimation(gameManager.playerAnimations[1]);

        // stop walking
        yield return StartCoroutine(CleanAfterClick());
    }

    private IEnumerator CleanAfterClick()
    {
        while (playerWalking)
            yield return new WaitForSeconds(0.05f);
        
        // player.GetComponent<SpriteAnimator>().PlayAnimation(null);
        yield return null;
    }
}
