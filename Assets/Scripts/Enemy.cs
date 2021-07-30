﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int m_Health;
    public int m_Damage;
    public float m_Speed;

    public NavMeshAgent m_Agent;
    public GameObject m_Player;

    // Start is called before the first frame update
    virtual public void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    virtual public void Update()
    {
        
    }
}