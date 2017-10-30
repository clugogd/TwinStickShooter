using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserSight : MonoBehaviour {

    [SerializeField]
    private LineRenderer laserSight;
    [SerializeField]
    private Color sightColor = Color.red;
    [SerializeField]
    private float sightWidth = 2.0f;
    [SerializeField]
    private Material desiredMaterial;
    [SerializeField]
    private float sightRange = 5.0f;

    // Use this for initialization
    void Start()
    {
        laserSight = GetComponent<LineRenderer>();
        laserSight.material = desiredMaterial;
        laserSight.startColor = sightColor;
        laserSight.startWidth = sightWidth;
        laserSight.endWidth = sightWidth;
    }

    // Update is called once per frame
    void Update ()
    {
    }
}
