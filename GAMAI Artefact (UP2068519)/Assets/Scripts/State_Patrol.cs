using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State_Patrol : AIBaseState
{
    int counter; 

    // Start is called before the first frame update
    public override void EnterState(StateManager AI)
    {
        Debug.Log("Hello from the Patrol State");
    }

    // Update is called once per frame
    public override void UpdateState(StateManager AI, NavMeshAgent agent, GameObject[] destinations, Vector3 playerPosition, bool seePlayer)
    {
        if(seePlayer == false)
        {
            for (int i = 0; i < destinations.Length; i++)
            {
                agent.SetDestination(destinations[counter].transform.position);
                if (Vector3.Distance(agent.transform.position, destinations[counter].transform.position) <= 2.0f)
                {
                    counter++;
                    if (counter >= destinations.Length)
                    {
                        counter = 0;
                        i = 0;
                    }
                }
            }
        }
        else
        {
            AI.SwitchState(AI.attackState);
        }
    }

    public override void OnCollisionEnter(StateManager AI, Collision collision, GameObject enemy)
    {

    }
}
