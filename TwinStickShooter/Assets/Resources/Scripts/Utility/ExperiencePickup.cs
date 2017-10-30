using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExperiencePickup : MonoBehaviour {

    [SerializeField]
    private float _value = 1.0f;
    public float VALUE { get { return _value; } set { _value = value; } }

    [SerializeField]
    private AudioClip collectSFX;

    [SerializeField]
    private GameObject collectVFX;

    private float collectDelay = 0.0f;
    AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
        audioSource = transform.root.gameObject.GetComponentInChildren<AudioSource>();
        if (audioSource == null)
        {
            this.gameObject.AddComponent<AudioSource>();
            audioSource = GetComponent<AudioSource>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator OnCollect()
    {
        yield return new WaitForSeconds(collectDelay);

        if (audioSource)
        {
            if (collectSFX)
                audioSource.PlayOneShot(collectSFX);
        }
        if (collectVFX)
            Destroy(Instantiate(collectVFX, transform.position, Quaternion.identity), 1.0f);

        yield return null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.tag.Contains("Player"))
        {
            other.transform.root.gameObject.GetComponent<Experience>().GainXP(_value);
            StartCoroutine("OnCollect");
        }      
    }
}
