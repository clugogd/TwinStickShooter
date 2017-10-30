using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPad : MonoBehaviour {

    public Transform warpTarget;
    private WarpPad targetPad;

    [SerializeField]
    private bool bCanBeUsed = true;

	// Use this for initialization
	void Start ()
    {
        targetPad = warpTarget.GetComponent<WarpPad>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void OnTriggerEnter(Collider other)
    {
        targetPad.bCanBeUsed = false;

        if (bCanBeUsed)
        {
            Warp warpScript = other.transform.gameObject.GetComponent<Warp>();
            if (warpScript)
                warpScript.OnWarp(warpTarget);
        }
    }

    void OnTriggerExit(Collider other)
    {
        bCanBeUsed = true;
    }
}
