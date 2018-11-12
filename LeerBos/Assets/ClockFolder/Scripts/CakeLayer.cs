using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeLayer {
    
    public int Score;
    public TimeSpan TargetTime;
    public GameObject CakeObject;

    public CakeLayer(TimeSpan targetTime)
    {
        Score = 100;
        TargetTime = targetTime;
    }
}
