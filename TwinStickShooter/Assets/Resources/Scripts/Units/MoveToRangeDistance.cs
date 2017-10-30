using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToRangeDistance : MonoBehaviour
{
    [SerializeField]
    private GameObject target = null;

    [SerializeField]
    private bool bHasAggro = false;
    public bool AGGRO { get { return bHasAggro; } set { bHasAggro = value; } }

    private NavMeshAgent navAgent;
    private Animator anim;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip movementSFX;

    private AggroCheck aggroScript;

    // Use this for initialization
    void Start()
    {
        navAgent = transform.gameObject.GetComponentInChildren<NavMeshAgent>();
        anim = transform.gameObject.GetComponentInChildren<Animator>();
        audioSource = transform.gameObject.GetComponentInChildren<AudioSource>();
        audioSource.clip = movementSFX;
        audioSource.loop = true;

        aggroScript = gameObject.GetComponentInChildren<AggroCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        if( aggroScript && target == null)
            target = aggroScript.TARGET;

        if (target)
        {
            //  Set new destination
            navAgent.SetDestination(target.transform.position);
            anim.SetFloat("speed", navAgent.velocity.magnitude);

            if (Mathf.Abs(navAgent.velocity.magnitude) > 0.01f)
            {
                audioSource.Play();
                GetComponent<RangedEnemy>().INRANGE = false;
            }
            else
                GetComponent<RangedEnemy>().INRANGE = true;
        }
    }

    void SetFiring()
    {

    }
    public void SetTarget(GameObject toTarget)
    {
        bHasAggro = true;
        target = toTarget;
    }
}
