using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetItemSprites : MonoBehaviour
{
    public Sprite[] goodSprites; // order is knife, doll, locket
    public Sprite[] badSprites;
    public Image[] itemImages;
    public Character player;

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            if (player.evilDecision[i]) itemImages[i].sprite = badSprites[i];
            else itemImages[i].sprite = goodSprites[i];
        }
    }
}
