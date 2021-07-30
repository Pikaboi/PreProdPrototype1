using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private PlayerCast m_playerCast;

    private Slider m_Bar;
    // Start is called before the first frame update
    void Start()
    {
        m_Bar = GetComponent<Slider>();

        m_Bar.maxValue = m_playerCast.m_MaxHealth;
        m_Bar.value = m_playerCast.m_Health;
    }

    // Update is called once per frame
    void Update()
    {
        m_Bar.maxValue = m_playerCast.m_MaxHealth;
        m_Bar.value = m_playerCast.m_Health;
    }
}
