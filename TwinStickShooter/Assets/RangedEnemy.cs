using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{

    [SerializeField]
    private Firing[] weapons;

    [SerializeField]
    private float hitStunDuration = 0.1f;

    NavMeshAgent agent;
    Animator animator;
    Health healthScript;
    Die deathScript;
    Patrol patrolScript;
    AggroCheck aggroScript;

    [SerializeField]
    private bool bIsFiring = false;

    [SerializeField]
    private bool bInRange = false;
    public bool INRANGE { get { return bInRange; } set { bInRange = value; } }

    Ray ray;
    RaycastHit hitInfo;

    // Use this for initialization
    void Start()
    {
        weapons = GetComponentsInChildren<Firing>();
        agent = GetComponentInChildren<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        healthScript = GetComponentInChildren<Health>();
        deathScript = GetComponentInChildren<Die>();
        aggroScript = GetComponentInChildren<AggroCheck>();
        patrolScript = GetComponentInChildren<Patrol>();
        if(patrolScript)
            patrolScript.DoPatrol();
        ray = new Ray(transform.position, transform.forward * 200.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //if(patrolScript)
        // patrolScript.enabled = !bInRange;

        if (bInRange)
        {
            if (!healthScript.ISDEAD)
            {
                foreach (Firing weapon in weapons)
                    weapon.DoFire();
            }
        }
    }

    bool CanFire()
    {
        if (Physics.Raycast(ray, out hitInfo, 1000.0f))
            return true;
        return false;
    }
    IEnumerator getHit()
    {
        healthScript.ApplyDamage(1.0f);

        if (!healthScript.ISDEAD)
        {
            if (agent)
                agent.Stop();
            yield return new WaitForSeconds(hitStunDuration);

            if (agent)
                agent.Resume();
        }
        else
        {
            StopCoroutine("getHit");

            TargetPlayer targetScript = gameObject.GetComponentInChildren<TargetPlayer>();
            if( targetScript)
               targetScript.SetAggro(false);

            for (int i = 0; i < weapons.Length; i++)
                weapons[i].StopFiring();

            deathScript.DoDeathEvent();
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains("Projectile"))
        {
            StartCoroutine("getHit");
        }
    }
}
