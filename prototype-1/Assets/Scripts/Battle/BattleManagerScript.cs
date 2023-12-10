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
    public SetDemonBody demonBody;

    public TypewriterUI typewriter;
    public int turnCount;

    // make some way to create stock quotes?

    void Start()
    {
        fightButton.SetActive(true);
        currentPhase = PHASE.PLAYERSTART;
        demonEye.SetEyeSprite(currentPhase);
        turnCount = 1;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
    }

    public void ChangeState(int index) {
        PHASE toPhase = (PHASE)index;
        switch (toPhase)
        {
            case PHASE.PLAYERSTART:
                turnCount += 1;
                fightButton.SetActive(true);
                typewriter.Write("The Demon slumbers.");
                currentPhase = PHASE.PLAYERSTART;
                break;
            case PHASE.PLAYERACT:
                typewriter.Write("The Demon stirs."); // "Select an action"?
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
        yield return new WaitForSeconds(3.2f);
        if (turnCount % 4 == 0) demon.DemonStrongAttack(player);
        else demon.DemonWeakAttack(player);
        yield return new WaitForSeconds(3.2f);
        demonBody.SetBodySprite(0);
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
