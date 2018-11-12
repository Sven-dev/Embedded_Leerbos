using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneChecker : Checker
{
    public Saveable IntroState;
    public Saveable CompleteState;
    [Space]
    public Npc Character;
    public List<GameObject> Decorations;

    protected override void Check()
    {
        if (IntroState.Get() == false)
        {
            IntroState.Set(true);
            Character.PlayDialogue(Character.IntroClips);
        }
        else if (CompleteState.Get() == true)
        {
            foreach (GameObject decoration in Decorations)
            {
                decoration.SetActive(true);
            }
        }
    }
}