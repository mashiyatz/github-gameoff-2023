using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DollText : MonoBehaviour
{
    public EndingCheck ruEvil;
    // Start is called before the first frame update
    private TextMeshProUGUI textMeshProComponent;

    public string evilDesc;
    public string notEvilDesc;
    void Start()
    {
        textMeshProComponent = GetComponent<TextMeshProUGUI>();
        
        if (ruEvil.isEvil)
        {
            textMeshProComponent.text = evilDesc;
        }
        else
        {
            textMeshProComponent.text = notEvilDesc;
        }
    }
}
