using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [Range(1, 10)] [SerializeField] private float spawnRate = 1f;
    void Start()
    {
        StartCoroutine(SpawnNewEnemy());
    }
    
    void Update()
    {
        
    }

    IEnumerator SpawnNewEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / spawnRate);
            float randomEnemy = UnityEngine.Random.Range(0, enemies.Length);
            if (randomEnemy < GameManager.sharedInstance.difficulty * 0.1f)
            {
                Instantiate(enemies[1]);
            }
            else
            {
                Instantiate(enemies[0]);
            }
        }
    }
}
