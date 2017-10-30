using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Aim : MonoBehaviour
{

    [SerializeField]
    private float range = 100.0f;
    public float RANGE { get { return range; } set { range = value; } }

    [SerializeField]
    private float turnSpeed = 2.0f;

    [SerializeField]
    private GameObject target = null;

    [SerializeField]
    private Transform pivot = null;

    private bool bHasTarget = false;
    public bool HASTARGET { get { return bHasTarget; } }

    private float turnMagnitude;

    private AggroCheck aggroScript;
    private NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = gameObject.GetComponentInChildren<NavMeshAgent>();
        if(agent)
            agent.stoppingDistance = range;

        aggroScript = gameObject.GetComponentInChildren<AggroCheck>();

        if (aggroScript)
            aggroScript.gameObject.transform.localScale = new Vector3(range, 0.5f, range);

        if (pivot == null)
            pivot = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (aggroScript)
                target = aggroScript.TARGET;
        }
        bHasTarget = IsInRange();
    }

    public void SetTarget(GameObject aggroObject)
    {
        target = aggroObject;
    }

    bool IsInRange()
    {
        if (target)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= range)
            {
                TurnToFaceTarget();
                return true;
            }
        }
        return false;
    }
    void TurnToFaceTarget()
    {
        if (target)
        {
            Vector3 lookPos = target.transform.position - pivot.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            pivot.rotation = Quaternion.Slerp(pivot.rotation, rotation, Time.deltaTime * turnSpeed);

        }
    }
}
