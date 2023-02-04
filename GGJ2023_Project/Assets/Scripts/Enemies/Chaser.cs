using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Chaser : MonoBehaviour
{
    public enum ChaserState
    {
        Patrolling,
        Chasing,
        Triggered
    }

    [SerializeField] private ChaserState startState;
    public ChaserState currentState;
    
    private bool isInvisible = true;

    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;

    [SerializeField] public float aggroRange;
    [SerializeField] private LayerMask checkPlayerMask;

    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask checkWallMask;

    public float maxDistanceFromPlayer;

    public bool hasHeardSound;

    [NonSerialized] public NavMeshAgent agent;
    private Transform player;

    private Vector3 currentDir;
    private Vector3 nextPoint;

    private Vector3 startPosition;

    private void Awake()
    {
        currentState = startState;
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void RestoreInitialPosition()
    {
        transform.position = startPosition;
        currentState = ChaserState.Patrolling;
        SetNextPoint();
    }

    private void Start()
    {
        startPosition = transform.position;
        SetNextPoint();
    }

    private void OnBecameInvisible()
    {
        isInvisible = true;
        agent.isStopped = false;
    }
    
    private void OnBecameVisible()
    {
        isInvisible = false;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    private void Patrol()
    {
        if (Vector3.Distance(transform.position, player.position) > maxDistanceFromPlayer)
        {
            Debug.Log("Dio can");
            agent.SetDestination(player.position);
            return;
        }
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
                else
                {
                    agent.isStopped = true;
                }
                break;
        }
        // Debug.Log(currentState);
    }

    private void CheckPlayer()
    {
        if (currentState == ChaserState.Chasing)
        {
            if (Vector3.Distance(transform.position, player.position) < aggroRange)
            {
                return;
            }
        }
        if (!isInvisible)
        {
            hasHeardSound = false;
            currentState = ChaserState.Chasing;
            return;
        }
        if (Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, aggroRange, checkPlayerMask, QueryTriggerInteraction.Ignore))
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
