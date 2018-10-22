using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartMover : Interactable
{
    public float MoveSpeed;
    public float MoveDrag;

    private Kart Kart;
    private bool Moving;

    private void Start()
    {
        Kart = transform.parent.GetComponent<Kart>();
        Moving = true;
    }

    protected override void Click(Vector3 clickposition)
    {
        float distance;
        int signum;
        GetDistance(clickposition, "x", out distance, out signum);
        StartCoroutine(_Move(distance, signum));
    }

    //position relative to the camera
    IEnumerator _Move(float x, int signum)
    {
        float speedChange = MoveSpeed - x / 2;
        while (speedChange > 0 && Moving)
        {
            if (Kart.Clamp() == true)
            {
                speedChange /= 2;
                signum = -signum;
            }

            Kart.transform.Translate(Vector3.right * speedChange * signum * Time.deltaTime);
            speedChange -= MoveDrag * Time.deltaTime;
            yield return null;
        }
    }

    //Halts the kart
    public void Stop()
    {
        StartCoroutine(_Stop());
    }

    IEnumerator _Stop()
    {
        Moving = false;
        yield return null;
        Moving = true;
                
    }

    //calculates the distance between the this and an object, based on an axis.
    void GetDistance(Vector3 obj, string axis, out float distance, out int signum)
    {
        switch (axis)
        {
            case "x":
            case "X":
                distance = Vector2.Distance(
                    new Vector2(transform.position.x, 0),
                    new Vector2(obj.x, 0));

                distance += 2 * Vector2.Distance(
                    new Vector2(0, transform.position.y),
                    new Vector2(0, obj.y));

                if (transform.position.x > obj.x)
                {
                    signum = 1;
                    break;
                }

                signum = -1;
                break;

            case "y":
            case "Y":
                distance = Vector2.Distance(
                    new Vector2(0, transform.position.y),
                    new Vector2(0, obj.y));

                distance += 2 * Vector2.Distance(
                    new Vector2(transform.position.x, 0),
                    new Vector2(obj.x, 0));

                if (transform.position.y > obj.y)
                {
                    signum = 1;
                    break;
                }

                signum = -1;
                break;

            case "z":
            case "Z":
                throw new System.Exception("Axis not implemented: " + axis);
            default:
                throw new System.Exception("Unknown axis: " + axis);
        }
    }
}