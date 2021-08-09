using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCast : MonoBehaviour
{
    //References to projectile prefabs
    [SerializeField] private GameObject Fireball;
    [SerializeField] private GameObject LobShot;
    [SerializeField] private GameObject Camera;
    [SerializeField] private GameObject Wall;

    //Debug
    [SerializeField] private IconSwap m_icons;

    [SerializeField] private GameObject LeftArm;
    [SerializeField] private GameObject RightArm;

    [SerializeField] private AudioSource m_AudioHeal;
    [SerializeField] private AudioSource m_AudioDamage;

    [SerializeField] private Animator m_anim;

    //Spell Enum
    public enum SpellType
    {
        Fireball,
        LobShot,
        Healing
    }
    private int SpellLength = 2;
    [SerializeField] SpellType currentSpell = SpellType.Fireball;
    public int HealCount = 3;


    //Reference to the Wall that has been spawned
    public GameObject CurrentWall;

    //Charge Related Values
    private float m_FireballSize = 0.25f;
    private float m_lobSpeed = 4.0f;

    //Player Stats
    public int m_Health;
    public int m_MaxHealth;
    public int m_Attack;
    public int m_Defense;

    //Spell Cooldowns
    public float m_fbcooldown = 0;
    public float m_lscooldown = 0;

    [SerializeField] private float m_fbMaxCooldown = 0;
    [SerializeField] private float m_lsMaxCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        //text.text = currentSpell.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        m_fbcooldown -= Time.deltaTime;
        m_lscooldown -= Time.deltaTime;

        //Show button presses
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpellCycle();
        }

        if (Input.GetMouseButton(1))
        {
            SpellCharge();
        }

        if (Input.GetMouseButtonUp(1))
        {
            SpellActivate();
        }

        if (Input.GetMouseButton(0))
        {
            //You wont pull it up until a wall has been removed
            if(CurrentWall == null)
            {
                m_anim.SetTrigger("LARM0");
                CurrentWall = Instantiate(Wall, transform.position + transform.forward * 5, transform.rotation.normalized);
            }
        }
    }

    void SpellCharge()
    {
        if (m_fbcooldown < 0)
        {
            m_FireballSize += Time.deltaTime * 2.0f;

            m_FireballSize = Mathf.Min(m_FireballSize, 1.5f);

        }

        if (m_lscooldown < 0)
        {
            m_lobSpeed += Time.deltaTime * 5.0f;

            m_lobSpeed = Mathf.Min(m_lobSpeed, 10.0f);
        }
    }

    void SpellActivate()
    {
        //Means we have all spells on one button
        switch (currentSpell)
        {
            case SpellType.Fireball:

                if (m_fbcooldown < 0)
                {
                    m_anim.SetTrigger("RARM0");
                    GameObject newFireball = Instantiate(Fireball, RightArm.transform.position + transform.forward * m_FireballSize, transform.rotation);
                    newFireball.GetComponent<Fireball>().SetValues(Camera.transform.forward, m_FireballSize, "PlayerProjectile", Mathf.RoundToInt(m_Attack * (1 + m_FireballSize)));

                    m_fbcooldown = m_fbMaxCooldown;
                }

                break;
            case SpellType.LobShot:

                if (m_lscooldown < 0)
                {
                    m_anim.SetTrigger("RARM0");
                    GameObject newLobShot = Instantiate(LobShot, RightArm.transform.position, transform.rotation);
                    newLobShot.GetComponent<LobShot>().setValues(m_lobSpeed, "PlayerProjectile", m_Attack * 3);

                    m_lscooldown = m_lsMaxCooldown;
                }
                break;
            case SpellType.Healing:
                if (HealCount > 0 && m_Health != m_MaxHealth)
                {
                    m_anim.SetTrigger("RARM0");
                    HealCount--;
                    m_Health += 20;
                    m_Health = Mathf.Min(m_MaxHealth, m_Health);
                    m_AudioHeal.Play();
                }
                break;
            default:
                //Prevents anything bad happening
                break;
        }

        m_FireballSize = 0.25f;
        m_lobSpeed = 4.0f;
    }

    void SpellCycle()
    {
        currentSpell += 1;

        if ((int)currentSpell > SpellLength)
        {
            currentSpell = 0;
        }

        switch (currentSpell)
        {
            case SpellType.Fireball:
                m_icons.showFB();
                break;
            case SpellType.LobShot:
                m_icons.showLS();
                break;
            case SpellType.Healing:
                m_icons.showHeal();
                break;
            default:
                m_icons.showFB();
                break;
        }
    }

    public void TakeDamage(int _Might)
    {
        m_AudioDamage.Play();
        m_Health -= Mathf.Max(_Might - m_Defense, 0);
    }

    public SpellType GetCurrentSpell()
    {
        return currentSpell;
    }

    public GameObject GetTrajectoryInfo()
    {
        return RightArm;
    }

    public float getLobSpeed()
    {
        return m_lobSpeed;
    }

    public float getFireballSize()
    {
        return m_FireballSize;
    }
}
