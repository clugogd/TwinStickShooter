using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    private int currentLevel = 1;
    public int CURRENTLEVEL { get { return currentLevel; } set { currentLevel = value; } }

    private int maxLevel = 100;
    public int MAXLEVEL { get { return maxLevel; } set { maxLevel = value; } }

    private float maxHP = 100.0f;
    public float MAXHP { get { return maxHP; } set { maxHP = value; } }

    private float currentHP = 100.0f;
    public float CURRENTHP { get { return currentHP; } set { currentHP = value; } }

    private float xpToNextLevel = 100.0f;
    public float XPTONEXTLEVEL { get { return xpToNextLevel; } set { xpToNextLevel = value; } }

    private float currentXP = 0.0f;
    public float CURRENTXP { get { return currentXP; } set { currentXP = value; } }

    private float maxEnergy = 100.0f;
    public float MAXENERGY { get { return maxEnergy; } set { maxEnergy = value; } }

    private float currentEnergy = 0.0f;
    public float CURRENTENERGY { get { return currentEnergy; } set { currentEnergy = value; } }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
