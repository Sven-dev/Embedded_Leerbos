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
    public ProgressItemMinigame ProgressItem;
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
        VictoryState.Set(true);
        aSource.Play();

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