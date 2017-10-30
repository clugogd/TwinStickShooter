using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamage : MonoBehaviour
{
    [SerializeField]
    private float directDamageAmount = 1.0f;
    [SerializeField, Range(0.0f, 1.0f)]
    private float criticalProbability = 0.5f;
    [SerializeField, Range(0.5f, 2.0f)]
    private float criticalMultiplier = 1.0f;
    [SerializeField]
    private bool bApplyStun = false;
    [SerializeField, Range(0.0f, 1.0f)]
    private float stunProbability = 0.5f;
    [SerializeField]
    private bool bApplyKnockback = false;
    [SerializeField, Range(0.0f, 1.0f)]
    private float knockbackProbability = 0.5f;
    [SerializeField]
    private bool bTriggerCameraShake = false;
    [SerializeField, Range(0.1f, 1.0f)]
    private float critIntensity = 0.5f;
    [SerializeField, Range(0.02f, 0.1f)]
    private float critDecay = 0.02f;
    [SerializeField]
    private bool bDebugLogEnabled = false;

    private GameObject target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator doDamage()
    {
        //  Apply damage
        float damageToApply = 0.0f;
        string debugString = transform.gameObject.name + " applied(";

        //  Check to see if the target has a Health component
        Health healthScript = target.GetComponentInChildren<Health>();

        bool isCrit = false;

        //  -Check for criticals
        if (Random.Range(0.0f, 1.0f) <= criticalProbability)
        {
            damageToApply = directDamageAmount * criticalMultiplier + directDamageAmount;

            if (bTriggerCameraShake)
            {
                //Camera.main.GetComponent<CameraShake>().Shake(0.2f, 0.7f, 0.02f);
                Camera.main.GetComponentInChildren<CameraShake>().ShakeCamera(critIntensity, critDecay);
            }
            debugString = debugString + damageToApply + ") <CRIT>";


            //  Apply specials 
            if (Random.Range(0.0f, 1.0f) <= knockbackProbability)
            {
                bApplyKnockback = true;
                //  Apply a backward force
                if (bDebugLogEnabled)
                    Debug.Log(gameObject.name + " applied [KNOCKBACK] to <" + target.gameObject.name + ">");
            }
            if (Random.Range(0.0f, 1.0f) <= stunProbability)
            {
                bApplyStun = true;
                //  Fire off the stun event
                if (bDebugLogEnabled)
                    Debug.Log(gameObject.name + " applied [STUN] to <" + target.gameObject.name + ">");
            }
            isCrit = true;
        }
        else
        {
            damageToApply = directDamageAmount;
            debugString = debugString + damageToApply + ") ";
        }

        //  If it has a Health component apply the modified damage
        if (healthScript)
            healthScript.ApplyDamage(damageToApply, isCrit);

        debugString = debugString + "to " + target.gameObject.name;
        if (bDebugLogEnabled)
            Debug.Log(debugString);

        //  Play feedback


        target = null;
        yield return null;
    }

    void OnTriggerEnter(Collider other)
    {
        target = other.transform.gameObject;
        StartCoroutine("doDamage");
        gameObject.SetActive(false);
    }
}
