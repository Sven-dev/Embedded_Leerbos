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
    
    //deprecated rotation method
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

    //calculate the total mass of Weighted Objects on both hands, then compare and animate the scales accordingly
    public void CheckWeights()
    {
        int leftMass = 0;
        int rightMass = 0;

        //calculate total mass of both scales
        foreach (WeightedObjectScript weight in LeftHand.Objects)
        {
            leftMass += weight.Mass;
        }
        foreach (WeightedObjectScript weight in RightHand.Objects)
        {
            rightMass += weight.Mass;
        }

        //get the difference. a negative difference means the right scale is heavier
        float difference = leftMass - rightMass;

        //bound the difference
        if (difference > MaxZRotation)
        {
            difference = MaxZRotation;
        }
        else if (difference < MinZRotation)
        {
            difference = MinZRotation;
        }

        //handle the possibility that the rotation is already correct
        if (difference != transform.rotation.eulerAngles.z)
        {
            //assemble target rotation
            Vector3 rotation = transform.rotation.eulerAngles;
            targetRotation = Quaternion.Euler(rotation.x, rotation.y, difference);

            //rotate the boy
            StartCoroutine(_RotateBeam());
        }
    }

    IEnumerator _RotateBeam()
    {
        //hold onto start rotation
        Quaternion currentTarget = targetRotation;

        //rotate the beam. if targetRotation field changes, the coroutine is stopped to make way for the new target rotation
        while (transform.rotation != targetRotation && currentTarget == targetRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * RotateSpeed);
            yield return null;
        }
        
    }
}
