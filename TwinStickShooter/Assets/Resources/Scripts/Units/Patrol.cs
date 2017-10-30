using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour {

    [SerializeField]
    private float patrolRadius = 10.0f;
    [SerializeField]
    private float patrolDistance = 2.0f;
    [SerializeField]
    private float findNewPatrolPointDelay = 1.0f;
    [SerializeField]
    private int mask = 1;

    private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = gameObject.GetComponentInChildren<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DoPatrol()
    {
        StartCoroutine("patrol");
    }

    IEnumerator patrol()
    {
        if (agent)
            agent.Stop();

        yield return new WaitForSeconds(findNewPatrolPointDelay);
        Vector3 newPatrolPoint = Random.insideUnitCircle * patrolRadius;
        newPatrolPoint += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(newPatrolPoint, out hit, patrolDistance, mask);
        if (agent)
        {
            agent.SetDestination(hit.position);
            agent.Resume();
        }
    }
}
