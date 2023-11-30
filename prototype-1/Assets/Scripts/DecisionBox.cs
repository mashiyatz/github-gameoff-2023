using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionBox : MonoBehaviour
{
    public NPCQuest currentQuest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        try { transform.GetChild(0).gameObject.SetActive(true); }
        catch { }
    }

    public void MakeConsumeDecision(bool doesConsume)
    {
        currentQuest.isSoulConsumed = doesConsume;
        currentQuest.isDecisionMade = true;
        gameObject.SetActive(false);
    }
}
