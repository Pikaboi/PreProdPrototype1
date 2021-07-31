using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CooldownBar : MonoBehaviour
{
    [SerializeField] private PlayerCast m_Player;

    private TMP_Text text;
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
                float cd = Mathf.Round(m_Player.m_fbcooldown * 100.0f) / 100.0f;
                text.text = cd.ToString();
                break;
            case PlayerCast.SpellType.LobShot:
                float cd2 = Mathf.Round(m_Player.m_lscooldown * 100.0f) / 100.0f;
                text.text = cd2.ToString();
                break;
            default:
                break;
        }
    }
}
