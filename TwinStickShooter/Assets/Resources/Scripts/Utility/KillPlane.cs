using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        if( other.transform.root.gameObject.name.Contains("Player"))
        {
            other.transform.root.GetComponent<Respawn>().Spawn();
        }
    }
}
