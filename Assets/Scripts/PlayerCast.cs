using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCast : MonoBehaviour
{
    //References to projectile prefabs
    [SerializeField] private GameObject Fireball;
    [SerializeField] private GameObject LobShot;
    [SerializeField] private GameObject Camera;
    [SerializeField] private GameObject Wall;

    //Debug
    [SerializeField] private TMPro.TMP_Text text;

    //Spell Enum
    private enum SpellType
    {
        Fireball,
        LobShot,
        Healing
    }
    private int SpellLength = 2;
    [SerializeField] SpellType currentSpell = SpellType.Fireball;

    //Reference to the Wall that has been spawned
    private GameObject CurrentWall;

    //Charge Related Values
    private float m_FireballSize = 0.25f;
    private float m_lobSpeed = 4.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        text.text = currentSpell.ToString();
    }

    // Update is called once per frame
    void Update()
    {
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
                CurrentWall = Instantiate(Wall, transform.position + transform.forward * 5, transform.rotation.normalized);
            }
        }
    }

    void SpellCharge()
    {
        m_FireballSize += Time.deltaTime * 0.1f;

        m_FireballSize = Mathf.Min(m_FireballSize, 1.5f);

        m_lobSpeed += Time.deltaTime;

        m_lobSpeed = Mathf.Min(m_lobSpeed, 10.0f);
    }

    void SpellActivate()
    {
        //Means we have all spells on one button
        switch (currentSpell)
        {
            case SpellType.Fireball:

                GameObject newFireball = Instantiate(Fireball, transform.position + transform.forward * 1.5f, transform.rotation);
                newFireball.GetComponent<Fireball>().SetValues(Camera.transform.forward, m_FireballSize);

                break;
            case SpellType.LobShot:

                GameObject newLobShot = Instantiate(LobShot, transform.position + transform.forward * 1.5f, transform.rotation);
                //newLobShot.GetComponent<LobShot>().Throw(m_lobSpeed);

                break;
            case SpellType.Healing:

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

        text.text = currentSpell.ToString();
    }
}
