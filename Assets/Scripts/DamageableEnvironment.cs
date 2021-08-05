using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEnvironment : MonoBehaviour
{
    [SerializeField] private int m_Health;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Health < 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile" || collision.gameObject.tag == "PlayerProjectile")
        {
            m_Health -= 5;
        }
    }
}
