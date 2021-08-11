using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySentry : Enemy
{
    [SerializeField] private GameObject m_enemyFireball;
    int m_Maxhealth;

    //Attack timer
    [SerializeField] private float m_AttackTimer = 3.0f;
    [SerializeField] private Animator m_anim;

    // Start is called before the first frame update
    override public void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Maxhealth = m_Health;
        m_HPBar.maxValue = m_Maxhealth;
        m_HPBar.value = m_Health;
        m_anim.SetBool("Idling", !m_Offense);
        //renderers = GetComponentsInChildren<MeshRenderer>();
        //defaultMat = renderers[0].material;
    }

    // Update is called once per frame
    override public void Update()
    {
        m_anim.SetBool("Idling", !m_Offense);

        ResetMaterials();
        UpdateHPBar();

        if (m_Health < m_Maxhealth)
        {
            m_Offense = true;
        }


        if (m_Health < 0)
        {
            m_anim.SetBool("Die", true);
        }
        else
        {
            if (m_Offense == true)
            {
                //Better Lookat Set up
                Lookat();
                AttackCooldown();
            }
        }

        if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            if (m_anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                Destroy(gameObject);
            }
        }

    }

    void AttackCooldown()
    {
        m_AttackTimer -= Time.deltaTime;

        if (m_AttackTimer < 0.0f)
        {
            //uses the same code as player projectiles
            //Use set size instead
            m_anim.SetTrigger("Cast");
            GameObject newFireball = Instantiate(m_enemyFireball, m_Aimer.transform.position + m_Aimer.transform.forward * 1.5f, m_Aimer.transform.rotation);
            newFireball.GetComponent<Fireball>().SetValues(m_Aimer.transform.forward, 0.25f, "EnemyProjectile", m_Attack);
            m_AttackTimer = Random.Range(6.0f, 9.0f);
        }
    }
}
