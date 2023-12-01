using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChecker : MonoBehaviour
{

    public NPCQuest knife;
    public NPCQuest doll;
    public NPCQuest charm;
    public GameObject TextUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(knife.isQuestCompleted && doll.isQuestCompleted && charm.isQuestCompleted)
        {
            TextUI.SetActive(true);
        }
        
    }
}
