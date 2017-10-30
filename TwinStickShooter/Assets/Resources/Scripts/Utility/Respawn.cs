using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    [SerializeField]
    private Transform spawnLocation;

    [SerializeField]
    private float respawnDelay = 3.0f;


	// Use this for initialization
	void Start ()
    {
        if (spawnLocation == null)
            spawnLocation = GameObject.Find("PlayerStart").transform;	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void Spawn()
    {
        StartCoroutine("doRespawn");
    }
    IEnumerator doRespawn()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        yield return new WaitForSeconds(respawnDelay);
        transform.position = spawnLocation.position;
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
    }
}
