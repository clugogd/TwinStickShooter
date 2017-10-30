using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class Flashlight : MonoBehaviour {

    [SerializeField]
    private Light flashlight;
    [SerializeField]
    private Transform lightSocket;
    [SerializeField]
    private string toggleButton = "flashlight";
    [SerializeField]
    private Texture cookie;
    [SerializeField]
    private Color color;

    [SerializeField]
    private bool bUseCookie = false;

	// Use this for initialization
	void Start ()
    {
        if (flashlight == null)
            flashlight = GetComponent<Light>();

        if (lightSocket == null)
            lightSocket = gameObject.transform;

        if (cookie && bUseCookie)
            flashlight.cookie = cookie;

        flashlight.color = color;
        flashlight.type = LightType.Spot;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if( Input.GetButtonDown(toggleButton))
        {
            flashlight.enabled = !flashlight.enabled;
        }

        if (bUseCookie && cookie)
            flashlight.cookie = cookie;
	}
}
