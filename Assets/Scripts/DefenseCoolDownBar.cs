using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DefenseCoolDownBar : MonoBehaviour
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
        if (m_Player.CurrentWall != null) {
            int cd = (int)m_Player.CurrentWall.GetComponent<WallSpell>().GetTimer() + 1;
            text.text = cd.ToString();
        } else
        {
            text.text = "";
        }
    }
}
