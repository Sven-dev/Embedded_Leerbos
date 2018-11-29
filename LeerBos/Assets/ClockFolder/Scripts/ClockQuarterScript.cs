using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockQuarterScript : MonoBehaviour {

    private List<Transform> quarters;
    private List<Quaternion> originalRotations;
    private int coroutineId;

    public Color FeedbackColor;

    // Use this for initialization
    void Start() {
        quarters = new List<Transform>();
        originalRotations = new List<Quaternion>();
        

        foreach (Transform quarter in GetComponentsInChildren<Transform>())
        {
            if (quarter.gameObject != gameObject)
            {
                quarters.Add(quarter);
                originalRotations.Add(quarter.localRotation);
            }
        }
    }

    public void GiveFeedback()
    {
        foreach (Transform quarter in quarters)
        {
            quarter.GetComponent<QuarterFeedbackScript>().GiveFeedback(FeedbackColor);
        }
    }

    public void ResetAll()
    {
        coroutineId++;
        for (int i = 0; i < quarters.Count; i++)
        {
            quarters[i].rotation = originalRotations[i];
        }
    }

    public void HighlightSide(int targetMinute)
    {
        for (int i = 0; i < quarters.Count; i++)
        {
            int targetIndex = i;
            Quaternion targetRotation = originalRotations[i];

            switch (targetMinute)
            {
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
            StartCoroutine(_RotateToTarget(quarters[i], originalRotations[targetIndex],coroutineId));
        }
    }

    private IEnumerator _RotateToTarget(Transform quarter, Quaternion targetRotation,int id) {

        while ((quarter.rotation.eulerAngles != targetRotation.eulerAngles) && coroutineId == id)
        {
            quarter.rotation = Quaternion.RotateTowards(quarter.localRotation, targetRotation, 1);
            yield return null;
        }
    }
}
