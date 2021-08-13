using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Goal : MonoBehaviour
{
    bool inZone = false;
    [SerializeField] private Image prompt;
    private void Update()
    {
        if (inZone)
        {
            prompt.enabled = true;
        } else
        {
            prompt.enabled = false;
        }

        //Because the prompt isnt working for some reason
        if (inZone)
        {
            SceneManager.LoadScene("EndScreen");
        }
    }

    //Pretty simple lmao
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            inZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inZone = false;
        }
    }
}
