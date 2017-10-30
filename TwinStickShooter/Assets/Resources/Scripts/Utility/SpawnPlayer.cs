using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField,Range(0,10)]
    private float spawnDelay = 0.0f;
    [SerializeField]
    private bool bSpawnOnStart = true;

	// Use this for initialization
	void Start ()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if( !player )
            StartCoroutine("doSpawn");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator doSpawn()
    {
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(playerPrefab, transform.position, Quaternion.identity);

        StopCoroutine("doSpawn");
    }
}
