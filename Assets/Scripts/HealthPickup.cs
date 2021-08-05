using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupItem
{
    [SerializeField] private int m_healCount;
    // Start is called before the first frame update
    override public void Start()
    {
        
    }

    // Update is called once per frame
    override public void Update()
    {
        if (canInteract == true)
        {
            prompt.gameObject.SetActive(true);
        } else
        {
            prompt.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Interact();
        }
    }

    public override void Interact()
    {
        if(m_player != null)
        {
            m_player.HealCount += m_healCount;
            Destroy(gameObject);
        }
    }

    public void SetHealthCount(int _count)
    {
        m_healCount = _count;
    }

}
