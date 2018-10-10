using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBeamScript : MonoBehaviour
{
    public float MinZRotation;
    public float MaxZRotation;
    public float RotateSpeed;

    public List<WeightedObjectScript> LeftObjects;
    public List<WeightedObjectScript> RightObjects;

    // Use this for initialization
    void Start()
    {
        CheckWeights();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckRotation();
    }

    void CheckRotation()
    {
        if (transform.rotation.eulerAngles.z < 360 + MinZRotation && transform.rotation.eulerAngles.z > MaxZRotation)
        {
            if (transform.rotation.eulerAngles.z > 360 / 2)
            {
                Vector3 rotation = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(rotation.x, rotation.y, MinZRotation);
            }
            else
            {
                Vector3 rotation = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(rotation.x, rotation.y, MaxZRotation);
            }

        }

    }


    void CheckWeights()
    {
        int leftMass = 0;
        int rightMass = 0;

        foreach (WeightedObjectScript weight in LeftObjects)
        {
            leftMass += weight.Mass;
        }
        foreach (WeightedObjectScript weight in RightObjects)
        {
            rightMass += weight.Mass;
        }

        int difference = leftMass - rightMass;

        print(difference);

        if (difference > 30)
        {
            difference = 30;
        }
        else if (difference < -30)
        {
            difference = -30;
        }

        StartCoroutine(_RotateBeam(difference));
    }

    IEnumerator _RotateBeam(int difference)
    {
        Vector3 rotation = transform.rotation.eulerAngles;

        while (transform.rotation.z != difference)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rotation.x, rotation.y, difference),
                Time.deltaTime * RotateSpeed);
            yield return null;
        }
    }
}
