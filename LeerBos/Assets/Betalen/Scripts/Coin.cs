using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Interactable
{
    public Counter Counter;
    public Transform MoveBetween;
    public ConveyorBelt Belt;
    [Space]
    public float MoveSpeed;
    public bool Moving;
    public bool CorrectCoin;
    [Space]
    public double Value;

    protected override void Click(Vector3 clickposition)
    {
        //if clicked while on belt, move to counter
        if (transform.parent == Belt.transform)
        {
            MoveTo(Counter.transform);
        }
        //if clicked while on counter, move to belt
        else if (transform.parent == Counter.transform)
        {
            MoveTo(Belt.transform);
        }

        transform.SetParent(MoveBetween, true);
    }

    public void MoveTo(Transform target)
    {
        StartCoroutine(_MoveTo(target));
    }

    //Organically moves the coin to the target position, finishes when the coin is at the center of the target (y-axis)
    IEnumerator _MoveTo(Transform target)
    {
        transform.GetChild(0).gameObject.layer = 11;
        Moving = true;
        while (Moving)
        {
            transform.Translate(Vector3.up * MoveSpeed / 2 * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - target.position.y) < 0.25f)
            {
                Moving = false;
            }

            yield return null;
        }
        transform.GetChild(0).gameObject.layer = 5;
    }

    IEnumerator _CorrectMoveTo(Transform target)
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * 2 * Time.deltaTime);
            yield return null;
        }
    }
}