using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DirectionEnum
{
    Up,
    UpLeft,
    Left,
    DownLeft,
    Down,
    DownRight,
    Right,
    UpRight
}

public class ConveyorBlock : Block
{
    private Belt Belt;
    [HideInInspector]
    public ConveyorConnector Connector;

    private void Awake()
    {
        Belt = transform.GetChild(0).GetComponent<Belt>();
        Connector = GetComponentInChildren<ConveyorConnector>();
    }

    protected override void Click(Vector3 clickposition)
    {
        StartCoroutine(_Rotate());
    }

    IEnumerator _Rotate()
    {
        transform.Rotate(Vector3.back * 90);
        yield return null;
    }
}