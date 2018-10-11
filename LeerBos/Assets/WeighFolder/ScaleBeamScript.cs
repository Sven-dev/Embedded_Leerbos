using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBeamScript : MonoBehaviour
{
    public float MinZRotation;
    public float MaxZRotation;
    public float RotateSpeed;

    public ScaleHandScript LeftHand;
    public ScaleHandScript RightHand;

    private Quaternion targetRotation;

    // Use this for initialization
    void Start()
    {

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


    public void CheckWeights()
    {
        int leftMass = 0;
        int rightMass = 0;

        foreach (WeightedObjectScript weight in LeftHand.Objects)
        {
            leftMass += weight.Mass;
        }
        foreach (WeightedObjectScript weight in RightHand.Objects)
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

        if (difference != transform.rotation.eulerAngles.z)
        {
            print("start coroutine");

            Vector3 rotation = transform.rotation.eulerAngles;
            targetRotation = Quaternion.Euler(rotation.x, rotation.y, difference);

            StartCoroutine(_RotateBeam());
        }
    }

    IEnumerator _RotateBeam()
    {
        Quaternion currentTarget = targetRotation;

        while (transform.rotation != targetRotation && currentTarget == targetRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * RotateSpeed);
            yield return null;
        }
        
    }
}
