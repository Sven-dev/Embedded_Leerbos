using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockQuarterScript : MonoBehaviour {

    private List<Transform> quarters;
    private List<Quaternion> originalRotations;
    private int coroutineId;

    public Color FeedbackColor;
    public float RotationSpeed;

    // Use this for initialization
    void Start() {
        quarters = new List<Transform>();
        originalRotations = new List<Quaternion>();
        
        //collect all of the quarters and their default rotations
        foreach (Transform quarter in GetComponentsInChildren<Transform>())
        {
            //GetComponents will grab itself too; make sure it doesn't
            if (quarter.gameObject != gameObject)
            {
                quarters.Add(quarter);
                originalRotations.Add(quarter.localRotation);
            }
        }
    }

    //light up green/other colour
    public void GiveFeedback()
    {
        foreach (Transform quarter in quarters)
        {
            quarter.GetComponent<QuarterFeedbackScript>().GiveFeedback(FeedbackColor);
        }
    }

    //return all quarters to their original positions
    public void ResetAll()
    {
        coroutineId++;
        for (int i = 0; i < quarters.Count; i++)
        {
            quarters[i].rotation = originalRotations[i];
        }
    }
    
    //moves all quarters to the side of the target minute, as a hint
    public void HighlightSide(int targetMinute)
    {
        //for all quarters
        for (int i = 0; i < quarters.Count; i++)
        {
            int targetIndex = i;

            //begin with target set to its original position,
            //in case the specific quarter doesn't need moving
            Quaternion targetRotation = originalRotations[i];

            switch (targetMinute)
            {
                //if target :00, go to positions 0 and 3
                case 0: case 60:
                    switch (i)
                    {
                        case 1:
                            targetIndex = 0;
                            break;
                        case 2:
                            targetIndex = 3;
                            break;
                    }
                    break;
                //if target :15, go to positions 1 and 0
                case 15:
                    switch (i)
                    {
                        case 2:
                            targetIndex = 1;
                            break;
                        case 3:
                            targetIndex = 0;
                            break;
                    }
                    break;
                //if target :30, go to positions 1 and 2
                case 30:
                    switch (i)
                    {
                        case 0:
                            targetIndex = 1;
                            break;
                        case 3:
                            targetIndex = 2;
                            break;
                    }
                    break;
                //if target :45, go to positions 3 and 2
                case 45:
                    switch (i)
                    {
                        case 0:
                            targetIndex = 3;
                            break;
                        case 1:
                            targetIndex = 2;
                            break;
                    }
                    break;
            }
            //move to the targeted rotation
            StartCoroutine(_RotateToTarget(quarters[i], originalRotations[targetIndex],coroutineId));
        }
    }

    //rotate to the targeted rotation
    private IEnumerator _RotateToTarget(Transform quarter, Quaternion targetRotation,int id) {

        while ((quarter.rotation.eulerAngles != targetRotation.eulerAngles) && coroutineId == id)
        {
            quarter.rotation = Quaternion.RotateTowards(quarter.localRotation, targetRotation, RotationSpeed);
            yield return null;
        }
    }
}
