using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum STATUS { HEALTHY, BLEEDING, DEAD }
    public STATUS currentStatus;

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
        if (_hp.value <= 0) currentStatus = STATUS.DEAD;
        else if (_hp.value < HP / 3) currentStatus = STATUS.BLEEDING;
        else currentStatus = STATUS.HEALTHY;
    }

    public void DamageEnemy(Character target, int damage)
    {
        target.ChangeHP(damage);
    }

    // demon
    public void DemonWeakAttack(Character target)
    {
        DamageEnemy(target, 3);
    }

    public void DemonStrongAttack(Character target)
    {
        DamageEnemy(target, 8);
    }

    public void DemonCharge()
    {
        ChangeHP(5);
    }

    // player-good
    public void PlayerWeakAttack(Character target)
    {
        DamageEnemy(target, 2);
    }

    public void PlayerDefend()
    {
        // don't take damage next turn?
        // causes enemy to reflect damage?
    }

    public void PlayerPray()
    {
        ChangeHP(5);
    }

    // player-bad
    public void PlayerStrongAttack(Character target)
    {
        DamageEnemy(target, 5);
    }

    public void PlayerExplosion(Character target)
    {
        DamageEnemy(target, 8);
        DamageEnemy(this, 3);
    }

    public void PlayerBlast(Character target)
    {
        int damage = Random.Range(0, 2) * 16;
        if (damage == 0) print("shame");
        else print("yay");
        DamageEnemy(target, damage);
        // can't move next turn
    }

    void Update()
    {
        
    }

    
}
