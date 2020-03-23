using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverPrice : AfkTimer
{
	// Use this for initialization
	void Start ()
    {
        Register.OnDrawerChange += CheckPrice;
	}

    private void CheckPrice(double price)
    {
        if (price < 0)
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
        Register.OnDrawerChange -= CheckPrice;
    }
}