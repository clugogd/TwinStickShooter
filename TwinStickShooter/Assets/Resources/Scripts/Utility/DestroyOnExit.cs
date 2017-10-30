using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionExit( Collision other )
    {
        if (other.transform.root.gameObject.tag == "Projectile")
        {
            other.transform.root.GetComponent<DestroyOnCollision>().KillObject();
        }
    }
}
