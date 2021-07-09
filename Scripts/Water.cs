using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private float originalSpeed;
    private Player _player;
    [SerializeField] private float speedReductionRatio = 0.5f;
    void Start()
    {
        _player = FindObjectOfType<Player>();
        originalSpeed = _player.runningSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.runningSpeed *= speedReductionRatio;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.runningSpeed = originalSpeed;
        }
    }
}
