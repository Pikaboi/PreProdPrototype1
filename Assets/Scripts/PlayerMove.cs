using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Usual Stuff
    [SerializeField] private Rigidbody m_rb;
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_Sensitivity;

    private float m_RotationY;

    private Vector3 m_direction;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Schmoving
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Mouse X") * m_Sensitivity * Time.deltaTime;

        m_RotationY -= horizontal;

        transform.localRotation = Quaternion.Euler(new Vector3(0.0f, m_RotationY, 0.0f));

        //To connect it too the velocity
        Vector3 m_movement = transform.right * x + transform.forward * z;

        //So we keep gravity
        m_rb.velocity = new Vector3(m_movement.normalized.x * m_Speed, m_rb.velocity.y, m_movement.normalized.z * m_Speed);
    }
}
