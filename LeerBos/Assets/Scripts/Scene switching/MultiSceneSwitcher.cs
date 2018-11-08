using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSceneSwitcher : Interactable
{
    protected SceneSwitcher SceneSwitcher;

    public List<Saveable> SaveValues;
    [Tooltip("Only put in scenes, please")]
    public List<Object> TargetScenes;
    public List<Sprite> Transitions;

    // Use this for initialization
    void Start ()
    {
        SceneSwitcher = Camera.main.GetComponent<SceneSwitcher>();
    }

    protected override void Click(Vector3 clickposition)
    {
        Compare();
    }

    private void Compare()
    {
        for (int i = 0; i < TargetScenes.Count; i++)
        {
            if (SaveValues[i].Get() == false)
            {
                SceneSwitcher.Switch(TargetScenes[i], Transitions[i]);
                break;
            }
        }
    }
}