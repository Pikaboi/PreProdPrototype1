using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySentry : Enemy
{
    [SerializeField] private GameObject m_enemyFireball;
    int m_Maxhealth;

    //Attack timer
    float m_AttackTimer = 3.0f;

    // Start is called before the first frame update
    override public void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Maxhealth = m_Health;
        m_HPBar.maxValue = m_Maxhealth;
        m_HPBar.value = m_Health;

        renderers = GetComponentsInChildren<MeshRenderer>();
        defaultMat = renderers[0].material;
    }

    // Update is called once per frame
    override public void Update()
    {
        //Better Lookat Set up
        Vector3 lookat = m_Player.transform.position - transform.position;
        lookat.y = 0;
        Quaternion Rotation = Quaternion.LookRotation(lookat);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Time.deltaTime);

        //uses the same code as player projectiles
        //Use set size instead
        GameObject newFireball = Instantiate(m_enemyFireball, transform.position + transform.forward * 1.5f, transform.rotation);
        newFireball.GetComponent<Fireball>().SetValues(transform.forward, 0.25f, "EnemyProjectile", m_Attack);
    }
}
