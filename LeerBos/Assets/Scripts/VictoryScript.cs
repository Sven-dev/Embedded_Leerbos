using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    public delegate void Victory();
    public static event Victory OnVictory;
    
    public Button ReloadButton;
    public Button NextGameButton;

    public Saveable VictoryState;
    public ProgressItemScript ProgressItem;
    public float ItemStartingFill;
    public float ItemTargetFill;

    private AudioSource aSource;
    
    // Use this for initialization
    void Awake ()
    {
        aSource = GetComponent<AudioSource>();
	}

    public void Enable()
    {
        OnVictory();
        gameObject.SetActive(true);
        //Doesn't work in freeplay mode, replaying the same game twice will mess up the save file
        VictoryState.Value++;
        aSource.Play();

        //Enables UI-elements based on the gamemode
        if (GlobalVariables.Standalone)
        {
            ReloadButton.Appear();
        }
        else
        {
            ProgressItem.SetButtonToShow(NextGameButton);
            ProgressItem.Show(ItemStartingFill, ItemTargetFill);
        }
    }
}