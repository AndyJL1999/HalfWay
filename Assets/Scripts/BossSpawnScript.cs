using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnScript : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

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

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        EnemyManager.enemyNum++;
    }
}
