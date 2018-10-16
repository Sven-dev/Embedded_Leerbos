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

    //collide with an object; might activate scale
    void OnCollisionEnter2D(Collision2D collision)
    {
        colliding = true;
        CheckConditions();
    }

    //collision ends, remove chance of activating scale
    void OnCollisionExit2D(Collision2D collision)
    {
        colliding = false;
    }

    //enter scale trigger area, get collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ScaleHand"))
        {
            inTrigger = true;
            handCollider = other;
            CheckConditions();
        }
    }

    //remove from the scale's list of objects
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ScaleHand"))
        {
            RemoveFromList();
        }
    }

    //check the two bools and add to the hand's weighted object list if true
    void CheckConditions()
    {
        //make sure the object is currently inside the trigger and is colliding
        //impossible to pass if collider hasnt been gained
        if (inTrigger && colliding)
        {
            //activate the scales
            handCollider.GetComponent<ScaleHandScript>().ActivateWeights(this);
        }
    }

    //for removing from the hand's list later
    protected void RemoveFromList()
    {
        //impossible to pass if the collider hasnt been gained
        if (inTrigger)
        {
            handCollider.GetComponent<ScaleHandScript>().RemoveFromList(this);
            inTrigger = false;
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

    protected override void Click(Vector3 clickposition)
    {

    }
}
