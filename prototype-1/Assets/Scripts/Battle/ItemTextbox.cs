using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemTextbox : MonoBehaviour
{
    private TextMeshProUGUI textMeshProComponent;

    public string evilDesc;
    public string notEvilDesc;
    public int index;
    public Character player;

    void Start()
    {
        textMeshProComponent = GetComponent<TextMeshProUGUI>();
        
        if (player.evilDecision[index])
        {
            textMeshProComponent.text = evilDesc;
        }
        else
        {
            textMeshProComponent.text = notEvilDesc;
        }
    }
}
