using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedObjectScript : Interactable
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
        colliding = true;
        CheckConditions();
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        colliding = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ScaleHand"))
        {
            inTrigger = true;
            handCollider = other;
            CheckConditions();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ScaleHand"))
        {
            inTrigger = false;
            RemoveFromList();
        }
    }

    //check the two bools and add to the hand's weighted object list if true
    void CheckConditions()
    {
        if (inTrigger && colliding)
        {
            handCollider.GetComponent<ScaleHandScript>().ActivateWeights(this);
        }
    }

    //for removing from the hand's list later
    protected bool RemoveFromList()
    {
        if (!inTrigger)
        {
            handCollider.GetComponent<ScaleHandScript>().RemoveFromList(this);
            return true;
        }
        return false;
    }

    protected override void Click(Vector3 clickposition)
    {

    }
}
