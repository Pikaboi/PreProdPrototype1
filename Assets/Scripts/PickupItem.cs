using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    public bool canInteract = false;
    public PlayerCast m_player;

    public Image prompt;
    // Start is called before the first frame update
    virtual public void Start()
    {
        
    }

    // Update is called once per frame
    virtual public void Update()
    {
        
    }

    virtual public void Interact()
    {
        Debug.Log("Interaction");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canInteract = true;
            m_player = other.gameObject.GetComponent<PlayerCast>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canInteract = false;
            m_player = null;
        }
    }
}
