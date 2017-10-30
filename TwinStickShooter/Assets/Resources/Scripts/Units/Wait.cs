using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wait : MonoBehaviour {

    private NavMeshAgent agent;
    private Animator animator;

	// Use this for initialization
	void Start ()
    {
        agent = transform.root.gameObject.GetComponentInChildren<NavMeshAgent>();
        animator = transform.root.gameObject.GetComponentInChildren<Animator>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    IEnumerator wait()
    {
        if(agent)
            agent.Stop();

        if( animator)
            animator.SetBool("idle", true);

        yield return null;
    }
}
