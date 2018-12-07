using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneChecker : Checker
{
    public Saveable IntroState;

    public List<Saveable> FirstTimeCompleteStates;
    public List<Saveable> CompleteStates;
    public GameObject[] Indicators;
    public Saveable CompleteState;
    [Space]
    public Npc Character;

    protected override void Check()
    {
        //if the player enters the area for the first time
        if (IntroState.Get() == false)
        {
            IntroState.Set(true);
            Character.PlayDialogue(Character.IntroClips);
        }

        for (int i = 0; i < FirstTimeCompleteStates.Count; i++)
        {
            if (FirstTimeCompleteStates[i] == true)
            {
                print("test");
                //FirstTimeCompleteStates[i].Set(5);
            }
        }

        //if one or multiple games have been completed
        for (int i = 0; i < CompleteStates.Count; i++)
        {
            if (CompleteStates[i].Get() == true)
            {
                Destroy(Indicators[i]);
            }
        }

        //If the area has been completed
        if (CompleteState.Get() == true)
        {
            Character.PlayDialogue(Character.VictoryClips[0]);
        }
    }
}