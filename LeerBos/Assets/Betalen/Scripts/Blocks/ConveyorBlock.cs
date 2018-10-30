using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConveyorBlock : Block
{
    public float Speed;
    private Transform Belt;
    private bool Rotating;

    void Start()
    {
        Belt = transform.GetChild(0);
        Rotating = false;
        StartCoroutine(_MoveBelt());
    }

    protected override void Click(Vector3 clickposition)
    {
        if (!Rotating)
        {
            StartCoroutine(_Rotate());
        }
    }

    IEnumerator _Rotate()
    {
        Quaternion target = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 90);
        Rotating = true;
        while (transform.rotation != target)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * 100);
            yield return null;            
        }

        Rotating = false;
        transform.rotation = target;
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