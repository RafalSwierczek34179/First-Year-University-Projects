using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Agent : MonoBehaviour
{
    static Transform[] waypoints;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        if (waypoints==null)
        {
            GameObject[] waypointObjects = GameObject.FindGameObjectsWithTag("Waypoint");
            waypoints = new Transform[waypointObjects.Length];

            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = waypointObjects[i].transform;
            }
        }
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[Random.Range(0, waypoints.Length)].position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
