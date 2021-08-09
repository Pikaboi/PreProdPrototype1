﻿using System.Collections;
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

    public GameObject m_Aimer;

    public NavMeshAgent m_Agent;
    public GameObject m_Player;

    public Material damage;
    public Material defaultMat;
    public float resetMatTimer = 0.0f;

    public bool m_Offense = false;

    public Slider m_HPBar;

    public GameObject healthDrop;

    public AudioSource m_AudioDamage;

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
        m_AudioDamage.Play();
        m_Health -= Mathf.Max(_might - m_Defense, 0);
        resetMatTimer = 0.5f;
    }

    public void ResetMaterials()
    {
        resetMatTimer -= Time.deltaTime;
        
        if(resetMatTimer < 0)
        {
        }
    }

    public void UpdateHPBar()
    {
        m_HPBar.value = m_Health;
    }

    public void Lookat()
    {
        Vector3 lookat = m_Player.transform.position - transform.position;
        lookat.y = 0;
        Quaternion Rotation = Quaternion.LookRotation(lookat);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Time.deltaTime);

        m_Aimer.transform.LookAt(m_Player.transform.position);
    }
}
