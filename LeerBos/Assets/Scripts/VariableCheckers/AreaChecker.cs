using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChecker : MonoBehaviour
{
    public Npc Character;
    public Saveable AreaState;

    private void Start()
    {
        Check();
    }

    protected virtual void Check()
    {
        //if the player enters the area for the first time
        if (AreaState.Value == 0)
        {
            AreaState.Value = 1;
            Character.PlayDialogue(Character.IntroClips);
        }
        //if the area is completed
        else if (AreaState.Value == 2)
        {
            Character.PlayDialogue(Character.VictoryClips);
        }
    }
}