using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionBox : MonoBehaviour
{
    public NPCQuest currentQuest;
    public GameObject expositionBox;

    private void OnDisable()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }
    }

    public void PlayButtonSound()
    {
        AkSoundEngine.PostEvent("Play_MouseClick", gameObject);
    }

    private void OnEnable()
    {
        expositionBox.SetActive(true);
    }

    public void MakeConsumeDecision(bool doesConsume)
    {

        currentQuest.isSoulConsumed = doesConsume;
        currentQuest.isDecisionMade = true;

        if(currentQuest.itemNum == 0)
        {
            MainManager.Instance.toyChoice = doesConsume;
        }
        if(currentQuest.itemNum == 1)
        {
            MainManager.Instance.knifeChoice = doesConsume;
        }
        if(currentQuest.itemNum == 2)
        {
            MainManager.Instance.locketChoice = doesConsume;
        }
        

        gameObject.SetActive(false);
    }
}
