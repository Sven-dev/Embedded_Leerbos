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

    private bool active = true;

    // Use this for initialization
    void Start()
    {
        currentTime = new DateTime(2018, 1, 1, 12, 0, 0);
        NewTargetTime();
        HourHand.localRotation = Quaternion.Euler(0f, 0f, targetTime.Hour * -hoursToDegrees);
        MinuteHand.localRotation = Quaternion.Euler(0f, 0f, currentTime.Minute * -minutesToDegrees);
        StartCoroutine(moveHands());
        StartCoroutine(updateCurrentTime());
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected override void Click(Vector3 clickposition)
    {
        print("click");
        CheckAnswer();
    }

    void NewTargetTime()
    {
        int typeSum = Enum.GetNames(typeof(TimeModifier)).Length;
        int rnd = UnityEngine.Random.Range(0, typeSum);
        TimeModifier modifier = (TimeModifier)rnd;
        rnd = UnityEngine.Random.Range(1, 13);
        string targetTimeText;

        targetTime = ProduceTime(modifier, rnd, out targetTimeText);
        TargetLabel.text = targetTimeText;
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
                text = "Kwart over " + text;
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

        if (targetMinute - 5 < currentMinute && currentMinute < targetMinute + 5)
        {
            print("correct!");
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
            MinuteHand.rotation = Quaternion.RotateTowards(MinuteHand.localRotation, Quaternion.Euler(0f, 0f, currentTime.Minute * -minutesToDegrees), TimeSpeed);
            //HourHand.Rotate(new Vector3(0f, 0f, targetTime.Hour * -hoursToDegrees));
            yield return null;
        }
    }

    IEnumerator updateCurrentTime()
    {
        while (active)
        {
            yield return new WaitForSeconds(1);
            IncreaseCurrentTime();
            yield return null;
        }
    }

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