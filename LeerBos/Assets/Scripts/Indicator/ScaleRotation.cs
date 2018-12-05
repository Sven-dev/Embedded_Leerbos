using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleRotation : AfkTimer
{
	// Use this for initialization
	void Start ()
    {
        ScaleBeamScript.OnRotationChange += CheckWeight;
	}

    private void CheckWeight(float difference)
    {
        if (difference > 0)
        {
            if (!Active)
            {
                Active = true;
                StartCoroutine(_IdleTimer());
            }

            return;
        }

        Active = false;
    }

    private void OnDestroy()
    {
        ScaleBeamScript.OnRotationChange -= CheckWeight;
    }
}