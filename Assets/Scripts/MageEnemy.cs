using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageEnemy : Enemy
{
    [SerializeField] private GameObject m_enemyFireball;

    //the states the enemy can be in
    public enum State
    {
        IDLE,
        DEFENSE,
        ATTACK,
        FINISHER,
        BACKWARDS
    }

    //The State the enemy is currently in
    [SerializeField] State CurrentState;

    //idle movement modifiers
    int m_IdleMove = 1;
    float m_idleTimer = 4.0f;

    //Defense timer
    float m_DefendTimer = 5.0f;

    //Attack timer
    float m_AttackTimer = 3.0f;
    int m_AttackCount = 0;

    //Finisher Charge Up
    float m_ChargeTimer = 4.0f;
    int m_Maxhealth;
    bool m_finisherReady = true;
    

    // Start is called before the first frame update
    override public void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Agent = GetComponent<NavMeshAgent>();
        m_Maxhealth = m_Health;
    }

    // Update is called once per frame
    override public void Update()
    {
        //Look at the player
        //May change to a vision mechanic?
        transform.LookAt(m_Player.transform.position);

        //Decrease Attack Timer
        //want it seperate too idling, and moving back
        //Otherwise AI will be giga stupid
        if (CurrentState != State.DEFENSE && CurrentState != State.FINISHER)
        {
            AttackCooldown();
        }

        if(m_Health <= m_Maxhealth / 2 && m_finisherReady)
        {
            CurrentState = State.FINISHER;
        }

        Debug.Log(CurrentState);

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

        if(m_Health < 0)
        {
            Destroy(gameObject);
        }
    }

    //Move the enemy back to keep distance from player
    void MoveBack()
    {
        m_Agent.Move(transform.forward * -1 * m_Speed * Time.deltaTime);

        //return to idle once its at a safe distance
        if(Vector3.Distance(transform.position, m_Player.transform.position) > 11.0f)
        {
            CurrentState = State.IDLE;
            IdleMove();
        }
    }

    //Fire a Projectile at a player
    void Attack()
    {
        //uses the same code as player projectiles
        //Use set size instead
        GameObject newFireball = Instantiate(m_enemyFireball, transform.position + transform.forward * 1.5f, transform.rotation);
        newFireball.GetComponent<Fireball>().SetValues(transform.forward, 0.25f, "EnemyProjectile", m_Attack);

        m_AttackCount++;

        if (m_AttackCount < 5)
        {
            CurrentState = State.IDLE;
        } else
        {
            CurrentState = State.DEFENSE;
            m_AttackCount = 0;
        }
    }

    //Strong attack used at low health
    void LargeAttack()
    {
        m_ChargeTimer -= Time.deltaTime;

        if(m_ChargeTimer < 0.0f)
        {
            GameObject newFireball = Instantiate(m_enemyFireball, transform.position + transform.forward * 2.5f, transform.rotation);
            newFireball.GetComponent<Fireball>().SetValues(transform.forward, 1.8f, "EnemyProjectile", m_Attack * 3);
            m_finisherReady = false;

            CurrentState = State.IDLE;
        }
    }

    //Defensive Movement, nullify attacks
    void Defend()
    {
        m_DefendTimer -= Time.deltaTime;

        if(m_DefendTimer < 0.0f)
        {
            m_DefendTimer = 5.0f;
            CurrentState = State.IDLE;
        }
    }


    //Idling, strafing around player
    void Idle()
    {
        //Check if player is too close
        if (Vector3.Distance(transform.position, m_Player.transform.position) < 10.0f)
        {
            CurrentState = State.BACKWARDS;
        }

        //Move Player
        Vector3 playerdis = transform.position - m_Player.transform.position;
        Vector3 dir = Vector3.Cross(playerdis, Vector3.up);
        m_Agent.SetDestination((transform.position + dir) * (m_IdleMove));

        //Change up movement when timer ends
        m_idleTimer -= Time.deltaTime;

        if(m_idleTimer < 0)
        {
            IdleMove();
            m_idleTimer = 4.0f;
        }

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

    void AttackCooldown()
    {
        m_AttackTimer -= Time.deltaTime;

        if(m_AttackTimer < 0.0f)
        {
            CurrentState = State.ATTACK;
            m_AttackTimer = Random.Range(1.0f, 6.0f);
        }
    }
}
