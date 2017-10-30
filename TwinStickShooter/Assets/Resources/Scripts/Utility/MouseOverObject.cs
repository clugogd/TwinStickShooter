using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverObject : MonoBehaviour {

    private Ray ray;
    private RaycastHit hitInfo;
    public bool bDisplayDebugInfo = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Camera.main)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (bDisplayDebugInfo)
                    Debug.Log(hitInfo.collider.name);
            }
        }
	}
}
