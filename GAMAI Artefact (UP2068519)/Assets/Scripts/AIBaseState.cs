using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIBaseState
{
    public abstract void EnterState(StateManager AI);

    public abstract void UpdateState(StateManager AI, NavMeshAgent agent, GameObject[] destinations, Vector3 playerPosition, Vector3 soundPosition, bool seePlayer, bool heardPlayer);

    public abstract void OnCollisionEnter(StateManager AI, Collision collision, GameObject enemy);
}

