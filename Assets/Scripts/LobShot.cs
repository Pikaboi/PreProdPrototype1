using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobShot : MonoBehaviour
{

    [SerializeField] private float m_Speed;
    [SerializeField] private float m_Height;
    private Rigidbody m_rb;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();

        m_rb.AddForce(new Vector3(0.0f, m_Height, 0.0f), ForceMode.Impulse);
        m_rb.AddForce(transform.forward * m_Speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    /*void FixedUpdate()
    {
        
    }*/

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
