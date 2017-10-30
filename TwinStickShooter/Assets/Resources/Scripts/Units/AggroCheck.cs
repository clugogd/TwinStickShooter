using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroCheck : MonoBehaviour {

    [SerializeField]
    private string aggroTag = "Player";

    [SerializeField]
    private bool bAggroRangeVisible = false;

    private Renderer meshRenderer;

    private GameObject _target = null;
    public GameObject TARGET { get { return _target; } }

    // Use this for initialization
	void Start ()
    {
        meshRenderer = GetComponent<Renderer>();
   	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnTriggerEnter(Collider other )
    {
        if (other.gameObject.tag == aggroTag)
        {
            _target = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == aggroTag)
        {
            _target = null;
        }
    }

    void OnMouseEnter()
    {
        meshRenderer.enabled = true;
    }
    void OnMouseExit()
    {
        meshRenderer.enabled = false;
    }
}
