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

    // Start is called before the first frame update
    override public void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Maxhealth = m_Health;
        m_HPBar.maxValue = m_Maxhealth;
        m_HPBar.value = m_Health;

        //renderers = GetComponentsInChildren<MeshRenderer>();
        //defaultMat = renderers[0].material;
    }

    // Update is called once per frame
    override public void Update()
    {
        ResetMaterials();
        UpdateHPBar();

        if (m_Health < m_Maxhealth)
        {
            m_Offense = true;
        }

        if (m_Offense == true)
        {
            //Better Lookat Set up
            Lookat();
            AttackCooldown();
        }

        if (m_Health < 0)
        {
            Destroy(gameObject);
        }
    }

    void AttackCooldown()
    {
        m_AttackTimer -= Time.deltaTime;

        if (m_AttackTimer < 0.0f)
        {
            //uses the same code as player projectiles
            //Use set size instead
            GameObject newFireball = Instantiate(m_enemyFireball, m_Aimer.transform.position + m_Aimer.transform.forward * 1.5f, m_Aimer.transform.rotation);
            newFireball.GetComponent<Fireball>().SetValues(m_Aimer.transform.forward, 0.25f, "EnemyProjectile", m_Attack);
            m_AttackTimer = Random.Range(1.0f, 6.0f);
        }
    }
}
