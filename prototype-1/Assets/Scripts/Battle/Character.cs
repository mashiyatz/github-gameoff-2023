using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum STATUS { HEALTHY, BLEEDING, DEAD }
    public STATUS currentStatus;
    public TypewriterUI typewriter;
    public Character target;
    public AttackEffectManager attackManage;

    public bool[] goodDecisions;

    [SerializeField]
    private int HP;


    private Stat _hp;

    void Start()
    {
        _hp = new(HP);
        currentStatus = STATUS.HEALTHY;
    }

    // general
    public void ChangeHP(int change)
    {
        _hp.ChangeStat(change);
        if (_hp.value <= 0)
        {
            if (CompareTag("Player")) typewriter.Write("You have perished.");
            else if (CompareTag("Boss")) typewriter.Write("The Demon is no more.");
            currentStatus = STATUS.DEAD;
        }
        else if (_hp.value < HP / 3)
        {
            if (CompareTag("Player")) typewriter.Write("You are gravely wounded.");
            else if (CompareTag("Boss")) typewriter.Write("The Demon shrieks in pain.");
            currentStatus = STATUS.BLEEDING;
        }
        else currentStatus = STATUS.HEALTHY;
        print($"HP: {_hp.value}");
    }

    public void DamageEnemy(Character target, int damage)
    {
        target.ChangeHP(-damage);
    }

    // demon
    public void DemonWeakAttack(Character target)
    {
        typewriter.Write("The Demon strikes.");
        GetComponent<SetDemonBody>().SetBodySprite(1);
        DamageEnemy(target, 3);
    }

    public void DemonStrongAttack(Character target)
    {
        typewriter.Write("The Demon rips.");
        GetComponent<SetDemonBody>().SetBodySprite(2);
        DamageEnemy(target, 8);
    }

    public void DollAttack()
    {
        if (goodDecisions[0]) PlayerDefend(target);
        else PlayerExplosion(target);
    }

    public void KnifeAttack()
    {
        if (goodDecisions[1]) PlayerWeakAttack(target);
        else PlayerExplosion(target);
    }

    public void LocketAttack()
    {
        if (goodDecisions[2]) PlayerPray();
        else PlayerBlast(target);
    }


    // player-good
    private void PlayerWeakAttack(Character target)
    {
        typewriter.Write("You swing at the Demon.");
        DamageEnemy(target, 3);
        attackManage.PlayLightAtt();
    }

    private void PlayerDefend(Character target)
    {
        typewriter.Write("You protect yourself.");
        DamageEnemy(target, Random.Range(2, 4));
        ChangeHP(Random.Range(1, 3));
        //attackManage.PlayHeavyAtt();
    }

    private void PlayerPray()
    {
        typewriter.Write("You tend to your wounds.");
        ChangeHP(5);
        attackManage.PlayHealing();
    }

    // player-bad
    private void PlayerStrongAttack(Character target)
    {
        typewriter.Write("You bludgeon the Demon.");
        DamageEnemy(target, 6);
        attackManage.PlayLightAtt();
    }

    private void PlayerExplosion(Character target)
    {
        typewriter.Write("You engulf the Demon in flames.");
        DamageEnemy(target, 8);
        DamageEnemy(this, 3);
        attackManage.PlayHeavyAtt();
    }

    private void PlayerBlast(Character target)
    {
        int damage = Random.Range(0, 2) * 10;
        if (damage == 0) typewriter.Write("The Demon eludes you.");
        else typewriter.Write("The Demon cries out in pain.");
        DamageEnemy(target, damage);
        //attackManage.PlayHeavyAtt();
    }

    void Update()
    {
        
    }

    
}
