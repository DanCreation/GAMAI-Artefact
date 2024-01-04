using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject[] destinations;
    public Vector3 startPosition;
    public int counter;

    

    // Start is called before the first frame update
    void Start()
    {
        startPosition = agent.transform.position;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        //for (int i = 0; i < destinations.Length; i++)
        //{
        //    agent.SetDestination(destinations[counter].transform.position);
        //    if (Vector3.Distance(agent.transform.position, destinations[counter].transform.position) <= 2.0f)
        //    {
        //        counter++;
        //    }
        //}
    }
}


