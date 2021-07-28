using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    private Vector3 m_direction;
    private Rigidbody m_rb;

    private float lifeTimer = 10.0f;

    private bool m_charging = false;

    private float scaleSize = 0.0f;

    private GameObject m_follow;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_charging)
        {
            //Nothing yet
        }
        else
        {
            m_rb.MovePosition(transform.position + m_direction * Time.deltaTime * m_Speed);

            lifeTimer -= Time.deltaTime;

            if (lifeTimer < 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void LateUpdate()
    {
        if (m_charging)
        {

        }
    }

    public void SetDirection(Vector3 direction)
    {
        m_direction = direction;
        Debug.Log(m_direction);
    }

    public void setFollow(GameObject _follow)
    {
        m_follow = _follow;
    }
}
