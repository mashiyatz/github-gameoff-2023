using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDemonEye : MonoBehaviour
{
    public Sprite[] sprites;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void SetEyeSprite(BattleManagerScript.PHASE phase)
    {
        switch (phase)
        {
            case BattleManagerScript.PHASE.PLAYERSTART:
                image.color = Color.white;
                image.sprite = sprites[0];
                break;
            case BattleManagerScript.PHASE.PLAYERACT:
                image.sprite = sprites[1];
                break;
            case BattleManagerScript.PHASE.ENEMY:
                image.color = Color.red;
                break;
        }
    }
}
