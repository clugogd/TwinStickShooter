using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashObject : MonoBehaviour
{
    [SerializeField]
    private float flashDuration = 0.5f;
    [SerializeField]
    private Renderer meshRenderer;

    private Color flashColor = Color.red;
    private Color normalColor;

    // Use this for initialization
    void Start()
    {
        if( meshRenderer == null)
            meshRenderer = GetComponentInChildren<Renderer>();
        normalColor = meshRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void doFlashObject(Color _color)
    {
        flashColor = _color;
        StartCoroutine("flashObject");
    }
    IEnumerator flashObject()
    {

        meshRenderer.material.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        meshRenderer.material.color = normalColor;
    }
}
