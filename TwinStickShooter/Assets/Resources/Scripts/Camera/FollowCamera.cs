using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public GameObject target;

    private Vector3 offset;
    [SerializeField]
    private bool bRenderOcclusion = false;
    [SerializeField]
    private bool bStaticCamera = true;

    [SerializeField]
    private bool bTest = true;

    void Awake()
    {
        if (bTest)
        {
            transform.position = new Vector3(0.0f, 20.0f, 0.0f);
        }
    }
    // Use this for initialization
    void Start()
    {
        if (target == null)
            target = GameObject.Find("Player");

        offset = transform.position;
    }

    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }

        if (bRenderOcclusion)
        {
            Debug.DrawRay(this.transform.position, (this.target.transform.position - this.transform.position), Color.magenta);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
            {
                if (hitInfo.collider)
                {
                    Renderer r = hitInfo.collider.GetComponent<Renderer>();
                    r.enabled = false;
                }
            }
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (target)
        {
            transform.position = target.transform.position + offset;

            if (bStaticCamera)
                transform.LookAt(target.transform.position);
        }
    }
}
