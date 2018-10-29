using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConveyorBlock : Block
{
    public float Speed;
    private Transform Belt;

    void Start()
    {
        Belt = transform.GetChild(0);
        StartCoroutine(_MoveBelt());
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

    IEnumerator _MoveBelt()
    {
        while (true)
        {
            foreach (Transform child in Belt)
            {
                child.transform.Translate(transform.right * Speed * Time.deltaTime);
            }
            yield return null;
        }
    }


    public override void AddCoin(Coin c)
    {
        c.transform.parent = Belt;
    }
}