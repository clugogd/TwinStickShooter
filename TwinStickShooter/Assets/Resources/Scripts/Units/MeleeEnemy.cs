using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MeleeEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    Health healthScript;
    Die deathScript;
    Wait waitScript;
    Attack attackScript;
    Patrol patrolScript;

    [SerializeField]
    GameObject target;

    [SerializeField]
    private bool bAttacking = false;
    [SerializeField]
    private float hitStunDuration = 0.5f;

    private float distanceToTarget = 0.0f;

    [SerializeField]
    private float aggroDistance = 10.0f;
    [SerializeField]
    private bool bHasBeenAggroed = false;

    private string szQuadrant;

    [SerializeField]
    private bool bDebugInfoDisplay = false;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        healthScript = GetComponent<Health>();
        deathScript = GetComponent<Die>();
        attackScript = GetComponentInChildren<Attack>();
        attackScript.enabled = true;
        patrolScript = GetComponentInChildren<Patrol>();
    }

    GameObject FindClosestTarget()
    {
        PlayerController[] objects = GameObject.FindObjectsOfType<PlayerController>();
        if (objects.Length > 0)
        {
            float closestDistance = Vector3.Distance(transform.position, objects[0].transform.position);
            int toTarget = 0;

            for (int i = 1; i < objects.Length; i++)
            {
                if (Vector3.Distance(transform.position, objects[i].transform.position) < closestDistance)
                {
                    toTarget = i;
                }
            }

            return objects[toTarget].gameObject;
        }
        return null;
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = FindClosestTarget();
        }

        if (target)
        {
            if ((target.transform.position - transform.position).magnitude < aggroDistance)
            {
                //  Is the target in front of me?
                Vector3 toTarget = (target.transform.position - transform.position).normalized;
                if (bDebugInfoDisplay)
                    print("Distance: <" + (target.transform.position - transform.position).magnitude + ">");

                float angle = Vector3.Cross(transform.forward, target.transform.forward).sqrMagnitude * Mathf.Rad2Deg;
                if (bDebugInfoDisplay)
                    print("angle:" + angle);

                if (Vector3.Dot(toTarget, transform.forward) >= 0)
                {
                    bHasBeenAggroed = true;
                    szQuadrant = "front";
                }
                else
                {
                    szQuadrant = "behind";
                }
                if (Vector3.Dot(toTarget, transform.right) > 0)
                {
                    szQuadrant += "right";
                }
                else if (Vector3.Dot(toTarget, transform.right) == 0)
                {
                    szQuadrant += "";
                }
                else
                {
                    szQuadrant += "left";
                }

                if (bHasBeenAggroed)
                {
                    agent.SetDestination(target.transform.position);
                    distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
                    if ((distanceToTarget < attackScript.RANGE))
                        attackScript.DoAttack();
                }
            }
            else
            {
                if (!bHasBeenAggroed)
                    patrolScript.DoPatrol();
            }
        }

        animator.SetFloat("speed", agent.velocity.magnitude);

    }

    void OnGUI()
    {
        if (bDebugInfoDisplay)
        {
            float offset = 0;
            float step = 32;
            GUI.BeginGroup(new Rect(10, 10, 200, 200));
            GUI.Label(new Rect(0, offset + step, 200, 32), "UNIT: <" + gameObject.name + ">");
            offset += step;
            GUI.Label(new Rect(0, offset + step, 200, 32), "Attacking: " + bAttacking);
            offset += step;
            GUI.Label(new Rect(0, offset + step, 200, 32), "Distance: " + distanceToTarget);
            offset += step;
            GUI.Label(new Rect(0, offset + step, 200, 32), "TargetIs: <" + szQuadrant + ">");
            GUI.EndGroup();
        }
    }

    IEnumerator getHit()
    {
        healthScript.ApplyDamage(attackScript.DAMAGE);

        if (!healthScript.ISDEAD)
        {
            agent.Stop();
            animator.SetTrigger("stun");
            yield return new WaitForSeconds(hitStunDuration);
            animator.SetTrigger("recover");
            agent.Resume();
        }
        else
        {
            StopCoroutine("getHit");
            attackScript.enabled = false;
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
