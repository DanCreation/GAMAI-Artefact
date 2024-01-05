using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject[] destinations;
    int counter;

    void Start()
    {
        counter = 0;
        Debug.Log("Patrol State");
    }

    public void Update()
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
}
