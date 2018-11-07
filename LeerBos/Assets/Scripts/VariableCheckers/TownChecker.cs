using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownChecker : Checker
{
    public Npc Ant;
    public GameObject Cake;

    protected override void Check()
    {
        if (!GlobalVariables.TownIntroduced)
        {
            GlobalVariables.TownIntroduced = true;
            Ant.PlayDialogue(Ant.IntroClips);
            
        }
        if (GlobalVariables.TownCompleted)
        {
            Ant.PlayDialogue(Ant.VictoryClips);
            Cake.SetActive(true);
        }
    }
}
