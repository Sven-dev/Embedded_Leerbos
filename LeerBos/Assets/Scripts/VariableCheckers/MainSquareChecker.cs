using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSquareChecker : Checker
{
    public Npc Mouse;

    protected override void Check()
    {
        if(!GlobalVariables.MainSquareIntroduced)
        {
            GlobalVariables.MainSquareIntroduced = true;
            Mouse.PlayDialogue(Mouse.IntroClips);
        }
    }
}
