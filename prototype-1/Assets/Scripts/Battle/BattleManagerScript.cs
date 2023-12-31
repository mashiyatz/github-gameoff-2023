using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManagerScript : MonoBehaviour
{
    public enum PHASE { PLAYERSTART, PLAYERACT, ENEMY, END }
    public PHASE currentPhase;

    public GameObject battleUI;
    public GameObject fightButton;
    public GameObject inventory;

    public Character player;
    public Character demon;
    public SetDemonEye demonEye;
    public SetDemonBody demonBody;

    public TypewriterEffect typewriter;
    public static int turnCount;
    public EndingCheck checkEnding;

    // make some way to create stock quotes?

    void Start()
    {
        currentPhase = PHASE.PLAYERACT;
        demonEye.SetEyeSprite(currentPhase);
        turnCount = 1;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
        fightButton.SetActive(true);
        inventory.SetActive(true);
    }


    // on click add start turn var, if turn +3, remove
    // make item disappear on giving it away

    IEnumerator ToEnd()
    {
        while (!typewriter.isFinishedWriting) {
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        checkEnding.TriggerEnding();
    }

    public void ChangeState(int index) {

        if ((player.currentStatus == Character.STATUS.DEAD) || (demon.currentStatus == Character.STATUS.DEAD))
        {
            if (player.currentStatus == Character.STATUS.DEAD) typewriter.Write("You have perished.");
            else if (demon.currentStatus == Character.STATUS.DEAD) typewriter.Write("The Demon is no more.");
            StartCoroutine(ToEnd());
            return;
        }

        PHASE toPhase = (PHASE)index;
        switch (toPhase)
        {
            case PHASE.PLAYERSTART:
                if (player.currentStatus == Character.STATUS.BLEEDING) typewriter.Write("Your wounds are severe.");
                // else typewriter.Write("The Demon slumbers before you.");
                else typewriter.Write("");
                demon.GetComponent<Animator>().Play("Demon_Blink");
                    // blink animation instead?
                turnCount += 1;
                player.UpdateOnNewTurn();
                currentPhase = PHASE.PLAYERSTART;
                break;
            case PHASE.PLAYERACT:
                if (player.isTurnSkipped) {
                    demonEye.SetEyeSprite(PHASE.PLAYERACT);
                    ChangeState(2);
                    break;
                }
                typewriter.Write("The Demon stirs. What is your move?"); 
                fightButton.SetActive(true);
                inventory.SetActive(true);
                currentPhase = PHASE.PLAYERACT;
                break;
            case PHASE.ENEMY:
                if (demon.currentStatus == Character.STATUS.BLEEDING) typewriter.Write("The Demon writhes in pain.");
                else typewriter.Write("The Demon watches you.");

                fightButton.SetActive(false);
                inventory.SetActive(false);
                currentPhase = PHASE.ENEMY;
                StartCoroutine(RunEnemyPhase());
                break;
        }
        // if (demon.currentStatus != Character.STATUS.BLEEDING) demonEye.SetEyeSprite(currentPhase); 
    }

    IEnumerator RunEnemyPhase()
    {
        // demonEye.SetEyeSprite(PHASE.ENEMY);
        demon.GetComponent<Animator>().Play("Demon_Watch");
        yield return new WaitForSeconds(3.2f);

        if (demon.isTurnSkipped) typewriter.Write("The Demon strikes at air.");
        else
        {
            if (turnCount % 4 == 0) demon.DemonStrongAttack(player);
            else demon.DemonWeakAttack(player);
        }

        yield return new WaitForSeconds(3.2f);
        demonBody.SetBodySprite(0);
        // demonEye.SetEyeSprite(PHASE.PLAYERSTART);
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
