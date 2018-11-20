using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchable : Interactable
{
    private SceneSwitcher SceneSwitcher;
    [Tooltip("Only put in scenes, please")]
    public string TargetString;
    public Sprite TransitionImage;

    protected override void Click(Vector3 clickposition)
    {
        if (TargetString != "")
        {
            Switch();
        }
    }

    protected void Switch()
    {
        SceneSwitcher = Camera.main.GetComponent<SceneSwitcher>();
        SceneSwitcher.Switch(
            TargetString, 
            TransitionImage);
    }
}