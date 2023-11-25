using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManagerScript : MonoBehaviour
{
    public enum PHASE { PLAYER, ENEMY }
    public PHASE currentPhase;

    void Start()
    {
        currentPhase = PHASE.PLAYER;
    }

    public void ChangeState(PHASE toPhase) { 
        if (toPhase == PHASE.PLAYER)
        {
            // turn on player UI
            currentPhase = PHASE.PLAYER;
        } else if (toPhase == PHASE.ENEMY)
        {
            // turn off player UI
            // start enemy actions
            currentPhase = PHASE.ENEMY;
        }
    }

    void Update()
    {
            
    }
}
