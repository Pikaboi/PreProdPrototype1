using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSwap : MonoBehaviour
{
    public Image m_Fireball;
    public Image m_Lobshot;
    public Image m_heal;

    private void Start()
    {
        m_Fireball.enabled = true;
        m_Lobshot.enabled = false;
        m_heal.enabled = false;
    }

    public void showFB()
    {
        m_Fireball.enabled = true;
        m_Lobshot.enabled = false;
        m_heal.enabled = false;
    }

    public void showLS()
    {
        m_Fireball.enabled = false;
        m_Lobshot.enabled = true;
        m_heal.enabled = false;
    }

    public void showHeal()
    {
        m_Fireball.enabled = false;
        m_Lobshot.enabled = false;
        m_heal.enabled = true;
    }
}
