using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField]
    private GameObject destroyedObject;
    [SerializeField]
    private GameObject destroyVFX;
    [SerializeField]
    private AudioClip destroySFX;
    [SerializeField]
    private float startDelay;
    [SerializeField]
    private bool bDestroyedObjectIsPersistent = true;
    [SerializeField]
    private float cleanUpDelay = 2.0f;

    AudioSource audioSource;
    MeshRenderer meshRenderer;
    Collider myCollider;

    private bool bObjectDestroyed = false;

    private SpawnPowerup spawnerScript;
    private Health healthScript;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        meshRenderer = GetComponent<MeshRenderer>();
        myCollider = GetComponent<Collider>();

        if(destroyedObject)
         destroyedObject.SetActive(false);

        spawnerScript = GetComponent<SpawnPowerup>();
        healthScript = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthScript)
        {
            if (healthScript.ISDEAD && !bObjectDestroyed)
                onDoDestruction();
        }
        if (destroyedObject)
        {
            if (bDestroyedObjectIsPersistent == false && destroyedObject.active)
            {
                Destroy(destroyedObject, cleanUpDelay);
                StopCoroutine("doDestruction");
            }
        }
    }

    public void onDoDestruction()
    {
        StartCoroutine("doDestruction");
    }

    IEnumerator doDestruction()
    {
        meshRenderer.enabled = false;
        myCollider.enabled = false;

        bObjectDestroyed = true;

        yield return new WaitForSeconds(startDelay);

        if( spawnerScript)
            spawnerScript.doSpawn();

        if (destroyVFX)
        {
            Destroy(Instantiate(destroyVFX, transform.position, Quaternion.identity), 2.0f);
        }
        if (audioSource)
        {
            if (destroySFX)
                audioSource.PlayOneShot(destroySFX);
        }

        if (destroyedObject)
            destroyedObject.SetActive(true);

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains("Projectile"))
        {
            if (healthScript)
            {
                healthScript.ApplyDamage(1.0f);
            }
            else
                onDoDestruction();
        }
    }
}
