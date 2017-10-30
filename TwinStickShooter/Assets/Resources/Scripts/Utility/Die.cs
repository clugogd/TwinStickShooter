using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Die : MonoBehaviour {

    [SerializeField]
    private float deathDelay = 0.5f;
    [SerializeField]
    private AudioClip deathSFX;
    [SerializeField]
    public GameObject deathVFX;

    private NavMeshAgent agent;
    private Animator animator;

    // Use this for initialization
    void Start ()
    {
        agent = transform.gameObject.GetComponentInChildren<NavMeshAgent>();
        animator = transform.gameObject.GetComponentInChildren<Animator>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void DoDeathEvent()
    {
        StartCoroutine("die");
    }

    IEnumerator die()
    {
        if( agent )
            agent.Stop();

        if (animator)
        {
            animator.SetTrigger("die");

            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            deathDelay = info.length;
        }
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(deathDelay);
        //Destroy(transform.root.gameObject);

        float vfxduration = deathVFX.GetComponentInChildren<ParticleSystem>().main.duration;

        if (deathVFX)
            Destroy(Instantiate(Resources.Load("Effects/" + deathVFX.name) as GameObject, transform.position, Quaternion.identity), vfxduration);

        gameObject.SetActive(false);
    }
}
