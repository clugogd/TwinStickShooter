using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if( other.transform.root.gameObject.tag == "Player")
        {
            transform.root.gameObject.GetComponentInChildren<PlatformMovement>().SetActive();
        }
    }
    //void OnTriggerExit(Collider other)
    //{
    //    if (other.transform.root.gameObject.tag == "Player")
    //    {
    //        transform.root.gameObject.GetComponentInChildren<PlatformMovement>().SetInactive();
    //    }
    //}
}
