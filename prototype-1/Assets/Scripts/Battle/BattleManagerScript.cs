using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManagerScript : MonoBehaviour
{
    public enum PHASE { PLAYERSTART, PLAYERACT, ENEMY }
    public PHASE currentPhase;

    public GameObject battleUI;
    public GameObject fightButton;
    public GameObject inventory;

    public Character player;
    public Character demon;
    public SetDemonEye demonEye;

    public TypewriterUI typewriter;
    public int turnCount;

    // make some way to create stock quotes?

    void Start()
    {
        currentPhase = PHASE.PLAYERSTART;
        turnCount = 0;
    }


    public void ChangeState(int index) {

        PHASE toPhase = (PHASE)index;
        switch (toPhase)
        {
            case PHASE.PLAYERSTART:
                fightButton.SetActive(true);
                typewriter.Write("The Demon sleeps.");
                currentPhase = PHASE.PLAYERSTART;
                break;
            case PHASE.PLAYERACT:
                typewriter.Write("The Demon stirs.");
                fightButton.SetActive(false);
                inventory.SetActive(true);
                currentPhase = PHASE.PLAYERACT;
                break;
            case PHASE.ENEMY:
                typewriter.Write("The Demon watches.");
                inventory.SetActive(false);
                currentPhase = PHASE.ENEMY;
                StartCoroutine(RunEnemyPhase());
                break;
        }
        if (demon.currentStatus != Character.STATUS.BLEEDING) demonEye.SetEyeSprite(currentPhase); 
    }

    IEnumerator RunEnemyPhase()
    {
        yield return new WaitForSeconds(0.25f);
        demon.DemonWeakAttack(player);
        yield return new WaitForSeconds(0.25f);
        ChangeState((int)PHASE.PLAYERSTART);
    }

    public void TransitionToEnemyPhase()
    {
        StartCoroutine(ChangeToEnemyPhase());
    }

    IEnumerator ChangeToEnemyPhase()
    {
        yield return new WaitForSeconds(0.25f);
        ChangeState((int)PHASE.ENEMY);
    }

    void Update()
    {
        
    }
}
