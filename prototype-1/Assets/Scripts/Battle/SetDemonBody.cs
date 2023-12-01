using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDemonBody : MonoBehaviour
{
    public Image image;
    public Sprite[] sprites;

    void Start()
    {
        image.sprite = sprites[0];
    }

    public void SetBodySprite(int index)
    {
        if (index > sprites.Length) index = 0;
        image.sprite = sprites[index];
    }
}
