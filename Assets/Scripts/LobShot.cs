using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobShot : MonoBehaviour
{

    [SerializeField] private float m_Speed;
    [SerializeField] private float m_Height;
    [SerializeField] private float m_Radius;

    private bool m_released = false;
    private Rigidbody m_rb;

    private int m_Might = 0;

    [SerializeField] private AudioSource m_LobRelease;
    [SerializeField] private AudioSource m_LobExplode;
    private bool hashit = false;

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

            m_LobRelease.Play();
        }

        if(hashit && !m_LobExplode.isPlaying && !m_LobRelease.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    public void setValues(float _speed, string _tag, int _Might)
    {
        gameObject.tag = _tag;
        m_Might = _Might;
        m_Speed = _speed;
        m_released = true;
    }

    //Destroy the shot on wall / enemy collision
    //This will start the explosion before it is destroyed
    private void OnCollisionEnter(Collision collision)
    {
        //Check Radius of explosion
        Collider[] hits = Physics.OverlapSphere(transform.position, m_Radius);

        m_LobExplode.Play();

        if (gameObject.tag == "EnemyProjectile")
        {
            //hurt player if in range

            foreach(Collider hit in hits)
            {
                if(hit.gameObject.tag == "Player")
                {
                    hit.gameObject.GetComponent<PlayerCast>().TakeDamage(m_Might);
                }
            }
        }

        if (gameObject.tag == "PlayerProjectile")
        {
            //hurt enemy
            foreach (Collider hit in hits)
            {
                if (hit.gameObject.tag == "Enemy")
                {
                    hit.gameObject.GetComponent<Enemy>().TakeDamage(m_Might);
                }
            }
        }

        RemovePhysics();
        hashit = true;
    }

    private void RemovePhysics()
    {
        m_rb.detectCollisions = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
