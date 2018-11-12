using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CakeLayer : MonoBehaviour {
    
    [HideInInspector]
    public int Score;
    public TimeSpan TargetTime;

    public void SetTime(TimeSpan targetTime)
    {
        Score = 100;
        TargetTime = targetTime;
    }

    public void SetImage(Sprite sprite)
    {
        gameObject.GetComponent<Image>().sprite = sprite;
    }
}
