using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiSceneSwitcher : Interactable
{
    protected SceneSwitcher SceneSwitcher;
    [Tooltip("Only put in scenes, please")]
    public List<Object> TargetScenes;
    public List<Sprite> TransitionImages;

    // Use this for initialization
    void Start ()
    {
        SceneSwitcher = Camera.main.GetComponent<SceneSwitcher>();
    }

    protected override void Click(Vector3 clickposition)
    {
        Compare();
    }

    protected abstract void Compare();
}