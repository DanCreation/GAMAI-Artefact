using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    public AIBaseState currentState;
    PatrolState patrol;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        patrol.Update();
    }

    //public void SwitchState(AIBaseState AI)
    //{
    //    currentState = AI;
    //    AI.EnterState(this);
    //}
}
