using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Warp : MonoBehaviour
{

    [SerializeField]
    private float warpTravelDuration;
    [SerializeField]
    private GameObject warpVFX;
    [SerializeField]
    private AudioClip warpSFX;

    private Transform target;

    PlayerController playerController;
    CharacterController characterController;
    SkinnedMeshRenderer myRenderer;
    AudioSource audioSource;
    NavMeshAgent agent;
    Collider collider;

    // Use this for initialization
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        characterController = gameObject.GetComponent<CharacterController>();
        collider = gameObject.GetComponent<Collider>();

        myRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        audioSource = gameObject.GetComponentInChildren<AudioSource>();
        agent = gameObject.GetComponentInChildren<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnWarp(Transform warpToTransform)
    {
        target = warpToTransform;
        StartCoroutine("DoWarp");
    }

    IEnumerator DoWarp()
    {
        //  Hide the unit
        myRenderer.enabled = false;
        collider.enabled = false;

        //  Disable navigation
        if (playerController)
        {
            playerController.enabled = false;
        }
        if (agent)
            agent.Stop();

        //  Play effect
        if( audioSource )
        {
            if (warpSFX)
                audioSource.PlayOneShot(warpSFX);
        }

        if ( warpVFX )
        {
            warpTravelDuration = warpVFX.GetComponentInChildren<ParticleSystem>().main.duration;
            Destroy(Instantiate(warpVFX,transform.position,Quaternion.identity), warpTravelDuration);
        }

        //  Wait for travel time
        yield return new WaitForSeconds(warpTravelDuration);

        //  Move the unit
        transform.position = target.position;

        //  Enable navigation
        if (playerController)
        {
            playerController.enabled = true;
            characterController.enabled = true;
        }
        if (agent)
            agent.Resume();

        //  Show the unit
        myRenderer.enabled = true;
        collider.enabled = true;

        //  Play arrival effect
        if (warpVFX)
        {
            Destroy(Instantiate(warpVFX, transform.position, Quaternion.identity), warpTravelDuration);
        }
    }
}
