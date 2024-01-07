using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    AIBaseState currentState;
    public State_Patrol patrolState = new State_Patrol();
    public AttackState attackState = new AttackState();
    public InvestigateState investigateState = new InvestigateState();
    public GameObject enemy;
    
    public NavMeshAgent agent;
    public GameObject[] destinations;
    Vector3 playerPosition;
    Vector3 soundPosition;

    // Start is called before the first frame update
    public void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        currentState = patrolState;
        currentState.EnterState(this);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision, enemy);
        //currentState = patrolState;
        //enemy.GetComponent<FieldOfView>().seePlayer = false;
        //currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        soundPosition = enemy.GetComponent<FieldOfHearing>().playerPosition;
        currentState.UpdateState(this, agent, destinations, playerPosition, soundPosition, enemy.GetComponent<FieldOfView>().seePlayer, enemy.GetComponent<FieldOfHearing>().heardPlayer);
    }

    public void SwitchState(AIBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

}
