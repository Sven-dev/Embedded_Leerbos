using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Switches to a game-scene based on a save-file value
public class MultiSceneSwitcher : MonoBehaviour, I_SmartwallInteractable
{
    protected SceneSwitcher SceneSwitcher;

    public Saveable GameState;
    [Tooltip("Only put in scenes, please")]
    public List<string> TargetScenes;
    public List<Sprite> Transitions;

    // Use this for initialization
    void Start ()
    {
        SceneSwitcher = Camera.main.GetComponent<SceneSwitcher>();
    }

    public void Hit(Vector3 clickposition)
    {
        Compare();
    }

    private void Compare()
    {
        switch(GameState.Value)
        {
            case 0: case 1:
                SceneSwitcher.Switch(TargetScenes[GameState.Value], Transitions[GameState.Value]);
                break;
        }
    }
}