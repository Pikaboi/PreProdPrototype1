using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    private Vector3 m_direction;
    private Rigidbody m_rb;

    private float lifeTimer = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Moves forward
        m_rb.MovePosition(transform.position + m_direction * Time.deltaTime * m_Speed);

        lifeTimer -= Time.deltaTime;

        //Despawns when time is up
        if(lifeTimer < 0.0f)
        {
            Destroy(gameObject);
        }
    }

    //Allows to set its direction to where we are facing
    public void SetValues(Vector3 direction, float _size)
    {
        m_direction = direction;
        transform.localScale = new Vector3(_size, _size, _size);
    }

    //Destroy the shot on wall / enemy collision
    //If an enemy deal the damage
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ah");
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
