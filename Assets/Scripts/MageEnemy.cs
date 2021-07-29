using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    int m_IdleMove = 1;


    // Start is called before the first frame update
    override public void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    override public void Update()
    {
        transform.LookAt(m_Player.transform.position);
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
        m_Agent.Move(transform.forward * -1 * m_Speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, m_Player.transform.position) > 10.0f)
        {
            CurrentState = State.IDLE;
            IdleMove();
        }
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
        if (Vector3.Distance(transform.position, m_Player.transform.position) < 10.0f)
        {
            CurrentState = State.BACKWARDS;
        }

        Vector3 playerdis = transform.position - m_Player.transform.position;
        Vector3 dir = Vector3.Cross(playerdis, Vector3.up);
        m_Agent.SetDestination((transform.position + dir) * (m_IdleMove * m_Speed * Time.deltaTime));
    }

    void IdleMove()
    {
        int rand = Random.Range(0, 2);

        Debug.Log(rand);

        if(rand == 0)
        {
            m_IdleMove = 1;
        } else
        {
            m_IdleMove = -1;
        }
    }
}
