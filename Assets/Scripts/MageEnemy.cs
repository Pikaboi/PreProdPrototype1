using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEnemy : Enemy
{   
    public enum State
    {
        IDLE,
        DEFENSE,
        ATTACK,
        FINISHER,
        BACKWARDS
    }

    [SerializeField] State CurrentState;

    // Start is called before the first frame update
    override public void Start()
    {
        
    }

    // Update is called once per frame
    override public void Update()
    {
        //Control all the States
        switch (CurrentState)
        {
            case State.IDLE:
                Idle();
                break;
            case State.ATTACK:
                Attack();
                break;
            case State.DEFENSE:
                Defend();
                break;
            case State.FINISHER:
                LargeAttack();
                break;
            case State.BACKWARDS:
                MoveBack();
                break;
            default:
                //If something odd happens, default to Idle
                Idle();
                break;
        }
    }

    //Move the enemy back to keep distance from player
    void MoveBack()
    {

    }

    //Fire a Projectile at a player
    void Attack()
    {

    }

    //Strong attack used at low health
    void LargeAttack()
    {

    }

    //Defensive Movement, nullify attacks
    void Defend()
    {

    }


    //Idling, strafing around player
    void Idle()
    {

    }
}
