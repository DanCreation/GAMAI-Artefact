using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public AIBaseState currentState;
    public PatrolState patrol = new PatrolState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = patrol;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(AIBaseState AI)
    {
        currentState = AI;
        AI.EnterState(this);
    }
}
