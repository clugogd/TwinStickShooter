using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private float startMovementDelay = 1.0f;
    [SerializeField]
    private float arrivalMovementDelay = 1.0f;


    [SerializeField]
    private bool bIsActive = false;
    [SerializeField]
    private bool bHasArrived = false;
    [SerializeField]
    private bool bIsLooping = false;
    private bool bCanMove = false;

    [SerializeField]
    private Transform[] movementNodes;
    [SerializeField]
    private int currentIndex = 0;

    private Collider myCollider;
    private Collider[] invisibleWalls;

    void Start()
    {
        myCollider = gameObject.GetComponent<Collider>();
        invisibleWalls = gameObject.GetComponentsInChildren<Collider>();

        StartCoroutine("DisableWalls");
    }

    void Update()
    {
        if (bIsActive)
        {
            if (!bHasArrived)
            {
                if (bCanMove)
                {
                    float currDistance = Mathf.Abs(Vector3.Distance(transform.position, movementNodes[currentIndex].position));
                    print("CURRENTDISTANCE<" + currDistance + ">");
                    if (currDistance >= 1.0f)
                    {
                        Vector3 moveDirection = movementNodes[currentIndex].position - transform.position;
                        transform.Translate(moveDirection * speed * Time.deltaTime);
                    }
                    else if (currDistance < 1.0f)
                    {
                        bCanMove = false;
                        bHasArrived = true;
                        StartCoroutine("SetInactive");
                    }
                }
            }
        }
    }

    IEnumerator EnableWalls()
    {
        UpdateIndex();

        yield return new WaitForSeconds(startMovementDelay);

        foreach (Collider wall in invisibleWalls)
            wall.enabled = true;

        bCanMove = true;
    }
    IEnumerator DisableWalls()
    {
        foreach (Collider wall in invisibleWalls)
            wall.enabled = false;
        myCollider.enabled = true;

        yield return new WaitForSeconds(arrivalMovementDelay);

        bCanMove = false;
    }

    public void SetActive()
    {
        bIsActive = true;
        StartCoroutine("EnableWalls");
    }
    public void SetInactive()
    {
        bIsActive = false;
        StartCoroutine("DisableWalls");
    }

    public void UpdateIndex()
    {
        if (currentIndex++ > movementNodes.Length - 1)
            currentIndex = 0;
    }
}
