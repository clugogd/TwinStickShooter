using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour {

    [SerializeField]
    private float sensitivity = 0.19f;
    [SerializeField]
    private float rateOfFire = 2.0f;
    [SerializeField]
    private float lifetime = 3.0f;
    [SerializeField]
    private int burstCount = 3;
    [SerializeField]
    private float burstDelay = 0.1f;
    [SerializeField]
    private bool bBurstFire;
    [SerializeField]
    private bool bCanFire = true;
    [SerializeField]
    private bool bSightLaser = true;

    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform [] muzzleOffset;
    [SerializeField]
    private AudioClip firingSFX;
    [SerializeField]
    private GameObject muzzleFlash;

    private AudioSource audioSource;

    private Mesh weaponMesh;

    [SerializeField]
    private string fireButton = "RT";

    [SerializeField]
    private Transform baseTransform;

    // Use this for initialization
    void Start()
    {
        if (weaponMesh)
            GetComponent<MeshFilter>().mesh = weaponMesh;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( ((Input.GetAxis(fireButton) > sensitivity) || Input.GetButton(fireButton)) && bCanFire)
        {
            if (bBurstFire)
                StartCoroutine("BurstFire");
            else
                StartCoroutine("Fire");
        }
    }

    IEnumerator Fire()
    {
        bCanFire = false;

        StartCoroutine("CreateProjectile");

        yield return new WaitForSeconds(rateOfFire);
        bCanFire = true;
    }

    IEnumerator CreateProjectile()
    {
        if (bBurstFire)
        {
            foreach (Transform muzzle in muzzleOffset)
            {
                Quaternion rot = Quaternion.RotateTowards(transform.rotation, muzzle.rotation,360);
                GameObject go = Instantiate(projectilePrefab, muzzle.position + new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, 0.0f),rot);
                Destroy(go, lifetime);
            }
        }
        else
        {
            foreach (Transform muzzle in muzzleOffset)
            {
                GameObject go = Instantiate(projectilePrefab, muzzle.position, Quaternion.identity);
                go.transform.forward = baseTransform.forward;
                go.transform.Rotate(transform.up, muzzle.rotation.y);
                Destroy(go, lifetime);
            }
        }

        if (audioSource)
        {
            if (firingSFX)
                audioSource.PlayOneShot(firingSFX);
        }

        if (muzzleFlash)
        {
            muzzleFlash.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            muzzleFlash.SetActive(false);
        }
    }

    IEnumerator BurstFire()
    {
        bCanFire = false;

        for( int i = 0; i < burstCount; i++ )
        {
            StartCoroutine("CreateProjectile");
            yield return new WaitForSeconds(burstDelay);
        }

        yield return new WaitForSeconds(rateOfFire);
        bCanFire = true;
    }

    void OnDrawGizmos()
    {
        if (bSightLaser)
        {
            Gizmos.color = Color.red;
            Vector3 direction = transform.root.transform.TransformDirection(Vector3.forward) * 5;
            Gizmos.DrawRay(transform.position, direction);
        }
    }
}
