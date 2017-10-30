using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObject : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 maxDistance;

    private Vector3 startPos;
    private Vector3 targetPos;

	// Use this for initialization
	void Start () {
        startPos = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        targetPos = new Vector3(startPos.x,
                               startPos.y + (maxDistance.y * Mathf.Sin(Time.time * speed)),
                               startPos.z);

        transform.localPosition = targetPos;
    }
}
