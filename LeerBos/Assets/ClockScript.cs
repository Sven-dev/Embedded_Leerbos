using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockScript : Interactable {

    private const float
        hoursToDegrees = 360f / 12f,
        minutesToDegrees = 360f / 60f,
        secondsToDegrees = 360f / 60f;

    public Text TargetLabel;

    public Transform HourHand,MinuteHand;

    public int TimeSpeed;
    private DateTime currentTime;
    private DateTime targetTime;
    private DateTime newTarget;

    private bool active = true;

    // Use this for initialization
    void Start()
    {
        //set starting time: 12:00
        currentTime = new DateTime(2018, 1, 1, 12, 0, 0);
        //set the first target time
        NewTargetTime();
        //set hour hand on target time in advance
        HourHand.localRotation = Quaternion.Euler(0f, 0f, targetTime.Hour * -hoursToDegrees);
        //start movement of minute hand
        StartCoroutine(moveHands());
        StartCoroutine(updateCurrentTime());
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected override void Click(Vector3 clickposition)
    {
        //detect click on clock, check current time shown
        CheckAnswer();
    }

    void NewTargetTime()
    {
        //get a random time modifier
        int typeSum = Enum.GetNames(typeof(TimeModifier)).Length;
        int rnd = UnityEngine.Random.Range(0, typeSum);
        TimeModifier modifier = (TimeModifier)rnd;
        //get a random hour (1-12)
        rnd = UnityEngine.Random.Range(1, 13);
        //string of the written-out time to put in the label
        string targetTimeText;
        //produce the new target time
        newTarget = ProduceTime(modifier, rnd, out targetTimeText);
        TargetLabel.text = targetTimeText;
        targetTime = newTarget;
        //set hour hand on target time. animate later?
        HourHand.localRotation = Quaternion.Euler(0f, 0f, targetTime.Hour * -hoursToDegrees);

        //broken method. saved for later
        //StartCoroutine(TransitionToTarget());
    }

    DateTime ProduceTime(TimeModifier type, int hour, out string text)
    {
        text = IntToWord(hour);

        switch(type)
        {
            case TimeModifier.Half:
                text = "Half " + text;
                return new DateTime(2018, 1, 1, hour-1, 30, 0);
            case TimeModifier.KwartOver:
                text = "Kwart over " + text;
                return new DateTime(2018, 1, 1, hour, 15, 0);
            case TimeModifier.KwartVoor:
                text = "Kwart voor " + text;
                return new DateTime(2018, 1, 1, hour-1, 45, 0);
            case TimeModifier.Uur:
                text = text + " Uur";
                return new DateTime(2018, 1, 1, hour, 0, 0);
            default:
                throw new ArgumentOutOfRangeException("Not a valid time modifier");
        }
        
    }

    void CheckAnswer()
    {
        int currentMinute = currentTime.Minute;
        int targetMinute = targetTime.Minute;

        //margin of error of 5 minutes
        if (targetMinute - 5 < currentMinute && currentMinute < targetMinute + 5)
        {
            print("correct!");
            //loop
            NewTargetTime();
        }
        else
        {
            print("incorrect");
        }
    }
    
    void IncreaseCurrentTime()
    {
        currentTime = currentTime.Add(new TimeSpan(0, 5, 0));
    }

    IEnumerator moveHands()
    {
        while (active)
        {
            //continuously turn towards the current time. will stop when reached. will move again when it changes.
            MinuteHand.rotation = Quaternion.RotateTowards(MinuteHand.localRotation, Quaternion.Euler(0f, 0f, currentTime.Minute * -minutesToDegrees), TimeSpeed);

            //rotating hour hand = too much work for prototype. commented for use later?
            
            //float target = Quaternion.Euler(0f, 0f, targetTime.Hour * -hoursToDegrees).eulerAngles.z;

            //if (HourHand.rotation.eulerAngles.z < target + 1 && HourHand.rotation.eulerAngles.z > target -1) {
            //    HourHand.Rotate(Vector3.back, Time.deltaTime * (TimeSpeed * 200));

            //}
            ////HourHand.rotation = Quaternion.RotateTowards(HourHand.localRotation, Quaternion.Euler(0f, 0f, targetTime.Hour * -hoursToDegrees), TimeSpeed * 2);


            yield return null;
        }
    }

    IEnumerator updateCurrentTime()
    {
        while (active)
        {
            //change the current time every few seconds to keep the minute hand moving
            yield return new WaitForSeconds(1);
            IncreaseCurrentTime();
            yield return null;
        }
    }

    //finnicky coroutine for moving hour hand bit by bit. too complex for prototype. saved for later
    //IEnumerator TransitionToTarget()
    //{
    //    DateTime thisTarget = newTarget;
    //    int currentHour = targetTime.Hour;

    //    while (currentHour != newTarget.Hour && thisTarget == newTarget)
    //    {
    //        currentHour++;
    //        if (currentHour > 12)
    //        {
    //            currentHour = 1;
    //        }
    //        targetTime = new DateTime(2018, 1, 1, currentHour, newTarget.Minute, 0);

    //        yield return new WaitForSeconds(0.5f);
    //    }
    //    if (thisTarget == newTarget)
    //    {
    //        targetTime = new DateTime(2018, 1, 1, currentHour, newTarget.Minute, 0);
    //    }
    //}

    string IntToWord(int nr)
    {
        switch (nr)
        {
            case 1:
                return "Een";
            case 2:
                return "Twee";
            case 3:
                return "Drie";
            case 4:
                return "Vier";
            case 5:
                return "Vijf";
            case 6:
                return "Zes";
            case 7:
                return "Zeven";
            case 8:
                return "Acht";
            case 9:
                return "Negen";
            case 10:
                return "Tien";
            case 11:
                return "Elf";
            case 12:
                return "Twaalf";
            default:
                throw new ArgumentOutOfRangeException("A number between 0 and 13 please");
        }
    }
}

public enum TimeModifier{
    KwartOver,
    KwartVoor,
    Half,
    Uur
    }