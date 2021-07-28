using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    private Vector3 m_direction;
    private Rigidbody m_rb;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_rb.MovePosition(transform.position + m_direction * Time.deltaTime * m_Speed);
    }

    public void SetDirection(Vector3 direction)
    {
        m_direction = direction;
        Debug.Log(m_direction);
    }
}
