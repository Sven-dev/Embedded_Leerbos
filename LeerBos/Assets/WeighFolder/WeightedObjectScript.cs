using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedObjectScript : MonoBehaviour
{
    public int Mass;
    private Collider2D handCollider;

    private bool colliding;
    private bool inTrigger;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        print("enter collision");
        colliding = true;
        CheckConditions();
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        print("exit collision");
        colliding = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("enter trigger");
        inTrigger = true;
        handCollider = other;
        CheckConditions();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        print("exit trigger");
        inTrigger = false;
        RemoveFromList();
    }

    //check the two bools and add to the hand's weighted object list if true
    void CheckConditions()
    {
        print("check conditions");
        if (inTrigger && colliding)
        {
            handCollider.GetComponent<ScaleHandScript>().ActivateWeights(this);
        }
    }

    //for removing from the hand's list later
    void RemoveFromList()
    {
        if (!inTrigger)
        {
            handCollider.GetComponent<ScaleHandScript>().RemoveFromList(this);

        }
    }
}
