using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    [SerializeField]
    private string levelToTransitionTo = "base";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if( other.transform.root.gameObject.tag.Contains("Player"))
        {
            other.transform.root.gameObject.GetComponentInChildren<PlayerController>().PLAYERCONTROL = false;
            GameInstance._instance.LevelTransitionWithFade(levelToTransitionTo);
        }
    }
}
