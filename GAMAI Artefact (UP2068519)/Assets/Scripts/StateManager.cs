using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    AIBaseState currentState;
    public State_Patrol patrolState = new State_Patrol();
    public AttackState attackState = new AttackState();

    GameObject enemy;
    
    public NavMeshAgent agent;
    public GameObject[] destinations;
    Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        currentState = patrolState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        currentState.UpdateState(this, agent, destinations, playerPosition, enemy.GetComponent<FieldOfView>().seePlayer);
    }

    public void SwitchState(AIBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

}
