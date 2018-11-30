using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBeamScript : MonoBehaviour
{
    public float MinZRotation;
    public float MaxZRotation;
    public float RotateSpeed;

    public float WeightDifferenceMultiplier;

    private ScalesManagerScript manager;
    public ScaleHandScript LeftHand;
    public ScaleHandScript RightHand;

    private Quaternion targetRotation;

    // Use this for initialization
    void Start()
    {
        manager = transform.parent.GetComponent<ScalesManagerScript>();
    }

    //calculate the total mass of Weighted Objects on both hands, then compare and animate the scales accordingly
    public void CheckWeights()
    {
        //calculate total mass of both scales
        int leftMass = LeftHand.GetTotalMass();
        int rightMass = RightHand.GetTotalMass();

        //get the difference. a negative difference means the right scale is heavier
        float difference = (leftMass - rightMass) * WeightDifferenceMultiplier;

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
        while (currentTarget == targetRotation && transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * RotateSpeed);
            yield return null;
        }
        //if the loop wasn't interrupted
        if (currentTarget == targetRotation)
        {
            //check if this is the correct answer
            manager.CheckAnswer();
        }
    }
}
