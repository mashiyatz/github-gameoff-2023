using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    private float heightChange = 0.3f;
    private float startHeight;

    private void Start()
    {
        startHeight = transform.position.y;
    }

    void Update()
    {
        float y = heightChange * Mathf.Sin(Time.time * 2);
        float newHeight = startHeight + y;
        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
    }
}
