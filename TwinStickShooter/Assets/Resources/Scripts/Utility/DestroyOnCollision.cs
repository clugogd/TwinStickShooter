using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{

    [SerializeField]
    private AudioClip hitSFX;
    [SerializeField]
    private GameObject hitVFX;

    [SerializeField]
    private string tagToIgnore = "Player";

    private AudioSource audioSource;
    private Light mylight;
    private MeshRenderer meshRenderer;
    private Collider myCollider;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        mylight = GetComponent<Light>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        myCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Kill()
    {
        if (audioSource)
        {
            if (hitSFX)
                audioSource.PlayOneShot(hitSFX);

            if (hitVFX)
                Destroy(Instantiate(hitVFX, transform.position, Quaternion.identity), 0.3f);
        }

        meshRenderer.enabled = false;
        myCollider.enabled = false;
        mylight.enabled = false;

        if (audioSource.clip == null)
        {
            yield return new WaitForSeconds(1.0f);
        }
        else
        {
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.transform.gameObject.name.Contains("Reflector") || other.transform.gameObject.tag == "Reflector")
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
                float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
                transform.root.eulerAngles = new Vector3(0, rot, 0);
            }
        }
        else
            KillObject();
    }

    public void KillObject()
    {
        StartCoroutine("Kill");
    }
}
