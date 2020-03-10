using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{

    [SerializeField] Transform destination;
    NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        if(navMeshAgent != null)
        {
            SetDestination();
        }
        else
        {
            Debug.LogError("Nav mesh component not attached to " + name);
        }
    }

    void SetDestination()
    {
        if(destination != null)
        {
            Vector3 targetDestination = destination.transform.position;
            navMeshAgent.SetDestination(targetDestination);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
