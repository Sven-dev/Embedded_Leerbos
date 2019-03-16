using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockScript : Interactable {
    
    [HideInInspector]
    public bool active = true;
    public int AmountOfRounds, TimeSpeed, AnswerErrorMargin, HandErrorMargin, ScorePenalty;

    public CakeScript Cake;
    public CakeLayer CakeLayerPrefab;
    public VictoryScript victoryScript;
    public Transform TempCakeTrans, OvenTrans;
    public Text ScoreLabel, TargetLabel;
    public TimeVoiceScript VoiceScript;

    public int HintSeconds;
    private int CurrentRoundSeconds;

    private ClockQuarterScript quarterScript;
    private AudioSource aSource;
    private Transform hourHand, minuteHand;
    private CakeLayer currentCakeLayer;
    private TimeSpan currentTime;
    private int previousMod, previousHour;

    private int rounds = 0;
    private int TotalScore;
    
    private int coroutineId = 0;

    private Vector3 defaultScale;
    

    private const float
        hoursToDegrees = 360f / 12f,
        minutesToDegrees = 360f / 60f,
        secondsToDegrees = 360f / 60f;

    // Use this for initialization
    void Start()
    {
        minuteHand = transform.GetChild(1);
        hourHand = transform.GetChild(2);
        aSource = GetComponent<AudioSource>();
        defaultScale = transform.localScale;
        quarterScript = GetComponentInChildren<ClockQuarterScript>();

        //set starting time: 12:00
        currentTime = new TimeSpan(12, 0, 0);
        //set the first target time
        NewTargetTime();
        //set hour hand on target time in advance
        hourHand.localRotation = Quaternion.Euler(0f, 0f, currentCakeLayer.TargetTime.Hours * -hoursToDegrees);
        //start movement of minute hand
        StartCoroutine(_moveHands());
        StartCoroutine(_updateCurrentTime());
    }

    protected override void Click(Vector3 clickposition)
    {
        //if game hasnt  ended
        if (active) {
            //detect click on clock, react, check current time shown
            StartCoroutine(_reaction());
            CheckAnswer();
        }
    }

    void NewTargetTime()
    {
        //get a random time
        TimeModifier modifier = GetNewRandomMod(previousMod);
        int hour = GetNewRandomHour(previousHour);
        //string of the written-out time to put in the label
        string targetTimeText;
        //produce the new cake layer, put it in the oven
        currentCakeLayer = Instantiate(CakeLayerPrefab,OvenTrans);
        currentCakeLayer.gameObject.transform.SetPositionAndRotation(TempCakeTrans.position, TempCakeTrans.rotation);

        //set the target time
        TimeSpan newTime = ProduceTime(modifier, hour, out targetTimeText);
        currentCakeLayer.SetTime(newTime);
        TargetLabel.text = targetTimeText;
        VoiceScript.PlayTimeSounds(hour, (int)modifier);
    }

    TimeModifier GetNewRandomMod(int previous)
    {
        //get sum of all possible modifiers (kept dynamic for later additions)
        int typeSum = Enum.GetNames(typeof(TimeModifier)).Length;
        int rnd;
        //no need to end while loop since we just return
        while (true)
        {
            rnd = UnityEngine.Random.Range(0, typeSum);
            if (rnd != previous)
            {
                //save to avoid repeats
                previousMod = rnd;
                return (TimeModifier)rnd;
            }
        }
    }

    int GetNewRandomHour(int previous)
    {
        int rnd;
        //no need to end while loop since we just return
        while (true)
        {
            rnd = UnityEngine.Random.Range(1, 13);
            if (rnd != previous)
            {
                //save to avoid repeats
                previousHour = rnd;
                return rnd;
            }
        }
    }

    TimeSpan ProduceTime(TimeModifier type, int hour, out string text)
    {
        text = IntToWord(hour);
        LanguageController.Instance.GetText(0);

        //if (currentlanguage == "Nederlands")
        //{
            switch (type)
            {
                case TimeModifier.Half:
                    text = LanguageController.Instance.GetText(14) + " " + text;
                    return new TimeSpan(hour - 1, 30, 0);
                case TimeModifier.KwartOver:
                    text = LanguageController.Instance.GetText(15) + " " + text;
                    return new TimeSpan(hour, 15, 0);
                case TimeModifier.KwartVoor:
                    text = LanguageController.Instance.GetText(16) + " " + text;
                    return new TimeSpan(hour - 1, 45, 0);
                case TimeModifier.Uur:
                    text = text + " " + LanguageController.Instance.GetText(13);
                    return new TimeSpan(hour, 0, 0);
                default:
                    throw new ArgumentOutOfRangeException("Not a valid time modifier");
            }
        //}
        /*else if (currentlanguage == "English")
         {
            switch(type)
            {
                case TimeModifier.Half:
                    text = LanguageController.Instance.GetText(14) + " " + IntToWord(hour-1);
                    return new TimeSpan(hour-1, 30, 0);
                case TimeModifier.KwartOver:
                    text = LanguageController.Instance.GetText(15) + " " + text;
                    return new TimeSpan(hour, 15, 0);
                case TimeModifier.KwartVoor:
                    text = LanguageController.Instance.GetText(16) + " " + text;
                    return new TimeSpan(hour-1, 45, 0);
                case TimeModifier.Uur:
                    text = text + " " + LanguageController.Instance.GetText(13);
                    return new TimeSpan(hour, 0, 0);
                default:
                    throw new ArgumentOutOfRangeException("Not a valid time modifier");
            }
         }
        else if (currentlanguage == "French")
        {

        }
        */
    }

    void CheckAnswer()
    {
        //get difference between target and current time
        float diff = (float)currentCakeLayer.TargetTime.Subtract(currentTime).TotalMinutes;
        //compensate for the possibility of minute overflow: 0 = 60
        float overflowDiff = Mathf.Abs(Mathf.Abs(diff) - 60);
        if (Mathf.Abs(diff) <= AnswerErrorMargin || (Mathf.Abs(diff) > 30 && overflowDiff <= AnswerErrorMargin))
        {
            quarterScript.GiveFeedback();
            aSource.Play();
            //add score of the current pie to the total
            TotalScore += currentCakeLayer.Score;
            UpdateScoreLabel();
            rounds++;
            //check whether this was the final round
            if (rounds < AmountOfRounds)
            {
                //loop: reset everything and update visuals
                CurrentRoundSeconds = 0;
                quarterScript.ResetAll();
                Cake.NextLayer(currentCakeLayer);
                NewTargetTime();
            }
            else
            {
                //end game
                Cake.NextLayer(currentCakeLayer);
                active = false;
                //wait a second to show the final cake
                StartCoroutine(_waitAndEnd(1));
            }
        }
    }

    public void ReduceScore()
    {
        //hand has taken some pie. score is reduced.
        TotalScore -= ScorePenalty;
        UpdateScoreLabel();
    }
    
    private void UpdateScoreLabel()
    {
        ScoreLabel.text = TotalScore.ToString();
    }

    IEnumerator _moveHands()
    {
        while (active)
        {
            //continuously turn minute hand towards the current time. will stop when reached. will move again when it changes.
            minuteHand.rotation = Quaternion.RotateTowards(minuteHand.localRotation, Quaternion.Euler(0f, 0f, currentTime.Minutes * -minutesToDegrees), TimeSpeed);
            

            //hour hand is more complicated
            //can't use RotateTowards cause it'll turn counterclockwise if that's closer
            //have to continuously rotate clockwise and stop it when close to the target. margin of error

            //calculate the current target Z rotation
            float targetZ = Quaternion.Euler(0f, 0f, (float)currentCakeLayer.TargetTime.TotalHours * -hoursToDegrees).eulerAngles.z;
            //get the difference between the target and current rotation
            float difference = hourHand.rotation.eulerAngles.z - targetZ;
            //if not within the margin of error, keep it rotating
            if (Mathf.Abs(difference) > HandErrorMargin)
            {
                hourHand.Rotate(Vector3.back, Time.deltaTime * (TimeSpeed * 200));
            }
            yield return null;
        }
    }

    //visual reaction
    private IEnumerator _reaction()
    {
        coroutineId++;
        int id = coroutineId;

        transform.localScale = defaultScale;
        
        //shrink, then return to original size
        while (transform.localScale.x > defaultScale.x / 1.1 && id == coroutineId)
        {
            transform.localScale = new Vector2(transform.localScale.x / 1.02f, transform.localScale.y / 1.02f);
            yield return null;
        }
        while (transform.localScale.x < defaultScale.x && id == coroutineId)
        {
            transform.localScale = new Vector2(transform.localScale.x * 1.02f, transform.localScale.y * 1.02f);
            yield return null;
        }
    }

    

    IEnumerator _updateCurrentTime()
    {
        while (active)
        {
            //change the current time every few seconds to keep the minute hand moving
            yield return new WaitForSeconds(1);

            currentTime = new TimeSpan(currentCakeLayer.TargetTime.Hours, currentTime.Minutes + 5, 0);

            //count the amount of seconds in the current round
            //if it passes the threshold, activate the visual hint
            CurrentRoundSeconds++;
            if (CurrentRoundSeconds == HintSeconds)
            {
                quarterScript.HighlightSide(currentCakeLayer.TargetTime.Minutes);
            }
            yield return null;
        }
    }

    IEnumerator _waitAndEnd(int seconds)
    {
        //bit of delay to show the full cake, then show the victory label
        yield return new WaitForSeconds(seconds);
        victoryScript.Enable();
    }

    string IntToWord(int nr)
    {
        switch (nr)
        {
            case 1:
                return LanguageController.Instance.GetText(1);
            case 2:
                return LanguageController.Instance.GetText(2);
            case 3:
                return LanguageController.Instance.GetText(3);
            case 4:
                return LanguageController.Instance.GetText(4);
            case 5:
                return LanguageController.Instance.GetText(5);
            case 6:
                return LanguageController.Instance.GetText(6);
            case 7:
                return LanguageController.Instance.GetText(7);
            case 8:
                return LanguageController.Instance.GetText(8);
            case 9:
                return LanguageController.Instance.GetText(9);
            case 10:
                return LanguageController.Instance.GetText(10);
            case 11:
                return LanguageController.Instance.GetText(11);
            case 12:
                return LanguageController.Instance.GetText(12);
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