using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    [SerializeField] private GameObject Fireball;
    [SerializeField] private GameObject Camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject newFireball = Instantiate(Fireball, transform.position, transform.rotation);
            newFireball.GetComponent<Fireball>().SetDirection(new Vector3(transform.right.x, Camera.transform.forward.z, transform.forward.z));
        }
    }
}
