using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmScript : Interactable {

    public ClockScript Clock;

    public Transform Target;
    public int TimeToDestination;
    public int PushBackSpeed;
    public int PushBackDuration;

    private Vector3 originPoint;
    private bool active, forward;
    private int coroutines;

    // Use this for initialization
    void Start () {
        coroutines = 0;
        originPoint = transform.position;
        active = true;
        StartCoroutine(_MoveTowardsTarget());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator _MoveTowardsTarget()
    {
        forward = true;
        float i = 0;
        while (active)
        {
            if (forward)
            {
                i += Time.deltaTime / TimeToDestination;
            }
            else
            {
                i -= Time.deltaTime / TimeToDestination*PushBackSpeed;
            }

            if (i > 1)
            {
                i = 1;
            }
            if (i < 0)
            {
                i = 0;
            }
            transform.position = Vector3.Lerp(originPoint, Target.position, i);
            yield return null;
        }
    }

    private IEnumerator _MoveBack(int duration)
    {
        coroutines++;
        int coroutineId = coroutines;
        float time = 0;
        forward = false;
        while (time < duration && coroutines == coroutineId)
        {
            time += Time.deltaTime;
            yield return null;
        }
        if (coroutines == coroutineId)
        {
            forward = true;
        }
    }

    protected override void Click(Vector3 clickposition)
    {
        StartCoroutine(_MoveBack(PushBackDuration));
    }

    public void GrabPie()
    {
        StartCoroutine(_MoveBack(10));
        Clock.ReduceScore();
    }
}
