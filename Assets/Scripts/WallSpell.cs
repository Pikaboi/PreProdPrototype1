﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpell : MonoBehaviour
{
    [SerializeField] private float Timer = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;

        if(Timer < 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyProjectile" || collision.gameObject.tag == "PlayerProjectile")
        {
            Timer -= 5.0f;
        }
    }

    //Add later code that weakens the barrier when it is hit
}
