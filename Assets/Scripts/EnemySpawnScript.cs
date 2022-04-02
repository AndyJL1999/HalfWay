using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public static bool bossIn;

    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public GameObject[] mainDoors;

    private void Start()
    {
        bossIn = false;

        if(mainDoors == null)
        {
            return;
        }
    }

    private void OnEnable()
    {
          InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    private void OnDisable()
    {
        CancelInvoke("Spawn");
    }
    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length - 3);

        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        EnemyManager.enemyNum++;

        if (mainDoors[0] == null)
        {
            Instantiate(enemy, spawnPoints[3].position, spawnPoints[3].rotation);
            EnemyManager.enemyNum++;
        }
        if (mainDoors[1] == null)
        {
            Instantiate(enemy, spawnPoints[4].position, spawnPoints[4].rotation);
            EnemyManager.enemyNum++;
        }
        if (mainDoors[2] == null)
        {
            Instantiate(enemy, spawnPoints[5].position, spawnPoints[5].rotation);
            EnemyManager.enemyNum++;
        }
    }
}
