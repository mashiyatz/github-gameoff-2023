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

        if (gameManager.GetCurrentState() != GMScript.STATE.MOVE) return;

        if (gameManager.GetCurrentState() == GMScript.STATE.MOVE)
        {
            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.y > 500) return;

                // if (Input.mousePosition.x > Camera.main.WorldToScreenPoint(transform.position).x)
                if (Input.mousePosition.x > 640)
                {
                    transform.Translate(movementSpeed * Time.deltaTime * Vector2.right);
                } else if (Input.mousePosition.x < 640)
                {
                    transform.Translate(movementSpeed * Time.deltaTime * Vector2.left);
                }
            }
        }


/*        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(movementSpeed * Time.deltaTime * new Vector3(horizontalInput, 0, 0));*/
    }
}
