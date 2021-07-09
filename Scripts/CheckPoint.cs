using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private int addedTime = 10;
    public AudioSource itemClip;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            itemClip.Play();
            GameManager.sharedInstance.time += addedTime;
            Destroy(gameObject, 0.1f);
        }
    }
}
