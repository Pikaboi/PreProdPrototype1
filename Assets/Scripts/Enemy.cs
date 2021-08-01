using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int m_Health;
    public int m_Attack;
    public int m_Defense;
    public float m_Speed;

    public NavMeshAgent m_Agent;
    public GameObject m_Player;

    public Slider m_HPBar;

    // Start is called before the first frame update
    virtual public void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    virtual public void Update()
    {
        
    }

    virtual public void TakeDamage(int _might)
    {
        m_Health -= Mathf.Max(_might - m_Defense, 0);
    }

    public void UpdateHPBar()
    {
        m_HPBar.value = m_Health;
    }
}
