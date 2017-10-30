using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerup : MonoBehaviour
{

    [SerializeField]
    private GameObject[] powerupPrefab;

    [SerializeField]
    private GameObject spawnVFX;

    [SerializeField]
    private AudioClip spawnSFX;

    [SerializeField, Range(0.0f, 1.0f)]
    private float spawnProbability = 1.0f;

    [SerializeField]
    private float spawnDelay = 0.0f;

    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void doSpawn()
    {
        float dropPercentage = Random.Range(0.0f, 1.0f);
        if (dropPercentage <= spawnProbability)
            StartCoroutine("spawnPowerup");
    }

    public void TestSpawn()
    {
        StartCoroutine("spawnPowerup");
    }

    IEnumerator spawnPowerup()
    {
        yield return new WaitForSeconds(spawnDelay);

        if (audioSource)
        {
            if (spawnSFX)
                audioSource.PlayOneShot(spawnSFX);
        }

        if (spawnVFX)
            Destroy(Instantiate(spawnVFX, transform.position, Quaternion.identity), 1.0f);

        int objectToSpawn = Random.Range(0, powerupPrefab.Length);
        Instantiate(powerupPrefab[objectToSpawn], transform.position + new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
    }
}
