using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5.0f;
    private GMScript gameManager;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GMScript>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        if (gameManager.GetCurrentState() != GMScript.STATE.MOVE)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        if (gameManager.GetCurrentState() == GMScript.STATE.MOVE)
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            if (Mathf.Abs(horizontalInput) > 0.05f)
            {
                animator.SetBool("isWalking", true);
                if (horizontalInput > 0) spriteRenderer.flipX = false;
                if (horizontalInput < 0) spriteRenderer.flipX = true;
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            transform.Translate(movementSpeed * Time.deltaTime * new Vector3(horizontalInput, 0, 0));
            
        }
    }
}
