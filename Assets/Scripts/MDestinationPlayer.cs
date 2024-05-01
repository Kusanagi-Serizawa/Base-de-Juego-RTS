using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MDestinationPlayer : MonoBehaviour
{
    //Para parte del codigo https://docs.unity3d.com/ScriptReference/AI.NavMeshAgent-destination.html
    public Transform Objetivo;
    Vector3 destino;
    NavMeshAgent agent;
    void Start()
    {
        // Cache agent component and destination
        agent = GetComponent<NavMeshAgent>();
        destino = agent.destination;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(a:destino, b: Objetivo.position) > 1.0f)
        {
            destino = Objetivo.position;
            agent.destination = destino;
        }
    }
}
