using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchable : Interactable
{
    private SceneSwitcher SceneSwitcher;
    [Tooltip("Only put in scenes, please")]
    public Object TargetScene;
    public Sprite TransitionImage;

    // Use this for initialization
    void Start ()
    {
        SceneSwitcher = Camera.main.GetComponent<SceneSwitcher>();
	}

    protected override void Click(Vector3 clickposition)
    {
        SceneSwitcher.Switch(TargetScene, TransitionImage);
    }
}