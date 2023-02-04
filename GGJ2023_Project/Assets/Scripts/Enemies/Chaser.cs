using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Chaser : MonoBehaviour
{
    private enum ChaserState
    {
        Patrolling,
        Chasing,
        Triggered
    }

    [SerializeField] private ChaserState startState;
    [SerializeField, ReadOnly] private ChaserState currentState;
    
    private bool isInvisible = true;

    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;

    [SerializeField] private float aggroRange;
    [SerializeField] private LayerMask checkPlayerMask;

    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask checkWallMask;

    public bool hasHeardSound;
    [NonSerialized] public Vector3 soundPosition;

    [NonSerialized] public NavMeshAgent agent;
    private Transform player;

    private Vector3 currentDir;
    private Vector3 nextPoint;

    private void Awake()
    {
        currentState = startState;
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetNextPoint();
    }

    // private void OnBecameInvisible()
    // {
    //     isInvisible = true;
    //     agent.isStopped = false;
    // }
    //
    // private void OnBecameVisible()
    // {
    //     isInvisible = false;
    //     agent.isStopped = true;
    //     agent.velocity = Vector3.zero;
    // }

    private void Patrol()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    SetNextPoint();
                }
            }
        }
    }

    private void SetNextPoint()
    {
        List<Vector3> possibleDirections = new();
        foreach (var dir in Vector3Extensions.CardinalsAroundY)
        {
            if (dir != -currentDir)
            {
                if (!Physics.Raycast(transform.position, dir, raycastDistance + .1f, checkWallMask))
                {
                    possibleDirections.Add(dir);
                    Debug.DrawRay(transform.position, dir * (raycastDistance + .1f), Color.green, 5);
                }
                else
                {
                    Debug.DrawRay(transform.position, dir * (raycastDistance + .1f), Color.red, 5);
                }
            }
        }
        
        if (possibleDirections.Count > 0)
        {
            int rng = Random.Range(0, possibleDirections.Count);
            currentDir = possibleDirections[rng];
            nextPoint = transform.position + currentDir * raycastDistance;
            agent.isStopped = false;
            agent.SetDestination(nextPoint);
        }
        else
        {
            nextPoint = transform.position - currentDir * raycastDistance;
            currentDir *= -1;
        }
        
        agent.isStopped = false;
        agent.SetDestination(nextPoint);
    }

    private void Update()
    {
        CheckPlayer();
        switch (currentState)
        {
            case ChaserState.Patrolling:
                agent.speed = patrolSpeed;
                Patrol();
                break;
            case ChaserState.Chasing:
                if (isInvisible)
                {
                    agent.speed = chaseSpeed;
                    agent.SetDestination(player.position);
                }
                break;
        }
    }

    private void CheckPlayer()
    {
        if (!isInvisible)
        {
            hasHeardSound = false;
            currentState = ChaserState.Chasing;
            return;
        }
        if (Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            if (Physics.Raycast(transform.position, player.position, aggroRange, checkPlayerMask, QueryTriggerInteraction.Ignore))
            {
                hasHeardSound = false;
                currentState = ChaserState.Chasing;
                return;
            }
        }
        if (hasHeardSound)
        {
            currentState = ChaserState.Triggered;
        }
        currentState = ChaserState.Patrolling;
    }
}
