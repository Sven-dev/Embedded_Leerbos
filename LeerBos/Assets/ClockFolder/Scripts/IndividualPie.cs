using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualPie {

    public int Score;
    public TimeSpan TargetTime;
    
    public IndividualPie(TimeSpan targetTime)
    {
        Score = 100;
        TargetTime = targetTime;
    }

    public void ReduceScore()
    {
        Score /= 2;
        if (Score < 10)
        {
            Score = 10;
        }
    }
}
