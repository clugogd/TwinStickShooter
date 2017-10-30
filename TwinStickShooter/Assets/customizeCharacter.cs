using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customizeCharacter : MonoBehaviour {

    public Transform headGearSocket;
    public Transform shoulderGearSocket;
    public Transform chestGearSocket;
    public Transform beltGearSocket;
    public Transform legGearSocket;
    public Transform bootGearSocket;
    public Transform offhandGearSocket;
    public Transform mainhandGearSocket;
    public Transform capeGearSocket;

    public GameObject headGear;
    public GameObject shoulderGear;
    public GameObject chestGear;
    public GameObject beltGear;
    public GameObject legGear;
    public GameObject bootGear;
    public GameObject offhandGear;
    public GameObject mainhandGear;
    public GameObject capeGear;


    // Use this for initialization
    void Start ()
    {
        AddItem(headGearSocket, headGear);
         AddItem(shoulderGearSocket,shoulderGear);
         AddItem(chestGearSocket,chestGear);
         AddItem(beltGearSocket,beltGear);
         AddItem(legGearSocket,legGear);
         AddItem(bootGearSocket,bootGear);
         AddItem(offhandGearSocket,offhandGear);
         AddItem(mainhandGearSocket,mainhandGear);
         AddItem(capeGearSocket,capeGear);
    }

    public void AddItem(Transform socket, GameObject toAdd)
    {
        if( socket )
        {
            if(toAdd)
            {
                GameObject newObject = Instantiate(Resources.Load("Prefabs/" + toAdd.name) as GameObject, socket.position, socket.rotation);
                newObject.transform.parent = socket;
            }
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
