using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    [SerializeField] private GameObject Fireball;
    [SerializeField] private GameObject LobShot;
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

            newFireball.GetComponent<Fireball>().SetDirection(Camera.transform.forward);
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject newLobShot = Instantiate(LobShot, transform.position, transform.rotation);
        }

        //Debug.DrawLine(Camera.transform.position, Camera.transform.position + Camera.transform.forward * 10, Color.blue);
    }
}
