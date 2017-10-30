using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject target = null;

    [SerializeField]
    private float rotationSpeed = 1.0f;

    [SerializeField]
    private bool bSightLaser = true;

    [SerializeField]
    private float sightRange = 5.0f;

    [SerializeField]
    private bool bHasAggro = false;

    [SerializeField]
    private Transform pivot;

    [SerializeField]
    private bool bDisplayDebugInfo = false;

    Animator anim;
    private float turnMagnitude;

    AggroCheck aggroScript;

    // Use this for initialization
    void Start()
    {
       // target = GameObject.FindGameObjectWithTag("Player");
        anim = this.gameObject.GetComponentInChildren<Animator>();
        aggroScript = gameObject.GetComponentInChildren<AggroCheck>();

        if (pivot == null)
            pivot = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if( aggroScript )
                target = aggroScript.TARGET;
        }
        if (target)
            TurnToTarget();

        if (!bHasAggro)
        {
            anim.SetBool("turnleft", false);
            anim.SetBool("turnright", false);
            anim.SetBool("idle", true);
        }
    }

    public void SetAggro(bool _aggro)
    {
        bHasAggro = _aggro;
    }

    void TurnToTarget()
    {
        if (target)
        {
            Vector3 lookPos = target.transform.position - transform.position;
            lookPos.y = 0.0f;
            Quaternion newRot = Quaternion.LookRotation(lookPos);

            if (turnMagnitude < newRot.eulerAngles.magnitude)
            {
                anim.SetBool("turnright", true);
                anim.SetBool("turnleft", false);
            }
            else if (turnMagnitude > newRot.eulerAngles.magnitude)
            {
                anim.SetBool("turnleft", true);
                anim.SetBool("turnright", false);
            }
            else
            {
                anim.SetBool("turnleft", false);
                anim.SetBool("turnright", false);
            }
            if(bDisplayDebugInfo)
                Debug.Log("NewMag: " + newRot.eulerAngles.magnitude + ", OldMag: " + turnMagnitude);

            turnMagnitude = newRot.eulerAngles.magnitude;

            if(bDisplayDebugInfo)
                Debug.Log("NewMag: " + newRot.eulerAngles.magnitude + ", OldMag: " + turnMagnitude);

            transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * rotationSpeed);
        }
    }
}
