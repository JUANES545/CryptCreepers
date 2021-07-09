using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] int health = 3;
    public bool powerShot;

    private void Start()
    {
        Destroy(gameObject, 5);
    }

    private void FixedUpdate()
    {
        transform.position += transform.right * (Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().takeDamage();
            if (!powerShot)
            {
                Destroy(gameObject);
            }
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }



    }
}
