using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneChecker : Checker
{
    public Saveable IntroState;
    public Saveable CompleteState;
    [Space]
    public Npc Character;

    protected override void Check()
    {
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