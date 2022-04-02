using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static bool roundSet;
    public static GameObject[] enemies;
    public static GameObject[] bosses;
    public static int enemyNum;

    public EnemySpawnScript spawner;
    public BossSpawnScript bossSpawner;

    void Start()
    {
        spawner.enabled = false;
        bossSpawner.enabled = false;
        roundSet = false;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        bosses = GameObject.FindGameObjectsWithTag("Boss");
    }

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        bosses = GameObject.FindGameObjectsWithTag("Boss");

        Debug.Log(enemyNum);

        switch (UI_Controller.pagesCollected)
        {
            case 1:
            case 2:
                {
                    if (enemyNum < 10 && roundSet == false)
                        spawner.enabled = true;
                    else
                    {
                        spawner.enabled = false;
                        enemyNum = 0;
                        roundSet = true;
                    }
                    break;
                }
            case 3:
            case 4:
            case 5:
                {
                    if (enemyNum < 15 && roundSet == false)
                    {
                        spawner.enabled = true;
                    }
                    else
                    {
                        spawner.enabled = false;
                        enemyNum = 0;
                        roundSet = true;
                    }
                    break;
                }
            case 6:
                {
                    if (enemyNum < 20 && roundSet == false)
                    {
                        spawner.enabled = true;
                        bossSpawner.enabled = true;
                    }
                    else
                    {
                        spawner.enabled = false;
                        bossSpawner.enabled = false;
                        enemyNum = 0;
                        roundSet = true;
                    }
                    break;
                }
        }
    }
}
