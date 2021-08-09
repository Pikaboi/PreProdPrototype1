using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageEnemy : Enemy
{
    [SerializeField] private GameObject m_enemyFireball;
    [SerializeField] private Transform m_patrol;
    private Vector3 m_ogPos;

    //the states the enemy can be in
    public enum State
    {
        IDLE,
        DEFENSE,
        ATTACK,
        FINISHER,
        BACKWARDS,
        DOCILE
    }

    //The State the enemy is currently in
    [SerializeField] State CurrentState;

    //idle movement modifiers
    int m_IdleMove = 1;
    float m_idleTimer = 4.0f;

    //Defense timer
    float m_DefendTimer = 5.0f;

    //Attack timer
    [SerializeField] private float m_AttackTimer = 3.0f;

    //Finisher Charge Up
    float m_ChargeTimer = 4.0f;
    int m_Maxhealth;
    bool m_finisherReady = true;

    private Vector3 currdest;

    // Start is called before the first frame update
    override public void Start()
    {
        m_ogPos = transform.position;
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Agent = GetComponent<NavMeshAgent>();
        m_Maxhealth = m_Health;
        m_HPBar.maxValue = m_Maxhealth;
        m_HPBar.value = m_Health;

        renderers = GetComponentsInChildren<MeshRenderer>();
        defaultMat = renderers[0].material;

        CurrentState = State.DOCILE;
        m_Agent.destination = m_patrol.position;
        currdest = m_patrol.position;
    }

    // Update is called once per frame
    override public void Update()
    {
        ResetMaterials();
        UpdateHPBar();
        //Look at the player
        //May change to a vision mechanic?
        if (CurrentState != State.DOCILE)
        {
            Lookat();
        } else
        {
            DocileLook();
        }

        //Decrease Attack Timer
        //want it seperate too idling, and moving back
        //Otherwise AI will be giga stupid
        if (CurrentState != State.FINISHER && CurrentState != State.DOCILE)
        {
            AttackCooldown();
        }

        if(m_Health <= m_Maxhealth / 2 && m_finisherReady)
        {
            CurrentState = State.FINISHER;
        }

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
            case State.DOCILE:
                Docile();
                break;
            default:
                //If something odd happens, default to Idle
                Idle();
                break;
        }

        if(m_Health < 0)
        {
            Instantiate(healthDrop, transform.position, transform.rotation);
            healthDrop.GetComponentInChildren<HealthPickup>().SetHealthCount(1);
            Destroy(gameObject);
        }
    }

    void DocileLook()
    {
        Vector3 lookat = m_patrol.transform.position - transform.position;
        lookat.y = 0;
        Quaternion Rotation = Quaternion.LookRotation(lookat);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Time.deltaTime);

        m_Aimer.transform.LookAt(m_patrol.transform.position);
    }

    void Docile()
    {
        if(m_Agent.remainingDistance == 0)
        {
            if(currdest == m_patrol.position)
            {
                currdest = m_ogPos;
                m_Agent.destination = m_ogPos;
            } else if(currdest == m_ogPos)
            {
                currdest = m_patrol.position;
                m_Agent.destination = m_patrol.position;
            }
        }
       

        if(m_Health < m_Maxhealth)
        {
            m_Offense = true;

            //Aggro his friends too
            Collider[] hits = Physics.OverlapSphere(transform.position, 10.0f);

            foreach(Collider hit in hits)
            {
                if(hit.GetComponent<Enemy>() != null)
                {
                    hit.GetComponent<Enemy>().m_Offense = true;
                }
            }
        }

        if (m_Offense == true)
        {
            CurrentState = State.IDLE;
            m_Agent.ResetPath();
        }
    }

    //Move the enemy back to keep distance from player
    void MoveBack()
    {
        if (Vector3.Distance(transform.position, m_Player.transform.position) < 8.0f){
            m_Agent.Move(transform.forward * -1 * m_Speed * Time.deltaTime);
        } else if (Vector3.Distance(transform.position, m_Player.transform.position) > 13.0f)
        {
            m_Agent.Move(transform.forward * m_Speed * Time.deltaTime);
        }

        //return to idle once its at a safe distance
        if(Vector3.Distance(transform.position, m_Player.transform.position) > 6.0f && Vector3.Distance(transform.position, m_Player.transform.position) < 10.0f)
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
        GameObject newFireball = Instantiate(m_enemyFireball, transform.position + m_Aimer.transform.forward * 1.5f, transform.rotation);
        newFireball.GetComponent<Fireball>().SetValues(m_Aimer.transform.forward, 0.25f, "EnemyProjectile", m_Attack);

        CurrentState = State.IDLE;
    }

    //Strong attack used at low health
    void LargeAttack()
    {
        m_ChargeTimer -= Time.deltaTime;

        if(m_ChargeTimer < 0.0f)
        {
            GameObject newFireball = Instantiate(m_enemyFireball, transform.position + transform.forward * 2.5f, transform.rotation);
            newFireball.GetComponent<Fireball>().SetValues(m_Aimer.transform.forward, 0.5f, "EnemyProjectile", m_Attack * 3);
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
        if (Vector3.Distance(transform.position, m_Player.transform.position) < 8.0f || Vector3.Distance(transform.position, m_Player.transform.position) > 13.0f)
        {
            CurrentState = State.BACKWARDS;
        }

        //Move Player
        m_Agent.Move(transform.right * m_Agent.speed * m_IdleMove * Time.deltaTime);

        //Change up movement when timer ends
        m_idleTimer -= Time.deltaTime;

        if(m_idleTimer < 0)
        {
            IdleMove();
            m_idleTimer = 4.0f;
            CurrentState = State.DEFENSE;
        }

    }

    void IdleMove()
    {
        int rand = Random.Range(0, 2);

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
