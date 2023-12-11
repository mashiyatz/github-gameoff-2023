using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum STATUS { HEALTHY, BLEEDING, DEAD }
    public STATUS currentStatus;
    // public TypewriterUI typewriter;
    public Character target;
    public AttackEffectManager attackManager;
    public BattleManagerScript battleManager;

    public bool[] goodDecisions;
    public delegate void UpdateTurnBasedStat(int turn);
    public event UpdateTurnBasedStat OnNewTurn;

    private int knifeUsedTurn;
    private int locketUsedTurn;
    private int dollUsedTurn;
    public bool isTakingRecoil = false;
    public bool isTurnSkipped = false;

    public HealthMonitor healthbar;

    [SerializeField]
    private int HP;
    [SerializeField]
    private int ATK;
        
    private Stat _hp;
    private Stat _atk;

    void Start()
    {
        _hp = new(HP);
        _atk = new(ATK);
        currentStatus = STATUS.HEALTHY;
        goodDecisions = new bool[3];

        if (CompareTag("Player"))
        {
            if (MainManager.Instance)
            {
                goodDecisions[0] = MainManager.Instance.toyChoice;
                goodDecisions[1] = MainManager.Instance.knifeChoice;
                goodDecisions[2] = MainManager.Instance.locketChoice;
            }
        }
    }

    public void UpdateOnNewTurn()
    {
        OnNewTurn?.Invoke(BattleManagerScript.turnCount);
        StartCoroutine(WaitForTransition(1, 3f));
    }

    // general
    public int GetCurrentHP()
    {
        return _hp.value;
    }

    public int GetMaxHP()
    {
        return HP;
    }

    public void ChangeHP(int change)
    {
        _hp.ChangeStat(change);

        if (_hp.value < 0) _hp.value = 0;
        else if (_hp.value > HP) _hp.value = HP;

        if (CompareTag("Player")) healthbar.ChangeHealth();

        if (_hp.value <= 0)
        {
            if (CompareTag("Player")) battleManager.typewriter.Write("You have perished.");
            else if (CompareTag("Boss")) battleManager.typewriter.Write("The Demon is no more.");
            currentStatus = STATUS.DEAD;
            battleManager.checkEnding.TriggerEnding();
        }
        else if (_hp.value < HP / 3)
        {
            currentStatus = STATUS.BLEEDING;
        }
        else currentStatus = STATUS.HEALTHY;
        print($"{tag} HP: {_hp.value}");
    }

    public void DamageEnemy(Character target, int damage)
    {
        target.ChangeHP(-damage);
        if (isTakingRecoil) ChangeHP(-damage / 4);
    }

    // demon
    public void DemonWeakAttack(Character target)
    {
        battleManager.typewriter.Write("The Demon strikes.");
        GetComponent<SetDemonBody>().SetBodySprite(1);
        DamageEnemy(target, Random.Range(5, 8));
    }

    public void DemonStrongAttack(Character target)
    {
        battleManager.typewriter.Write("The Demon tears through.");
        GetComponent<SetDemonBody>().SetBodySprite(2);
        DamageEnemy(target, Random.Range(10, 14));
    }

    // player
    public void PlayerAttack()
    {
        battleManager.typewriter.Write("You swing at the Demon.");
        DamageEnemy(target, _atk.value);
        attackManager.PlayLightAtt();
    }

    IEnumerator WaitForTransition(int nextState, float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
        while (!battleManager.typewriter.isFinishedWriting || !attackManager.isFinishedPlaying) yield return null;
        yield return new WaitForSeconds(0.5f);
        battleManager.ChangeState(nextState);
    }

    public void DollAttack()
    {
        if (goodDecisions[0]) DollAttackBad(target);
        else DollAttackGood(target);

        StartCoroutine(WaitForTransition(2, 2f));
    }

    public void KnifeAttack()
    {
        if (goodDecisions[1]) KnifeAttackBad();
        else KnifeAttackGood(target);

        StartCoroutine(WaitForTransition(2, 2f));
    }

    public void LocketAttack()
    {
        if (goodDecisions[2]) LocketAttackBad();
        else LocketAttackGood();

        StartCoroutine(WaitForTransition(2, 2f));
    }


    // player-good
    private void KnifeAttackGood(Character target)
    {
        battleManager.typewriter.Write("You charge at the Demon.");
        DamageEnemy(target, 15);
        knifeUsedTurn = BattleManagerScript.turnCount;
        OnNewTurn += Recharge;
        isTurnSkipped = true;
        attackManager.PlayHeavyAtt();
    }

    private void Recharge(int turnNumber)
    {
        if (turnNumber == knifeUsedTurn + 1)
        {
            battleManager.typewriter.Write("You are overwhelmed by fatigue.");
        } else if (turnNumber == knifeUsedTurn + 2)
        {
            OnNewTurn -= Recharge;
            isTurnSkipped = false;
        }
    }

    private void DollAttackGood(Character target)
    {
        battleManager.typewriter.Write("You brace yourself against the Demon.");
        target.isTakingRecoil = true;
        dollUsedTurn = BattleManagerScript.turnCount;
        OnNewTurn += MakeEnemyRecoil;
    }

    private void MakeEnemyRecoil(int turnNumber)
    {
        if (turnNumber == dollUsedTurn + 3)
        {
            target.isTakingRecoil = false;
            OnNewTurn -= MakeEnemyRecoil;
        }
    }

    private void LocketAttackGood()
    {
        battleManager.typewriter.Write("You tend to your wounds.");
        ChangeHP(30);
        attackManager.PlayHealing();
    }

    // player-bad
    private void KnifeAttackBad()
    {
        battleManager.typewriter.Write("You are consumed by bloodlust.");
        _atk.ChangeStat(5);
        knifeUsedTurn = BattleManagerScript.turnCount;
        OnNewTurn += RevertAttack;
        attackManager.PlayHeavyAtt();
    }

    private void RevertAttack(int turnNumber)
    {
        if (turnNumber == knifeUsedTurn + 4)
        {
            _atk.ChangeStat(-5);
            OnNewTurn -= RevertAttack;
        }
    }

    private void DollAttackBad(Character target)
    {
        battleManager.typewriter.Write("You distract the Demon with your illusion.");
        ChangeHP(-5);
        dollUsedTurn = BattleManagerScript.turnCount;
        OnNewTurn += SkipEnemyTurn;
        target.isTurnSkipped = true;
    } 

    private void SkipEnemyTurn(int turnNumber)
    {
        if (turnNumber == dollUsedTurn + 1)
        {
            battleManager.typewriter.Write("The illusion fades.");
            target.isTurnSkipped = false;
            OnNewTurn -= SkipEnemyTurn;
        }
    }

    private void LocketAttackBad()
    {
        battleManager.typewriter.Write("Your flesh is purged of wounds.");
        locketUsedTurn = BattleManagerScript.turnCount;
        OnNewTurn += HealPerTurn;
        ChangeHP(10);
        attackManager.PlayHealing();
    }

    private void HealPerTurn(int turnNumber)
    {
        if (turnNumber == locketUsedTurn + 3)
        {
            ChangeHP(10);
            attackManager.PlayHealing();
            OnNewTurn -= HealPerTurn;
        }
    }

    void Update()
    {
        
    }

    
}
