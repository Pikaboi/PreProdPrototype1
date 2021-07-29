using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobShot : MonoBehaviour
{

    [SerializeField] private float m_Speed;
    [SerializeField] private float m_Height;

    private bool m_released = false;
    private Rigidbody m_rb;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //I need this to only activate once
        //After the speed has been set
        if (m_released)
        {
            m_rb.AddForce(new Vector3(0.0f, m_Height, 0.0f), ForceMode.Impulse);
            m_rb.AddForce(transform.forward * m_Speed, ForceMode.Impulse);
            m_released = false;
        }
    }

    public void setSpeed(float _speed)
    {
        m_Speed = _speed;
        m_released = true;
    }

    //Destroy the shot on wall / enemy collision
    //This will start the explosion before it is destroyed
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
