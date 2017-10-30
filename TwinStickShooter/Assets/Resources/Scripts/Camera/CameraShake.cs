using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shake_decay;
    public float shake_intensity;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (shake_intensity > 0)
        {
            transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
            transform.rotation = new Quaternion(
                originRotation.x + Random.Range(-shake_intensity, shake_intensity) * Time.deltaTime,
                originRotation.y + Random.Range(-shake_intensity, shake_intensity) * Time.deltaTime,
                originRotation.z + Random.Range(-shake_intensity, shake_intensity) * Time.deltaTime,
                originRotation.w + Random.Range(-shake_intensity, shake_intensity) * Time.deltaTime);
            shake_intensity -= shake_decay;
        }
    }

    public void ShakeCamera(float intensity, float decay)
    {
        if (anim)
        {
            anim.SetTrigger("shake");
        }
        else
        {
            originPosition = transform.position;
            originRotation = transform.rotation;
            shake_intensity = intensity;
            shake_decay = decay;
        }
    }
}
