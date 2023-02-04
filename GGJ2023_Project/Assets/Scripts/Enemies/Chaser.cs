using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    [ReadOnly] public bool canMove;

    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void OnBecameInvisible()
    {
        canMove = true;
    }

    private void OnBecameVisible()
    {
        canMove = false;
    }

    private void Update()
    {
        if (canMove)
        {
            
        }
    }
}
