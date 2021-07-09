using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float health = 1f;
    [SerializeField] private float speedEnemy = 1f;
    [SerializeField] private float forceAtack = 1f;
    [SerializeField] private int scorePoints = 100;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioClip deathClip;
    
    
    private void Start()
    {
        player = FindObjectOfType<Player>().transform; //transform de nuestro player
        GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint"); 
        //para que los enemigos salgan de manera aleatoria de los diferentes SpawnPoints ubicados
        int randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoint.Length);
        transform.position = spawnPoint[randomSpawnPoint].transform.position;
    }
    
    /*private void Update()
    {
        direction = player.transform.position - transform.position;
        directionUnit = (Vector3)direction / direction.magnitude;
        transform.position += directionUnit * Time.deltaTime * enemySpeed;

        angle = Mathf.Atan2(directionUnit.y, directionUnit.x) * Mathf.Rad2Deg;
        
        anim.SetFloat("angle", angle);
    }*/
    private void FixedUpdate()
    {
        Vector2 direction = player.position - transform.position;
        transform.position += (Vector3) direction.normalized * (Time.deltaTime * speedEnemy);
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        anim.SetFloat("Angle", angle);
    }

    public void takeDamage()
    {
        health -= forceAtack;
        AudioSource.PlayClipAtPoint(deathClip, transform.position);
        if (health <= 0)
        {
            GameManager.sharedInstance.Score += scorePoints;
            Destroy(gameObject, 0.1f); //destrucción del enemigo y tiempo de suceso
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage();
        }
    }
}
