using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIBaseState
{
    public NavMeshAgent agent;
    public GameObject[] destinations;

    public Vector3 startPosition;
    public int counter;

    public void Start()
    {
        agent = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Variables>().agent;
        destinations = GameObject.FindGameObjectWithTag("Destinations").GetComponent<Variables>().destinations;
    }

    public abstract void EnterState(StateManager AI);
    public abstract void UpdateState(StateManager AI);
}

