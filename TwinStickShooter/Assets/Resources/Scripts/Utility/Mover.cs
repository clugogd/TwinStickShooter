using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private bool bHasTrail = false;

    [SerializeField]
    private AudioClip travelSFX;

    private Rigidbody rb;
    private TrailRenderer trail;
    private AudioSource audioSource;
    private bool bMoving = true;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        trail.enabled = bHasTrail;

        if (audioSource)
        {
            if (travelSFX)
            {
                audioSource.clip = travelSFX;
                audioSource.loop = true;
                audioSource.Play();
            }
        }

        rb.AddForce(transform.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (!bMoving)
            rb.velocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == transform.gameObject.tag)
            return;

        bMoving = false;
    }
}
