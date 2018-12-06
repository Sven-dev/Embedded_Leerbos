using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneChecker : Checker
{
    public Saveable IntroState;
    public Saveable CompleteState;
    public List<Saveable> GameStates;
    public GameObject[] Indicators;

    [Space]
    public Npc Character;

    protected override void Check()
    {
        for (int i = 0; i < GameStates.Count; i++)
        {
            if (GameStates[i].Get() == true)
            {
                Destroy(Indicators[i]);
            }
        }

        if (IntroState.Get() == false)
        {
            IntroState.Set(true);
            Character.PlayDialogue(Character.IntroClips);

        }
        else if (CompleteState.Get() == true)
        {
            Character.PlayDialogue(Character.VictoryClips[0]);
        }
    }
}