using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserBeam : MonoBehaviour
{

    [SerializeField]
    private Transform startPosition;
    [SerializeField]
    private Transform endPosition;

    [SerializeField]
    private float damageAmount = 1.0f;
    [SerializeField]
    private float activeDuration = 1.0f;
    [SerializeField]
    private float maxWidth = 1.0f;
    [SerializeField]
    private float minWidth = 0.1f;
    [SerializeField]
    private float firingDelay = 0.0f;

    [SerializeField]
    private float currentWidth = 0.1f;
    [SerializeField]
    private bool bIsActive = false;
    [SerializeField]
    private bool bCanDamage = false;

    [SerializeField]
    private AudioClip activeSFX;

    private LineRenderer lineRenderer;
    private Collider myCollider;
    private AudioSource audioSource;

    static int hitCount = 0;

    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        myCollider = GetComponent<Collider>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        lineRenderer.SetPosition(0, new Vector3(startPosition.localPosition.x, startPosition.localPosition.y, startPosition.localPosition.z));
        lineRenderer.SetPosition(0, new Vector3(startPosition.localPosition.x, startPosition.localPosition.y, startPosition.localPosition.z));
        lineRenderer.startWidth = lineRenderer.endWidth = currentWidth;

        StartCoroutine("FireLaser");
    }

    // Update is called once per frame
    void Update()
    {

        if (bIsActive)
        {
            StartCoroutine("FireLaser");
        }
    }

    IEnumerator FireLaser()
    {
        yield return new WaitForSeconds(firingDelay);
        bCanDamage = true;

        lineRenderer.startWidth = lineRenderer.endWidth = Mathf.Lerp(minWidth, maxWidth, Time.deltaTime);
        yield return new WaitForSeconds(activeDuration);
        lineRenderer.startWidth = lineRenderer.endWidth = Mathf.Lerp(maxWidth, minWidth, Time.deltaTime);

        bCanDamage = false;

        if (lineRenderer.startWidth == minWidth)
            bIsActive = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            print(gameObject.tag + " damaged " + other.gameObject.tag + "(x" + hitCount + ")");

        hitCount++;

        bIsActive = true;
    }
}
