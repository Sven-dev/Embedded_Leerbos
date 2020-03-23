using System.Collections;
using UnityEngine;

public class KartMover : MonoBehaviour, I_SmartwallInteractable
{
    public float MoveSpeed;
    public float MoveDrag;
    [Space]
    public float XBoundMin;
    public float XBoundMax;

    private AudioSource Bounce;
    private AudioSource Grind;
    private Kart Kart;
    private bool Moving;

    private void Start()
    {
        AudioSource[] audio = GetComponents<AudioSource>();
        Bounce = audio[0];
        Grind = audio[1];

        Kart = transform.parent.GetComponent<Kart>();
        Moving = true;
    }

    public void Hit(Vector3 clickposition)
    {
        float distance;
        int signum;
        GetDistance(clickposition, "x", out distance, out signum);
        StartCoroutine(_Move(distance, signum));
    }

    /// <summary>
    /// Moves the kart
    /// </summary>
    /// <param name="xdistance">The distance between the center of the kart and the position thrown on the x-axis. Smaller distance means higher kart speed </param>
    /// <param name="signum">The direction the kart is moving. Is either 1 or -1</param>
    /// <returns></returns>
    IEnumerator _Move(float xdistance, int signum)
    {
        float speedChange = MoveSpeed - xdistance / 2;
        while (speedChange > 0 && Moving)
        {
            if (Clamp())
            {
                Bounce.Play();
                speedChange /= 2;
                signum = -signum;
            }

            Kart.transform.Translate(Vector2.left * speedChange * signum * Time.deltaTime);
            speedChange -= MoveDrag * Time.deltaTime;
            yield return null;

            Grind.volume = speedChange / MoveSpeed;
        }
    }

    //Clamps the plane to the upper and right boundaries, meaning the plane can't leave those sides of the screen
    public bool Clamp()
    {
        bool clamped = false;
        if (transform.position.x <= XBoundMin || transform.position.x >= XBoundMax)
        {
            clamped = true;
        }

        Kart.transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, XBoundMin, XBoundMax),
            transform.position.y);

        return clamped;
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