using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] List<Enemy> enemies;
    List<Enemy> enemycopies;

    [SerializeField] bool m_Activated = false;
    // Start is called before the first frame update
    void Start()
    {
        Enemy[] collection = GameObject.FindObjectsOfType<Enemy>();
        foreach(Enemy col in collection)
        {
            enemies.Add(col);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (m_Activated == false)
            {
                SaveUpdates();
            }
        }
    }

    void SaveUpdates()
    {
        foreach (Enemy e in enemies)
        {
            if(e == null)
            {
                enemies.Remove(e);
            }
        }

        m_Activated = true;
    }

    public void ResetWorld()
    {
        foreach(Enemy e in enemies)
        {

        }
    }
}
