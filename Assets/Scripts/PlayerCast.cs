using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCast : MonoBehaviour
{
    [SerializeField] private GameObject Fireball;
    [SerializeField] private GameObject LobShot;
    [SerializeField] private GameObject Camera;

    [SerializeField] private TMPro.TMP_Text text;

    private enum SpellType
    {
        Fireball,
        LobShot,
        Healing
    }

    private int SpellLength = 2;

    [SerializeField] SpellType currentSpell = SpellType.Fireball;

    // Start is called before the first frame update
    void Start()
    {
        text.text = currentSpell.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpellCycle();
        }

        if (Input.GetMouseButtonDown(1))
        {
            
        }

        if (Input.GetMouseButton(1))
        {
            
        }

        if (Input.GetMouseButtonUp(1))
        {
            SpellActivate();
        }
    }

    void SpellInstantiate()
    {
        switch (currentSpell)
        {
            case SpellType.Fireball:

                GameObject newFireball = Instantiate(Fireball, transform.position, transform.rotation);
                newFireball.GetComponent<Fireball>().SetDirection(Camera.transform.forward);

                break;
            case SpellType.LobShot:

                GameObject newLobShot = Instantiate(LobShot, transform.position, transform.rotation);

                break;
            case SpellType.Healing:
                
                break;
            default:
                //Prevents anything bad happening
                break;
        }
    }

    void SpellCharge()
    {

    }

    void SpellActivate()
    {
        //Means we have all spells on one button
        switch (currentSpell)
        {
            case SpellType.Fireball:

                GameObject newFireball = Instantiate(Fireball, transform.position, transform.rotation);
                newFireball.GetComponent<Fireball>().SetDirection(Camera.transform.forward);

                break;
            case SpellType.LobShot:

                GameObject newLobShot = Instantiate(LobShot, transform.position, transform.rotation);

                break;
            case SpellType.Healing:

                break;
            default:
                //Prevents anything bad happening
                break;
        }
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
