using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCam : MonoBehaviour
{
    [SerializeField] private GameObject m_follow;
    [SerializeField] private float m_sensitivity;
    private Vector3 m_offset;
    private float m_rotationX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = m_follow.transform.position + m_offset;
    }

    void Update()
    {
        //Camera Rotates up and down
        float vert = Input.GetAxisRaw("Mouse Y") * m_sensitivity * Time.deltaTime;

        m_rotationX -= vert;

        m_rotationX = Mathf.Clamp(m_rotationX, -25.0f, 25.0f);

        transform.localRotation = Quaternion.Euler(m_rotationX, 0.0f, 0.0f);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Pretty simple since we are going very combat heavy
        gameObject.transform.position = m_follow.transform.position + m_offset;
    }
}
