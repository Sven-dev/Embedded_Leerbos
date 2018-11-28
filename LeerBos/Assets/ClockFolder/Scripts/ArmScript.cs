using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmScript : Interactable {
    
    public ClockScript Clock;
    public CakeScript Cake;

    public Transform Target;
    public int TimeToDestination, PushBackSpeed, PushBackDuration, StartDelay;
    public float DangerDistance;

    private AudioSource aSource;
    private Vector3 originPoint;
    private bool forward;
    private int coroutines;
    private HandScript hand;
    private float prevRnd;
    private bool delayDone;

    // Use this for initialization
    void Start () {
        //get components
        hand = GetComponentInChildren<HandScript>();
        aSource = GetComponent<AudioSource>();
        //coroutine IDs
        coroutines = 0;
        //save the origin point so we can return there later
        originPoint = transform.position;
        //only start moving once called by the cake script
        StartCoroutine(_MoveTowardsTarget());
    }

    //update lerp target (new cake layer)
    public void ChangeTarget(Transform transform)
    {
        Target = transform;
    }

    private void PlayRandomSound()
    {
        //make sure pitch isnt the same as the previous one
        float i = Random.Range(0.8f, 1.2f);
        if (prevRnd == i)
        {
            PlayRandomSound();
        }
        //play, save sound for next go
        else
        {
            prevRnd = i;
            aSource.pitch = i;
            aSource.Play();
        }
    }

    //reset delay so the arms never move in sync, even if the game essentially resets
    public void ResetDelay()
    {
        delayDone = false;
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
            if (forward && Cake.LayersPresent)
            {
                if (!delayDone)
                {
                    //apply the start delay so the hands dont all move in sync
                    yield return new WaitForSeconds(StartDelay);
                    delayDone = true;
                }
                else
                {
                    //calculate new i value; higher because forward
                    i += Time.deltaTime / TimeToDestination;

                    //hand not flashing
                    if (!hand.Flash)
                    {
                        //start flashing if this close to target
                        if (i > DangerDistance)
                        {
                            hand.StartFlashing();
                        }
                    }
                }
            }
            //moving backwards
            else
            {
                //calculate new i value; lower because backwards
                i -= Time.deltaTime / TimeToDestination * PushBackSpeed;

                //hand flashing
                if (hand.Flash)
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

    private IEnumerator _MoveBack(int duration,bool success)
    {
        //this coroutine keeps track of how long the arm should move backwards.
        //to avoid double coroutines, it keeps track of an ID and ends if the int changes
        coroutines++;
        int coroutineId = coroutines;
        float time = 0;
        //go backwards
        forward = false;
        //if we're moving back because cake was grabbed
        if (success)
        {
            hand.CloseHand();
        }
        //if we're moving back because we got hit
        else
        {
            PlayRandomSound();
        }
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
            hand.OpenHand();
        }
    }

    protected override void Click(Vector3 clickposition)
    {
        StartCoroutine(_MoveBack(PushBackDuration,false));
    }

    public void GrabPie()
    {
        //hand has reached destination.
        //make sure we're going forward as a double collision precaution
        if (forward)
        {
            //turn off flash, move back to origin, lower the score of the player
            StartCoroutine(_MoveBack(PushBackDuration * 3,true));
            hand.StopFlashing();
            Clock.ReduceScore();
            Cake.RemoveCakeSlice();
        }
    }
}
