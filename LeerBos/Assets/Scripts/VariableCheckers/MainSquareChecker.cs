using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSquareChecker : ZoneChecker
{
    public List<Saveable> ZoneStates;  

    protected override void Check()
    {
        for(int i = 0; i < ZoneStates.Count; i++)
        {
            if (ZoneStates[i].Get() == true)
            {
                Character.PlayDialogue(Character.VictoryClips[i+1]);
                return;
            }
        }

        base.Check();
    }
}