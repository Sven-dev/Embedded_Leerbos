using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchable : Interactable
{
    private SceneSwitcher SceneSwitcher;
    public string TargetString;
    public Sprite TransitionImage;

    protected override void Click(Vector3 clickposition)
    {
        Switch();
    }

    protected void Switch()
    {
        if (TargetString != "")
        {
            SceneSwitcher = Camera.main.GetComponent<SceneSwitcher>();
            SceneSwitcher.Switch(TargetString, TransitionImage);
        }
    }
}