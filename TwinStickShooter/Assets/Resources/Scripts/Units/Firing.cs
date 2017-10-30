using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float lifetime = 2.0f;

    [SerializeField]
    private float firingDelay = 0.0f;

    [SerializeField]
    private float rateOfFire = 1.0f;

    [SerializeField]
    private bool bCanFire = true;

    [SerializeField]
    private AudioClip firingSFX;

    private AudioSource audioSource;

    Animator anim;

    // Use this for initialization
    void Start()
    {
        audioSource = gameObject.GetComponentInChildren<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        anim = transform.root.gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DoFire()
    {
        if (bCanFire)
            StartCoroutine("Fire");
    }

    public void StopFiring()
    {
        StopCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        bCanFire = false;
       
        if( firingSFX )
        {
            if( audioSource )
            {
                audioSource.PlayOneShot(firingSFX);
            }
        }

        yield return new WaitForSeconds(firingDelay);
        Destroy(Instantiate(projectilePrefab, this.transform.position, this.transform.rotation), lifetime);

        if (anim)
            anim.SetTrigger("fire");

        yield return new WaitForSeconds(rateOfFire);
        bCanFire = true;
    }
}
