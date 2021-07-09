using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject checkpointPrefab;
    [SerializeField] private int chekpointSpawnDelay = 6;
    [SerializeField] private float spawnRadius = 10;

    [SerializeField] GameObject[] powerUpPrefab;
    [SerializeField] private int powerUpSpawnDelay = 10;
    void Start()
    {
        StartCoroutine(SpawnCheckpointRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }*/


    IEnumerator SpawnCheckpointRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(chekpointSpawnDelay);
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
            Instantiate(checkpointPrefab, randomPosition, quaternion.identity);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpSpawnDelay);
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
            int randomNum = Random.Range(0, powerUpPrefab.Length);
            Instantiate(powerUpPrefab[randomNum], randomPosition, Quaternion.identity);
        }
    }
}
