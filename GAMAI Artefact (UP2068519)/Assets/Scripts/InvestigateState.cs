using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InvestigateState : AIBaseState
{
    public override void EnterState(StateManager AI)
    {
        Debug.Log("Hello from the Investigate State");
    }

    public override void UpdateState(StateManager AI, NavMeshAgent agent, GameObject[] destinations, Vector3 playerPosition, Vector3 soundPosition, bool seePlayer, bool heardPlayer)
    {
        agent.SetDestination(soundPosition);
        if (Vector3.Distance(agent.transform.position, soundPosition) <= 2.0f)
        {
            AI.SwitchState(AI.patrolState);
        }
    }

    public override void OnCollisionEnter(StateManager AI, Collision collision, GameObject enemy)
    {
        
    }

}
