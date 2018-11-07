using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmScript : Interactable {
    
    public ClockScript Clock;
    
    public Transform Target;
    public int TimeToDestination;
    public int PushBackSpeed;
    public int PushBackDuration;
    public float DangerDistance;

    private Vector3 originPoint;
    private bool forward;
    private int coroutines;
    private HandScript hand;

    // Use this for initialization
    void Start () {
        //get hand
        hand = GetComponentInChildren<HandScript>();
        //coroutine IDs
        coroutines = 0;
        //save the origin point so we can return there later
        originPoint = transform.position;
        //start moving
        StartCoroutine(_MoveTowardsTarget());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator _MoveTowardsTarget()
    {
        //we only exit this coroutine when the game ends, so we can do some setup here
        forward = true;
        //i is how close we are to the target. 0 = origin, 1 = target
        float i = 0;
        while (Clock.active)
        {
            //moving forward
            if (forward)
            {
                //calculate new i value; higher because forward
                i += Time.deltaTime / TimeToDestination;

                //hand not flashing
                if (!hand.flash)
                {
                    //start flashing if this close to target
                    if (i > DangerDistance)
                    {
                        hand.StartFlashing();
                    }
                }
            }
            //moving backwards
            else
            {
                //calculate new i value; lower because backwards
                i -= Time.deltaTime / TimeToDestination*PushBackSpeed;

                //hand flashing
                if (hand.flash)
                {
                    //stop flashing if this far from target
                    if (i < DangerDistance)
                    {
                        hand.StopFlashing();
                    }
                }
            }

            //bounds. i has to stay between 0 and 1
            if (i > 1)
            {
                i = 1;
            }
            if (i < 0)
            {
                i = 0;
            }

            //set current position to be as far between origin and target as i is from 0 and 1
            transform.position = Vector3.Lerp(originPoint, Target.position, i);
            yield return null;
        }
    }

    private IEnumerator _MoveBack(int duration)
    {
        //this coroutine keeps track of how long the arm should move backwards.
        //to avoid double coroutines, it keeps track of an ID and ends if the int changes
        coroutines++;
        int coroutineId = coroutines;
        float time = 0;
        //go backwards
        forward = false;
        //make sure this while ends if another of the same coroutine starts
        while (time < duration && coroutines == coroutineId)
        {
            time += Time.deltaTime;
            yield return null;
        }
        //dont do this if a new coroutine started
        if (coroutines == coroutineId)
        {
            //go forward
            forward = true;
        }
    }

    protected override void Click(Vector3 clickposition)
    {
        StartCoroutine(_MoveBack(PushBackDuration));
    }

    public void GrabPie()
    {
        //hand has reached destination.
        //make sure we're going forward as a double collision precaution
        if (forward)
        {
            //turn off flash, move back to origin, lower the score of the player
            StartCoroutine(_MoveBack(PushBackDuration * 3));
            hand.StopFlashing();
            Clock.ReduceScore();
        }
    }
}
