using UnityEngine;

public class PatrolState : AIBaseState
{
    public AIScript AIScript;
    public override void EnterState(StateManager AI)
    {
        Debug.Log("Patrol State");
    }

    public override void UpdateState(StateManager AI)
    {

        for (int i = 0; i < AIScript.destinations.Length; i++)
        {
            AIScript.agent.SetDestination(AIScript.destinations[AIScript.counter].transform.position);
            if (Vector3.Distance(AIScript.agent.transform.position, AIScript.destinations[AIScript.counter].transform.position) <= 2.0f)
            {
                AIScript.counter++;
            }
        }
    }
}
