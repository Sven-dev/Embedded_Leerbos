using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchable : Interactable
{
    private SceneSwitcher SceneSwitcher;
    public string TargetString;
    public Sprite TransitionImage;

    private bool Switching = false;

    protected override void Click(Vector3 clickposition)
    {
        Switch();
    }

    protected void Switch()
    {
        if (!Switching && TargetString != "")
        {
            Switching = true;
            SceneSwitcher = Camera.main.GetComponent<SceneSwitcher>();
            SceneSwitcher.Switch(TargetString, TransitionImage);
        }
    }

    protected void SwitchImmediate()
    {
        if (!Switching && TargetString != "")
        {
            Switching = true;
            SceneSwitcher = Camera.main.GetComponent<SceneSwitcher>();
            SceneSwitcher.SwitchImmediate(TargetString);
        }
    }
}