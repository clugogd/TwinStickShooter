using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurret : MonoBehaviour
{
    private float firingOffset = 0.1f;
    private float firingVFXDelay = 0.0f;

    private GameObject target;
    private Aim aimScript;
    private Firing[] weapons;
    public GameObject[] muzzleFlashes;

    Animator animator;
    Health healthScript;
    Die deathScript;
    FlashObject flashScript;
    
    [SerializeField]
    private float damageValue = 1.0f;
    [SerializeField]
    private float hitStunDuration = 0.5f;

    // Use this for initialization
    void Start()
    {
        aimScript = gameObject.GetComponentInChildren<Aim>();
        weapons = gameObject.GetComponentsInChildren<Firing>();
        flashScript = gameObject.GetComponentInChildren<FlashObject>();
        healthScript = gameObject.GetComponentInChildren<Health>();
        deathScript = gameObject.GetComponentInChildren<Die>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aimScript)
        {
            if (aimScript.HASTARGET)
            {
                StartCoroutine("Fire");
            }
        }
    }

    IEnumerator Fire()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].DoFire();
            muzzleFlashes[i].SetActive(true);
            yield return new WaitForSeconds(firingOffset + firingVFXDelay);
            muzzleFlashes[i].SetActive(false);
        }
    }
    IEnumerator getHit()
    {
        healthScript.ApplyDamage(damageValue);

        if (!healthScript.ISDEAD)
        {
            yield return new WaitForSeconds(hitStunDuration);
        }
        else
        {
            StopCoroutine("getHit");
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
