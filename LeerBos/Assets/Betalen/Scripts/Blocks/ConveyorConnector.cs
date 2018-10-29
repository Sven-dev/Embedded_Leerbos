using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConveyorConnector : Connector
{
    public ConveyorBlock Conveyor;

    // Use this for initialization
    void Awake()
    {
        Conveyor = transform.parent.GetComponent<ConveyorBlock>();
    }
}