using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

    public GameObject interactionPrompt;
    public AudioClip interactPromptDisplaySFX;
    public AudioClip interactPromptUseSFX;

    private AudioSource AS;

    // Use this for initialization
	void Start ()
    {
        AS = GetComponent<AudioSource>();
        if (AS == null)
            gameObject.AddComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if( interactionPrompt.activeSelf && Input.GetButton("Fire2"))
        {
            if( AS )
            {
                if (interactPromptUseSFX)
                    AS.PlayOneShot(interactPromptUseSFX);
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            interactionPrompt.SetActive(true);

            if( AS )
            {
                if (interactPromptDisplaySFX)
                    AS.PlayOneShot(interactPromptDisplaySFX);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interactionPrompt.SetActive(false);
        }
    }
}
