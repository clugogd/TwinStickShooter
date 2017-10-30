using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour {

    public float directAmount = 3.0f;
    public float tickAmount = 1.0f;
    public float tickFrequency = 0.2f;
    public float effectDuration = 3.0f;

    [SerializeField]
    private float currentTimer = 0.0f;
    [SerializeField]
    private float currentDuration = 0.0f;
    [SerializeField]
    private bool bEffectActive = false;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (bEffectActive)
        {
            currentTimer += Time.deltaTime;
            currentDuration += Time.deltaTime;

            if (currentDuration > effectDuration)
                Destroy(this.transform.gameObject);
        }
	}
    void OnEnable()
    {
        bEffectActive = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if( other.transform.gameObject.name.Contains("Player"))
            other.transform.gameObject.GetComponent<Health>().ApplyDamage(directAmount);
    }

    void OnTriggerStay(Collider other)
    {
        if (currentTimer > tickFrequency)
        {
            if (other.transform.gameObject.name.Contains("Player"))
                other.transform.gameObject.GetComponent<Health>().ApplyDamage(tickAmount);

            currentTimer = 0.0f;
        }
    }
}
