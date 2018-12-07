using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    public delegate void Victory();
    public static event Victory OnVictory;
    
    public Saveable VictoryState;
    public ProgressItemScript ProgressItem;
    public float ItemStartingFill;
    public float ItemTargetFill;

    private AudioSource aSource;
    private Button reloadButton;
    private Button nextGameButton;

    // Use this for initialization
    void Awake ()
    {
        aSource = GetComponent<AudioSource>();
        Button[] buttons = GetComponentsInChildren<Button>();
        nextGameButton = buttons[0];
        reloadButton = buttons[1];
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
            reloadButton.Appear();
        }
        else
        {
            ProgressItem.SetButtonToShow(nextGameButton);
            ProgressItem.Show(ItemStartingFill, ItemTargetFill);
        }
    }
}