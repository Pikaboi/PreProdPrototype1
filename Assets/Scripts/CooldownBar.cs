using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CooldownBar : MonoBehaviour
{
    [SerializeField] private PlayerCast m_Player;

    private TMP_Text text;
    [SerializeField] private Slider m_fireballslider;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCast.SpellType spell =  m_Player.GetCurrentSpell();

        switch (spell)
        {
            case PlayerCast.SpellType.Fireball:

                m_fireballslider.gameObject.SetActive(true);
                m_fireballslider.value = m_Player.getFireballSize();

                float cd = Mathf.Round(m_Player.m_fbcooldown * 100.0f) / 100.0f;

                if (cd > 0)
                {
                    text.text = cd.ToString();
                    m_fireballslider.gameObject.SetActive(false);
                } else
                {
                    text.text = "";
                    m_fireballslider.gameObject.SetActive(true);
                }
                break;
            case PlayerCast.SpellType.LobShot:
                m_fireballslider.gameObject.SetActive(false);
                float cd2 = Mathf.Round(m_Player.m_lscooldown * 100.0f) / 100.0f;
                if (cd2 > 0)
                {
                    text.text = cd2.ToString();
                }
                else
                {
                    text.text = "";
                }
                break;
            case PlayerCast.SpellType.Healing:
                m_fireballslider.gameObject.SetActive(false);
                text.text = m_Player.HealCount.ToString();
                break;
            default:
                break;
        }
    }
}
