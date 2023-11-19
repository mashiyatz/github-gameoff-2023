using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5.0f;
    private GMScript gameManager;
    
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GMScript>();
    }

    void Update()
    {

        if (gameManager.GetCurrentState() != GMScript.STATE.PLAY) return;

        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(movementSpeed * Time.deltaTime * new Vector3(horizontalInput, 0, 0));
    }
}
