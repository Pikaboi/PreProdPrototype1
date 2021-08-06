using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemies;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach(Enemy e in enemies)
        {
            if(e == null)
            {
                enemies.Remove(e);
            }
        }

        if (enemies.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    public void AggroEnemies()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.m_Offense = true;
        }
    }
}
