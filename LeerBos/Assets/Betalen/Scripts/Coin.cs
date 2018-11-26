using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Interactable
{
    public Transform RegisterTarget;
    public Transform MoveBetween;
    public Transform BeltTarget;
    [Space]
    public float MoveSpeed;
    public bool CorrectCoin;
    [Space]
    public double Value;
    public int SpawnMultiplier;
    private GameObject Collider;

    [Space]
    public AudioClip MoveUp;
    public AudioClip MoveDown;
    private AudioSource Audio;

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
        Collider = transform.GetChild(0).gameObject;
    }

    protected override void Click(Vector3 clickposition)
    {
        //if it's on the belt
        if (transform.parent.tag == "ConveyorBelt")
        {
            MoveTo(RegisterTarget);
        }

        //if it's in the register
        else if (transform.parent.tag == "CashRegister")
        {
            MoveTo(BeltTarget);
        }
    }

    public void MoveTo(Transform target, bool audio = true)
    {
        transform.SetParent(MoveBetween, true);
        StartCoroutine(_MoveTo(target));
        if (audio)
        {
            if (target.position.y > transform.position.y)
            {
                Audio.pitch = Random.Range(0.8f, 1.2f);
                Audio.PlayOneShot(MoveUp);
            }
            else
            {
                Audio.PlayOneShot(MoveDown);
            }
        }
    }

    //Organically moves the coin to the target position, finishes when the coin is at the center of the target (y-axis)
    IEnumerator _MoveTo(Transform target)
    {
        Collider.layer = 11;
        bool moving = true;
        while (moving)
        {
            //transform.Translate(Vector3.up * MoveSpeed / 2 * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - target.position.y) < 0.25f)
            {
                moving = false;
            }

            yield return null;
        }

        Collider.layer = 5;
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