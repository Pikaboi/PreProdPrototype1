using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] barriers;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach(GameObject barrier in barriers)
            {
                barrier.SetActive(true);
                barrier.GetComponent<EnemyManager>().AggroEnemies();
            }
        }

        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
