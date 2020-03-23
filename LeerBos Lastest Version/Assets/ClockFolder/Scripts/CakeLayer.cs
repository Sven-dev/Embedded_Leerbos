using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CakeLayer : MonoBehaviour {

    [HideInInspector]
    public int Score = 100;
    public TimeSpan TargetTime;

    public void SetTime(TimeSpan targetTime)
    {
        TargetTime = targetTime;
    }

    //change sprite when hand takes slice
    public void SetImage(Sprite sprite)
    {
        gameObject.GetComponent<Image>().sprite = sprite;
    }
}
