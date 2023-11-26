using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum STATUS { HEALTHY, BLEEDING, FROZEN, DEAD }
    public STATUS currentStatus;

    [SerializeField]
    private int HP;
    [SerializeField]
    private int ATK;
    [SerializeField]
    private int DEF;

    // current stats
    private Stat _hp;
    private Stat _atk;
    private Stat _def;

    void Start()
    {
        _hp = new(HP);
        _atk = new(ATK);
        _def = new(DEF);
        currentStatus = STATUS.HEALTHY;
    }

    public void TakeDamage(Stat enemyAttack)
    {
        Buff(_hp, enemyAttack.value - _def.value);
        if (_hp.value <= 0) currentStatus = STATUS.DEAD;
    }

    public void Buff(Stat stat, int change)
    {
        stat.ChangeStat(change);
    }

    public void Attack(Character enemy)
    {
        enemy.TakeDamage(_atk);
    }

    void Update()
    {
        
    }

    
}
