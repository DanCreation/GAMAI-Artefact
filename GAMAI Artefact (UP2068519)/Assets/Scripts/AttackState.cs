using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : AIBaseState
{
    // Start is called before the first frame update
    public override void EnterState(StateManager AI)
    {
        Debug.Log("Hello from the Attack State");
    }

    //Update is called once per frame
    public override void UpdateState(StateManager AI, NavMeshAgent agent, GameObject[] destinations, Vector3 playerPosition, bool seePlayer)
    {
        agent.SetDestination(playerPosition);
    }

    public override void OnCollisionEnter(StateManager AI)
    {
        if (AI.gameObject.CompareTag("Player"))
        {
            AI.SwitchState(AI.patrolState);
            
        }
    }
}
