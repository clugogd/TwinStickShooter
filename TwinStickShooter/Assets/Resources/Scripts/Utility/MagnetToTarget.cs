using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetToTarget : MonoBehaviour
{

    private GameObject target;

    [SerializeField]
    private string tagToMoveToward;
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float magnetRadius = 10.0f;

    // Use this for initialization
    void Start()
    {
        if (tagToMoveToward != "")
            target = GameObject.FindGameObjectWithTag(tagToMoveToward);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag(tagToMoveToward);
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) < magnetRadius)
                transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.tag != tagToMoveToward)
            return;

        Destroy(transform.root.gameObject);
    }
}
