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

    public GameObject m_Aimer;

    public NavMeshAgent m_Agent;
    public GameObject m_Player;

    public MeshRenderer[] renderers;
    public Material damage;
    public Material defaultMat;
    public float resetMatTimer = 0.0f;

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
        foreach(MeshRenderer r in renderers)
        {
            r.material = damage;
        }
        resetMatTimer = 0.5f;
    }

    public void ResetMaterials()
    {
        resetMatTimer -= Time.deltaTime;
        
        if(resetMatTimer < 0)
        {
            foreach (MeshRenderer r in renderers)
            {
                r.material = defaultMat;
            }
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
