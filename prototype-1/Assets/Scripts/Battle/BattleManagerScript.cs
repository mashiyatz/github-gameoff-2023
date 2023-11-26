using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManagerScript : MonoBehaviour
{
    public enum PHASE { PLAYER, ENEMY }
    public PHASE currentPhase;

    public GameObject battleUI;
    public Button fightButton;

    public Character player;
    public Character enemy;

    void Start()
    {
        currentPhase = PHASE.PLAYER;
    }

    public void ChangeState(PHASE toPhase) { 
        if (toPhase == PHASE.PLAYER)
        {
            // turn on player UI
            battleUI.SetActive(true);
            fightButton.interactable = true;
            currentPhase = PHASE.PLAYER;
        } else if (toPhase == PHASE.ENEMY)
        {
            // turn off player UI
            // start enemy actions
            battleUI.SetActive(false);
            fightButton.interactable = false;
            currentPhase = PHASE.ENEMY;
            StartCoroutine(RunEnemyPhase());
        }
    }

    IEnumerator RunEnemyPhase()
    {
        yield return new WaitForSeconds(0.25f);
        enemy.Attack(player);
        yield return new WaitForSeconds(0.25f);
        ChangeState(PHASE.PLAYER);
    }

    public void TransitionToEnemyPhase()
    {
        StartCoroutine(ChangeToEnemyPhase());
    }

    IEnumerator ChangeToEnemyPhase()
    {
        yield return new WaitForSeconds(0.25f);
        ChangeState(PHASE.ENEMY);
    }

    void Update()
    {
        
    }
}
