using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour {
    public DoorBehaviour Controller;
    Quaternion rotation;
    public GameObject text;
    void Awake()
    {
        rotation = transform.rotation;
    }
    void LateUpdate()
    {
        //Debug.Log(Controller.RightDoor.transform.rotation.y);
        if(Controller.RightDoor.transform.rotation.y == 1)
        { 
            text.transform.rotation = rotation;
        }
    }
}
